using UnityEditor;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Nodes.Editor {
    [CustomEditor(typeof(Node))]
    public class NodeInspector : UnityEditor.Editor {
        
        #region Node Implementation

        public override void OnInspectorGUI() {
            if (GUILayout.Button("Add new path")) {
                Debug.Log("TEST");
            }
            base.OnInspectorGUI();
        }

        #endregion
    }
}