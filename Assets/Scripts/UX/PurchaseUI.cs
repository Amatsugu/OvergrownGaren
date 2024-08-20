using Balconies;

using Building;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PurchaseUI : UIHover
{
	public PurchaseDisplay purchaseDisplayPrefab;
	public AudioClip openSound;

	private List<PurchaseDisplay> _purchaseDisplays = new();
	private bool _isPurchasing = false;

	private AudioSource _audioSource;

	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
		GameManager.Events.OnBalconyUnlocked += OnBalconyUnlocked;
		_audioSource = GetComponent<AudioSource>();
		if(_audioSource == null)
			_audioSource = gameObject.AddComponent<AudioSource>();
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
		if (openSound != null)
			_audioSource.PlayOneShot(openSound);
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