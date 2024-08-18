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
        
		public void AddResources(params ResourceIdentifier[] resources)
		{
			foreach (var res in resources)
				AddResource(res);
		}

        public bool AddResource(ResourceIdentifier resource)
        {
            if (resource.qty < 0)
            {
                return false;
            }

            if (_resourcesMap.TryGetValue(resource.type, out var data))
            {
                data.Amount += resource.qty;
            }
            else
            {
                data = new ResourceData
                {
                    Type = resource.type, 
                    Amount = resource.qty
                };
                _resourcesMap[resource.type] = data;
                OnResourceCreated?.Invoke(data);
            }
            
            GameManager.Events.InvokeResourcesAdded(resource, data.Amount);
            
            return true;
        }
        
		public void SpendResources(ResourceIdentifier[] resources)
		{
			foreach (var res in resources)
				SpendResource(res);
		}

        public bool SpendResource(ResourceIdentifier resource)
        {
            if (resource < 0)
            {
                return false;
            }

            if (!_resourcesMap.TryGetValue(resource.type, out var data))
            {
                return false;   // no resources
            }

            if (resource > data.Amount)
            {
                return false; // not enough
            }

            data.Amount -= resource.qty;
            
            GameManager.Events.InvokeResourcesSpent(resource, data.Amount);
            return true;
        }

        public bool IsEnough(ResourceIdentifier resource)
        {
            if (!_resourcesMap.TryGetValue(resource.type, out var data))
            {
                return false;
            }

            return resource < data.Amount;
        }

		public bool IsEnough(ResourceIdentifier[] resources)
		{
			foreach (var resource in resources)
			{
				if(!IsEnough(resource))
					return false;
			}
			return true;
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