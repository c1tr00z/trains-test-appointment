using UnityEditor;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Editor {
    [CustomEditor(typeof(Map))]
    public class MapInspector : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            if (GUILayout.Button("Init")) {
                FindObjectOfType<Map>().Init(true);
            }
            base.OnInspectorGUI();
        }
    }
}