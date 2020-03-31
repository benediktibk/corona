using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic
{
    public class XYGraph<X, Y>
    {
        private readonly Image _image;
        private const double _ratioXAxisLengthToImageSize = 0.90;
        private const double _ratioYAxisLengthToImageSize = 0.95;

        public XYGraph(int width, int height, IAxis<X> xAxis, IAxis<Y> yAxis, IReadOnlyList<DataPoint<X, Y>> dataPoints, X tickMarkDistanceXAxis, Y tickMarkDistanceYAxis) {
            var elements = new List<IGraphicElement>();
            var allXValues = dataPoints.Select(dataPoint => dataPoint.XValue).ToList();
            var allYValues = dataPoints.Select(dataPoint => dataPoint.YValue).ToList();
            var originOffset = new Vector((1 - _ratioXAxisLengthToImageSize) * 0.75 * width, (1 - _ratioYAxisLengthToImageSize) / 2 * height);

            var elementsXAxis = xAxis.CreateGraphicElementsForHorizontalAxis(xAxis.NumericOperations.FindSmallest(allXValues), xAxis.NumericOperations.FindBiggest(allXValues), tickMarkDistanceXAxis);
            elements.AddRange(elementsXAxis);

            var elementsYAxis = yAxis.CreateGraphicElementsForVerticalAxis(yAxis.NumericOperations.FindSmallest(allYValues), yAxis.NumericOperations.FindBiggest(allYValues), tickMarkDistanceYAxis);
            elements.AddRange(elementsYAxis);

            var transformGraphToImageSize = new Transformation(new Matrix(new Vector(_ratioXAxisLengthToImageSize * width, 0), new Vector(0, _ratioYAxisLengthToImageSize * height)), originOffset);
            elements = transformGraphToImageSize.Apply(elements);
            var transformImageToSvgCoordinates = new Transformation(new Matrix(new Vector(1, 0), new Vector(0, -1)), new Vector(0, height));
            elements = transformImageToSvgCoordinates.Apply(elements);
            _image = new Image(width, height, elements);
        }

        public string ToSvg() {
            return _image.CreateXml();
        }
    }
}
