using TMPro;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.MineResources {
    public class ResourceCounterUI : MonoBehaviour {
        
        #region Serialized Fields

        [SerializeField] private TMP_Text _resourcesLabel;

        #endregion

        #region Unity Events

        private void OnEnable() {
            ResourceController.ResourcesAmountChanged += OnResourcesAmountChanged;
        }

        private void OnDisable() {
            ResourceController.ResourcesAmountChanged -= OnResourcesAmountChanged;
        }

        #endregion

        #region Class Implementation
        
        private void OnResourcesAmountChanged(float resourcesAmount) {
            _resourcesLabel.text = resourcesAmount.ToString();
        }

        #endregion
    }
}