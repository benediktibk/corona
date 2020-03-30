using System.Text;

namespace ScalableVectorGraphic
{
    public class Circle : IGraphicElement
    {
        public Circle(double radius, Color color, Point position) {
            Radius = radius;
            Color = color;
            Position = position;
        }

        public double Radius { get; }
        public Color Color { get; }
        public Point Position { get; }

        public void AppendXmlTo(StringBuilder stringBuilder) {
            throw new System.NotImplementedException();
        }
    }
}
