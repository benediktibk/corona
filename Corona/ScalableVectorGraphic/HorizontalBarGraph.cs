using Math;
using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic {
    public class HorizontalBarGraph<BarType, ValueType> : IGraph {
        private readonly Image _image;
        private const double _axisWidth = 0.002;
        private const double _xOffsetVerticalAxisLabel = 0.002;
        private const double _xOffsetBarLabels = 0.002;
        private const double _yAxisOffsetForLabels = 0.05;
        private const string _legendFont = "monospace";
        private const double _legendFontSize = 0.02;
        private const double _borderWidth = 0.002;
        private const double _ratioXAxisLengthToImageSize = 0.80;
        private const double _ratioYAxisLengthToImageSize = 0.85;

        public HorizontalBarGraph(int width, int height, ILabelGenerator<BarType> verticalAxis, IAxis<ValueType> horizontalAxis, DataSeriesBar<BarType, ValueType> dataSeries) {
            var maximumValue = dataSeries.FindMaximumValueAsDouble(horizontalAxis.NumericOperations) * 1.05;
            var elements = CreateGraphicElements(horizontalAxis, verticalAxis, dataSeries.DataPoints, maximumValue);
            elements = TransformElements(width, height, elements);
            elements.Insert(0, new Rectangle("background", new Point(0, 0), new Point(width, height), Color.White, Color.Black, _borderWidth * System.Math.Sqrt(width * height)));
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

        private List<IGraphicElement> CreateGraphicElements(IAxis<ValueType> horizontalAxis, ILabelGenerator<BarType> verticalAxis, IReadOnlyList<DataPoint<BarType, ValueType>> values, double maximumValue) {
            var elements = new List<IGraphicElement>();
            var barWidth = 1.0 / values.Count * 0.9;
            var barSpacing = 1.0 / values.Count * 0.08;
            var valuesReversed = values.Reverse().ToList();
            elements.AddRange(horizontalAxis.CreateGraphicElementsForHorizontalAxis(0, maximumValue));
            elements.AddRange(CreateGraphicElementsForVerticalAxis(valuesReversed, verticalAxis, barSpacing, barWidth));
            elements.AddRange(CreateGraphicElementsForBars(valuesReversed, horizontalAxis, maximumValue, barSpacing, barWidth));

            return elements;
        }

        private List<IGraphicElement> CreateGraphicElementsForVerticalAxis(IReadOnlyList<DataPoint<BarType, ValueType>> values, ILabelGenerator<BarType> verticalAxis, double barSpacing, double barWidth) {
            var elements = new List<IGraphicElement> {
                new Line("vertical axis", new Point(0, 0), new Point(0, 1), Color.Black, _axisWidth)
            };

            for (var i = 0; i < values.Count; ++i) {
                var yPosition = barSpacing + barWidth / 2.0 + barSpacing * i + barWidth * i;
                elements.Add(new Text("vertical axis label", new Point((-1) * _xOffsetVerticalAxisLabel, yPosition), verticalAxis.CreateLabel(values[i].XValue), Color.Black, 0, _legendFont, _legendFontSize, "hanging", "end"));
            }

            return elements;
        }

        private List<IGraphicElement> CreateGraphicElementsForBars(IReadOnlyList<DataPoint<BarType, ValueType>> values, IAxis<ValueType> horizontalAxis, double maximumValue, double barSpacing, double barWidth) {
            var elements = new List<IGraphicElement>();
            var numericOperations = horizontalAxis.NumericOperations;
            var transformation = horizontalAxis.CreateAxisTransformation(0, maximumValue);

            for (var i = 0; i < values.Count; ++i) {
                var yPosition = barSpacing + barWidth / 2.0 + barSpacing * i + barWidth * i;
                var originalValue = values[i].YValue;
                var valueAsDouble = numericOperations.ConvertToDoubleEquivalent(originalValue);
                var barLength = transformation.Apply(valueAsDouble);
                elements.Add(new Rectangle($"bar for value {originalValue}", new Point(0, yPosition + barWidth / 2), new Point(barLength, yPosition - barWidth / 2), new Color(189, 113, 38), Color.Black, 0));
                elements.Add(new Text($"value label for {originalValue}", new Point(barLength + _xOffsetBarLabels, yPosition), horizontalAxis.CreateLabel(valueAsDouble), Color.Black, 0, _legendFont, _legendFontSize, "middle", "start"));
            }

            return elements;
        }

        public string ToSvg() {
            return _image.CreateXml();
        }

        public string ToSvgCompressed() {
            return _image.CreateXmlCompressed();
        }
    }
}
