using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class LinearAxis<T> : IAxis<T>
    {
        private const double _axisWidth = 0.001;
        private const double _tickMarkLength = 0.01;
        private const double _tickMarkWidth = 0.0005;
        private const string _labelFont = "monospace";
        private const double _fontSize = 0.02;
        private const double _labelOffsetFromAxis = -0.02;

        public LinearAxis(IGenericNumericOperations<T> numericOperations) {
            NumericOperations = numericOperations;
        }

        public IGenericNumericOperations<T> NumericOperations { get; }

        public List<IGraphicElement> CreateGraphicElements(T minimumValue, T maximumValue, T tickMarkDistance) {
            var result = new List<IGraphicElement>();
            result.Add(new Line("axis", new Point(0, 0), new Point(1, 0), Color.Black, _axisWidth));
            var nextTickRequired = false;

            for (var i = minimumValue; NumericOperations.SmallerThan(i, maximumValue); i = NumericOperations.Add(i, tickMarkDistance)) {
                double position = NumericOperations.ScaleBetween0And1(minimumValue, maximumValue, i);

                if (nextTickRequired) {
                    result.Add(new Line("axis tick mark", new Point(position, 0), new Point(position, _tickMarkLength), Color.Black, _tickMarkWidth));
                }

                var label = NumericOperations.CreateLabel(i);
                var labelOffsetFromTick = (label.Length / 2.0) * (_fontSize / -10.0);
                result.Add(new Text("axis tick label", new Point(position /*+ labelOffsetFromTick*/, _labelOffsetFromAxis), label, Color.Black, 0, _labelFont, _fontSize));
                nextTickRequired = true;
            }

            return result;
        }
    }
}
