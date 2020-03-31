using System.Diagnostics;
using System.Globalization;
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

        public void AppendXmlTo(StringBuilder stringBuilder, CultureInfo culture) {
            stringBuilder.Append($"<!-- {Description} -->\n");
            stringBuilder.Append($"<line x1=\"{Start.X.ToString(culture)}\" y1=\"{Start.Y.ToString(culture)}\" x2=\"{End.X.ToString(culture)}\" y2=\"{End.Y.ToString(culture)}\" style=\"stroke:{Color.ToSvg()};stroke-width:{Width.ToString(culture)}\" />\n");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Line(Description, transformation.ApplyToPoint(Start), transformation.ApplyToPoint(End), Color, transformation.ApplyToWidth(Width));
        }
    }
}
