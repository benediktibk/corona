using Math;
using ScalableVectorGraphic;
using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class Transformation
    {
        private readonly Matrix _matrix;
        private readonly Vector _offset;
        private readonly double _widthScaling;

        public Transformation(Matrix matrix, Vector offset) {
            _matrix = matrix;
            _offset = offset;
            _widthScaling = System.Math.Sqrt(System.Math.Abs(_matrix.Determinant));
        }

        public List<IGraphicElement> Apply(List<IGraphicElement> elements) {
            var result = new List<IGraphicElement>();

            foreach (var element in elements) {
                result.Add(element.ApplyTransformation(this));
            }

            return result;
        }

        public Point ApplyToPoint(Point point) {
            return new Point(_matrix * point.ToVector() + _offset);
        }

        public double ApplyToWidth(double value) {
            return value * _widthScaling;
        }
    }
}
