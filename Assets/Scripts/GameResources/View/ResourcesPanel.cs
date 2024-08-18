using System;
using System.Linq;
using Resources;
using Unity.VisualScripting;
using UnityEngine;

namespace GameResources.View
{
    [Serializable]
    public class ResourceIconMapping
    {
        public ResourceType ResourceType;
        public Sprite Icon;
    }
    
    public class ResourcesPanel : MonoBehaviour
    {
        public WidgetResource _prefab;
        public Transform _resourcesContainer;
        public ResourceIconMapping[] _iconMappings;
        
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
            var icon = _iconMappings.FirstOrDefault(m => m.ResourceType == resourceData.Type)?.Icon;
            createdWidget.Bind(resourceData.Type, icon);
        }
    }
}