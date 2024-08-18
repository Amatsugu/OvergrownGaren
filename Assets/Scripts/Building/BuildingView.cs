using Balconies;
using UnityEngine;

namespace Building
{
    public class BuildingView : MonoBehaviour
    {
        public BuildingData BuildingData;
        
        public void Bind(BuildingData buildingData)
        {
            BuildingData = buildingData;
            
            var allBalconies = GetComponentsInChildren<BalconyView>();
            foreach (var balconyView in allBalconies)
            {
                var balconyData = BuildingData.CreateBalcony();
                balconyView.Bind(balconyData);
            }
        }
    }
}