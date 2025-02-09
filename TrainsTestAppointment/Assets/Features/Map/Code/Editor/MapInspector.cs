using System.Linq;
using UnityEditor;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Editor {
    [CustomEditor(typeof(Map))]
    public class MapInspector : UnityEditor.Editor {

        #region Private Fields

        private Material _railMaterial;

        #endregion

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
            _railMaterial = EditorGUILayout.ObjectField("Rail material", _railMaterial, typeof(Material), false) as Material;
            if (GUILayout.Button("Rebuild rails")) {
                RebuildRailsVisualization();
            }
            base.OnInspectorGUI();
        }

        #endregion

        #region Class Implementation
        
        /// <summary>
        /// Ineffective but easy to implement
        /// </summary>
        public void RebuildRailsVisualization() {
            var mapTransform = MapObject.transform;
            for (var i = 0; i < mapTransform.childCount; i++) {
                var child = mapTransform.GetChild(i);
                if (child.name == "Rails") {
                    DestroyImmediate(child.gameObject);
                }
            }

            var newRailsObject = new GameObject("Rails");
            newRailsObject.transform.parent = mapTransform;
            newRailsObject.transform.localPosition = Vector3.zero;

            var allPaths = MapObject.AllPaths;
            allPaths.ForEach(p => {
                var lineGO = new GameObject($"LR_{newRailsObject.transform.childCount}");
                lineGO.transform.parent = newRailsObject.transform;
                var lineRenderer = lineGO.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 2;
                lineRenderer.sharedMaterial = _railMaterial;
                lineRenderer.SetPositions(p.Nodes.Select(n => n.transform.position).ToArray());
            });
            
            EditorUtility.SetDirty(MapObject);
        }

        #endregion
    }
}
