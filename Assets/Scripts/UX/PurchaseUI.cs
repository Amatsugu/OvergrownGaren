using Balconies;

using Building;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PurchaseUI : UIHover
{
	public PurchaseDisplay purchaseDisplayPrefab;

	private List<PurchaseDisplay> _purchaseDisplays = new();
	private bool _isPurchasing = false;

	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
		GameManager.Events.OnBalconyUnlocked += OnBalconyUnlocked;
	}

	private void OnBalconyUnlocked(BalconyData _)
	{
		ClearDisplays();
		ShowDisplays();
	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
		if (Input.GetKeyUp(KeyCode.F))
		{
			if (_isPurchasing)
				HidePurchaseDisplays();
			else
				ShowPurchaseDisplays();
		}
	}

	private void ShowPurchaseDisplays()
	{
		ShowDisplays();
		_isPurchasing = true;
	}



	private void HidePurchaseDisplays()
	{
		ClearDisplays();
		_isPurchasing = false;
	}

	private void ShowDisplays()
	{
		var availableBalonies = GameManager.BalconiesService.ReadyToUnlockBalconies;
		foreach (var balcony in availableBalonies)
		{
			if (balcony.IsUnlocked)
				continue;
			var display = Instantiate(purchaseDisplayPrefab, rTransform);
			display.Show(balcony.view);
			_purchaseDisplays.Add(display);
		}
	}

	private void ClearDisplays()
	{
		foreach (var d in _purchaseDisplays)
			Destroy(d.gameObject);
		_purchaseDisplays.Clear();
	}
}