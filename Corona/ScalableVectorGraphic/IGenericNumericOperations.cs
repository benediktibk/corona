namespace ScalableVectorGraphic
{
    public interface IGenericNumericOperations<T>
    {
        string CreateLabel(double value);
        double ConvertToDoubleEquivalent(T value);
    }
}