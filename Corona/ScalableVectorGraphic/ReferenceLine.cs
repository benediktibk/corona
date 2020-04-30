using System.Collections.Generic;

namespace ScalableVectorGraphic {
    public class ReferenceLine<T> {
        private const double _lineWidth = 0.002;
        private const double _axisOffset = 0.01;
        private const double _lineOffset = 0.01;
        private const string _labelFont = "monospace";
        private const double _fontSize = 0.02;
        private const double _strokeLength = 0.01;

        public ReferenceLine(T value, string label, Color color) {
            Value = value;
            Label = label;
            Color = color;
        }

        public T Value { get; }
        public string Label { get; }
        public Color Color { get; }

        public List<IGraphicElement> CreateGraphicElements(IAxisTransformation axisTransformation, IGenericNumericOperations<T> numericOperations) {
            var result = new List<IGraphicElement>();
            var position = axisTransformation.Apply(numericOperations.ConvertToDoubleEquivalent(Value));
            result.Add(new DottedLine($"reference line {Label}", new Point(0, position), new Point(1, position), Color, _lineWidth, _strokeLength, _strokeLength));
            result.Add(new Text($"label for reference line {Label}", new Point(_axisOffset, position + _lineOffset), Label, Color, 0, _labelFont, _fontSize, "baseline", "start"));
            return result;
        }
    }
}
