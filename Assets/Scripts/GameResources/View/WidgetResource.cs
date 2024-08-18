using Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameResources.View
{
    public class WidgetResource : MonoBehaviour
    {
        public TMP_Text _textAmount;
        public Image _imgIcon;
        
        public ResourceType ResourceType { get; private set; }
        
        public void Bind(ResourceType resourceType, Sprite icon)
        {
            ResourceType = resourceType;
            _imgIcon.sprite = icon;

            GameManager.Events.OnResourcesAdded += OnResourcesAmountChanged;
            GameManager.Events.OnResourcesSpent += OnResourcesAmountChanged;

            _textAmount.text = GameManager.ResourcesService.GetAmount(resourceType).ToString();
        }

        private void OnDestroy()
        {
            // GameManager.Events.OnResourcesAdded += OnResourcesAmountChanged;
            // GameManager.Events.OnResourcesSpent += OnResourcesAmountChanged;
        }

        private void OnResourcesAmountChanged(ResourceType type, int amount, int amountTotal)
        {
            if (type != ResourceType)
            {
                return;
            }
            
            var isEnabled = amountTotal > 0;
            gameObject.SetActive(isEnabled);
            
            _textAmount.text = amountTotal.ToString();
        }
    }
}