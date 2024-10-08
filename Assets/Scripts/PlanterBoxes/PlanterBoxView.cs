﻿using System;
using UnityEngine;

namespace PlanterBoxes
{
    public class PlanterBoxView : MonoBehaviour
    {
        public GameObject _availablePlanterBox;
        
        public PlanterBoxData Data { get; private set; }

        public void Bind(PlanterBoxData data)
        {
            if (Data != null)
            {
                return;
            }
            
            Data = data;
            
            Debug.Log("Planter Box created. Balcony: " + data.BalconyId + ", Planter Box: " + data.Id, gameObject);
            
            data.OnAvailableChanged += OnBoxAvailableChanged;
        }

        private void OnBoxAvailableChanged(bool isAvailable)
        {
            _availablePlanterBox.SetActive(isAvailable);
        }


        private void OnDestroy()
        {
            // Data.OnAvailableChanged -= OnBoxAvailableChanged;
        }
    }
}