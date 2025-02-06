using UnityEngine;
namespace c1tr00z.TrainsAppointment.Utils {
    public static class CircleUtils {
        #region Class Implementation

        /// <summary>
        /// calculating point on circle with radius a with delta angle -90 degrees
        /// </summary>
        /// <returns></returns>
        public static Vector2 CalculatePointOnCircle(Vector2 center, float angle, float radius) {
            float finalAngle = -90 + angle;
            return new Vector2(
                center.x + radius * Mathf.Cos(Mathf.Deg2Rad * finalAngle),
                center.y + radius * Mathf.Sin(Mathf.Deg2Rad * finalAngle));
        }

        #endregion
    }
}