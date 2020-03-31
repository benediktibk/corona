using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class LinearAxis<T> : IAxis<T>
    {
        private readonly IGenericNumericOperations<T> _numericOperations;
        private const double _axisWidth = 1;
        private const double _tickMarkLength = 0.01;
        private const double _tickMarkWidth = 0.5;
        private const string _labelFont = "monospace";
        private const int _fontSize = 5;
        private const double _labelOffset = -0.1;

        public LinearAxis(IGenericNumericOperations<T> numericOperations) {
            _numericOperations = numericOperations;
        }

        public List<IGraphicElement> CreateGraphicElements(T minimumValue, T maximumValue, T tickMarkDistance) {
            var result = new List<IGraphicElement>();
            result.Add(new Line(new Point(0, 0), new Point(0, 1), Color.Black, _axisWidth));

            for (var i = minimumValue; _numericOperations.SmallerThan(i, maximumValue); i = _numericOperations.Add(i, tickMarkDistance)) {
                double position = _numericOperations.ScaleBetween0And1(minimumValue, maximumValue, i);
                result.Add(new Line(new Point(position, 0), new Point(position, _tickMarkLength), Color.Black, _tickMarkWidth));
                var label = _numericOperations.CreateLabel(i);
                result.Add(new Text(new Point(position, _labelOffset), label, Color.Black, 0, _labelFont, _fontSize));
            }

            return result;
        }
    }
}
