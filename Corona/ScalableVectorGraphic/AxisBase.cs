using System;
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
        private const double _axisLabelOffsetFromHorizontalAxis = _labelOffsetFromHorizontalAxis + _fontSize + 0.01;
        private const double _axisLabelOffsetFromVerticalAxis = _labelOffsetFromVerticalAxis;
        private readonly string _axisLabel;

        public AxisBase(IGenericNumericOperations<T> numericOperations, string label) {
            NumericOperations = numericOperations;
            _axisLabel = label;
        }

        public IGenericNumericOperations<T> NumericOperations { get; }

        public abstract IAxisTransformation CreateAxisTransformation(double minimumValue, double maximumValue);

        public abstract string CreateLabel(double value);

        public List<IGraphicElement> CreateGraphicElementsForHorizontalAxis(double minimumValue, double maximumValue) {
            var result = new List<IGraphicElement>();
            result.Add(new Line("horizontal axis", new Point(0, 0), new Point(1, 0), Color.Black, _axisWidth));
            var axisTransformation = CreateAxisTransformation(minimumValue, maximumValue);

            for (var i = axisTransformation.CalculateNextTick(axisTransformation.AxisStartValue); i <= axisTransformation.AxisEndValue * 1.01; i = axisTransformation.CalculateNextTick(i)) {
                double position = axisTransformation.Apply(i);
                result.Add(new Line("horizontal axis tick mark", new Point(position, (-0.5) * _tickMarkLength), new Point(position, 0.5 * _tickMarkLength), Color.Black, _tickMarkWidth));
                var label = CreateLabel(i);
                result.Add(new Text("horizontal axis tick label", new Point(position, (-1) * _labelOffsetFromHorizontalAxis), label, Color.Black, 0, _labelFont, _fontSize, "hanging", "middle"));
                result.Add(new Line("vertical grid", new Point(position, 0), new Point(position, 1), Color.Black, _gridWidth));
            }

            result.Add(new Text("horizontal axis label", new Point(0.5, (-1) * _axisLabelOffsetFromHorizontalAxis), _axisLabel, Color.Black, 0, _labelFont, _fontSize, "hanging", "middle"));

            return result;
        }

        public List<IGraphicElement> CreateGraphicElementsForVerticalAxis(double minimumValue, double maximumValue) {
            var result = new List<IGraphicElement>();
            result.Add(new Line("vertical axis", new Point(0, 0), new Point(0, 1), Color.Black, _axisWidth));
            var axisTransformation = CreateAxisTransformation(minimumValue, maximumValue);
            var tickPositions = new List<double>();

            for (var i = axisTransformation.CalculateNextTick(axisTransformation.AxisStartValue); i < axisTransformation.AxisEndValue * 1.01; i = axisTransformation.CalculateNextTick(i)) {
                double position = axisTransformation.Apply(i);
                result.Add(new Line("vertical axis tick mark", new Point((-0.5) * _tickMarkLength, position), new Point(0.5 * _tickMarkLength, position), Color.Black, _tickMarkWidth));
                var label = CreateLabel(i);
                result.Add(new Text("vertical axis tick label", new Point((-1) * _labelOffsetFromVerticalAxis, position), label, Color.Black, 0, _labelFont, _fontSize, "middle", "end"));
                result.Add(new Line("horizontal grid", new Point(0, position), new Point(1, position), Color.Black, _gridWidth));
                tickPositions.Add(position);
            }

            var labelPosition = CalculateVerticalLabelPosition(tickPositions);
            result.Add(new Text("horizontal axis label", new Point((-1) * _axisLabelOffsetFromVerticalAxis, labelPosition), _axisLabel, Color.Black, 270, _labelFont, _fontSize, "middle", "middle"));

            return result;
        }

        public static double CalculateVerticalLabelPosition(IReadOnlyList<double> tickPositions) {
            var labelPosition = 0.5;

            if (tickPositions.Count < 2) {
                return labelPosition;
            }

            var closestTick = tickPositions[0];
            var nextTick = tickPositions[1];

            for (var i = 1; i < tickPositions.Count - 1; ++i) {
                if (Math.Abs(labelPosition - tickPositions[i]) < Math.Abs(labelPosition - closestTick)*0.99) {
                    closestTick = tickPositions[i];
                    nextTick = tickPositions[i + 1];
                }
            }

            labelPosition = (nextTick + closestTick) / 2;

            return labelPosition;
        }
    }
}
