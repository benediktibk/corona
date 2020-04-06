namespace Math
{
    public class PointLinearDistancePenaltyFunction : IPenaltyFunctionIntegrable
    {
        private readonly double _gradient;
        private readonly double _maximumValue;
        private readonly Vector _position;

        public PointLinearDistancePenaltyFunction(Vector position, double gradient, double maximumValue) {
            _position = position;
            _gradient = gradient;
            _maximumValue = maximumValue;
        }

        public Vector CalculateGradient(Vector position) {
            var distance = CalculateDistance(position);

            if (distance == 0 || _maximumValue < _gradient * distance) {
                return new Vector(0, 0);
            }

            var completeFactor =  (-1) * _gradient / distance;
            return completeFactor * (position - _position);
        }

        public double CalculateValue(Vector position) {
            var distance = CalculateDistance(position);
            return System.Math.Max(0, _maximumValue - _gradient * distance);
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
