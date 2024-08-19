using System.Collections.Generic;
using System.Linq;
using GameResources.View;
using Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UX
{
    public class UIShopSailTab : MonoBehaviour
    {
        public WidgetResource _prefab;
        public Transform _container;
        public Button _btnSellOne;
        public Button _btnSellAll;
        public TMP_Text _textPriceOne;
        public TMP_Text _textPriceAll;
        

        private readonly List<WidgetResource> _widgets = new();
        private WidgetResource _selectedWidget;
        
        private void OnEnable()
        {
            RefreshResources();
            
            _btnSellOne.onClick.AddListener(OnSellOneClicked);
            _btnSellAll.onClick.AddListener(OnSellAllClicked);

            _selectedWidget = null;
            _btnSellAll.gameObject.SetActive(false);
            _btnSellOne.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _btnSellOne.onClick.RemoveListener(OnSellOneClicked);
            _btnSellAll.onClick.RemoveListener(OnSellAllClicked);
        }

        private void RefreshResources()
        {
            for (var i = 0; i < _container.childCount; i++)
            {
                Destroy(_container.GetChild(i).gameObject);
            }

            var allSellableResources = GameManager.ResourcesService.AllResources.Where(r => r.Type != ResourceType.Coins && r.Amount > 0);
            foreach (var sellableResource in allSellableResources)
            {
                var widget = Instantiate(_prefab, _container);
                widget.Bind(sellableResource.Type);
                
                widget.OnClicked += OnResourceWidgetClicked;
                widget.OnDestroyed += OnResourceWidgetDestroyed;
                
                _widgets.Add(widget);
            }
        }

        private void DeselectAll()
        {
            foreach (var widgetResource in _widgets)
            {
                widgetResource.Deselect();
            }
        }

        private void OnResourceWidgetClicked(WidgetResource clickedWidget)
        {
            DeselectAll();
            clickedWidget.SetSelected();
            _selectedWidget = clickedWidget;
            
            _btnSellAll.gameObject.SetActive(true);
            _btnSellOne.gameObject.SetActive(true);
            _textPriceOne.text = $"Sell One (+{_selectedWidget.ResourceType.GetCost().qty})";
            var resourcesAmount = GameManager.ResourcesService.GetAmount(_selectedWidget.ResourceType);
            var totalPrice = resourcesAmount * _selectedWidget.ResourceType.GetCost().qty;
            _textPriceAll.text = $"Sell All (+{totalPrice})";
        }
        
        private void OnResourceWidgetDestroyed(WidgetResource destroyedWidget)
        {
            destroyedWidget.OnDestroyed -= OnResourceWidgetDestroyed;
            _widgets.Remove(destroyedWidget);
        }
        
        private void OnSellOneClicked()
        {
            if (_selectedWidget == null)
            {
                return;
            }
            
            GameManager.ResourcesService.AddResources((ResourceType.Coins, _selectedWidget.ResourceType.GetCost().qty));
            GameManager.ResourcesService.SpendResources((_selectedWidget.ResourceType,1));

            if (GameManager.ResourcesService.GetAmount(_selectedWidget.ResourceType) <= 0)
            {
                Destroy(_selectedWidget);
                _selectedWidget = null;
                _btnSellAll.gameObject.SetActive(false);
                _btnSellOne.gameObject.SetActive(false);
            }
        }

        private void OnSellAllClicked()
        {
            if (_selectedWidget == null)
            {
                return;
            }

            var amount = GameManager.ResourcesService.GetAmount(_selectedWidget.ResourceType);
            GameManager.ResourcesService.AddResources((ResourceType.Coins, _selectedWidget.ResourceType.GetCost().qty * amount));
            GameManager.ResourcesService.SpendResources((_selectedWidget.ResourceType,amount));
            Destroy(_selectedWidget);
            _selectedWidget = null;
            _btnSellAll.gameObject.SetActive(false);
            _btnSellOne.gameObject.SetActive(false);
        }
    }
}