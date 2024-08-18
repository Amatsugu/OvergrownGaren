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
		var availableBalonies = GameManager.BalconiesService.ReadyToUnlockBalconies;
		foreach (var balcony in availableBalonies)
		{
			if (balcony.IsUnlocked)
				continue;
			var display = Instantiate(purchaseDisplayPrefab, rTransform);
			display.Show(balcony.view);
			_purchaseDisplays.Add(display);
		}
		_isPurchasing = true;
	}

	private void HidePurchaseDisplays()
	{
		foreach (var d in _purchaseDisplays)
			Destroy(d.gameObject);
		_purchaseDisplays.Clear();
		_isPurchasing = false;
	}
}