using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Nodes {
    public class Mine : Node {
        #region Serialized Fields

        [SerializeField] private float _miningMultiplier;

        #endregion

        #region Accessors

        public float MiningMultiplier => _miningMultiplier;

        #endregion
    }
}