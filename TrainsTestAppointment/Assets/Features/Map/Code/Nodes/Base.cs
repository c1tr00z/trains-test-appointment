using System;
using c1tr00z.TrainsAppointment.MineResources;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Nodes {
    public class Base : Node, IPassableNode {
        
        #region Serialized Fields

        [SerializeField] private float _currencyMultiplier;

        #endregion

        #region Accessors

        public float CurrencyMultiplier => _currencyMultiplier;

        #endregion

        #region IPassableNode Implementation

        public void Pass(INodePasser passer) {
            if (passer is not IResourceHolder resourceHolder) {
                return;
            }

            if (!resourceHolder.HasResource) {
                return;
            }
            
            resourceHolder.RemoveResource();
            ResourcesUtils.OnResourceCollected(_currencyMultiplier);
        }

        #endregion
    }
}