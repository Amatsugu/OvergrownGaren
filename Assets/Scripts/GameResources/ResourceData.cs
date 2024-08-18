using System;

namespace Resources
{
    public class ResourceData
    {
        public event Action<int> OnAmountChanged; 
        
        public ResourceType Type { get; set; }
        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnAmountChanged?.Invoke(_amount);
                }
            }
        }

        private int _amount;
    }
}