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
            var allMinimumXValues = new List<double>();
            var allMaximumXValues = new List<double>();
            var allMinimumYValues = new List<double>();
            var allMaximumYValues = new List<double>();

            foreach (var dataSeries in allDataSeries) {
                dataSeries.FindRangeOfXValuesAsDouble(xAxis.NumericOperations, out var minimumX, out var maximumX);
                dataSeries.FindRangeOfYValuesAsDouble(yAxis.NumericOperations, out var minimumY, out var maximumY);
                allMinimumXValues.Add(minimumX);
                allMaximumXValues.Add(maximumX);
                allMinimumYValues.Add(minimumY);
                allMaximumYValues.Add(maximumY);
            }

            var overallMinimumX = allMinimumXValues.Min();
            var overallMaximumX = allMaximumXValues.Max();
            var overallMinimumY = allMinimumYValues.Min();
            var overallMaximumY = allMaximumYValues.Max();

            var originOffset = new Vector((1 - _ratioXAxisLengthToImageSize) * 0.75 * width, (1 - _ratioYAxisLengthToImageSize) / 2 * height);

            elements.AddRange(xAxis.CreateGraphicElementsForHorizontalAxis(overallMinimumX, overallMaximumX, tickMarkDistanceXAxis));
            elements.AddRange(yAxis.CreateGraphicElementsForVerticalAxis(overallMinimumY, overallMaximumY, tickMarkDistanceYAxis));

            var dataSeriesElements = new List<IGraphicElement>();
            foreach (var dataSeries in allDataSeries) {
                dataSeriesElements.AddRange(dataSeries.CreateGraphicElements(xAxis.NumericOperations, yAxis.NumericOperations));
            }

            var xScale = 1.0 / (overallMaximumX - overallMinimumX);
            var xOffset = (-1.0) * overallMinimumX * xScale;
            var yScale = 1.0 / (overallMaximumY - overallMinimumY);
            var yOffset = (-1.0) * overallMinimumY * yScale;
            var transformationDataSeriesToGraph = new Transformation(new Matrix(xScale, yScale), new Vector(xOffset, yOffset));
            dataSeriesElements = transformationDataSeriesToGraph.Apply(dataSeriesElements);
            elements.AddRange(dataSeriesElements);

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
