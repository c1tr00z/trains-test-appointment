using UnityEditor;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Nodes.Editor {
    [CustomEditor(typeof(Node))]
    public class NodeInspector : UnityEditor.Editor {

        #region Accessors

        private Node NodeObject => target as Node;

        #endregion
        
        #region Node Implementation

        public override void OnInspectorGUI() {
            if (GUILayout.Button("Add new node")) {
                FindObjectOfType<Map>().MakeAnotherNode(NodeObject);
            }
            base.OnInspectorGUI();
        }

        #endregion
    }
}