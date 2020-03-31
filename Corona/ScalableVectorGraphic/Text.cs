using System.Text;

namespace ScalableVectorGraphic
{
    public class Text : IGraphicElement
    {
        public Text(Point position, string content, Color color, double rotationInDegrees, string font, int fontSize) {
            Position = position;
            Content = content;
            Color = color;
            RotationInDegrees = rotationInDegrees;
            Font = font;
            FontSize = fontSize;
        }

        public Point Position { get; }
        public string Content { get; }
        public Color Color { get; }
        public double RotationInDegrees { get; }
        public string Font { get; }
        public int FontSize { get; }

        public void AppendXmlTo(StringBuilder stringBuilder) {
            stringBuilder.Append($"<text x=\"{Position.X}\" y=\"{Position.Y}\" font-family=\"{Font}\" fill=\"rgb({Color.Red},{Color.Green},{Color.Blue})\" transform=\"rotate({RotationInDegrees} {Position.X},{Position.Y})\" font-size=\"{FontSize}em\">");
            stringBuilder.Append(Content);
            stringBuilder.Append("</text>");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Text(transformation.Apply(Position), Content, Color, RotationInDegrees, Font, FontSize);
        }
    }
}
