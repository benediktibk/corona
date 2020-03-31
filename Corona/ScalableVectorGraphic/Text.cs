using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace ScalableVectorGraphic
{
    [DebuggerDisplay("Text {Description} {Content}")]
    public class Text : IGraphicElement
    {
        public Text(string description, Point position, string content, Color color, double rotationInDegrees, string font, double fontSize) {
            Description = description;
            Position = position;
            Content = content;
            Color = color;
            RotationInDegrees = rotationInDegrees;
            Font = font;
            FontSize = fontSize;
        }

        public string Description { get; }
        public Point Position { get; }
        public string Content { get; }
        public Color Color { get; }
        public double RotationInDegrees { get; }
        public string Font { get; }
        public double FontSize { get; }

        public void AppendXmlTo(StringBuilder stringBuilder, CultureInfo culture) {
            stringBuilder.Append($"<!-- {Description} -->\n");
            stringBuilder.Append($"<text x=\"{Position.X.ToString(culture)}\" y=\"{Position.Y.ToString(culture)}\" font-family=\"{Font}\" fill=\"{Color.ToSvg()}\" transform=\"rotate({RotationInDegrees.ToString(culture)} {Position.X.ToString(culture)},{Position.Y.ToString(culture)})\" font-size=\"{(int)FontSize}\">");
            stringBuilder.Append(Content);
            stringBuilder.Append("</text>\n");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Text(Description, transformation.ApplyToPoint(Position), Content, Color, RotationInDegrees, Font, transformation.ApplyToWidth(FontSize));
        }
    }
}
