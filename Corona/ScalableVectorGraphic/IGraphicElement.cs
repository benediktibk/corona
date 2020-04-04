namespace ScalableVectorGraphic
{
    public interface IGraphicElement
    {
        string Description { get; }

        void AddTo(ISvgXmlWriter svgXmlWriter);
        IGraphicElement ApplyTransformation(Transformation transformation);
    }
}
