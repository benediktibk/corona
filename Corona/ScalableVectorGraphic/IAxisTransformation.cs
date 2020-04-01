namespace ScalableVectorGraphic
{
    public interface IAxisTransformation
    {
        double ScalingFactor { get; }
        double AxisStartValue { get; }
        double AxisEndValue { get; }

        double Apply(double value);
        double CalculateNextTick(double value);
    }
}
