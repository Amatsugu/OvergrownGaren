using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(GrowthController))]
public class Plant : MonoBehaviour
{
	

	public float NormalizedAge => age / plant.maxAge;
	public bool IsHarvestable => age > plant.maxAge;
	public bool IsDead => health > 0;

	public float age;

	public float water;

	public bool hasWeeds;

	public float health;

	public float yeildMultiplier;

	[HideInInspector]
	public PlantDefination plant;

	private GrowthController _growthController;

	private void Start()
	{
		_growthController = GetComponent<GrowthController>();
	}

	private void Update()
	{
		
		var growthMulti = GetGrowthRate();

		water -= GameManager.DryRate * Time.deltaTime;
		if(water < 0)
			water = 0;

		age += Time.deltaTime * growthMulti;
	}

	public float GetGrowthRate()
	{
		var growthMulti = GameManager.BaseGrowthRate;

		if (water <= 0)
			growthMulti /= 2f;
		if (hasWeeds)
			growthMulti /= 2f;
		return growthMulti;
	}

	private void LateUpdate()
	{

		_growthController.OnGrow(NormalizedAge, IsDead);
	}

	public void GetHarvest()
	{

	}

	public void Water(float ammount)
	{
		water += ammount;
	}
}