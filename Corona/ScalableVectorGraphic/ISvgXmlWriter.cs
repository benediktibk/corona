using System.Globalization;

namespace ScalableVectorGraphic
{
    public interface ISvgXmlWriter
    {
        CultureInfo Culture { get; }

        void AddSingleTag(string tag, string attributes);
        void AddTagWithContent(string tag, string attributes, string content);
    }
}