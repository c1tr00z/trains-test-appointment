using System.Collections.Generic;
namespace c1tr00z.TrainsAppointment.Utils {
    public static class EnumerableUtils {
        #region Class Implementation

        public static List<T> ToUniqueList<T>(this IEnumerable<T> enumerable) {
            var newList = new List<T>();
            foreach (var item in enumerable) {
                if (newList.Contains(item)) {
                    continue;
                }
                newList.Add(item);
            }
            return newList;
        }

        #endregion
    }
}