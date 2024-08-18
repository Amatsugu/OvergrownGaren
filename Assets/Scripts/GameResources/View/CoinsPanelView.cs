using Resources;
using TMPro;
using UnityEngine;

namespace GameResources.View
{
    public class CoinsPanelView : MonoBehaviour
    {
        public TMP_Text _textCoinsAmount;

        private void Start()
        {
            GameManager.Events.OnResourcesAdded += OnResourcesAmountChanged;
            GameManager.Events.OnResourcesSpent += OnResourcesAmountChanged;

            _textCoinsAmount.text = GameManager.ResourcesService.GetAmount(ResourceType.Coins).ToString();
        }

        private void OnDestroy()
        {
            // GameManager.Events.OnResourcesAdded -= OnResourcesAmountChanged;
            // GameManager.Events.OnResourcesSpent -= OnResourcesAmountChanged;
        }

        private void OnResourcesAmountChanged(ResourceIdentifier resource, int amountTotal)
        {
            if (resource.type != ResourceType.Coins)
            {
                return;
            }

            _textCoinsAmount.text = amountTotal.ToString();
        }
    }
}