using System;

namespace ScalableVectorGraphic {
    public class LinearAxisDateTime : LinearAxis<DateTime> {
        public LinearAxisDateTime(IGenericNumericOperations<DateTime> numericOperations, string label) :
            base(numericOperations, label) {
        }

        public override string CreateLabel(double value) {
            var valueConverted = NumericOperations.ConvertFromDoubleEquivalent(value);
            return valueConverted.ToString("dd.MM.yyyy");
        }
    }
}
