using System;
using System.Collections.Generic;
using PlanterBoxes;

namespace Balconies
{
    public class BalconyData
    {
        public event Action OnUnlocked;
        public event Action<PlanterBoxData> OnPlanterBoxAdded;

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

        public List<PlanterBoxData> PlanterBoxes = new();

        private bool _isUnlocked;
        private int _lastPlanterBoxId = 0;

        public PlanterBoxData CreatePlanterBoxData()
        {
            var data = new PlanterBoxData
            {
                Id = _lastPlanterBoxId,
                IsAvailable = false,
                BalconyId = Id
            };
            
            PlanterBoxes.Add(data);
            OnPlanterBoxAdded?.Invoke(data);

            _lastPlanterBoxId++;

            return data;
        }
    }
}