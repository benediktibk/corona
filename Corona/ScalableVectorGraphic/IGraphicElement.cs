using System.Globalization;
using System.Text;

namespace ScalableVectorGraphic
{
    public interface IGraphicElement
    {
        string Description { get; }

        void AppendXmlTo(StringBuilder stringBuilder, CultureInfo culture);
        IGraphicElement ApplyTransformation(Transformation transformation);
    }
}
