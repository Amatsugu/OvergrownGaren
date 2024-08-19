using Resources;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ShopResource : MonoBehaviour
{
	public TextMeshProUGUI iconDisplay;
	public TextMeshProUGUI nameDisplay;
	public TextMeshProUGUI priceDisplay;


	private ResourceType _resource;
	private Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	public void SetResource(ResourceType resource)
	{
		_resource = resource;
		iconDisplay.SetText(resource.GetSprite());
		nameDisplay.SetText(resource.ToDisplayString());
		priceDisplay.SetText(resource.GetCost().ToString());
		Refresh();
	}

	public void Refresh()
	{
		_button.interactable = GameManager.ResourcesService.HasResources(_resource.GetCost());
	}
}
