using System.Collections.Generic;
using System.Linq;
using Building;
using GameResources;
using UnityEngine;

namespace Balconies
{
    public class BalconiesService
    {
        public List<BalconyData> AllBalconies => _buildingData.Balconies;
        public List<BalconyData> ReadyToUnlockBalconies = new();
        
        private readonly BuildingData _buildingData;
        private readonly ResourcesService _resourcesService;

        public BalconiesService(BuildingData buildingData, ResourcesService resourcesService)
        {
            _buildingData = buildingData;
            _resourcesService = resourcesService;
        }

        public bool UnlockBalcony(int balconyId)
        {
            // we can add purchasing here or not
            var balcony = _buildingData.Balconies.FirstOrDefault(b => b.Id == balconyId);

            if (balcony == null)
            {
                return false;
            }

            if (!balcony.IsReadyToUnlock)
            {
                Debug.Log("Balcony is not ready to unlock");
                return false; // not ready
            }

            balcony.IsUnlocked = true;
            
            GameManager.Events.InvokeBalconyUnlocked(balcony);
            return true;
        }

        public bool MarkReadyToUnlock(int balconyId)
        {
            var balcony = _buildingData.Balconies.FirstOrDefault(b => b.Id == balconyId);

            if (balcony == null)
            {
                return false;
            }

            if (balcony.IsReadyToUnlock)
            {
                return false;
            }

            balcony.IsReadyToUnlock = true;
            
            ReadyToUnlockBalconies.Add(balcony);
            
            GameManager.Events.InvokeBalconyReadyToUnlock(balcony);
            return true;
        }

        public bool IsReadyToUnlock(int balconyId)
        {
            var balcony = _buildingData.Balconies.FirstOrDefault(b => b.Id == balconyId);

            if (balcony == null)
            {
                return false;
            }

            return balcony.IsReadyToUnlock;
        }
    }
}