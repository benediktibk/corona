using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public abstract class AxisBase<T> : IAxis<T>
    {
        private const double _axisWidth = 0.002;
        private const double _tickMarkLength = 0.01;
        private const double _tickMarkWidth = 0.001;
        private const double _gridWidth = 0.0005;
        private const string _labelFont = "monospace";
        private const double _fontSize = 0.02;
        private const double _labelOffsetFromHorizontalAxis = 0.01;
        private const double _labelOffsetFromVerticalAxis = 0.01;

        public AxisBase(IGenericNumericOperations<T> numericOperations) {
            NumericOperations = numericOperations;
        }

        public IGenericNumericOperations<T> NumericOperations { get; }

        public abstract IAxisTransformation CreateAxisTransformation(double minimumValue, double maximumValue);

        public List<IGraphicElement> CreateGraphicElementsForHorizontalAxis(double minimumValue, double maximumValue) {
            var result = new List<IGraphicElement>();
            result.Add(new Line("horizontal axis", new Point(0, 0), new Point(1, 0), Color.Black, _axisWidth));
            var axisTransformation = CreateAxisTransformation(minimumValue, maximumValue);

            for (var i = axisTransformation.CalculateNextTick(axisTransformation.AxisStartValue); i < axisTransformation.AxisEndValue; i = axisTransformation.CalculateNextTick(i)) {
                double position = axisTransformation.Apply(i);
                result.Add(new Line("horizontal axis tick mark", new Point(position, (-0.5) * _tickMarkLength), new Point(position, 0.5 * _tickMarkLength), Color.Black, _tickMarkWidth));
                var label = NumericOperations.CreateLabel(i);
                result.Add(new Text("horizontal axis tick label", new Point(position, (-1) * _labelOffsetFromHorizontalAxis), label, Color.Black, 0, _labelFont, _fontSize, "hanging", "middle"));
                result.Add(new Line("vertical grid", new Point(position, 0), new Point(position, 1), Color.Black, _gridWidth));
            }

            return result;
        }

        public List<IGraphicElement> CreateGraphicElementsForVerticalAxis(double minimumValue, double maximumValue) {
            var result = new List<IGraphicElement>();
            result.Add(new Line("vertical axis", new Point(0, 0), new Point(0, 1), Color.Black, _axisWidth));
            var axisTransformation = CreateAxisTransformation(minimumValue, maximumValue);

            for (var i = axisTransformation.CalculateNextTick(axisTransformation.AxisStartValue); i < axisTransformation.AxisEndValue; i = axisTransformation.CalculateNextTick(i)) {
                double position = axisTransformation.Apply(i);
                result.Add(new Line("vertical axis tick mark", new Point((-0.5) * _tickMarkLength, position), new Point(0.5 * _tickMarkLength, position), Color.Black, _tickMarkWidth));
                var label = NumericOperations.CreateLabel(i);
                result.Add(new Text("vertical axis tick label", new Point((-1) * _labelOffsetFromVerticalAxis, position), label, Color.Black, 0, _labelFont, _fontSize, "middle", "end"));
                result.Add(new Line("horizontal grid", new Point(0, position), new Point(1, position), Color.Black, _gridWidth));
            }

            return result;
        }
    }
}
