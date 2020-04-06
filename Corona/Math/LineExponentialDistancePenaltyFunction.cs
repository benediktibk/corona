namespace Math
{
    public class LineExponentialDistancePenaltyFunction : IPenaltyFunctionIntegrable
    {
        private readonly Vector _offset;
        private readonly Vector _direction;
        private readonly double _exponentialBase;
        private readonly double _maximumValue;
        private readonly double _distanceOffset;

        public LineExponentialDistancePenaltyFunction(Vector offset, Vector direction, double exponentialBase, double maximumValue) {
            _offset = offset;
            _direction = 1/ direction.Norm * direction;
            _exponentialBase = exponentialBase;
            _maximumValue = maximumValue;
            _distanceOffset = System.Math.Log(_maximumValue) / System.Math.Log(_exponentialBase);
        }

        public Vector CalculateGradient(Vector position) {
            var distanceVector = CalculateDistance(position);
            var distance = distanceVector.Norm;
            var logBase = System.Math.Log(_exponentialBase);
            var value = System.Math.Pow(_exponentialBase, _distanceOffset - distanceVector.Norm);
            var completeFactor = value * logBase / distance * (-1);
            return completeFactor * distanceVector;
        }

        public double CalculateValue(Vector position) {
            var distance = CalculateDistance(position);
            return System.Math.Pow(_exponentialBase, _distanceOffset - distance.Norm);
        }

        private Vector CalculateDistance(Vector position) {
            return (position - _offset) - ((position - _offset) * _direction) * _direction;
        }

        public double CalculateValueSumInRectangle(Vector position, double width, double height) {
            throw new System.NotImplementedException();
        }
    }
}
