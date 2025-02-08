using c1tr00z.TrainsAppointment.Map;
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

        // public void 

        #endregion
    }
}