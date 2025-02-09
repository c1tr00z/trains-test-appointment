using System.Collections.Generic;
using System.Linq;
using c1tr00z.TrainsAppointment.Map;
using c1tr00z.TrainsAppointment.Map.Nodes;
using c1tr00z.TrainsAppointment.Utils;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Pathfinding {
    public static class PathfindingUtils {

        #region Private Fields

        private static PathfindingController _pathfinding;

        #endregion

        #region Accessors

        private static PathfindingController PathfindingController {
            get {
                if (_pathfinding is null) {
                    _pathfinding = Object.FindObjectOfType<PathfindingController>();
                }
                return _pathfinding;
            }
        }

        #endregion
        
        #region Class Implementation

        public static List<Route> FindAllPossibleRoutes(Node startNode, Node targetNode) {
            return PathfindingController.FindAllPossibleRoutes(startNode, targetNode);
        }

        public static Route ToRoute(this List<Path> paths, Node startNode) {
            if (paths.Count == 0) {
                return new Route();
            }

            Node nextNode = null;
            Route route = new Route();

            foreach (var path in paths) {
                if (nextNode is null) {
                    nextNode = path.Nodes.FirstOrDefault(n => n != startNode);
                    route.Enqueue(new RoutePart {
                        startNode = startNode,
                        targetNode = nextNode,
                        lenght = path.Length,
                    });
                    continue;
                }
                var firstNode = nextNode;
                nextNode = path.Nodes.FirstOrDefault(n => n != firstNode);
                route.Enqueue(new RoutePart {
                    startNode = firstNode,
                    targetNode = nextNode,
                    lenght = path.Length,
                });
            }
            
            return route;
        }

        #endregion
    }
}