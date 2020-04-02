namespace ScalableVectorGraphic
{
    public interface IGenericNumericOperations<T>
    {
        double ConvertToDoubleEquivalent(T value);
        T ConvertFromDoubleEquivalent(double value);
    }
}