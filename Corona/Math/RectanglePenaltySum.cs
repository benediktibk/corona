using System.Collections.Generic;
using System.Linq;

namespace Math
{
    public class RectanglePenaltySum : IPenaltyFunction
    {
        private readonly List<IPenaltyFunction> _penaltyFunctions;
        private readonly double _width;
        private readonly double _height;
        private readonly double _stepSize;
        private readonly double _scale;

        public RectanglePenaltySum(IReadOnlyList<IPenaltyFunction> penaltyFunctions, double width, double height, double stepSize, double scale) {
            _penaltyFunctions = penaltyFunctions.ToList();
            _width = width;
            _height = height;
            _stepSize = stepSize;
            _scale = scale;
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

            return _scale * gradient;
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

            return _scale * result;
        }
    }
}
