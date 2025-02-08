using UnityEditor;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Editor {
    [CustomEditor(typeof(Map))]
    public class MapInspector : UnityEditor.Editor {

        #region Accessors

        private Map Map => target as Map;

        #endregion
        
        #region Editor Implementation

        public override void OnInspectorGUI() {
            if (GUILayout.Button("Init")) {
                Map.Init(true);
                EditorUtility.SetDirty(Map);
            }
            base.OnInspectorGUI();
        }

        #endregion
    }
}