using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class XYGraph
    {
        private readonly Image _image;

        public XYGraph(int width, int height) {
            var elements = new List<IGraphicElement> {
                new Line(new Point(10, 10), new Point(width - 5, 10), Color.Black, 1),
                new Line(new Point(10, 10), new Point(10, height - 5), Color.Black, 1),
                new Text(new Point(50, 5), "x-axis", Color.Green, 0, "Arial, Helvetica, sans-serif", 1),
                new Circle(1, Color.Blue, new Point(50, 50))
            };
            var transformation = new Transformation(new Matrix(new Vector(1, 0), new Vector(0, -1)), new Vector(0, height));
            _image = new Image(width, height, elements, transformation);
        }

        public string ToSvg() {
            return _image.CreateXml();
        }
    }
}
