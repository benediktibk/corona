using System;
using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic
{
    public class XYGraph<X, Y>
    {
        private readonly Image _image;
        private const double _ratioXAxisLengthToImageSize = 0.80;
        private const double _ratioYAxisLengthToImageSize = 0.95;

        public XYGraph(int width, int height, IAxis<X> xAxis, IAxis<Y> yAxis, IReadOnlyList<DataSeries<X, Y>> allDataSeries) {
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

            var dataSeriesRange = new DataSeriesRange(allMinimumXValues.Min(), allMaximumXValues.Max(), allMinimumYValues.Min(), allMaximumYValues.Max());

            var originOffset = new Vector((1 - _ratioXAxisLengthToImageSize) * 0.75 * width, (1 - _ratioYAxisLengthToImageSize) / 2 * height);

            elements.AddRange(xAxis.CreateGraphicElementsForHorizontalAxis(dataSeriesRange.MinimumX, dataSeriesRange.MaximumX));
            elements.AddRange(yAxis.CreateGraphicElementsForVerticalAxis(dataSeriesRange.MinimumY, dataSeriesRange.MaximumY));

            var xAxisTransformation = xAxis.CreateAxisTransformation(dataSeriesRange.MinimumX, dataSeriesRange.MaximumX);
            var yAxisTransformation = yAxis.CreateAxisTransformation(dataSeriesRange.MinimumY, dataSeriesRange.MaximumY);

            foreach (var dataSeries in allDataSeries) {
                elements.AddRange(dataSeries.CreateGraphicElements(xAxis.NumericOperations, yAxis.NumericOperations, xAxisTransformation, yAxisTransformation));
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
