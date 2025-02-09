using System.Collections.Generic;
using System.Linq;
using c1tr00z.TrainsAppointment.Map.Nodes;
using UnityEngine;
namespace c1tr00z.TrainsAppointment.Map {
    public static class MapUtils {

        #region Private Fields

        private static Map _map;

        #endregion

        #region Accessors

        private static Map MapObject => _map ??= Object.FindObjectOfType<Map>();

        #endregion
        
        #region Class Implementation

        public static List<Path> GetPaths(this Node node) {
            return MapObject.GetNodePaths(node);
        }

        public static List<Mine> GetAllMines() {
            return MapObject.GetAllNodes().OfType<Mine>().ToList();
        }
        
        public static List<Base> GetAllBases() {
            return MapObject.GetAllNodes().OfType<Base>().ToList();
        }

        public static Node GetRandomNode() {
            var allNodes = MapObject.GetAllNodes();
            return allNodes[Random.Range(0, allNodes.Count)];
        }

        #endregion
    }
}