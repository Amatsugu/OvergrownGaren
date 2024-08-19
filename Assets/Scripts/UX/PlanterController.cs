using Resources;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

	public PlantDefination[] plants;

	public PlantDefination _selectedPlant;


	private Dictionary<ResourceType, List<ResourceType>> _seedLookup;
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

	private void Awake()
	{
		_seedLookup = new Dictionary<ResourceType, List<ResourceType>>();
		foreach (var plant in plants)
		{
			var output = plant.yeild.Select(r => r.type).Where(r => r.IsItem());
			foreach (var item in output)
			{
				if(_seedLookup.TryGetValue(item, out var seeds))
					seeds.Add(plant.seedResource);
				else
					_seedLookup.Add(item, new List<ResourceType>() { plant.seedResource });
			}
		}
	}

	private void ProcessPlanterSelection()
	{
		if (!Input.GetKeyDown(KeyCode.Mouse0))
			return;
		if(_selectedPlant == null) 
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
		}
		else
		{
			if(GameManager.ResourcesService.SpendResource((_selectedPlant.seedResource, 1)))
			{
				planter.Plant(_selectedPlant);
			}
		}
	}

	public ResourceType[] GetSeedsFor(ResourceType resource)
	{
		if (_seedLookup.TryGetValue(resource, out var seeds))
			return seeds.ToArray();
		return Array.Empty<ResourceType>();
	}

	public ResourceType[] GetSeedsFor(ResourceType[] resources)
	{
		return resources.SelectMany(GetSeedsFor).Distinct().ToArray();
	}

	public void SelectPlant(ResourceType resource)
	{
		_selectedPlant = plants.FirstOrDefault(p => p.seedResource == resource);
	}
}