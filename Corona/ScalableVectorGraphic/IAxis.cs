using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public interface IAxis<T>
    {
        IGenericNumericOperations<T> NumericOperations { get; }

        List<IGraphicElement> CreateGraphicElements(T minimumValue, T maximumValue, T tickMarkDistance);
    }
}