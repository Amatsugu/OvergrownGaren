using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents 
{
	public event Action<PlantDefination> OnCropHarvested;
	public event Action<PlantDefination> OnCropPlanted;

	public void InvokeOnCropHarvested(PlantDefination plant)
	{
		OnCropHarvested?.Invoke(plant);
	}

	public void InvokeOnCropPlanted(PlantDefination plant)
	{
		OnCropPlanted?.Invoke(plant);
	}
}
