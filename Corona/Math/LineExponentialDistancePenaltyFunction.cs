namespace Math
{
    public class LineExponentialDistancePenaltyFunction : IPenaltyFunctionIntegrable
    {
        private readonly Vector _offset;
        private readonly Vector _direction;
        private readonly double _exponentialBase;
        private readonly double _maximumValue;

        public LineExponentialDistancePenaltyFunction(Vector offset, Vector direction, double exponentialBase, double maximumValue) {
            _offset = offset;
            _direction = 1/ direction.Norm * direction;
            _exponentialBase = exponentialBase;
            _maximumValue = maximumValue;
        }

        public Vector CalculateGradient(Vector position) {
            throw new System.NotImplementedException();
        }

        public double CalculateValue(Vector position) {
            throw new System.NotImplementedException();
        }

        public double CalculateValueSumInRectangle(Vector position, double width, double height) {
            throw new System.NotImplementedException();
        }
    }
}
