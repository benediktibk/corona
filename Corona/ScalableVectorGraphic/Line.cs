using System.Diagnostics;
using System.Text;

namespace ScalableVectorGraphic
{
    [DebuggerDisplay("Line {Description} ({Start.X.ToString(\"F2\")}, {Start.Y.ToString(\"F2\")}) - ({End.X.ToString(\"F2\")}, {End.Y.ToString(\"F2\")})")]
    public class Line : IGraphicElement
    {
        public Line(string description, Point start, Point end, Color color, double width) {
            Description = description;
            Start = start;
            End = end;
            Color = color;
            Width = width;
        }

        public Point Start { get; }
        public Point End { get; }
        public Color Color { get; }
        public double Width { get; }

        public string Description { get; }

        public void AppendXmlTo(StringBuilder stringBuilder) {
            stringBuilder.Append($"<line x1=\"{Start.X}\" y1=\"{Start.Y}\" x2=\"{End.X}\" y2=\"{End.Y}\" style=\"stroke:{Color.ToSvg()};stroke-width:{Width}\" />");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Line(Description, transformation.Apply(Start), transformation.Apply(End), Color, Width);
        }
    }
}
