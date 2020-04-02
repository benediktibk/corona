namespace ScalableVectorGraphic
{
    public class LogarithmicAxis<T> : AxisBase<T>
    {
        private readonly string _labelFormat;

        public LogarithmicAxis(IGenericNumericOperations<T> numericOperations, string label, string labelFormat) :
            base(numericOperations, label) {
            _labelFormat = labelFormat;
        }
        
        public override IAxisTransformation CreateAxisTransformation(double minimumValue, double maximumValue) {
            return new LogarithmicAxisTransformation(minimumValue, maximumValue);
        }

        public override string CreateLabel(double value) {
            return value.ToString(_labelFormat);
        }
    }
}
