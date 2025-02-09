using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.TrainsAppointment.Map.Nodes;
namespace c1tr00z.TrainsAppointment.Pathfinding {
    public class Route : Queue<RoutePart> {
        #region Class Implementation

        public float CalculatePrice(float speed, float timeToMine) {
            var allPaths = this.ToList();
            var timeToFinish = 0f;
            
            for (int i = 0; i < allPaths.Count; i++) {
                var path = allPaths[i];
                timeToFinish += path.lenght / speed;
                if (path.targetNode is Mine mine) {
                    timeToFinish += mine.MiningMultiplier * timeToMine;
                }
            }

            if (allPaths.LastOrDefault().targetNode is Base baseNode) {
                return baseNode.CurrencyMultiplier / timeToFinish;
            }
            
            throw new Exception("Wrong path. Last node is not Base");
        }

        public void AddToRoute(Route other) {
            var otherList = other.ToList();
            otherList.ForEach(Enqueue);
        }

        #endregion
    }
}