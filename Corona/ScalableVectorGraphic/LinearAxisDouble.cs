namespace ScalableVectorGraphic {
    public class LinearAxisDouble : LinearAxis<double> {
        private readonly string _labelFormat;

        public LinearAxisDouble(IGenericNumericOperations<double> numericOperations, string label, string labelFormat) :
            base(numericOperations, label) {
            _labelFormat = labelFormat;
        }

        public override string CreateLabel(double value) {
            return value.ToString(_labelFormat);
        }
    }
}
