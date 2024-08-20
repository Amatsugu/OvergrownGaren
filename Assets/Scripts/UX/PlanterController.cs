using Resources;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[RequireComponent(typeof(GameManager)), RequireComponent(typeof(AudioSource))]
public class PlanterController : MonoBehaviour
{
	public enum UIState
	{
		Idle,
		PlanterBox,
	}

	public UIState state;
	public AudioClip wateringSound;
	public PlantDefination[] plants;

	[HideInInspector]
	public PlantDefination _selectedPlant;


	private Dictionary<ResourceType, List<ResourceType>> _seedLookup;
	private AudioSource _audioSource;
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
		_audioSource = GetComponent<AudioSource>();
		if(_audioSource == null)
			_audioSource = gameObject.AddComponent<AudioSource>();
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
		
		var mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var hit = Physics2D.Raycast(mPos, mPos, 10);
		if(hit.collider == null) 
			return;
		
		if (!hit.collider.TryGetComponent<PlanterBox>(out var planter)) 
			return;

		if (planter.HasCrop)
		{
			if (planter.Crop.IsHarvestable)
			{
				planter.Harvest();
				if(planter.Crop.plant.harvestingSound != null)
					_audioSource.PlayOneShot(planter.Crop.plant.harvestingSound);
			}
			else
			{
				planter.Crop.Water(5);
				if(wateringSound != null)
					_audioSource.PlayOneShot(wateringSound);
			}
			
			return;
		}
		
		if(_selectedPlant == null) 
			return;
		
		if(GameManager.ResourcesService.SpendResources((_selectedPlant.seedResource, 1)))
		{
			planter.Plant(_selectedPlant);
			if(_selectedPlant.plantingSound != null)
				_audioSource.PlayOneShot(_selectedPlant.plantingSound);
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