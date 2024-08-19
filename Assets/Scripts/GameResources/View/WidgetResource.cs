using Resources;
using TMPro;
using UnityEngine;
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
		private ResourcesPanel _resourcesPanel;

		public ResourceType ResourceType { get; private set; }

		private void Start()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(() => SetSelected()); 
		}

		public void Bind(ResourceType resourceType, ResourcesPanel panel)
        {
			ResourceType = resourceType;
			_resourcesPanel = panel;
            _iconDisplay.SetText(resourceType.GetSprite());
			var qty = GameManager.ResourcesService.GetAmount(resourceType);
			_titleText.SetText(resourceType.ToDisplayString(qty > 1));

            GameManager.Events.OnResourcesAdded += OnResourcesAmountChanged;
            GameManager.Events.OnResourcesSpent += OnResourcesAmountChanged;

            _textAmount.text = qty.ToString();
        }

        private void OnDestroy()
        {
			_resourcesPanel.widgets.Remove(this);
        }

		public void SetSelected()
		{
			isSelected = true;
			_resourcesPanel.DeselectAll();
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
	}
}