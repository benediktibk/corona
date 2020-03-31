using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public interface IAxis<T>
    {
        List<IGraphicElement> CreateGraphicElements(T minimumValue, T maximumValue, T tickMarkDistance);
    }
}