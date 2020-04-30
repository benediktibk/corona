using System.Diagnostics;

namespace ScalableVectorGraphic {
    [DebuggerDisplay("Line {Description} ({Start.X.ToString(\"F2\")}, {Start.Y.ToString(\"F2\")}) - ({End.X.ToString(\"F2\")}, {End.Y.ToString(\"F2\")})")]
    public class Line : IGraphicElement {
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

        public void AddTo(ISvgXmlWriter svgXmlWriter) {
            svgXmlWriter.AddSingleTag("line", $"x1=\"{Start.X.ToString(svgXmlWriter.Culture)}\" y1=\"{Start.Y.ToString(svgXmlWriter.Culture)}\" x2=\"{End.X.ToString(svgXmlWriter.Culture)}\" y2=\"{End.Y.ToString(svgXmlWriter.Culture)}\" style=\"stroke:{Color.ToSvg()};stroke-width:{Width.ToString(svgXmlWriter.Culture)}\"");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Line(Description, transformation.ApplyToPoint(Start), transformation.ApplyToPoint(End), Color, transformation.ApplyToWidth(Width));
        }
    }
}
