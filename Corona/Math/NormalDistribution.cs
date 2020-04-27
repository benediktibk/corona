﻿namespace Math
{
    public class NormalDistribution
    {
        private readonly double _expectation;
        private readonly double _standardDeviation;

        public NormalDistribution(double expectation, double standardDeviation) {
            _expectation = expectation;
            _standardDeviation = standardDeviation;
        }

        public double CalculateSumTo(double x) {
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            int sign = 1;

            if (x < 0) {
                sign = -1;
            }

            x = System.Math.Abs(x) / System.Math.Sqrt(2.0);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * System.Math.Exp(-x * x);

            return 0.5 * (1.0 + sign * y);
        }

        public double CalculateSumBetween(double start, double end) {
            return CalculateSumTo(end) - CalculateSumTo(start);
        }
    }
}
