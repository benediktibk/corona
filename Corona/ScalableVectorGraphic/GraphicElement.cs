using System.Text;

namespace ScalableVectorGraphic
{
    public interface IGraphicElement
    {
        void AppendXmlTo(StringBuilder stringBuilder);
    }
}
