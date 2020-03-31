using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class Transformation
    {
        private readonly Matrix _matrix;
        private readonly Vector _offset;

        public Transformation(Matrix matrix, Vector offset) {
            _matrix = matrix;
            _offset = offset;
        }

        public List<IGraphicElement> Apply(List<IGraphicElement> elements) {
            var result = new List<IGraphicElement>();

            foreach (var element in elements) {
                result.Add(element.ApplyTransformation(this));
            }

            return result;
        }

        public Point Apply(Point point) {
            return new Point(_matrix * new Vector(point) + _offset);
        }
    }
}
