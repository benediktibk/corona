namespace Math
{
    public class PointLogDistancePenaltyFunction : IPenaltyFunctionIntegrable
    {
        public PointLogDistancePenaltyFunction(Vector position) {
            Position = position;
        }

        public Vector Position { get; }

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
