using System;
using System.Collections.Generic;
using Balconies;
using Unity.Mathematics;
using UnityEngine;

namespace Building
{
    public class BuildingView : MonoBehaviour
    {
        public BuildingData BuildingData;

        private readonly Dictionary<BalconyData, BalconyView> _balconiesMap = new();
        
        public void Bind(BuildingData buildingData)
        {
            BuildingData = buildingData;

            var allBalconies = GetComponentsInChildren<BalconyView>();
            foreach (var balconyView in allBalconies)
            {
                var balconyData = BuildingData.CreateBalcony();
                balconyView.Bind(balconyData);
                
                _balconiesMap[balconyData] = balconyView;
            }
            
            GameManager.Events.OnBalconyUnlocked += OnBalconyUnlocked;
        }

        private void OnDestroy()
        {
            // GameManager.Events.OnBalconyUnlocked -= OnBalconyUnlocked;
        }

        private void OnBalconyUnlocked(BalconyData balconyData)
        {
            var sourceBalconyView = _balconiesMap[balconyData];
            
            FindBalconyAndTryToMarkItReadyToUnlock(sourceBalconyView.transform, Vector2.up);
            FindBalconyAndTryToMarkItReadyToUnlock(sourceBalconyView.transform, Vector2.down);
            FindBalconyAndTryToMarkItReadyToUnlock(sourceBalconyView.transform, Vector2.left);
            FindBalconyAndTryToMarkItReadyToUnlock(sourceBalconyView.transform, Vector2.right);
        }

        private void FindBalconyAndTryToMarkItReadyToUnlock(Transform t, Vector2 direction)
        {
            var hit = Physics2D.Raycast(t.position, direction);
            var hitBalcony = hit.collider?.GetComponentInParent<BalconyView>();

            if (hitBalcony)
            {
                GameManager.BalconiesService.MarkReadyToUnlock(hitBalcony.Data.Id);
            }
        }
    }
}