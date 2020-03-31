using System;

namespace ScalableVectorGraphic
{
    public class NumericOperationsDateTimeForDatesOnly : IGenericNumericOperations<DateTime>
    {
        private const string _labelFormat = "dd.MM.yyyy";

        public NumericOperationsDateTimeForDatesOnly(DateTime reference) {
            Reference = reference;
        }

        public DateTime Reference { get; }

        public string CreateLabel(double value) {
            var convertedValue = Reference.Add(TimeSpan.FromDays(value));
            return convertedValue.ToString(_labelFormat);
        }

        public double ConvertToDoubleEquivalent(DateTime value) {
            return value.Subtract(Reference).TotalDays;
        }
    }
}
