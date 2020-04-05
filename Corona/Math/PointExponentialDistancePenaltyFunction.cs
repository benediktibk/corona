namespace Math
{
    public class PointExponentialDistancePenaltyFunction : IPenaltyFunctionIntegrable
    {
        private readonly double _offset;
        private readonly double _exponentialBase;
        private readonly double _maximumValue;
        private readonly Vector _position;

        public PointExponentialDistancePenaltyFunction(Vector position, double exponentialBase, double maximumValue) {
            _position = position;
            _exponentialBase = exponentialBase;
            _maximumValue = maximumValue;
            _offset = (-1) * System.Math.Log(_maximumValue) / System.Math.Log(_exponentialBase);
        }

        public Vector CalculateGradient(Vector position) {
            var distance = CalculateDistanceWithOffset(position);
            var logBase = System.Math.Log(_exponentialBase);
            var value = System.Math.Pow(_exponentialBase, distance);
            var completeFactor = value * logBase / distance;
            return completeFactor * (position - _position);
        }

        public double CalculateValue(Vector position) {
            var distance = CalculateDistanceWithOffset(position);
            return System.Math.Pow(_exponentialBase, distance);
        }

        private double CalculateDistanceWithOffset(Vector position) {
            return System.Math.Sqrt((position - _position).Norm) - _offset;
        }

        public double CalculateValueSumInRectangle(Vector position, double width, double height) {
            throw new System.NotImplementedException();
        }
    }
}
