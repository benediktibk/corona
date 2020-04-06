using System.Collections.Generic;
using System.Linq;

namespace Math
{
    public class RectanglePenaltySum : IPenaltyFunction
    {
        public readonly List<IPenaltyFunction> _penaltyFunctions;
        public readonly Vector _position;
        public readonly double _width;
        public readonly double _height;

        public RectanglePenaltySum(IReadOnlyList<IPenaltyFunction> penaltyFunctions, Vector position, double width, double height) {
            _penaltyFunctions = penaltyFunctions.ToList();
            _position = position;
            _width = width;
            _height = height;
        }

        public Vector CalculateGradient(Vector position) {
            var gradient = new Vector(0, 0);

            foreach (var penaltyFunction in _penaltyFunctions) {
                gradient = gradient + penaltyFunction.CalculateGradient(position);
            }

            return gradient;
        }

        public double CalculateValue(Vector position) {
            throw new System.NotImplementedException();
        }
    }
}
