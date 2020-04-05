namespace Math
{
    public interface IPenaltyFunctionIntegrable : IPenaltyFunction
    {
        double CalculateValueSumInRectangle(Vector position, double width, double height);
    }
}
