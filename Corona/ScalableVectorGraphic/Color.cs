namespace ScalableVectorGraphic {
    public class Color {
        public Color(int red, int green, int blue) :
            this(red, green, blue, 1) {
        }

        public Color(int red, int green, int blue, double alpha) {
            RedComponent = red;
            GreenComponent = green;
            BlueComponent = blue;
            AlphaComponent = alpha;
        }

        public int RedComponent { get; }
        public int GreenComponent { get; }
        public int BlueComponent { get; }
        public double AlphaComponent { get; }

        public Color ChangeAlpha(double alpha) {
            return new Color(RedComponent, GreenComponent, BlueComponent, alpha);
        }

        public string ToSvg(ISvgXmlWriter svgXmlWriter) {
            if (AlphaComponent < 1) {
                return $"rgb({RedComponent},{GreenComponent},{BlueComponent})";
            }
            else {
                return $"rgba({RedComponent},{GreenComponent},{BlueComponent},{AlphaComponent.ToString(svgXmlWriter.Culture)})";
            }
        }

        public static Color White => new Color(255, 255, 255);
        public static Color Black => new Color(0, 0, 0);
        public static Color Red => new Color(255, 0, 0);
        public static Color Green => new Color(0, 255, 0);
        public static Color Blue => new Color(0, 0, 255);
    }
}
