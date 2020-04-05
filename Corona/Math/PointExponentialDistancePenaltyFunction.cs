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
            _offset = System.Math.Log(_maximumValue) / System.Math.Log(_exponentialBase);
        }

        public Vector CalculateGradient(Vector position) {
            var distance = CalculateDistance(position);
            var logBase = System.Math.Log(_exponentialBase);
            var value = System.Math.Pow(_exponentialBase, _offset - distance);
            var completeFactor = value * logBase / distance;
            return completeFactor * (position - _position);
        }

        public double CalculateValue(Vector position) {
            var distance = CalculateDistance(position);
            return System.Math.Pow(_exponentialBase, _offset - distance);
        }

        private double CalculateDistance(Vector position) {
            var distanceVector = position - _position;
            return distanceVector.Norm;
        }

        public double CalculateValueSumInRectangle(Vector position, double width, double height) {
            throw new System.NotImplementedException();
        }
    }
}
