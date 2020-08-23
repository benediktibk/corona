using System.Collections.Generic;

namespace ScalableVectorGraphic {
    public interface IAxis<T> {
        IGenericNumericOperations<T> NumericOperations { get; }
        List<IGraphicElement> CreateGraphicElementsForHorizontalAxis(double minimumValue, double maximumValue);
        List<IGraphicElement> CreateGraphicElementsForVerticalAxis(double minimumValue, double maximumValue);
        IAxisTransformation CreateAxisTransformation(double minimumValue, double maximumValue);
        string CreateLabel(double value);
    }
}