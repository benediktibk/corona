namespace ScalableVectorGraphic
{
    public class LinearAxisTransformation : IAxisTransformation
    {
        private readonly double _scale;
        private readonly double _offset;

        public LinearAxisTransformation(double minimum, double maximum) {
            _scale = 1.0 / (maximum - minimum);
            _offset = (-1.0) * minimum * _scale;
        }

        public double Scale(double value) {
            return value * _scale + _offset;
        }
    }
}
