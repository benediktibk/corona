namespace ScalableVectorGraphic
{
    public class SvgXmlWriterCompressed : SvgXmlWriterBase
    {
        public SvgXmlWriterCompressed(int height, int width) :
            base(height, width) {
        }

        protected override string GetDescription(string description) {
            return string.Empty;
        }

        protected override string GetLineFeed() {
            return string.Empty;
        }
    }
}
