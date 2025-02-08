using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Nodes {
    public class Base : Node {
        #region Serialized Fields

        [SerializeField] private float _currencyMultiplier;

        #endregion

        #region Accessors

        public float CurrencyMultiplier => _currencyMultiplier;

        #endregion

        #region Class Implementation

        // private 

        #endregion
    }
}