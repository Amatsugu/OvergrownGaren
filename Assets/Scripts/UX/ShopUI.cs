using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIPanel
{
	public ShopResource resourcePrefab;
	public RectTransform content;

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
	}

	public override void Hide()
	{
		base.Hide();
		DestroyChildren(content);
		_shopItems.Clear();
	}
}
