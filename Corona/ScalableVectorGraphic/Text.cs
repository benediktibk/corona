using Math;
using System.Diagnostics;

namespace ScalableVectorGraphic
{
    [DebuggerDisplay("Text {Description} {Content}")]
    public class Text : IGraphicElement
    {
        public Text(string description, Point position, string content, Color color, double rotationInDegrees, string font, double fontSize, string dominantBaseLine, string textAnchor) {
            Description = description;
            Position = position;
            Content = content;
            Color = color;
            RotationInDegrees = rotationInDegrees;
            Font = font;
            FontSize = fontSize;
            DominantBaseLine = dominantBaseLine;
            TextAnchor = textAnchor;
        }

        public string Description { get; }
        public Point Position { get; }
        public string Content { get; }
        public Color Color { get; }
        public double RotationInDegrees { get; }
        public string Font { get; }
        public double FontSize { get; }
        public string DominantBaseLine { get; }
        public string TextAnchor { get; }

        public void AddTo(ISvgXmlWriter svgXmlWriter) {
            svgXmlWriter.AddTagWithContent("text", $"x=\"{Position.X.ToString(svgXmlWriter.Culture)}\" y=\"{Position.Y.ToString(svgXmlWriter.Culture)}\" font-family=\"{Font}\" fill=\"{Color.ToSvg()}\" transform=\"rotate({RotationInDegrees.ToString(svgXmlWriter.Culture)} {Position.X.ToString(svgXmlWriter.Culture)},{Position.Y.ToString(svgXmlWriter.Culture)})\" font-size=\"{(int)FontSize}\" dominant-baseline=\"{DominantBaseLine}\" text-anchor=\"{TextAnchor}\"", Content);
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Text(Description, transformation.ApplyToPoint(Position), Content, Color, RotationInDegrees, Font, transformation.ApplyToWidth(FontSize), DominantBaseLine, TextAnchor);
        }
    }
}
