using System;
using System.Linq;
using c1tr00z.TrainsAppointment.Map.Nodes;
using c1tr00z.TrainsAppointment.Utils;
using UnityEditor;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map.Editor {
    [CustomEditor(typeof(Path))]
    public class PathInspector : UnityEditor.Editor {

        #region Accessors

        private Path PathObject => target as Path;

        #endregion
        
        #region Editor Implementation

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (PathObject.Nodes.Length < 2) {
                PathErrorLabel();
                return;
            }
            var singlePathNode = PathObject.Nodes.FirstOrDefault(n => n.GetPaths().Count == 1);
            if (singlePathNode is null) {
                MultiplePathsNodeEdit();
            } else {
                SinglePathNodeEdit(singlePathNode);
            }
        }

        #endregion

        #region Class Implementation

        private void MultiplePathsNodeEdit() {
            var pathNodes = PathObject.Nodes;
            if (pathNodes.Length < 2) {
                PathErrorLabel();
                return;
            }
            // node that will move
            var moveableNode = pathNodes.FirstOrDefault(n => n.GetPaths().Count < 3);
            // neighbour node that will not move
            var staticNode = pathNodes.FirstOrDefault(n => n != moveableNode);
            // path, that will move but will not change lenght
            var radiusPath = moveableNode.GetPaths().FirstOrDefault(p => p != PathObject);
            // node, around which we will rotate moveableNode
            var centerNode = radiusPath.Nodes.FirstOrDefault(n => n != moveableNode);
            GUILayout.Label($"LLLL: {PathObject.Length}");
            var staticNodePosition = staticNode.transform.position;
            var centerNodePosition = centerNode.transform.position;
            var centerToStaticDistance = (staticNodePosition - centerNodePosition).magnitude;
            var newLength = EditorGUILayout.Slider("Length", PathObject.Length,
                Math.Abs(radiusPath.Length - centerToStaticDistance), radiusPath.Length * 2);
            if (Mathf.Approximately(newLength, PathObject.Length)) {
                return;
            }
            var middlePoint = CalculateMiddlePoint(centerNodePosition, staticNodePosition, moveableNode.transform.position, radiusPath.Length, newLength);
            var distanceFromMiddlePoint = CalculateDistanceFromMiddlePoint(middlePoint, centerNodePosition, radiusPath.Length);
            var points = new Vector3[] {
                new(middlePoint.x + ((staticNodePosition.z - centerNodePosition.z) / centerToStaticDistance) * distanceFromMiddlePoint, 0,
                    middlePoint.z - ((staticNodePosition.x - centerNodePosition.x) / centerToStaticDistance) * distanceFromMiddlePoint),
                new(middlePoint.x - ((staticNodePosition.z - centerNodePosition.z) / centerToStaticDistance) * distanceFromMiddlePoint, 0,
                    middlePoint.z + ((staticNodePosition.x - centerNodePosition.x) / centerToStaticDistance) * distanceFromMiddlePoint),
            };
            var nextPoint = points.MinElement(p => (moveableNode.transform.position - p).magnitude);
            moveableNode.transform.position = nextPoint;
            PathObject.Length = newLength;
        }
        
        private void SinglePathNodeEdit(Node node) {
            var otherNode = PathObject.Nodes.FirstOrDefault(n => n != node);
            if (otherNode is null) {
                PathErrorLabel();
                return;
            }
            var newLenght = EditorGUILayout.FloatField("Lenght", PathObject.Length);
            if (Mathf.Approximately(newLenght, PathObject.Length)) {
                return;
            }
            PathObject.Length = newLenght;
            EditorUtility.SetDirty(PathObject);
            var direction = (node.transform.position - otherNode.transform.position).normalized;
            node.transform.position = otherNode.transform.position + direction * newLenght;
            EditorUtility.SetDirty(node);
        }

        private void PathErrorLabel() {
            EditorGUILayout.HelpBox("Wrong path settings. Probably nodes missing", MessageType.Error);
        }
        
        private Vector3 CalculateMiddlePoint(Vector3 centerPosition, Vector3 staticPosition, Vector3 moveablePosition, float centerRadius, float newLenght) {
            var centerToStaticDistance = (staticPosition - centerPosition).magnitude;
            var centerAngle = Mathf.Rad2Deg
                              * Mathf.Acos((Mathf.Pow(centerRadius, 2) + Mathf.Pow(centerToStaticDistance, 2)
                                            - Mathf.Pow(newLenght, 2)) / (2 * centerRadius * centerToStaticDistance));
            
            var cathetus = centerRadius * Mathf.Sin(Mathf.Deg2Rad * centerAngle);
            return centerPosition + Mathf.Sqrt(Mathf.Pow(centerRadius, 2) - Mathf.Pow(cathetus, 2)) * (staticPosition - centerPosition).normalized;
        }

        private float CalculateDistanceFromMiddlePoint(Vector3 middlePoint, Vector3 center, float centerRadius) {
            var distance = (middlePoint - center).magnitude;
            return Mathf.Sqrt(Mathf.Pow(centerRadius, 2) - Mathf.Pow(distance, 2));
        }

        private float CalculateAngle(Vector3 startPoint, Vector3 vec1EndPoint, Vector3 vec2EndPoint) {
            return Vector2.Angle((vec1EndPoint - startPoint).ToVector2FromHorizontal(),
                (vec2EndPoint - startPoint).ToVector2FromHorizontal());
        }

        #endregion
    }
}