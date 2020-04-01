namespace ScalableVectorGraphic
{
    public class LogarithmicAxis<T> : AxisBase<T>
    {
        public LogarithmicAxis(IGenericNumericOperations<T> numericOperations) :
            base(numericOperations) {
        }
        
        public override IAxisTransformation CreateAxisTransformation(double minimumValue, double maximumValue) {
            return new LogarithmicAxisTransformation(minimumValue, maximumValue);
        }
    }
}
