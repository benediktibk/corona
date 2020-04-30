using System.Globalization;
using System.Text;

namespace ScalableVectorGraphic {
    public abstract class SvgXmlWriterBase : ISvgXmlWriter {
        private readonly StringBuilder _content;
        private readonly StringBuilder _contentWithClosingTag;

        public CultureInfo Culture { get; }

        public SvgXmlWriterBase(int height, int width) {
            _content = new StringBuilder();
            _contentWithClosingTag = new StringBuilder();
            Culture = CultureInfo.CreateSpecificCulture("en-US");

            _content.Append("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"no\"?>");
            _content.Append(GetLineFeed());
            _content.Append($"<svg height=\"{height}\" width=\"{width}\" xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\">");
            _content.Append(GetLineFeed());
        }

        protected abstract string GetLineFeed();
        protected abstract string GetDescription(string description);

        public void Add(IGraphicElement element) {
            _content.Append(GetDescription(element.Description));
            _content.Append(GetLineFeed());
            element.AddTo(this);
        }

        public void AddSingleTag(string tag, string attributes) {
            _content.Append("<");
            _content.Append(tag);
            _content.Append(" ");
            _content.Append(attributes);
            _content.Append(" />");
            _content.Append(GetLineFeed());
        }

        public void AddTagWithContent(string tag, string attributes, string content) {
            _content.Append("<");
            _content.Append(tag);
            _content.Append(" ");
            _content.Append(attributes);
            _content.Append(">");
            _content.Append(content);
            _content.Append("</");
            _content.Append(tag);
            _content.Append(">");
            _content.Append(GetLineFeed());
        }

        public string GetXmlString() {
            _contentWithClosingTag.Clear();
            _contentWithClosingTag.Append(_content.ToString());
            _contentWithClosingTag.Append($"</svg>");
            return _contentWithClosingTag.ToString();
        }
    }
}
