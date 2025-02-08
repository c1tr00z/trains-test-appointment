using UnityEngine;
namespace c1tr00z.TrainsAppointment.Utils {
    public static class VectorUtils {
        #region Class Implementation

        public static Vector3 ToVector3Vertical(this Vector2 vector2) {
            return new Vector3(vector2.x, vector2.y, 0);
        }
        
        public static Vector3 ToVector3Horizontal(this Vector2 vector2) {
            return new Vector3(vector2.x, 0, vector2.y);
        }

        public static Vector2 ToVector2FromHorizontal(this Vector3 vector3) {
            return new Vector2(vector3.x, vector3.z);
        }

        #endregion
    }
}