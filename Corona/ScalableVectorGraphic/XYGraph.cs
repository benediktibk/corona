using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class XYGraph
    {
        private readonly Image _image;

        public XYGraph(int width, int height) {
            var elements = new List<IGraphicElement> {
                new Line(new Point(10, 10), new Point(195, 10), new Color(255, 0, 0), 1),
                new Line(new Point(10, 10), new Point(10, 95), new Color(255, 0, 0), 1),
                new Text(new Point(50, 5), "x-axis", new Color(0, 255, 0), 0, "Arial, Helvetica, sans-serif", 1),
                new Circle(1, new Color(0, 0, 255), new Point(50, 50))
            };
            var transformation = new Transformation(new Matrix(new Vector(1, 0), new Vector(0, -1)), new Vector(0, height));
            _image = new Image(width, height, elements, transformation);
        }

        public string ToSvg() {
            return _image.CreateXml();
        }
    }
}
