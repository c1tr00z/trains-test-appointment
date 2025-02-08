using System.Collections.Generic;
using System.Linq;
using c1tr00z.TrainsAppointment.Map;
using c1tr00z.TrainsAppointment.Map.Nodes;
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

        public Route FindAllPossiblePaths() {
            // var allPaths = Map.AllPaths.ToList();
            // var allNodes = Map.GetAllNodes();
            // var allMines = allNodes.OfType<Mine>().ToList();
            // var allBases = allNodes.OfType<Base>().ToList();
            // allMines.ForEach(m => {
            //     var startPaths = allPaths.Where(p => p.Nodes.Contains(m)).ToList();
            //     var allRoutesFound = new List<List<Path>>();
            //     var allChainedNodes = new List<List<Node>>();
            // });
            return new Route();
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