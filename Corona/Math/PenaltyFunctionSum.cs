using System.Collections.Generic;
using System.Linq;

namespace Math
{
    public class PenaltyFunctionSum : IPenaltyFunction
    {
        private readonly List<IPenaltyFunction> _penaltyFunctions;

        public PenaltyFunctionSum(IReadOnlyList<IPenaltyFunction> penaltyFunctions) {
            _penaltyFunctions = penaltyFunctions.ToList();
        }

        public Vector CalculateGradient(Vector position) {
            var result = new Vector(0, 0);
            var gradients = new List<Vector>();
            var values = new List<double>();

            foreach (var penaltyFunction in _penaltyFunctions) {
                gradients.Add(penaltyFunction.CalculateGradient(position));
                values.Add(penaltyFunction.CalculateValue(position));
            }

            var maxPenalty = values.Max();

            for (var i = 0; i < _penaltyFunctions.Count; ++i) {
                result = result + values[i] / maxPenalty / gradients[i].Norm * gradients[i];
            }

            return result;
        }

        public double CalculateValue(Vector position) {
            var result = 0.0;

            foreach (var penaltyFunction in _penaltyFunctions) {
                result = result + penaltyFunction.CalculateValue(position);
            }

            return result;
        }
    }
}
