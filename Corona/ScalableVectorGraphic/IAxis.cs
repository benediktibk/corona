using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public interface IAxis<T>
    {
        IGenericNumericOperations<T> NumericOperations { get; }

        List<IGraphicElement> CreateGraphicElementsForHorizontalAxis(T minimumValue, T maximumValue, T tickMarkDistance);
        List<IGraphicElement> CreateGraphicElementsForVerticalAxis(T minimumValue, T maximumValue, T tickMarkDistance);
    }
}