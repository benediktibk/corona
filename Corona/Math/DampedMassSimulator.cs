using NLog;
using System.Collections.Generic;

namespace Math
{
    public static class DampedMassSimulator
    {
        public static void RunSimulation(double runTime, double positionEpsilon, double timeStep, IReadOnlyList<IPhysicalObject> physicalObjects) {
            var logger = LogManager.GetCurrentClassLogger();
            double t;

            for (t = 0.0; t < runTime; t += timeStep) {
                var overallPositionChange = 0.0;

                foreach (var physicalObject in physicalObjects) {
                    var oldPosition = physicalObject.PositionOfCenter;
                    physicalObject.ApplyForces(timeStep);
                    var newPosition = physicalObject.PositionOfCenter;
                    overallPositionChange += (oldPosition - newPosition).Norm;
                }

                if (overallPositionChange < positionEpsilon) {
                    logger.Debug($"finish simulation as position change {overallPositionChange} is smaller than epsilon {positionEpsilon}");
                    break;
                }
            }

            logger.Debug($"finished simulation after {t}s");
        }
    }
}
