using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public static class OverlapMinimizer
    {
        public static Point PlaceRectangleOverPoints(Rectangle rectangle, IReadOnlyList<Point> points, Point mostLefLowerPoint, Point mostRightUpperPoint) {
            const double gradient = 2;
            const double maximumValueBorders = 1;
            double maximumValuePoint = maximumValueBorders / points.Count;

            var penaltyFunctions = new List<Math.IPenaltyFunction>();
            var bottom = new Math.LineLinearDistancePenaltyFunction(new Math.Vector(0, 0), new Math.Vector(1, 0), gradient, maximumValueBorders, false, true);
            var top = new Math.LineLinearDistancePenaltyFunction(new Math.Vector(0, 1), new Math.Vector(1, 0), gradient, maximumValueBorders, true, false);
            var left = new Math.LineLinearDistancePenaltyFunction(new Math.Vector(0, 0), new Math.Vector(0, 1), gradient, maximumValueBorders, true, false);
            var right = new Math.LineLinearDistancePenaltyFunction(new Math.Vector(1, 0), new Math.Vector(0, 1), gradient, maximumValueBorders, false, true);

            penaltyFunctions.Add(bottom);
            penaltyFunctions.Add(top);
            penaltyFunctions.Add(left);
            penaltyFunctions.Add(right);

            foreach (var point in points) {
                penaltyFunctions.Add(new Math.PointLinearDistancePenaltyFunction(point.ToVector(), gradient, maximumValuePoint));
            }

            var start = new Math.Vector(0.5, 0.5);
            var penaltyFunction = new Math.RectanglePenaltySum(penaltyFunctions, rectangle.Width, rectangle.Height, 0.001, 1e-2);
            var result = Math.GradientMethodMinimization.Minimize(start, penaltyFunction, 10, 1e-5);
            if (result.X > 0 && result.X < 1 && result.Y > 0 && result.Y < 1) {
                return new Point(result);
            }
            else {
                return new Point(0.4, 0.8);
            }
        }
    }
}
