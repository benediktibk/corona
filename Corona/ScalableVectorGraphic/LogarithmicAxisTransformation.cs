namespace ScalableVectorGraphic
{
    public class LogarithmicAxisTransformation : IAxisTransformation
    {
        private readonly double _offset;

        public LogarithmicAxisTransformation(double minimum, double maximum) {
            AxisStartValue = System.Math.Pow(10, System.Math.Floor(System.Math.Log10(minimum)));
            AxisEndValue = System.Math.Pow(10, System.Math.Ceiling(System.Math.Log10(maximum)));
            ScalingFactor = 1.0 / (System.Math.Log10(AxisEndValue) - System.Math.Log10(AxisStartValue));
            _offset = (-1.0) * System.Math.Log10(AxisStartValue) * ScalingFactor;
        }

        public double ScalingFactor { get; }
        public double AxisStartValue { get; }
        public double AxisEndValue { get; }

        public double Apply(double value) {
            return System.Math.Log10(value) * ScalingFactor + _offset;
        }

        public double ApplyToLineWidth(double value) {
            return value;
        }

        public double CalculateNextTick(double value) {
            return value * 10;
        }
    }
}
