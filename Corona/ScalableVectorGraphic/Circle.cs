namespace ScalableVectorGraphic {
    public class Circle : IGraphicElement {
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

        public void AddTo(ISvgXmlWriter svgXmlWriter) {
            svgXmlWriter.AddSingleTag("circle", $"cx=\"{Position.X.ToString(svgXmlWriter.Culture)}\" cy=\"{Position.Y.ToString(svgXmlWriter.Culture)}\" r=\"{Radius.ToString(svgXmlWriter.Culture)}\" fill=\"{Color.ToSvg(svgXmlWriter)}\"");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Circle(Description, transformation.ApplyToWidth(Radius), Color, transformation.ApplyToPoint(Position));
        }
    }
}
