namespace ScalableVectorGraphic
{
    public interface IGraphicElement
    {
        string Description { get; }

        void AddTo(SvgXmlWriter svgXmlWriter);
        IGraphicElement ApplyTransformation(Transformation transformation);
    }
}
