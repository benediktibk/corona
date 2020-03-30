using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public string CreateXml() {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"<svg height=\"{Height}\" width=\"{Width}\">");

            foreach (var element in _elements) {
                element.AppendXmlTo(stringBuilder);
            }

            stringBuilder.Append($"</svg>");
            return stringBuilder.ToString();
        }
       
    }
}
