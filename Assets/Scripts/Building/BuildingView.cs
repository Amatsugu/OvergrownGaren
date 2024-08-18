using System;
using System.Collections.Generic;
using Balconies;
using Unity.Mathematics;
using UnityEngine;

using UnityEngineInternal;

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

			foreach (var b in allBalconies)
				b.Init();
        }

        private void OnDestroy()
        {
            // GameManager.Events.OnBalconyUnlocked -= OnBalconyUnlocked;
        } 

		private void OnBalconyUnlocked(BalconyData balconyData)
		{
			Debug.Log($"Unlocking {balconyData.view.name}", balconyData.view);
            var sourceBalconyView = _balconiesMap[balconyData];
            
            FindBalconyAndTryToMarkItReadyToUnlock(sourceBalconyView.transform, Vector2.up);
            FindBalconyAndTryToMarkItReadyToUnlock(sourceBalconyView.transform, Vector2.down);
            FindBalconyAndTryToMarkItReadyToUnlock(sourceBalconyView.transform, Vector2.left);
            FindBalconyAndTryToMarkItReadyToUnlock(sourceBalconyView.transform, Vector2.right);
        }

        private void FindBalconyAndTryToMarkItReadyToUnlock(Transform t, Vector2 direction)
        {
            var hit = Physics2D.Raycast(t.position, direction, 4, 1 << 6); //6 Balcony Layer
			Debug.DrawRay(t.position, direction * 4, Color.magenta, 5);
			if (hit.collider == null)
				return;
            var hitBalcony = hit.collider.GetComponentInParent<BalconyView>();

			Debug.Log(hit.collider.gameObject.name, hit.collider);
            if (hitBalcony)
            {
                GameManager.BalconiesService.MarkReadyToUnlock(hitBalcony.Data.Id);
            }
        }
    }
}