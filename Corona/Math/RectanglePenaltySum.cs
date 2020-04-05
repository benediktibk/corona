using System.Collections.Generic;
using System.Linq;

namespace Math
{
    public class RectanglePenaltySum : IPenaltyFunction
    {
        public readonly List<IPenaltyFunctionIntegrable> _penaltyFunctions;
        public readonly Vector _position;
        public readonly double _width;
        public readonly double _height;

        public RectanglePenaltySum(IReadOnlyList<IPenaltyFunctionIntegrable> penaltyFunctions, Vector position, double width, double height) {
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
            var result = 0.0;

            foreach (var penaltyFunction in _penaltyFunctions) {
                result += penaltyFunction.CalculateValueSumInRectangle(_position + position, _width, _height);
            }

            return result;
        }
    }
}
