namespace Math
{
    public class LineLogDistancePenaltyFunction : IPenaltyFunctionIntegrable
    {
        public LineLogDistancePenaltyFunction(Vector offset, Vector direction) {
            Offset = offset;
            Direction = direction;
        }

        public Vector Offset { get; }
        public Vector Direction { get; }

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
