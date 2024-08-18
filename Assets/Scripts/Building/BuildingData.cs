using System;
using System.Collections.Generic;
using Balconies;

namespace Building
{
    public class BuildingData
    {
        public event Action<BalconyData> OnBalconyAdded; 
        
        public List<BalconyData> Balconies = new();

        private int _lastBalconyId;

        public BalconyData CreateBalcony()
        {
            var data = new BalconyData
            {
                Id = _lastBalconyId,
                IsUnlocked = false
            };
            
            Balconies.Add(data);
            OnBalconyAdded?.Invoke(data);

            _lastBalconyId++;

            return data;
        }
    }
}