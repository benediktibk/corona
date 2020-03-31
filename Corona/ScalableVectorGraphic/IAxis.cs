using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public interface IAxis<T>
    {
        IGenericNumericOperations<T> NumericOperations { get; }

        List<IGraphicElement> CreateGraphicElementsForHorizontalAxis(double minimumValue, double maximumValue, T tickMarkDistance);
        List<IGraphicElement> CreateGraphicElementsForVerticalAxis(double minimumValue, double maximumValue, T tickMarkDistance);
    }
}