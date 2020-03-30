using System.Text;

namespace ScalableVectorGraphic
{
    public class Text : IGraphicElement
    {
        public Text(Point position, string content, Color color, double rotationInDegrees) {
            Position = position;
            Content = content;
            Color = color;
            RotationInDegrees = rotationInDegrees;
        }

        public Point Position { get; }
        public string Content { get; }
        public Color Color { get; }
        public double RotationInDegrees { get; }

        public void AppendXmlTo(StringBuilder stringBuilder) {
            throw new System.NotImplementedException();
        }
    }
}
