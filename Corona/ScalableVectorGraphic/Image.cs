using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScalableVectorGraphic
{
    public class Image
    {
        private readonly List<IGraphicElement> _elements;

        public Image(int width, int height, IReadOnlyList<IGraphicElement> elements, Transformation transformation) {
            Width = width;
            Height = height;
            _elements = elements.ToList();
            Transformation = transformation;
        }

        public int Width { get; }
        public int Height { get; }
        public IReadOnlyList<IGraphicElement> Elements => _elements;
        public Transformation Transformation { get; }

        public string CreateXml() {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>");
            stringBuilder.Append("\n");
            stringBuilder.Append($"<svg height=\"{Height}\" width=\"{Width}\" xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\">");
            stringBuilder.Append("\n");

            foreach (var element in _elements) {
                var elementTransformed = element.ApplyTransformation(Transformation);
                elementTransformed.AppendXmlTo(stringBuilder);
                stringBuilder.Append("\n");
            }

            stringBuilder.Append($"</svg>");
            return stringBuilder.ToString();
        }
       
    }
}
