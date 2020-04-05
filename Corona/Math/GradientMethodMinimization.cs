namespace Math
{
    public static class GradientMethodMinimization
    {
        public static Vector Minimize(Vector start, IPenaltyFunction penaltyFunction, int maximumIterations, double penaltyChangeTermination) {
            var current = start;
            var penalty = penaltyFunction.CalculateValue(current);

            for (var i = 0; i < maximumIterations; ++i) {
                var gradient = penaltyFunction.CalculateGradient(current);
                var stepSize = CalculateStepSize(penaltyFunction, current, gradient, maximumIterations, penalty, out var nextPenalty);
                current = current + stepSize * gradient;

                if (System.Math.Abs(nextPenalty - penalty) < penaltyChangeTermination) {
                    break;
                }

                penalty = nextPenalty;
            }

            return current;            
        }

        private static double CalculateStepSize(IPenaltyFunction penaltyFunction, Vector currentPosition, Vector gradient, double maximumIterations, double currentPenalty, out double nextPenalty) {
            const double beta = 0.5;
            var stepSize = 1.0;
            nextPenalty = double.MaxValue;

            for (var i = 0; i < maximumIterations; ++i) {
                nextPenalty = penaltyFunction.CalculateValue(currentPosition + stepSize * gradient);

                if (nextPenalty < currentPenalty) {
                    return stepSize;
                }

                stepSize *= beta;
            }

            return 0;
        }
    }
}
