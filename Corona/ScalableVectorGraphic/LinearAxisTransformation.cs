namespace ScalableVectorGraphic
{
    public class LinearAxisTransformation : IAxisTransformation
    {
        private readonly double _offset;

        public LinearAxisTransformation(double minimum, double maximum) {
            AxisStartValue = minimum;
            AxisEndValue = maximum;
            ScalingFactor = 1.0 / (AxisEndValue - AxisStartValue);
            _offset = (-1.0) * AxisStartValue * ScalingFactor;
        }

        public double ScalingFactor { get; }
        public double AxisStartValue { get; }
        public double AxisEndValue { get; }

        public double Apply(double value) {
            return value * ScalingFactor + _offset;
        }
    }
}
