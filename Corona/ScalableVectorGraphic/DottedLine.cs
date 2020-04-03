using System.Diagnostics;

namespace ScalableVectorGraphic
{
    [DebuggerDisplay("DottedLine {Description} ({Start.X.ToString(\"F2\")}, {Start.Y.ToString(\"F2\")}) - ({End.X.ToString(\"F2\")}, {End.Y.ToString(\"F2\")})")]
    public class DottedLine : IGraphicElement
    {
        public DottedLine(string description, Point start, Point end, Color color, double width, double strokeLength, double emptyLength) {
            Description = description;
            Start = start;
            End = end;
            Color = color;
            Width = width;
            StrokeLength = strokeLength;
            EmptyLength = emptyLength;
        }

        public Point Start { get; }
        public Point End { get; }
        public Color Color { get; }
        public double Width { get; }
        public double StrokeLength { get; }
        public double EmptyLength { get; }

        public string Description { get; }

        public void AddTo(SvgXmlWriterBase svgXmlWriter) {
            svgXmlWriter.AddSingleTag("line", $"x1=\"{Start.X.ToString(svgXmlWriter.Culture)}\" y1=\"{Start.Y.ToString(svgXmlWriter.Culture)}\" x2=\"{End.X.ToString(svgXmlWriter.Culture)}\" y2=\"{End.Y.ToString(svgXmlWriter.Culture)}\" style=\"stroke:{Color.ToSvg()};stroke-width:{Width.ToString(svgXmlWriter.Culture)}\" stroke-dasharray=\"{StrokeLength.ToString(svgXmlWriter.Culture)},{EmptyLength.ToString(svgXmlWriter.Culture)}\"");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new DottedLine(Description, transformation.ApplyToPoint(Start), transformation.ApplyToPoint(End), Color, transformation.ApplyToWidth(Width), transformation.ApplyToWidth(StrokeLength), transformation.ApplyToWidth(EmptyLength));
        }
    }
}
