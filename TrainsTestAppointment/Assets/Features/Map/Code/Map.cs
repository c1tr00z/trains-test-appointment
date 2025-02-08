using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.TrainsAppointment.Map.Nodes;
using c1tr00z.TrainsAppointment.Utils;
using UnityEngine;
using Random = UnityEngine.Random;
namespace c1tr00z.TrainsAppointment.Map {
    [ExecuteInEditMode]
    public class Map : MonoBehaviour {

        #region Serialized Fields

        [SerializeField] private List<Path> _paths = new();

        [SerializeField] private Node _nodePrefab;

        #endregion

        #region Class Implementation

        public void Init(bool reinit) {
            if (_paths.Count > 0 && !reinit) {
                return;
            }

            if (_paths.Count > 0) {
                var allNodes = _paths.SelectMany(p => p.Nodes).ToUniqueList();
                allNodes.ForEach(n => DestroyImmediate(n.gameObject));
                _paths.ForEach(path => {
                    DestroyImmediate(path.gameObject);
                });
                _paths.Clear();
            }

            var nodes = new Node[] {
                MakeNode(CircleUtils.CalculatePointOnCircle(new Vector2(0, 0), 0, 50).ToVector3Horizontal()),
                MakeNode(CircleUtils.CalculatePointOnCircle(new Vector2(0, 0), 120, 50).ToVector3Horizontal()),
                MakeNode(CircleUtils.CalculatePointOnCircle(new Vector2(0, 0), 240, 50).ToVector3Horizontal()),
            };
            
            _paths.Add(MakePath(nodes[0], nodes[1]));
            _paths.Add(MakePath(nodes[1], nodes[2]));
            _paths.Add(MakePath(nodes[2], nodes[0]));
        }

        private Path MakePath(Node nodeA, Node nodeB) {
            var pathGO = new GameObject($"Path{_paths.Count}");
            var path = pathGO.AddComponent<Path>();
            path.transform.parent = transform;
            path.SetNodeA(nodeA);
            path.SetNodeB(nodeB);
            path.Length = (nodeA.transform.position - nodeB.transform.position).magnitude;
            return path;
        }

        private Node MakeNode(Vector3 point) {
            var node = Instantiate<Node>(_nodePrefab);
            node.gameObject.name = $"Node{_paths.Count}_{Random.Range(0, 9999)}";
            node.transform.position = point;
            node.transform.parent = transform;
            return node;
        }

        public void MakeAnotherNode(Node parentNode) {
            var parentPath = GetNodePaths(parentNode).FirstOrDefault();
            var direction = (parentPath.Nodes.FirstOrDefault(n => n != parentNode).transform.position - parentNode.transform.position).normalized;
            var newPosition = parentNode.transform.position + direction * 10;
            var newNode = MakeNode(newPosition);
            _paths.Add(MakePath(parentNode, newNode));
        }

        public List<Path> GetNodePaths(Node node) {
            return _paths.Where(p => p.Nodes.Contains(node)).ToList();
        }

        public void ResetPathsValues() {
            _paths.ForEach(p => {
                p.Length = (p.Nodes[0].transform.position - p.Nodes[1].transform.position).magnitude;
            });
        }

        #endregion

        #region Unity Events

        private void OnDrawGizmos() {
            if (_paths.Count == 0) {
                return;
            }

            Gizmos.color = Color.red;
            _paths.ForEach(p => Gizmos.DrawLine(p.Nodes[0].transform.position, p.Nodes[1].transform.position));
        }

        #endregion
    }
}