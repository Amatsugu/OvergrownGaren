using PlanterBoxes;
using Resources;
using UnityEngine;

namespace Balconies
{
    public class BalconyView : MonoBehaviour
    {
        public ResourceType _buildPriceType;
        public int _buildPriceAmount;
        public bool _readyToUnlockByDefault;
        
        public GameObject _unAvailableBalcony;
        public GameObject _availableBalcony;
        public Transform _planterBoxesContainer;
        
        public BalconyData Data { get; private set; }
        
        public void Bind(BalconyData data)
        {
            Data = data;

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

            Data.OnUnlocked += OnBalconyUnlocked;
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