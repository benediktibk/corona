using System;

namespace ScalableVectorGraphic
{
    public class NumericOperationsDateTimeForDatesOnly : IGenericNumericOperations<DateTime>
    {
        public NumericOperationsDateTimeForDatesOnly(DateTime reference) {
            Reference = reference;
        }

        public DateTime Reference { get; }

        public DateTime ConvertFromDoubleEquivalent(double value) {
            return Reference.AddDays(value);
        }

        public double ConvertToDoubleEquivalent(DateTime value) {
            return value.Subtract(Reference).TotalDays;
        }
    }
}
