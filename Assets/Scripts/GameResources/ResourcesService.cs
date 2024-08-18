using System;
using System.Collections.Generic;
using System.Linq;
using Resources;

namespace GameResources
{
    public class ResourcesService
    {
        public event Action<ResourceData> OnResourceCreated;

        public ResourceData[] AllResources => _resourcesMap.Values.ToArray();
        
        private readonly Dictionary<ResourceType, ResourceData> _resourcesMap = new();
        
        public bool AddResource(ResourceType type, int amount)
        {
            if (amount < 0)
            {
                return false;
            }

            if (_resourcesMap.TryGetValue(type, out var data))
            {
                data.Amount += amount;
            }
            else
            {
                data = new ResourceData
                {
                    Type = type, 
                    Amount = amount
                };
                _resourcesMap[type] = data;
                OnResourceCreated?.Invoke(data);
            }
            
            GameManager.Events.InvokeResourcesAdded((type, amount), data.Amount);
            
            return true;
        }
        
        public bool SpendResource(ResourceType type, int amount)
        {
            if (amount < 0)
            {
                return false;
            }

            if (!_resourcesMap.TryGetValue(type, out var data))
            {
                return false;   // no resources
            }

            if (data.Amount <= amount)
            {
                return false; // not enough
            }

            data.Amount -= amount;
            
            GameManager.Events.InvokeResourcesSpent((type, amount), data.Amount);
            return true;
        }

        public bool IsEnough(ResourceType type, int amount)
        {
            if (!_resourcesMap.TryGetValue(type, out var data))
            {
                return false;
            }

            return data.Amount >= amount;
        }

        public int GetAmount(ResourceType type)
        {
            if (_resourcesMap.TryGetValue(type, out var data))
            {
                return data.Amount;
            }

            return 0;
        }
    }
}