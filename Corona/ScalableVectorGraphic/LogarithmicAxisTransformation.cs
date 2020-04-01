using System;

namespace ScalableVectorGraphic
{
    public class LogarithmicAxisTransformation : IAxisTransformation
    {
        private readonly double _offset;

        public LogarithmicAxisTransformation(double minimum, double maximum) {
            AxisStartValue = Math.Pow(10, Math.Floor(Math.Log10(minimum)));
            AxisEndValue = Math.Pow(10, Math.Ceiling(Math.Log10(maximum)));
            ScalingFactor = 1.0 / (Math.Log10(AxisEndValue) - Math.Log10(AxisStartValue));
            _offset = (-1.0) * Math.Log10(AxisStartValue) * ScalingFactor;
        }

        public double ScalingFactor { get; }
        public double AxisStartValue { get; }
        public double AxisEndValue { get; }

        public double Apply(double value) {
            return Math.Log10(value) * ScalingFactor + _offset;
        }

        public double CalculateNextTick(double value) {
            return value * 10;
        }
    }
}
