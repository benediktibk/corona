using System.Diagnostics;

namespace ScalableVectorGraphic
{
    [DebuggerDisplay("Rectangle {Description} ({LeftUpperCorner.X.ToString(\"F2\")}, {LeftUpperCorner.Y.ToString(\"F2\")}) - ({RightLowerCorner.X.ToString(\"F2\")}, {RightLowerCorner.Y.ToString(\"F2\")})")]
    public class Rectangle : IGraphicElement
    {
        public Rectangle(string description, Point leftUpperCorner, Point rightLowerCorner, Color backroundColor, Color lineColor, double lineWidth) {
            Description = description;
            LeftUpperCorner = leftUpperCorner;
            RightLowerCorner = rightLowerCorner;
            BackgroundColor = backroundColor;
            LineColor = lineColor;
            LineWidth = lineWidth;
        }

        public Point LeftUpperCorner { get; }
        public Point RightLowerCorner { get; }
        public Color BackgroundColor { get; }
        public Color LineColor { get; }
        public double LineWidth { get; }
        public double Width => System.Math.Abs(RightLowerCorner.X - LeftUpperCorner.X);
        public double Height => System.Math.Abs(LeftUpperCorner.Y - RightLowerCorner.Y);

        public string Description { get; }

        public void AddTo(ISvgXmlWriter svgXmlWriter) {
            svgXmlWriter.AddSingleTag("rect", $"x=\"{LeftUpperCorner.X.ToString(svgXmlWriter.Culture)}\" y=\"{LeftUpperCorner.Y.ToString(svgXmlWriter.Culture)}\" width=\"{Width.ToString(svgXmlWriter.Culture)}\" height=\"{Height.ToString(svgXmlWriter.Culture)}\" style=\"fill:{BackgroundColor.ToSvg()};stroke:{LineColor.ToSvg()};stroke-width:{LineWidth.ToString(svgXmlWriter.Culture)}\"");
        }

        public IGraphicElement ApplyTransformation(Transformation transformation) {
            return new Rectangle(Description, transformation.ApplyToPoint(LeftUpperCorner), transformation.ApplyToPoint(RightLowerCorner), BackgroundColor, LineColor, transformation.ApplyToWidth(LineWidth));
        }
    }
}
