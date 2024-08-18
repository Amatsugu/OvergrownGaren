using Balconies;
using Building;
using GameResources;
using Resources;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float dryRate = .5f;
	public float baseGrowthRate = .5f;
	public float weedChance = .2f;
	public ResourceType[] defaultUnlocks;

	public BuildingView _buildingView;

	public static float DryRate => INST.dryRate;
	public static float BaseGrowthRate => INST.baseGrowthRate;
	public static float WeedChance => INST.weedChance;
	public static GameEvents Events => INST._events;
	public static BuildingData BuildingData => INST._buildingView.BuildingData;
	public static ResourcesService ResourcesService => INST._resourcesService;
	public static BalconiesService BalconiesService => INST._balconiesService;
	public static TimeController TimeController => INST._timeController;
	public static ShopUnlocks Unlocks => INST._shopUnlocks;

	public static GameManager INST
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindFirstObjectByType<GameManager>();
				return _instance;
			}

			return _instance;
		}
	}

	private static GameManager _instance;
	private readonly GameEvents _events = new();
	private readonly ResourcesService _resourcesService = new();
	private readonly ShopUnlocks _shopUnlocks = new ();
	private BalconiesService _balconiesService;
	private TimeController _timeController;

	private void Awake()
	{
		_timeController = GetComponent<TimeController>();
	}

	private void Start()
	{
		// Initial state
		var buildingData = new BuildingData();
		_resourcesService.AddResources((ResourceType.Coins, 1000), (ResourceType.SeedsCommon, 12), (ResourceType.SeedsSuper, 85));
		_balconiesService = new BalconiesService(buildingData, _resourcesService);

		Unlocks.UnlockResources(defaultUnlocks);

		// View bindings
		_buildingView.Bind(buildingData);
	}
}
