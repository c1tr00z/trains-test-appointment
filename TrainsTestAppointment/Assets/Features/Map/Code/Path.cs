using c1tr00z.TrainsAppointment.Map.Nodes;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map {
    public class Path : MonoBehaviour {
        
        #region Serialized Fields

        [SerializeField] private Node _nodeA;
        
        [SerializeField] private Node _nodeB;

        [SerializeField] private float _length = 50;

        #endregion

        #region Accessors

        public float Length => _length;

        public Node[] Nodes => new[] {
            _nodeA, _nodeB
        };

        #endregion

        #region Class Implementation

        public void SetNodeA(Node newNodeA) {
            _nodeA = newNodeA;
        }

        public void SetNodeB(Node newNodeB) {
            _nodeB = newNodeB;
        }

        #endregion
    }
}