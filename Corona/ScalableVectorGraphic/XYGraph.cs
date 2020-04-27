using Math;
using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic
{
    public class XYGraph<X, Y>
    {
        private readonly Image _image;
        private const double _ratioXAxisLengthToImageSize = 0.85;
        private const double _ratioYAxisLengthToImageSize = 0.85;
        private const double _yAxisOffsetForLabels = 0.05;
        private const double _legendLineWidth = 0.002;
        private const double _legendDotRadius = 0.005;
        private const double _legendFontSize = 0.02;
        private const double _legendHeightPerCountry = 0.03;
        private const double _legendLineLength = 0.03;
        private const double _legendLetterWidth = _legendFontSize * 0.5;
        private const double _legendMarginRight = 0.01;
        private const double _legendDotOffsetLeft = 0.02;
        private const double _legendBorderWidth = 0.002;
        private const string _legendFont = "monospace";
        private static readonly Color _legendBackgroundColor = new Color(245, 245, 245);
        private readonly OverlapMinimizer _overlapMinizmer = new OverlapMinimizer();

        public XYGraph(int width, int height, IAxis<X> xAxis, IAxis<Y> yAxis, IReadOnlyList<DataSeries<X, Y>> allDataSeries, bool legend, bool background) :
            this(width, height, xAxis, yAxis, allDataSeries, new List<ReferenceLine<Y>>(), legend, background) {
        }

        public XYGraph(int width, int height, IAxis<X> xAxis, IAxis<Y> yAxis, IReadOnlyList<DataSeries<X, Y>> allDataSeries, IReadOnlyList<ReferenceLine<Y>> yReferenceLines, bool legend, bool background) {
            var dataSeriesRange = FindDataSeriesRange(xAxis, yAxis, allDataSeries, yReferenceLines);
            var elements = CreateGraphicElements(xAxis, yAxis, allDataSeries, yReferenceLines, legend, dataSeriesRange);
            elements = TransformElements(width, height, elements);

            if (background) {
                elements.Insert(0, new Rectangle("background", new Point(0, 0), new Point(width, height), Color.White, Color.Black, _legendBorderWidth * System.Math.Sqrt(width * height)));
            }

            _image = new Image(width, height, elements);
        }

        private static List<IGraphicElement> TransformElements(int width, int height, List<IGraphicElement> elements) {
            var originOffset = new Vector((1 - _ratioXAxisLengthToImageSize) * 0.7 * width, ((1 - _ratioYAxisLengthToImageSize) / 2 + _yAxisOffsetForLabels) * height * 0.7);
            var transformGraphToImageSize = new Transformation(new Matrix(_ratioXAxisLengthToImageSize * width, _ratioYAxisLengthToImageSize * height), originOffset);
            elements = transformGraphToImageSize.Apply(elements);
            var transformImageToSvgCoordinates = new Transformation(new Matrix(1, -1), new Vector(0, height));
            elements = transformImageToSvgCoordinates.Apply(elements);
            return elements;
        }

        private List<IGraphicElement> CreateGraphicElements(IAxis<X> xAxis, IAxis<Y> yAxis, IReadOnlyList<DataSeries<X, Y>> allDataSeries, IReadOnlyList<ReferenceLine<Y>> yReferenceLines, bool legend, DataSeriesRange dataSeriesRange) {
            var elements = new List<IGraphicElement>();
            elements.AddRange(xAxis.CreateGraphicElementsForHorizontalAxis(dataSeriesRange.MinimumX, dataSeriesRange.MaximumX));
            elements.AddRange(yAxis.CreateGraphicElementsForVerticalAxis(dataSeriesRange.MinimumY, dataSeriesRange.MaximumY));

            var xAxisTransformation = xAxis.CreateAxisTransformation(dataSeriesRange.MinimumX, dataSeriesRange.MaximumX);
            var yAxisTransformation = yAxis.CreateAxisTransformation(dataSeriesRange.MinimumY, dataSeriesRange.MaximumY);

            foreach (var referenceLine in yReferenceLines) {
                elements.AddRange(referenceLine.CreateGraphicElements(yAxisTransformation, yAxis.NumericOperations));
            }

            var allDataPoints = new List<Point>();

            foreach (var dataSeries in allDataSeries) {
                elements.AddRange(dataSeries.CreateGraphicElements(xAxis.NumericOperations, yAxis.NumericOperations, xAxisTransformation, yAxisTransformation, out var dataPoints));
                allDataPoints.AddRange(dataPoints);
            }

            if (legend) {
                elements.AddRange(CreateLegend(allDataSeries, allDataPoints));
            }

            return elements;
        }

        private static DataSeriesRange FindDataSeriesRange(IAxis<X> xAxis, IAxis<Y> yAxis, IReadOnlyList<DataSeries<X, Y>> allDataSeries, IReadOnlyList<ReferenceLine<Y>> yReferenceLines) {
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

            foreach (var referenceLine in yReferenceLines) {
                allMinimumYValues.Add(yAxis.NumericOperations.ConvertToDoubleEquivalent(referenceLine.Value));
                allMaximumYValues.Add(yAxis.NumericOperations.ConvertToDoubleEquivalent(referenceLine.Value));
            }

            var dataSeriesRange = new DataSeriesRange(allMinimumXValues.Min(), allMaximumXValues.Max(), allMinimumYValues.Min(), allMaximumYValues.Max());
            return dataSeriesRange;
        }

        public string ToSvg() {
            return _image.CreateXml();
        }

        public string ToSvgCompressed() {
            return _image.CreateXmlCompressed();
        }

        private List<IGraphicElement> CreateLegend(IReadOnlyList<DataSeries<X, Y>> dataSeries, IReadOnlyList<Point> dataPoints) {
            var elements = new List<IGraphicElement>();
            var maxLength = 0;

            for (var i = 0; i < dataSeries.Count; i++) {
                var current = dataSeries[i];
                var y = i * _legendHeightPerCountry + _legendFontSize / 2;

                elements.Add(new Text($"label {current.Label}", new Point(_legendDotOffsetLeft * 2, y), current.Label, Color.Black, 0, _legendFont, _legendFontSize, "middle", "start"));
                elements.Add(new Circle($"dot for {current.Label}", _legendDotRadius, current.Color, new Point(_legendDotOffsetLeft, y)));
                elements.Add(new Line($"line for {current.Label}", new Point(_legendDotOffsetLeft - _legendLineLength / 2, y), new Point(_legendDotOffsetLeft + _legendLineLength / 2, y), current.Color, _legendLineWidth));
                maxLength = System.Math.Max(maxLength, current.Label.Length);
            }

            var overallWidth = _legendDotOffsetLeft * 2 + maxLength * _legendLetterWidth + _legendMarginRight;
            var overallHeight = dataSeries.Count * _legendHeightPerCountry;
            var background = new Rectangle("legend background", new Point(0, overallHeight), new Point(overallWidth, 0), _legendBackgroundColor, Color.Black, _legendBorderWidth);
            elements.Insert(0, background);

            var legendPosition = _overlapMinizmer.PlaceRectangleOverPoints(background, dataPoints);

            legendPosition = new Point(System.Math.Max(0, legendPosition.X), System.Math.Max(0, legendPosition.Y));
            legendPosition = new Point(System.Math.Min(1, legendPosition.X - overallWidth), System.Math.Min(1, legendPosition.Y - overallHeight));

            var transformation = new Transformation(new Matrix(1, 1), legendPosition.ToVector());
            elements = transformation.Apply(elements);

            return elements;
        }
    }
}
