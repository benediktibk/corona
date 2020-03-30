using System.Text;

namespace ScalableVectorGraphic
{
    public class Line : IGraphicElement
    {
        public Line(Point start, Point end, Color color, double width) {
            Start = start;
            End = end;
            Color = color;
            Width = width;
        }

        public Point Start { get; }
        public Point End { get; }
        public Color Color { get; }
        public double Width { get; }

        public void AppendXmlTo(StringBuilder stringBuilder) {
            throw new System.NotImplementedException();
        }
    }
}
