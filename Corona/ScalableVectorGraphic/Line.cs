using System.Text;

namespace ScalableVectorGraphic
{
    public class Line : IGraphicElement
    {
        public Line(Point start, Point end, Color color, double width) {
            Start = start;
            End = end;
            Color = color;
            Width = width;
        }

        public Point Start { get; }
        public Point End { get; }
        public Color Color { get; }
        public double Width { get; }

        public void AppendXmlTo(StringBuilder stringBuilder) {
            stringBuilder.Append($"<line x1=\"{Start.X}\" y1=\"{Start.Y}\" x2=\"{Start.X}\" y2=\"{Start.Y}\" style=\"stroke:rgb({Color.Red},{Color.Green},{Color.Blue});stroke-width:{Width}\" />");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Line(transformation.Apply(Start), transformation.Apply(End), Color, Width);
        }
    }
}
