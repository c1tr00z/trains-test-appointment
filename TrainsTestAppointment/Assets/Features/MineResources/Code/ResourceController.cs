using System;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.MineResources {
    public class ResourceController : MonoBehaviour {

        #region Events

        public static event Action<float> ResourcesAmountChanged; 

        #endregion
        
        #region Private Fields

        private float _resourcesAmount;

        #endregion

        #region Unity Events

        private void OnEnable() {
            ResourcesUtils.ResourceCollected += OnResourceCollected;
        }

        private void OnDisable() {
            ResourcesUtils.ResourceCollected -= OnResourceCollected;
        }

        #endregion

        #region Class Implementation

        private void OnResourceCollected(float resourcesDelta) {
            _resourcesAmount += resourcesDelta;
            ResourcesAmountChanged?.Invoke(_resourcesAmount);
        }

        #endregion
    }
}