using System.Collections.Generic;
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

        #endregion
    }
}