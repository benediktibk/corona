using System;
using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic
{
    public class NumericOperationsDouble : IGenericNumericOperations<double>
    {
        private const string _labelFormat = "F1";

        public bool SmallerThan(double a, double b) {
            return a < b;
        }

        public double Add(double a, double b) {
            return a + b;
        }

        public string CreateLabel(double value) {
            return value.ToString(_labelFormat);
        }

        public double ScaleBetween0And1(double minimumValue, double maximumValue, double value) {
            return (value - minimumValue) / (maximumValue - minimumValue);
        }

        public double FindSmallest(IReadOnlyList<double> values) {
            return values.Min();
        }

        public double FindBiggest(IReadOnlyList<double> values) {
            return values.Max();
        }

        public double ConvertToDoubleEquivalent(double value) {
            return value;
        }
    }
}
