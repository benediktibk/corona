namespace ScalableVectorGraphic
{
    public class NumericOperationsDouble : IGenericNumericOperations<double>
    {
        private const string _labelFormat = "F1";

        public string CreateLabel(double value) {
            return value.ToString(_labelFormat);
        }

        public double ConvertToDoubleEquivalent(double value) {
            return value;
        }
    }
}
