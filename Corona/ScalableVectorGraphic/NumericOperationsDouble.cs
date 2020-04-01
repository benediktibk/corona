namespace ScalableVectorGraphic
{
    public class NumericOperationsDouble : IGenericNumericOperations<double>
    {
        public string CreateLabel(double value) {
            return value.ToString("F0");
        }

        public double ConvertToDoubleEquivalent(double value) {
            return value;
        }
    }
}
