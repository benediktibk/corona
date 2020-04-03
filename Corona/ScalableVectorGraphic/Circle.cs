using System.Globalization;
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

        public void AppendXmlTo(StringBuilder stringBuilder, CultureInfo culture) {
            stringBuilder.Append($"<!-- {Description} -->{System.Environment.NewLine}");
            stringBuilder.Append($"<circle cx=\"{Position.X.ToString(culture)}\" cy=\"{Position.Y.ToString(culture)}\" r=\"{Radius.ToString(culture)}\" fill=\"{Color.ToSvg()}\" />{System.Environment.NewLine}");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Circle(Description, transformation.ApplyToWidth(Radius), Color, transformation.ApplyToPoint(Position));
        }
    }
}
