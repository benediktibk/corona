namespace ScalableVectorGraphic
{
    public class LinearAxis<T> : AxisBase<T>
    {
        public LinearAxis(IGenericNumericOperations<T> numericOperations) :
            base(numericOperations) {
        }

        public override IAxisTransformation CreateAxisTransformation(double minimumValue, double maximumValue) {
            return new LinearAxisTransformation(minimumValue, maximumValue);
        }
    }
}
