namespace ScalableVectorGraphic
{
    public class LinearAxisTransformation : IAxisTransformation
    {
        private readonly double _offset;

        public LinearAxisTransformation(double minimum, double maximum) {
            ScalingFactor = 1.0 / (maximum - minimum);
            _offset = (-1.0) * minimum * ScalingFactor;
        }

        public double ScalingFactor { get; }

        public double Apply(double value) {
            return value * ScalingFactor + _offset;
        }
    }
}
