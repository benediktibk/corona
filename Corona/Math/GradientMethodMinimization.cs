namespace Math
{
    public static class GradientMethodMinimization
    {
        public static Vector Minimize(Vector start, IPenaltyFunction penaltyFunction, int maximumIterations, double penaltyChangeTermination) {
            const double beta = 0.5;
            var current = start;
            var penalty = penaltyFunction.CalculateValue(current);

            for (var i = 0; i < maximumIterations; ++i) {
                var gradient = (-1) * penaltyFunction.CalculateGradient(current);

                var stepSize = 1.0;
                var nextPenalty = double.MaxValue;
                var improvementFound = false;

                for (var j = 0; j < maximumIterations; ++j) {
                    nextPenalty = penaltyFunction.CalculateValue(current + stepSize * gradient);

                    if (nextPenalty < penalty) {
                        improvementFound = true;
                        break;
                    }

                    stepSize *= beta;
                }

                if (!improvementFound) {
                    return current;
                }

                current = current + stepSize * gradient;

                if (System.Math.Abs(nextPenalty - penalty) < penaltyChangeTermination) {
                    break;
                }

                penalty = nextPenalty;
            }

            return current;            
        }
    }
}
