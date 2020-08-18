using System.Collections.Generic;

namespace ScalableVectorGraphic {
    public class HorizontalBarGraph<BarType, ValueType> : IGraph {
        private readonly Image _image;
        private const double _barWidth = 0.1;
        private const double _barSpacing = 0.05;
        private const double _axisWidth = 0.002;
        private const double _xOffsetVerticalAxisLabel = 0.002;
        private const string _legendFont = "monospace";
        private const double _legendFontSize = 0.02;

        public HorizontalBarGraph(int width, int height, ILabelGenerator<BarType> verticalAxis, IAxis<ValueType> horizontalAxis, DataSeriesBar<BarType, ValueType> dataSeries) {
            var maximumValue = dataSeries.FindMaximumValueAsDouble(horizontalAxis.NumericOperations);
            var elements = CreateGraphicElements(horizontalAxis, verticalAxis, dataSeries.DataPoints, maximumValue);
            _image = new Image(width, height, elements);
        }

        private List<IGraphicElement> CreateGraphicElements(IAxis<ValueType> horizontalAxis, ILabelGenerator<BarType> verticalAxis, IReadOnlyList<DataPoint<BarType, ValueType>> values, double maximumValue) {
            var elements = new List<IGraphicElement>();
            elements.AddRange(horizontalAxis.CreateGraphicElementsForHorizontalAxis(0, maximumValue));
            elements.AddRange(CreateGraphicElementsForVerticalAxis(values, verticalAxis));
            elements.AddRange(CreateGraphicElementsForBars(values, horizontalAxis, maximumValue));
            return elements;
        }

        private List<IGraphicElement> CreateGraphicElementsForVerticalAxis(IReadOnlyList<DataPoint<BarType, ValueType>> values, ILabelGenerator<BarType> verticalAxis) {
            var elements = new List<IGraphicElement> {
                new Line("vertical axis", new Point(0, 0), new Point(0, 1), Color.Black, _axisWidth)
            };

            for (var i = 0; i < values.Count; ++i) {
                var yPosition = _barSpacing + _barSpacing * i + _barWidth * i;
                elements.Add(new Text("vertical axis label", new Point((-1) * _xOffsetVerticalAxisLabel, yPosition), verticalAxis.CreateLabel(values[i].XValue), Color.Black, 90, _legendFont, _legendFontSize, "hanging", "middle"));
            }

            return elements;
        }

        private List<IGraphicElement> CreateGraphicElementsForBars(IReadOnlyList<DataPoint<BarType, ValueType>> values, IAxis<ValueType> horizontalAxis, double maximumValue) {
            var elements = new List<IGraphicElement>();
            var numericOperations = horizontalAxis.NumericOperations;
            var transformation = horizontalAxis.CreateAxisTransformation(0, maximumValue);

            for (var i = 0; i < values.Count; ++i) {
                var yPosition = _barSpacing + _barSpacing * i + _barWidth * i;
                var valueAsDouble = numericOperations.ConvertToDoubleEquivalent(values[i].YValue);
                var barLength = transformation.Apply(valueAsDouble);
                elements.Add(new Rectangle("bar", new Point(0, yPosition + _barWidth / 2), new Point(barLength, yPosition - _barWidth / 2), Color.Black, Color.Black, 0));
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
