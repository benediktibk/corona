using System.Text;

namespace ScalableVectorGraphic
{
    public class Circle : IGraphicElement
    {
        public Circle(string description, double radius, Color color, Point position) {
            Description = description;
            Radius = radius;
            Color = color;
            Position = position;
        }

        public string Description { get; }
        public double Radius { get; }
        public Color Color { get; }
        public Point Position { get; }

        public void AppendXmlTo(StringBuilder stringBuilder) {
            stringBuilder.Append($"<!-- {Description} -->\n");
            stringBuilder.Append($"<circle cx=\"{Position.X}\" cy=\"{Position.Y}\" r=\"{Radius}\" fill=\"{Color.ToSvg()}\" />\n");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Circle(Description, Radius, Color, transformation.Apply(Position));
        }
    }
}
