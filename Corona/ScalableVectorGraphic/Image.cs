using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic
{
    public class Image
    {
        private readonly List<IGraphicElement> _elements;

        public Image(int width, int height, IReadOnlyList<IGraphicElement> elements) {
            Width = width;
            Height = height;
            _elements = elements.ToList();
        }

        public int Width { get; }
        public int Height { get; }
        public IReadOnlyList<IGraphicElement> Elements => _elements;

        public string CreateXml(bool compressed) {
            var svgXmlWriter = new SvgXmlWriterDetailed(Height, Width);

            foreach (var element in _elements) {
                svgXmlWriter.Add(element);
            }

            return svgXmlWriter.GetXmlString();
        }
       
    }
}
