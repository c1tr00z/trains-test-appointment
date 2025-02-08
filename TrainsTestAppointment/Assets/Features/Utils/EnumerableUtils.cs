using System;
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

        public static T MinElement<T>(this IEnumerable<T> enumerable, Func<T, float> selector) {
            float minValue = 0;
            T minItem = default;
            var hasMinItem = false;
            foreach (var item in enumerable) {
                var valueToCompare = selector(item);
                if (!hasMinItem || minValue > valueToCompare) {
                    minValue = valueToCompare;
                    hasMinItem = true;
                    minItem = item;
                }
            }
            return minItem;
        }

        #endregion
    }
}