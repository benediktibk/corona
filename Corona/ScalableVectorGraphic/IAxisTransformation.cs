namespace ScalableVectorGraphic
{
    public interface IAxisTransformation
    {
        double ScalingFactor { get; }

        double Apply(double value);
    }
}
