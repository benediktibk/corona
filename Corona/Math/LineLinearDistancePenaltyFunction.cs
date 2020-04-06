namespace Math
{
    public class LineLinearDistancePenaltyFunction : IPenaltyFunctionIntegrable
    {
        private readonly Vector _offset;
        private readonly Vector _direction;
        private readonly double _gradient;
        private readonly double _maximumValue;
        private readonly bool _rightSideMaximumValue;
        private readonly bool _leftSideMaximumValue;

        public LineLinearDistancePenaltyFunction(Vector offset, Vector direction, double gradient, double maximumValue, bool leftSideMaximumValue, bool rightSideMaximumValue) {
            _offset = offset;
            _direction = 1 / direction.Norm * direction;
            _gradient = gradient;
            _maximumValue = maximumValue;
            _leftSideMaximumValue = leftSideMaximumValue;
            _rightSideMaximumValue = rightSideMaximumValue;
        }

        public Vector CalculateGradient(Vector position) {
            var distanceVector = CalculateDistance(position);
            var distance = distanceVector.Norm;

            if (distance == 0) {
                return new Vector(0, 0);
            }

            if (_rightSideMaximumValue || _leftSideMaximumValue) {
                var isLeft = Vector.IsLeftOfLine(_offset, _direction, position);

                if (isLeft && _leftSideMaximumValue) {
                    return distanceVector;
                }

                if (!isLeft && _rightSideMaximumValue) {
                    return distanceVector;
                }
            }

            var completeFactor = (-1) * _gradient / distance;
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
            return System.Math.Max(0, _maximumValue - _gradient * distance.Norm);
        }

        private Vector CalculateDistance(Vector position) {
            return (position - _offset) - ((position - _offset) * _direction) * _direction;
        }

        public double CalculateValueSumInRectangle(Vector position, double width, double height) {
            throw new System.NotImplementedException();
        }
    }
}
