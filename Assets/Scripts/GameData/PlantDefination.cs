using System;
using System.Collections;
using System.Collections.Generic;
using Resources;
using UnityEngine;


[CreateAssetMenu(menuName = "Plant Defination")]
public class PlantDefination : ScriptableObject
{
	public GameObject prefab;
	public float maxAge = 10;
	public float unitCost;
	public float unitSell;
	public bool isEvil;
	public ResourceIdentifier[] yeild;

	public Plant CreatePlant(Transform location)
	{
		var instance = Instantiate(prefab, location, false);

		if (!instance.TryGetComponent<Plant>(out var p))
			p = instance.AddComponent<Plant>();
		p.plant = this;
		return p;
	
	}
}
