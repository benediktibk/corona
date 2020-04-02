namespace ScalableVectorGraphic
{
    public class LogarithmicAxis<T> : AxisBase<T>
    {
        public LogarithmicAxis(IGenericNumericOperations<T> numericOperations, string label) :
            base(numericOperations, label) {
        }
        
        public override IAxisTransformation CreateAxisTransformation(double minimumValue, double maximumValue) {
            return new LogarithmicAxisTransformation(minimumValue, maximumValue);
        }
    }
}
