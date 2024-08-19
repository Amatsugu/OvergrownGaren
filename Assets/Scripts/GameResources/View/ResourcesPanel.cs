using System;
using System.Collections.Generic;
using System.Linq;
using Resources;
using Unity.VisualScripting;
using UnityEngine;

namespace GameResources.View
{
    
    public class ResourcesPanel : MonoBehaviour
    {
        public WidgetResource _prefab;
        public Transform _resourcesContainer;

		public List<WidgetResource> widgets = new();
        
        private void Start()
        {
            var allResources = GameManager.ResourcesService.AllResources;
            foreach (var resourceData in allResources)
            {
                CreateWidget(resourceData);
            }
            
            GameManager.ResourcesService.OnResourceCreated += OnResourceCreated;
        }

        private void OnDestroy()
        {
            // GameManager.ResourcesService.OnResourceCreated -= OnResourceCreated;
        }

        private void OnResourceCreated(ResourceData resourceData)
        {
            CreateWidget(resourceData);
        }

        private void CreateWidget(ResourceData resourceData)
        {
            if (resourceData.Type == ResourceType.Coins)
            {
                return; // no need for coins
            }
            
            var createdWidget = Instantiate(_prefab, _resourcesContainer);
            createdWidget.Bind(resourceData.Type);
            
            createdWidget.OnClicked += OnResourceWidgetClicked;
            createdWidget.OnDestroyed += OnResourceWidgetDestroyed;
            
			widgets.Add(createdWidget);
        }

        private void OnResourceWidgetClicked(WidgetResource clickedWidget)
        {
            DeselectAll();
            clickedWidget.SetSelected();
        }
        
        private void OnResourceWidgetDestroyed(WidgetResource destroyedWidget)
        {
            destroyedWidget.OnDestroyed -= OnResourceWidgetDestroyed;
            widgets.Remove(destroyedWidget);
        }

        public void DeselectAll()
		{
			foreach (var item in widgets)
				item.Deselect();
		}
    }
}