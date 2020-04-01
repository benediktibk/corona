using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class LogarithmicAxis<T> : IAxis<T>
    {
        private const double _axisWidth = 0.002;
        private const double _tickMarkLength = 0.01;
        private const double _tickMarkWidth = 0.001;
        private const string _labelFont = "monospace";
        private const double _fontSize = 0.02;

        public LogarithmicAxis(IGenericNumericOperations<T> numericOperations) {
            NumericOperations = numericOperations;
        }

        public IGenericNumericOperations<T> NumericOperations { get; }

        public IAxisTransformation CreateAxisTransformation(double minimumValue, double maximumValue) {
            return new LogarithmicAxisTransformation(minimumValue, maximumValue);
        }

        public List<IGraphicElement> CreateGraphicElementsForHorizontalAxis(double minimumValue, double maximumValue, T tickMarkDistance) {
            var result = new List<IGraphicElement>();
            result.Add(new Line("horizontal axis", new Point(0, 0), new Point(1, 0), Color.Black, _axisWidth));
            var tickMarkDistanceAsDouble = NumericOperations.ConvertToDoubleEquivalent(tickMarkDistance);
            var axisTransformation = CreateAxisTransformation(minimumValue, maximumValue);

            for (var i = minimumValue + tickMarkDistanceAsDouble; i < maximumValue; i += tickMarkDistanceAsDouble) {
                double position = axisTransformation.Apply(i);
                result.Add(new Line("horizontal axis tick mark", new Point(position, 0), new Point(position, _tickMarkLength), Color.Black, _tickMarkWidth));
                var label = NumericOperations.CreateLabel(i);
                var halfLabelLength = label.Length / 2.0;
                var labelOffsetFromTick = halfLabelLength * _fontSize * (-0.5);
                result.Add(new Text("horizontal axis tick label", new Point(position + labelOffsetFromTick, (-1.1) * _fontSize), label, Color.Black, 0, _labelFont, _fontSize));
            }

            return result;
        }

        public List<IGraphicElement> CreateGraphicElementsForVerticalAxis(double minimumValue, double maximumValue, T tickMarkDistance) {
            var result = new List<IGraphicElement>();
            result.Add(new Line("vertical axis", new Point(0, 0), new Point(0, 1), Color.Black, _axisWidth));
            var tickMarkDistanceAsDouble = NumericOperations.ConvertToDoubleEquivalent(tickMarkDistance);
            var axisTransformation = CreateAxisTransformation(minimumValue, maximumValue);

            for (var i = minimumValue + tickMarkDistanceAsDouble; i < maximumValue; i += tickMarkDistanceAsDouble) {
                double position = axisTransformation.Apply(i);
                result.Add(new Line("vertical axis tick mark", new Point(0, position), new Point(_tickMarkLength, position), Color.Black, _tickMarkWidth));
                var label = NumericOperations.CreateLabel(i);
                var labelOffsetFromTick = label.Length * _fontSize * (-1);
                result.Add(new Text("vertical axis tick label", new Point(labelOffsetFromTick, position - _fontSize/2), label, Color.Black, 0, _labelFont, _fontSize));
            }

            return result;
        }
    }
}
