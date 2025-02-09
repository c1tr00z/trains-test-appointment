using System;
using Features.MineResources.Code;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Nodes {
    public class Base : Node, IPassableNode {

        #region Events

        public static event Action<float> ResourceCollected;

        #endregion
        
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
            ResourceCollected?.Invoke(_currencyMultiplier);
        }

        #endregion
    }
}