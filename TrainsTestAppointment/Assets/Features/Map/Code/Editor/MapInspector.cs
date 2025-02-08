using UnityEditor;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Editor {
    [CustomEditor(typeof(Map))]
    public class MapInspector : UnityEditor.Editor {

        #region Accessors

        private Map MapObject => target as Map;

        #endregion
        
        #region Editor Implementation

        public override void OnInspectorGUI() {
            if (GUILayout.Button("Init")) {
                MapObject.Init(true);
                EditorUtility.SetDirty(MapObject);
            }
            if (GUILayout.Button("Reset paths values")) {
                MapObject.ResetPathsValues();
            }
            base.OnInspectorGUI();
        }

        #endregion
    }
}
