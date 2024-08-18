using System;
using System.Collections.Generic;
using PlanterBoxes;

using UnityEngine;

namespace Balconies
{
    public class BalconyData
    {
        public event Action OnUnlocked;
        public event Action<bool> OnReadyToUnlockChanged;
		public BalconyView view;
        public int Id { get; set; }
        public bool IsUnlocked
        {
            get => _isUnlocked;
            set
            {
                if (_isUnlocked != value)
                {
                    _isUnlocked = value;
                    if (value)
                    {
                        OnUnlocked?.Invoke();
                    }
                }
            }
        }
        public bool IsReadyToUnlock
        {
            get => _isReadyToUnlock;
            set
            {
                if (_isReadyToUnlock != value)
                {
                    _isReadyToUnlock = value;
                    OnReadyToUnlockChanged?.Invoke(value);
                }
            }
        }


        private bool _isUnlocked;
        private bool _isReadyToUnlock;

        
    }
}