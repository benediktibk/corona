using System.Text;

namespace ScalableVectorGraphic
{
    public interface IGraphicElement
    {
        string Description { get; }

        void AppendXmlTo(StringBuilder stringBuilder);
        IGraphicElement ApplyTransformation(Transformation transformation);
    }
}
