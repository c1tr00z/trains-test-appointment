using System.Collections;
using Features.MineResources.Code;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Nodes {
    public class Mine : Node, IPassableNode {
        
        #region Serialized Fields

        [SerializeField] private float _miningMultiplier;

        #endregion

        #region Accessors

        public float MiningMultiplier => _miningMultiplier;

        #endregion

        #region IPassableNode Implementation

        public void Pass(INodePasser passer) {
            StartCoroutine(C_WaitAndRelease(passer));
        }

        #endregion

        #region Class Implementation

        private IEnumerator C_WaitAndRelease(INodePasser passer) {
            passer.Occupy();
            if (passer is IResourceHolder resourceHolder) {
                var timeToWait = resourceHolder.TimeToMine * MiningMultiplier;
                yield return new WaitForSeconds(timeToWait);
                resourceHolder.AddResource();
            }
            passer.Release();
        }

        #endregion
    }
}