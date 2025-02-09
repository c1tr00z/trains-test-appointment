using System.Collections.Generic;
using System.Linq;
using c1tr00z.TrainsAppointment.Map;
using c1tr00z.TrainsAppointment.Map.Nodes;
using c1tr00z.TrainsAppointment.Utils;
using Unity.VisualScripting;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Pathfinding {
    public class PathfindingController : MonoBehaviour {
        #region Private Fields

        private Map.Map _map;

        #endregion

        #region Accessors

        private Map.Map Map {
            get {
                if (_map is null) {
                    _map = FindObjectOfType<Map.Map>();
                }
                return _map;
            }
        }

        #endregion

        #region Class Implementation

        public List<Route> FindAllPossibleRoutes(Node startNode, Node targetNode) {
            var allPossiblePaths = FindPaths(startNode, targetNode, startNode, new List<Path>());
            return allPossiblePaths.Select(paths => paths.ToRoute(startNode)).Where(r => r.LastOrDefault().targetNode == targetNode).ToList();
        }

        private List<List<Path>> FindPaths(Node startNode, Node targetNode, Node lastNode, List<Path> takenPaths) {
            var result = new List<List<Path>>();
            if (takenPaths.Count == 0) {
                var initialPaths = startNode.GetPaths();
                foreach (var path in initialPaths) {
                    var otherNode = path.Nodes.FirstOrDefault(n => n != startNode);
                    if (otherNode == targetNode) {
                        result.Add(new List<Path> { path });
                        continue;
                    }
                    var newTakenPaths = new List<Path> { path };
                    result.AddRange(FindPaths(startNode, targetNode, otherNode, newTakenPaths));
                }
                return result;
            }

            var lastNodePaths = lastNode.GetPaths().Where(p => !takenPaths.Contains(p)).ToList();
            if (lastNodePaths.Count == 0) {
                return new List<List<Path>> {
                    takenPaths
                };
            }
            foreach (var path in lastNodePaths) {
                var otherNode = path.Nodes.FirstOrDefault(n => n != lastNode);
                var newTakenPaths = takenPaths.ToList();
                newTakenPaths.Add(path);
                result.AddRange(FindPaths(startNode, targetNode, otherNode, newTakenPaths));
            }
            
            return result;
        }

        
        

        // private bool FindNextPaths(Node startNode, Node targetNode, List<List<Path>> allRoutesFound, List<List<Node>> allChainedNodes, List<Path> allPaths) {
        //     if (allRoutesFound.Count == 0) {
        //         var allPathsFits = allPaths.Where(p => p.Nodes.Contains(startNode)).ToList();
        //         foreach (var p in allPathsFits) {
        //             allRoutesFound.Add(new List<Path> { p });
        //             if (p.Nodes.Contains(targetNode)) {
        //                 allChainedNodes.Add(new List<Node> { startNode, targetNode });
        //                 return true;
        //             }
        //             var 
        //         }
        //     }
        //     return false;
        // }

        #endregion
    }
}