using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic
{
    public class XYGraph<X, Y>
    {
        private readonly Image _image;
        private const double _ratioXAxisLengthToImageSize = 0.90;
        private const double _ratioYAxisLengthToImageSize = 0.95;

        public XYGraph(int width, int height, IAxis<X> xAxis, IAxis<Y> yAxis, IReadOnlyList<DataSeries<X, Y>> allDataSeries, X tickMarkDistanceXAxis, Y tickMarkDistanceYAxis) {
            var elements = new List<IGraphicElement>();
            var allMinimumXValues = new List<X>();
            var allMaximumXValues = new List<X>();
            var allMinimumYValues = new List<Y>();
            var allMaximumYValues = new List<Y>();

            foreach (var dataSeries in allDataSeries) {
                dataSeries.FindRangeOfXValues(xAxis.NumericOperations, out var minimumX, out var maximumX);
                dataSeries.FindRangeOfYValues(yAxis.NumericOperations, out var minimumY, out var maximumY);
                allMinimumXValues.Add(minimumX);
                allMaximumXValues.Add(maximumX);
                allMinimumYValues.Add(minimumY);
                allMaximumYValues.Add(maximumY);
            }

            var overallMinimumX = xAxis.NumericOperations.FindSmallest(allMinimumXValues);
            var overallMaximumX = xAxis.NumericOperations.FindBiggest(allMinimumXValues);
            var overallMinimumY = yAxis.NumericOperations.FindSmallest(allMinimumYValues);
            var overallMaximumY = yAxis.NumericOperations.FindBiggest(allMinimumYValues);

            var originOffset = new Vector((1 - _ratioXAxisLengthToImageSize) * 0.75 * width, (1 - _ratioYAxisLengthToImageSize) / 2 * height);

            elements.AddRange(xAxis.CreateGraphicElementsForHorizontalAxis(overallMinimumX, overallMaximumX, tickMarkDistanceXAxis));
            elements.AddRange(yAxis.CreateGraphicElementsForVerticalAxis(overallMinimumY, overallMaximumY, tickMarkDistanceYAxis));

            var dataSeriesElements = new List<IGraphicElement>();
            foreach (var dataSeries in allDataSeries) {
                dataSeriesElements.AddRange(dataSeries.CreateGraphicElements(xAxis.NumericOperations, yAxis.NumericOperations));
            }

            var transformGraphToImageSize = new Transformation(new Matrix(_ratioXAxisLengthToImageSize * width, _ratioYAxisLengthToImageSize * height), originOffset);
            elements = transformGraphToImageSize.Apply(elements);
            var transformImageToSvgCoordinates = new Transformation(new Matrix(1, -1), new Vector(0, height));
            elements = transformImageToSvgCoordinates.Apply(elements);
            _image = new Image(width, height, elements);
        }

        public string ToSvg() {
            return _image.CreateXml();
        }
    }
}
