using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float dryRate = .5f;
	public float baseGrowthRate = .5f;
	public float weedChance = .2f;

	public static float DryRate => INST.dryRate;
	public static float BaseGrowthRate => INST.baseGrowthRate;
	public static float WeedChance => INST.weedChance;
	public static GameEvents Events => INST._events;

	public static GameManager INST
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindFirstObjectByType<GameManager>();
				_instance._events = new GameEvents();
				return _instance;
			}
			return _instance;
		}
	}

	private static GameManager _instance;
	private GameEvents _events = new();

}
