using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public static class OverlapMinimizer
    {
        public static Point PlaceRectangleOverPoints(Rectangle rectangle, IReadOnlyList<Point> points) {
            const double weight = 1;
            const double springDistanceBorder = 0.3;
            const double springStiffness = 1;
            const double springLength = 1;
            const double runTime = 10;
            const double positionEpsilon = 1e-5;
            const double timeStep = 1e-2;

            var rectangleWithSprings = new Math.PhysicalRectangle(weight, rectangle.Width, rectangle.Height);

            for (var x = springDistanceBorder; x < 1; x += springDistanceBorder) {
                var connectionPoint = new Math.FixedPoint(new Math.Vector(x, 0));
                var spring = new Math.Spring(springLength, springStiffness, connectionPoint, rectangleWithSprings);
                rectangleWithSprings.AddSpringBottom(spring);
            }

            for (var x = springDistanceBorder; x < 1; x += springDistanceBorder) {
                var connectionPoint = new Math.FixedPoint(new Math.Vector(x, 1));
                var spring = new Math.Spring(springLength, springStiffness, connectionPoint, rectangleWithSprings);
                rectangleWithSprings.AddSpringTop(spring);
            }

            for (var y = springDistanceBorder; y < 1; y += springDistanceBorder) {
                var connectionPoint = new Math.FixedPoint(new Math.Vector(0, y));
                var spring = new Math.Spring(springLength, springStiffness, connectionPoint, rectangleWithSprings);
                rectangleWithSprings.AddSpringLeft(spring);
            }

            for (var y = springDistanceBorder; y < 1; y += springDistanceBorder) {
                var connectionPoint = new Math.FixedPoint(new Math.Vector(1, y));
                var spring = new Math.Spring(springLength, springStiffness, connectionPoint, rectangleWithSprings);
                rectangleWithSprings.AddSpringRight(spring);
            }

            foreach (var point in points) {
                var connectionPoint = new Math.FixedPoint(point.ToVector());
                var spring = new Math.Spring(springLength, springStiffness, connectionPoint, rectangleWithSprings);
                rectangleWithSprings.AddSpringCenter(spring);
            }

            for (var t = 0.0; t < runTime; t += timeStep) {
                var oldPosition = rectangleWithSprings.PositionOfCenter;
                rectangleWithSprings.ApplyForces(timeStep);
                var newPosition = rectangleWithSprings.PositionOfCenter;

                if ((oldPosition - newPosition).Norm < positionEpsilon) {
                    break;
                }
            }

            var result = rectangleWithSprings.PositionOfCenter - new Math.Vector(rectangle.Width / 2, rectangle.Height / 2);
            return new Point(result);
        }
    }
}
