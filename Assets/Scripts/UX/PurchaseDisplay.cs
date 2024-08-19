using Balconies;

using System.Collections;
using System.Collections.Generic;
using System.Text;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PurchaseDisplay : UIHover
{
	public Image Image;
	public TextMeshProUGUI text;

	private BalconyView _view;
	private Button _button;

	protected override void Awake()
	{
		_button = GetComponent<Button>();
		GameManager.Events.OnResourcesAdded += OnResourceChanged;
		GameManager.Events.OnResourcesSpent += OnResourceChanged;
	}

	protected override void Update()
	{
		base.Update();
	}

	public void Show(BalconyView balcony)
	{
		_view = balcony;
		
		var sb = new StringBuilder();
		SetActive(true);
		foreach (var res in balcony._buildCost)
			sb.AppendLine(res.ToString());
		text.SetText(sb);
		rTransform.position = balcony.transform.position;
		_button.onClick.AddListener(() =>
		{
			if (!GameManager.ResourcesService.HasResources(balcony._buildCost))
				return;
			GameManager.ResourcesService.SpendResources(balcony._buildCost);
			GameManager.BalconiesService.UnlockBalcony(balcony.Data.Id);
		});

		_button.interactable = GameManager.ResourcesService.HasResources(_view._buildCost);
	}

	void OnResourceChanged(ResourceIdentifier _, int __)
	{
		_button.interactable = GameManager.ResourcesService.HasResources(_view._buildCost);

	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if(GameManager.Events != null)
		{
			GameManager.Events.OnResourcesAdded -= OnResourceChanged;
			GameManager.Events.OnResourcesSpent -= OnResourceChanged;
		}
	}

	public void Hide()
	{
		SetActive(false);
	}
}
