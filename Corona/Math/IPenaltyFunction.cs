namespace Math {
    public interface IPenaltyFunction {
        Vector CalculateGradient(Vector position);
        double CalculateValue(Vector position);
    }
}