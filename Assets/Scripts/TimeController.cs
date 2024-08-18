using System.Collections; 
using System.Collections.Generic;

using UnityEngine;

public class TimeController : MonoBehaviour
{
	public float DayProgress => _forceTime ? _forcedTime : (_curTime % secondsPerDay) / secondsPerDay;
	public int CurrentDay => (int)((_curTime - dayStartTime * secondsPerDay) / secondsPerDay);
	
	public float secondsPerDay = 100;
	[Range(0f, 1f)]
	public float dayStartTime;

	private float _curTime;
	private float _forcedTime = 0;
	private bool _forceTime = false;
	private int _currentDay = 0;
#if UNITY_EDITOR
	private Rect _windowPos = new Rect(100, 100, 300, 200);
#endif

	private void Update()
	{
		_curTime = Time.time;
		if(CurrentDay > _currentDay)
		{
			_currentDay = CurrentDay;
			GameManager.Events.InvokeOnDayStart(CurrentDay);
			Debug.Log($"Day {_currentDay} Starting. Time: {DayProgress}");
		}
	}

#if UNITY_EDITOR

	private void OnGUI()
	{
		_windowPos = GUI.Window(0, _windowPos, a =>
		{
			_forceTime = GUI.Toggle(new Rect(10, 20, 290, 20), _forceTime, "Force Time");
			if (_forceTime)
			{
				_forcedTime = GUI.HorizontalSlider(new Rect(10, 40, 290, 20), _forcedTime, 0, 1);
			}
			GUI.Label(new Rect(10, 60, 290, 20), $"Day: {CurrentDay:n}");
			GUI.Label(new Rect(10,	80, 290, 20), $"Day Progress: {DayProgress:n}");
			GUI.DragWindow();
		}, "Time Control");
	}

#endif
}