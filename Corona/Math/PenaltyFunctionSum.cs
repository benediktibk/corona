using System.Collections.Generic;
using System.Linq;

namespace Math {
    public class PenaltyFunctionSum : IPenaltyFunction {
        private readonly List<IPenaltyFunction> _penaltyFunctions;

        public PenaltyFunctionSum(IReadOnlyList<IPenaltyFunction> penaltyFunctions) {
            _penaltyFunctions = penaltyFunctions.ToList();
        }

        public Vector CalculateGradient(Vector position) {
            var result = new Vector(0, 0);

            foreach (var penaltyFunction in _penaltyFunctions) {
                result = result + penaltyFunction.CalculateGradient(position);
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
