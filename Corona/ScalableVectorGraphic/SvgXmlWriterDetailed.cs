namespace ScalableVectorGraphic
{
    public class SvgXmlWriterDetailed : SvgXmlWriterBase
    {
        public SvgXmlWriterDetailed(int height, int width) :
            base(height, width) {
        }

        protected override string GetDescription(string description) {
            return $"<!-- {description} -->";
        }

        protected override string GetLineFeed() {
            return System.Environment.NewLine;
        }
    }
}
