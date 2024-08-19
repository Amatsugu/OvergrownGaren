using UnityEngine;

public class PlanterBox : MonoBehaviour
{
	public Transform planterSlot;
	public GameObject _markerNeedWater;

	public bool HasCrop => _crop != null;
	public Plant Crop => _crop;


	private Plant _crop;

	private void Awake()
	{
		_markerNeedWater.SetActive(false);
	}

	public void Plant(PlantDefination plant)
	{
		if (_crop)
		{
			_crop.WaterValueChanged -= OnWaterValueChanged;
		}
		
		GameManager.Events.InvokeOnCropPlanted(plant);
		_crop = plant.CreatePlant(planterSlot);
		
		_crop.WaterValueChanged += OnWaterValueChanged;
	}

	private void OnWaterValueChanged(float waterValue)
	{
		var isMarkerNeedWaterEnabled = waterValue <= 0;
		
		_markerNeedWater.SetActive(isMarkerNeedWaterEnabled);
	}

	public void Uproot()
	{
	}

	public void Harvest()
	{
		var harvest = _crop.GetHarvest();
		GameManager.Events.InvokeOnCropHarvested(_crop.plant);

		GameManager.ResourcesService.AddResources(harvest);
		Destroy(_crop.gameObject);

		_crop.WaterValueChanged -= OnWaterValueChanged;
		_markerNeedWater.SetActive(false);
	}

	private void OnDestroy()
	{
		if (_crop)
		{
			_crop.WaterValueChanged -= OnWaterValueChanged;
		}
	}


#if UNITY_EDITOR
	public void OnGUI()
	{
		if (_crop == null)
			return;
		var plantPos = _crop.transform.position;
		var screenPos = Camera.main.WorldToScreenPoint(plantPos);
		screenPos.y = Screen.height - screenPos.y;
		GUI.BeginGroup(new Rect(screenPos, new(200,   100)));
		GUI.Label(new Rect(0, 0, 200, 20), $"Age: {_crop.age}");
		GUI.Label(new Rect(0, 20, 200, 20), $"Water: {_crop.water}");
		GUI.Label(new Rect(0, 40, 200, 20), $"Rate: {_crop.GetGrowthRate()}");
		GUI.Label(new Rect(0, 60, 200, 20), $"Harvest: {_crop.IsHarvestable}");

		GUI.EndGroup();
	}
#endif
}