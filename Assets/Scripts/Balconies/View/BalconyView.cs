﻿using PlanterBoxes;
using Resources;
using UnityEngine;

namespace Balconies
{
    public class BalconyView : MonoBehaviour
    {
		public ResourceIdentifier[] _buildCost;
        public bool _readyToUnlockByDefault;
        public bool _isUnlockedByDefault;
        
        public GameObject _unAvailableBalcony;
        public GameObject _availableBalcony;
        public GameObject _isReadyToUnlockMarker;
        public Transform _planterBoxesContainer;
        
        public BalconyData Data { get; private set; }

#if UNITY_EDITOR
		private void OnValidate()
		{
			if(_isUnlockedByDefault && !_readyToUnlockByDefault)
				_readyToUnlockByDefault = true;
		}
#endif

		public void Bind(BalconyData data)
        {
            Data = data;
			data.view = this;
            Data.OnUnlocked += OnBalconyUnlocked;
            Data.OnReadyToUnlockChanged += OnReadyToUnlock;

            var allPlanterBoxes = _planterBoxesContainer.GetComponentsInChildren<PlanterBoxView>(true);
            foreach (var planterBoxView in allPlanterBoxes)
            {
                var planterBoxData = data.CreatePlanterBoxData();
                planterBoxView.Bind(planterBoxData);
            }

            if (_readyToUnlockByDefault)
            {
                GameManager.BalconiesService.MarkReadyToUnlock(data.Id);
            }

            if (_isUnlockedByDefault)
            {
                GameManager.BalconiesService.UnlockBalcony(data.Id);
            }
        }

        private void OnReadyToUnlock(bool isReady)
        {
            var isEnabled = isReady && !Data.IsUnlocked;
            _isReadyToUnlockMarker.SetActive(isEnabled);
        }

        private void OnBalconyUnlocked()
        {
            _unAvailableBalcony.SetActive(false);
            _availableBalcony.SetActive(true);
        }

        private void OnDestroy()
        {
            Data.OnUnlocked -= OnBalconyUnlocked;
            Data.OnReadyToUnlockChanged -= OnReadyToUnlock;
        }
    }
}