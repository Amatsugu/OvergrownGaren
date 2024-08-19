using System;
using Resources;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace GameResources.View
{
    public class WidgetResource : MonoBehaviour
    {
		public GameObject selectedIndicator;
        public TMP_Text _textAmount;
        public TextMeshProUGUI _iconDisplay;
        public TextMeshProUGUI _titleText;
		public bool isSelected;

		private Button _button;

		public ResourceType ResourceType { get; private set; }

		public event Action<WidgetResource> OnClicked;
		public event Action<WidgetResource> OnDestroyed; 

		private void Start()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(OnClick); 
		}

		public void Bind(ResourceType resourceType)
        {
			ResourceType = resourceType;
            _iconDisplay.SetText(resourceType.GetSprite());
			var qty = GameManager.ResourcesService.GetAmount(resourceType);
			_titleText.SetText(resourceType.ToDisplayString(qty > 1));

            GameManager.Events.OnResourcesAdded += OnResourcesAmountChanged;
            GameManager.Events.OnResourcesSpent += OnResourcesAmountChanged;

            _textAmount.text = qty.ToString();
        }

        private void OnDestroy()
        {
	        OnDestroyed?.Invoke(this);

	        if (GameManager.INST != null)
	        {
		        GameManager.Events.OnResourcesAdded -= OnResourcesAmountChanged;
		        GameManager.Events.OnResourcesSpent -= OnResourcesAmountChanged;
	        }
        }

		public void SetSelected()
		{
			isSelected = true;
			selectedIndicator.SetActive(true);
			GameManager.PlanterController.SelectPlant(ResourceType);
		}

		public void Deselect()
		{
			isSelected = false;
			selectedIndicator.SetActive(false);
		}

        private void OnResourcesAmountChanged(ResourceIdentifier resource, int amountTotal)
        {
            if (resource.type != ResourceType)
            {
                return;
            }
            
            var isEnabled = amountTotal > 0;
            gameObject.SetActive(isEnabled);
            
            _textAmount.text = amountTotal.ToString();
			_titleText.SetText(ResourceType.ToDisplayString(amountTotal > 1));
		}

        private void OnClick()
        {
	        OnClicked?.Invoke(this);
        }
	}
}