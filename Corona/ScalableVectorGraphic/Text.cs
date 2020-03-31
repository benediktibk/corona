using System.Diagnostics;
using System.Text;

namespace ScalableVectorGraphic
{
    [DebuggerDisplay("Text {Description} {Content}")]
    public class Text : IGraphicElement
    {
        public Text(string description, Point position, string content, Color color, double rotationInDegrees, string font, int fontSize) {
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
        public int FontSize { get; }

        public void AppendXmlTo(StringBuilder stringBuilder) {
            stringBuilder.Append($"<text x=\"{Position.X}\" y=\"{Position.Y}\" font-family=\"{Font}\" fill=\"{Color.ToSvg()}\" transform=\"rotate({RotationInDegrees} {Position.X},{Position.Y})\" font-size=\"{FontSize}\">");
            stringBuilder.Append(Content);
            stringBuilder.Append("</text>");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Text(Description, transformation.Apply(Position), Content, Color, RotationInDegrees, Font, FontSize);
        }
    }
}
