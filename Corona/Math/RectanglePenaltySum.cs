using System.Collections.Generic;
using System.Linq;

namespace Math
{
    public class RectanglePenaltySum : IPenaltyFunction
    {
        public readonly List<IPenaltyFunction> _penaltyFunctions;
        public readonly double _width;
        public readonly double _height;
        public readonly double _stepSize;

        public RectanglePenaltySum(IReadOnlyList<IPenaltyFunction> penaltyFunctions, double width, double height, double stepSize) {
            _penaltyFunctions = penaltyFunctions.ToList();
            _width = width;
            _height = height;
            _stepSize = stepSize;
        }

        public Vector CalculateGradient(Vector position) {
            var gradient = new Vector(0, 0);

            foreach (var penaltyFunction in _penaltyFunctions) {
                for (var x = 0.0; x < _width; x += _stepSize) {
                    for (var y = 0.0; y < _height; y += _stepSize) {
                        gradient = gradient + _stepSize * penaltyFunction.CalculateGradient(position + new Vector(x, y));
                    }
                }
            }

            return gradient;
        }

        public double CalculateValue(Vector position) {
            var result = 0.0;

            foreach (var penaltyFunction in _penaltyFunctions) {
                for (var x = 0.0; x < _width; x += _stepSize) {
                    for (var y = 0.0; y < _height; y += _stepSize) {
                        result = result + _stepSize * penaltyFunction.CalculateValue(position + new Vector(x, y));
                    }
                }
            }

            return result;
        }
    }
}
