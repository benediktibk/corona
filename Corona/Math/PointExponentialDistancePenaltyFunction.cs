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
            throw new System.NotImplementedException();
        }

        public double CalculateValue(Vector position) {
            var distance = (position - _position).Norm;
            return System.Math.Pow(_exponentialBase, distance);
        }

        public double CalculateValueSumInRectangle(Vector position, double width, double height) {
            throw new System.NotImplementedException();
        }
    }
}
