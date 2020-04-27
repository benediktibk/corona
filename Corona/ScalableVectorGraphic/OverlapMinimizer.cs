using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class OverlapMinimizer    {

        private readonly double _weight = 1;
        private readonly double _springDistanceBorder;
        private readonly double _springStiffness = 1;
        private readonly double _springLengthBorder = 1;
        private readonly double _springLengthPoints = 1;
        private readonly double _runTime = 10;
        private readonly double _positionEpsilon = 1e-10;
        private readonly double _timeStep = 1e-2;

        public OverlapMinimizer(double springsPerBorder) {
            _springDistanceBorder = 1.0 / (springsPerBorder + 1);
        }

        public OverlapMinimizer() :
            this(10) {
        }

        public Point PlaceRectangleOverPoints(Rectangle rectangle, IReadOnlyList<Point> points) {
            CreatePhysicalObjects(rectangle, points, out var rectangleWithSprings, out var physicalObjects);

            Math.DampedMassSimulator.RunSimulation(_runTime, _positionEpsilon, _timeStep, physicalObjects);

            var result = rectangleWithSprings.PositionOfCenter - new Math.Vector(rectangle.Width / 2, rectangle.Height / 2);
            return new Point(result);
        }

        private void CreatePhysicalObjects(Rectangle rectangle, IReadOnlyList<Point> points, out Math.PhysicalRectangle rectangleWithSprings, out List<Math.IPhysicalObject> physicalObjects) {
            rectangleWithSprings = new Math.PhysicalRectangle(_weight, rectangle.Width, rectangle.Height, new Math.Vector(0.5, 0.5), 0);
            for (var x = _springDistanceBorder; x < 1; x += _springDistanceBorder) {
                var connectionPoint = new Math.FixedPoint(new Math.Vector(x, 0));
                var spring = new Math.Spring(_springLengthBorder, _springStiffness, connectionPoint, rectangleWithSprings);
                rectangleWithSprings.AddSpringBottom(spring);
            }

            for (var x = _springDistanceBorder; x < 1; x += _springDistanceBorder) {
                var connectionPoint = new Math.FixedPoint(new Math.Vector(x, 1));
                var spring = new Math.Spring(_springLengthBorder, _springStiffness, connectionPoint, rectangleWithSprings);
                rectangleWithSprings.AddSpringTop(spring);
            }

            for (var y = _springDistanceBorder; y < 1; y += _springDistanceBorder) {
                var connectionPoint = new Math.FixedPoint(new Math.Vector(0, y));
                var spring = new Math.Spring(_springLengthBorder, _springStiffness, connectionPoint, rectangleWithSprings);
                rectangleWithSprings.AddSpringLeft(spring);
            }

            for (var y = _springDistanceBorder; y < 1; y += _springDistanceBorder) {
                var connectionPoint = new Math.FixedPoint(new Math.Vector(1, y));
                var spring = new Math.Spring(_springLengthBorder, _springStiffness, connectionPoint, rectangleWithSprings);
                rectangleWithSprings.AddSpringRight(spring);
            }

            foreach (var point in points) {
                var connectionPoint = new Math.FixedPoint(point.ToVector());
                var spring = new Math.Spring(_springLengthPoints, _springStiffness, connectionPoint, rectangleWithSprings);
                rectangleWithSprings.AddSpringCenter(spring);
            }

            physicalObjects = new List<Math.IPhysicalObject> {
                rectangleWithSprings
            };
        }
    }
}
