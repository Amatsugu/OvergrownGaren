using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class PlanterBox : MonoBehaviour
{
	public bool HasCrop => _crop != null;
	public Plant Crop => _crop;

	public Transform planterSlot;

	private Plant _crop;

	public void Plant(PlantDefination plant)
	{
		GameManager.Events.InvokeOnCropPlanted(plant);
		_crop = plant.CreatePlant(planterSlot);
	}

	public void Uproot()
	{
	}

	public void Harvest()
	{
		_crop.GetHarvest();
		GameManager.Events.InvokeOnCropHarvested(_crop.plant);
		Destroy(_crop.gameObject);
	}


#if UNITY_EDITOR
	public void OnGUI()
	{
		if (_crop == null)
			return;
		var plantPos = _crop.transform.position;
		var screenPos = Camera.main.WorldToScreenPoint(plantPos);
		GUI.BeginGroup(new Rect(screenPos, new(200, 100)));
		GUI.Label(new Rect(0, 0, 200, 20), $"Age: {_crop.age}");
		GUI.Label(new Rect(0, 20, 200, 20), $"Water: {_crop.water}");
		GUI.Label(new Rect(0, 40, 200, 20), $"Rate: {_crop.GetGrowthRate()}");
		GUI.Label(new Rect(0, 60, 200, 20), $"Harvest: {_crop.IsHarvestable}");

		GUI.EndGroup();
	}
#endif
}