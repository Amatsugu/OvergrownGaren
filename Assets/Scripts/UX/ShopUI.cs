using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopTabProperties
{
	public GameObject TabRoot;
	public Button ButtonTab;
}

public class ShopUI : UIPanel
{
	public ShopResource resourcePrefab;
	public RectTransform content;
	public ShopTabProperties[] _tabsProperties;
	
	private List<ShopResource> _shopItems = new();
	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
		GameManager.Events.OnResourcesChange += RefreshIcons;
	}
	
	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

	public void RefreshIcons(ResourceIdentifier _)
	{
		foreach (var item in _shopItems)
			item.Refresh();
	}

	public override void Show()
	{
		base.Show();

		DestroyChildren(content);
		_shopItems.Clear();
		var resources = GameManager.Unlocks.GetUnlocks();
		foreach (var resource in resources)
		{
			var item = Instantiate(resourcePrefab, content);
			item.SetResource(resource);
			var btn = item.GetComponent<Button>();
			btn.onClick.AddListener(() =>
			{
				if (GameManager.ResourcesService.SpendResources(resource.GetCost()))
					GameManager.ResourcesService.AddResources((resource, 1));
			});
			_shopItems.Add(item);
		}
		
		foreach (var tabProperties in _tabsProperties)
		{
			tabProperties.ButtonTab.onClick.AddListener(() => OnTabClicked(tabProperties));
		}

		if (_tabsProperties.Length > 0)
		{
			ActivateTab(_tabsProperties[0]);
		}
	}

	public override void Hide()
	{
		base.Hide();
		DestroyChildren(content);
		_shopItems.Clear();
		
		foreach (var tabProperties in _tabsProperties)
		{
			tabProperties.ButtonTab.onClick.RemoveAllListeners();
		}
	}
	
	private void OnTabClicked(ShopTabProperties clickedTabProperties)
	{
		ActivateTab(clickedTabProperties);
	}

	private void ActivateTab(ShopTabProperties tabForActivating)
	{
		foreach (var tabProperties in _tabsProperties)
		{
			tabProperties.TabRoot.SetActive(false);
			tabProperties.ButtonTab.interactable = true;
		}

		tabForActivating.TabRoot.SetActive(true);
		tabForActivating.ButtonTab.interactable = false;
	}
}
