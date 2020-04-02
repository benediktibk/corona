namespace ScalableVectorGraphic
{
    public class LinearAxis<T> : AxisBase<T>
    {
        public LinearAxis(IGenericNumericOperations<T> numericOperations, string label) :
            base(numericOperations, label) {
        }

        public override IAxisTransformation CreateAxisTransformation(double minimumValue, double maximumValue) {
            return new LinearAxisTransformation(minimumValue, maximumValue);
        }
    }
}
