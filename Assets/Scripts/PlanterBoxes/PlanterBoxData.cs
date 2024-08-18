using System;

namespace PlanterBoxes
{
    public class PlanterBoxData
    {
        public event Action<bool> OnAvailableChanged; 
        
        public int Id;
        public int BalconyId { get; set; }

        public bool IsAvailable
        {
            get => _isAvailable;
            set
            {
                if (value != _isAvailable)
                {
                    _isAvailable = value;
                    OnAvailableChanged?.Invoke(_isAvailable);
                }
            }
        }

        private bool _isAvailable;
    }
}