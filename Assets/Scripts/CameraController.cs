using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Speeds")]
	public float minMoveSpeed = 1;
	public float maxMoveSpeed = 5;
	public float minZoom = 3;
	public float maxZoom = 10;
	public AnimationCurve zoomSpeedCurve;
	public AnimationCurve zoomCurve;
	public float zoomSmoothing = 1;

	[Header("Input")]
	public float scrollSensitivity = 0.1f;

	[Header("Bounds")]
	public Vector2 minBounds = new Vector2(0, 0);
	public Vector2 maxBounds = new Vector2(10, 10);
	public float yZoomBoundOffset = 10;

	private float _curZoom;
	private Transform _transform;
	private Camera _camera;

	// Start is called before the first frame update
	private void Start()
	{
		_transform = transform;
		_camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	private void Update()
	{
		var move = ProcessInputs();

		var moveSpeed = math.lerp(maxMoveSpeed, minMoveSpeed, zoomSpeedCurve.Evaluate(_curZoom));
		var zoomLevel = math.lerp(minZoom, maxZoom, zoomCurve.Evaluate(_curZoom));


		var pos = _transform.position + (moveSpeed * Time.deltaTime * move);
		_camera.orthographicSize = zoomLevel;

		_transform.position = ConstrainBounds(pos);
	}

	private Vector3 ProcessInputs()
	{
		var scroll = Input.mouseScrollDelta.y;
		if (math.abs(scroll) > 0) { 
			_curZoom -= (scroll * scrollSensitivity);
		}
		var move = Vector3.zero;



		if (Input.GetKey(KeyCode.W))
			move.y += 1;
		else if(Input.GetKey(KeyCode.S))
			move.y -= 1;

		if (Input.GetKey(KeyCode.A))
			move.x -= 1;
		else if( Input.GetKey(KeyCode.D))
			move.x += 1;

		return move;
	}

	private Vector3 ConstrainBounds(Vector3 pos)
	{
		var yOffset = math.lerp(minBounds.y, yZoomBoundOffset, zoomCurve.Evaluate(_curZoom));
		if (pos.x < minBounds.x)
			pos.x = minBounds.x;
		if (pos.y < minBounds.y + yOffset)
			pos.y = minBounds.y + yOffset;
		if (pos.x > maxBounds.x)
			pos.x = maxBounds.x;
		if (pos.y > maxBounds.y)
			pos.y = maxBounds.y;
		return pos;
	}
}