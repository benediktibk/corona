using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class LinearAxis<T> : IAxis<T>
    {
        private const double _axisWidth = 0.002;
        private const double _tickMarkLength = 0.01;
        private const double _tickMarkWidth = 0.001;
        private const string _labelFont = "monospace";
        private const double _fontSize = 0.02;
        private const double _labelOffsetFromAxis = -0.02;

        public LinearAxis(IGenericNumericOperations<T> numericOperations) {
            NumericOperations = numericOperations;
        }

        public IGenericNumericOperations<T> NumericOperations { get; }

        public List<IGraphicElement> CreateGraphicElementsForHorizontalAxis(T minimumValue, T maximumValue, T tickMarkDistance) {
            var result = new List<IGraphicElement>();
            result.Add(new Line("horizontal axis", new Point(0, 0), new Point(1, 0), Color.Black, _axisWidth));

            for (var i = NumericOperations.Add(minimumValue, tickMarkDistance); NumericOperations.SmallerThan(i, maximumValue); i = NumericOperations.Add(i, tickMarkDistance)) {
                double position = NumericOperations.ScaleBetween0And1(minimumValue, maximumValue, i);
                result.Add(new Line("horizontal axis tick mark", new Point(position, 0), new Point(position, _tickMarkLength), Color.Black, _tickMarkWidth));
                var label = NumericOperations.CreateLabel(i);
                var halfLabelLength = label.Length / 2.0;
                var labelOffsetFromTick = halfLabelLength * _fontSize * (-0.5);
                result.Add(new Text("horizontal axis tick label", new Point(position + labelOffsetFromTick, _labelOffsetFromAxis), label, Color.Black, 0, _labelFont, _fontSize));
            }

            return result;
        }

        public List<IGraphicElement> CreateGraphicElementsForVerticalAxis(T minimumValue, T maximumValue, T tickMarkDistance) {
            var result = new List<IGraphicElement>();
            result.Add(new Line("vertical axis", new Point(0, 0), new Point(0, 1), Color.Black, _axisWidth));

            for (var i = NumericOperations.Add(minimumValue, tickMarkDistance); NumericOperations.SmallerThan(i, maximumValue); i = NumericOperations.Add(i, tickMarkDistance)) {
                double position = NumericOperations.ScaleBetween0And1(minimumValue, maximumValue, i);
                result.Add(new Line("vertical axis tick mark", new Point(0, position), new Point(_tickMarkLength, position), Color.Black, _tickMarkWidth));
                var label = NumericOperations.CreateLabel(i);
                var labelOffsetFromTick = label.Length * _fontSize * (-1);
                result.Add(new Text("vertical axis tick label", new Point(labelOffsetFromTick, position - _fontSize/2), label, Color.Black, 0, _labelFont, _fontSize));
            }

            return result;
        }
    }
}
