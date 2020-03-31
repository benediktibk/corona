using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public interface IGenericNumericOperations<T>
    {
        T Add(T a, T b);
        bool SmallerThan(T a, T b);
        string CreateLabel(T value);
        double ScaleBetween0And1(T minimumValue, T maximumValue, T value);
        T FindSmallest(IReadOnlyList<T> values);
        T FindBiggest(IReadOnlyList<T> values);
    }
}