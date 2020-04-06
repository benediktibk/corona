namespace Math
{
    public class LineExponentialDistancePenaltyFunction : IPenaltyFunctionIntegrable
    {
        private readonly Vector _offset;
        private readonly Vector _direction;
        private readonly double _exponentialBase;
        private readonly double _maximumValue;
        private readonly double _distanceOffset;
        private readonly bool _rightSideMaximumValue;
        private readonly bool _leftSideMaximumValue;

        public LineExponentialDistancePenaltyFunction(Vector offset, Vector direction, double exponentialBase, double maximumValue, bool leftSideMaximumValue, bool rightSideMaximumValue) {
            _offset = offset;
            _direction = 1/ direction.Norm * direction;
            _exponentialBase = exponentialBase;
            _maximumValue = maximumValue;
            _distanceOffset = System.Math.Log(_maximumValue) / System.Math.Log(_exponentialBase);
            _leftSideMaximumValue = leftSideMaximumValue;
            _rightSideMaximumValue = rightSideMaximumValue;
        }

        public Vector CalculateGradient(Vector position) {
            var distanceVector = CalculateDistance(position);

            if (_rightSideMaximumValue || _leftSideMaximumValue) {
                var isLeft = Vector.IsLeftOfLine(_offset, _direction, position);

                if (isLeft && _leftSideMaximumValue) {
                    return distanceVector;
                }

                if (!isLeft && _rightSideMaximumValue) {
                    return distanceVector;
                }
            }

            var distance = distanceVector.Norm;
            var logBase = System.Math.Log(_exponentialBase);
            var value = System.Math.Pow(_exponentialBase, _distanceOffset - distanceVector.Norm);
            var completeFactor = value * logBase / distance * (-1);
            return completeFactor * distanceVector;
        }

        public double CalculateValue(Vector position) {
            if (_rightSideMaximumValue || _leftSideMaximumValue) {
                var isLeft = Vector.IsLeftOfLine(_offset, _direction, position);

                if (isLeft && _leftSideMaximumValue) {
                    return _maximumValue;
                }

                if (!isLeft && _rightSideMaximumValue) {
                    return _maximumValue;
                }
            }

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
