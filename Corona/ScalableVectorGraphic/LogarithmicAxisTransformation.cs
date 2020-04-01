using System;

namespace ScalableVectorGraphic
{
    public class LogarithmicAxisTransformation : IAxisTransformation
    {
        private readonly double _offset;

        public LogarithmicAxisTransformation(double minimum, double maximum) {
            ScalingFactor = 1.0 / (Math.Log10(maximum) - Math.Log10(minimum));
            _offset = (-1.0) * Math.Log10(minimum) * ScalingFactor;
        }

        public double ScalingFactor { get; }

        public double Apply(double value) {
            return Math.Log10(value) * ScalingFactor + _offset;
        }
    }
}
