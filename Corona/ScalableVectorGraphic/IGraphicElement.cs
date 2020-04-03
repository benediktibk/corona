namespace ScalableVectorGraphic
{
    public interface IGraphicElement
    {
        string Description { get; }

        void AddTo(SvgXmlWriterBase svgXmlWriter);
        IGraphicElement ApplyTransformation(Transformation transformation);
    }
}
