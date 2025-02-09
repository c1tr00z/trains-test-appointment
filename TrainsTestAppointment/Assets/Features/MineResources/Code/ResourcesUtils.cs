using System;
namespace c1tr00z.TrainsAppointment.MineResources {
    public static class ResourcesUtils {
        
        #region Events

        public static event Action<float> ResourceCollected;

        #endregion

        #region Class Implementation

        public static void OnResourceCollected(float resource) => ResourceCollected?.Invoke(resource);

        #endregion
    }
}