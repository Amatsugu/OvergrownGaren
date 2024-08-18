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
        
        public BalconyData Data { get; private set; }

#if UNITY_EDITOR
		private void OnValidate()
		{
			if(_isUnlockedByDefault && !_readyToUnlockByDefault)
				_readyToUnlockByDefault = true;
		}
#endif

		private void Awake()
		{
			_unAvailableBalcony.SetActive(true);
			_availableBalcony.SetActive(false);
		}

		public void Bind(BalconyData data)
        {
            Data = data;
			data.view = this;
            Data.OnUnlocked += OnBalconyUnlocked;
        }

		public void Init()
		{
			if (_readyToUnlockByDefault)
			{
				GameManager.BalconiesService.MarkReadyToUnlock(Data.Id);
			}

			if (_isUnlockedByDefault)
			{
				GameManager.BalconiesService.UnlockBalcony(Data.Id);
			}
		}


        private void OnBalconyUnlocked()
        {
            _unAvailableBalcony.SetActive(false);
            _availableBalcony.SetActive(true);
        }

        private void OnDestroy()
        {
            Data.OnUnlocked -= OnBalconyUnlocked;
        }
    }
}