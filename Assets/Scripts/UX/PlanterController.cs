using System;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class PlanterController : MonoBehaviour
{
	public enum UIState
	{
		Idle,
		PlanterBox,
	}

	public UIState state;

	public PlantDefination testPlant;
	
	// Update is called once per frame
	private void Update()
	{

		switch (state)
		{
			case UIState.Idle:
				ProcessPlanterSelection(); 
				break;

			case UIState.PlanterBox:
				break;
		}
	}

	private void ProcessPlanterSelection()
	{
		if (!Input.GetKeyDown(KeyCode.Mouse0))
			return;

		var mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var hit = Physics2D.Raycast(mPos, mPos, 10);
		if(hit.collider == null) 
			return;
		
		if (!hit.collider.TryGetComponent<PlanterBox>(out var planter)) 
			return;

		if (planter.HasCrop)
		{
			if (planter.Crop.IsHarvestable)
				planter.Harvest();
			else
				planter.Crop.Water(5);
		}else
			planter.Plant(testPlant);


	}
}