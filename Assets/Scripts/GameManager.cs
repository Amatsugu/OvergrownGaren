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
	public ResourceIdentifier[] startingInventory = new ResourceIdentifier[] { (ResourceType.SeedsCommon, 3) };

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
	public static PlanterController PlanterController => INST._planterController;
	public static QuestNotifications Notifications => INST._notifs;

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
	private PlanterController _planterController;
	private ShopUI _shopUI;
	private QuestWindow _questWindow;
	private QuestTracker _questTracker;
	private QuestNotifications _notifs;

	private void Awake()
	{
		_timeController = GetComponent<TimeController>();
		_planterController = GetComponent<PlanterController>();
		_shopUI = FindAnyObjectByType<ShopUI>(FindObjectsInactive.Include);
		_questWindow = FindAnyObjectByType<QuestWindow>(FindObjectsInactive.Include);
		_questTracker = FindAnyObjectByType<QuestTracker>(FindObjectsInactive.Include);
		_notifs = FindAnyObjectByType<QuestNotifications>(FindObjectsInactive.Include);
	}

	private void Start()
	{
		// Initial state
		var buildingData = new BuildingData();
		_resourcesService.AddResources(startingInventory);
		_balconiesService = new BalconiesService(buildingData, _resourcesService);

		Unlocks.UnlockResources(defaultUnlocks);

		// View bindings
		_buildingView.Bind(buildingData);
	}

	private void Update()
	{
		if(Input.GetKeyUp(KeyCode.E))
		{
			if(_shopUI.IsOpen)
				_shopUI.Hide();
			else
				_shopUI.Show();
		}
		if (Input.GetKeyUp(KeyCode.Q))
		{
			if (_questWindow.IsOpen)
				_questWindow.Hide();
			else
				_questTracker.ShowQuests();
		}
	}
}
