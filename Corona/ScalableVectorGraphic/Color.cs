namespace ScalableVectorGraphic
{
    public class Color
    {
        public Color(int red, int green, int blue) {
            RedComponent = red;
            GreenComponent = green;
            BlueComponent = blue;
        }

        public int RedComponent { get; }
        public int GreenComponent { get; }
        public int BlueComponent { get; }

        public string ToSvg() {
            return $"rgb({RedComponent},{GreenComponent},{BlueComponent})";
        }

        public static Color White => new Color(255, 255, 255);
        public static Color Black => new Color(0, 0, 0);
        public static Color Red => new Color(255, 0, 0);
        public static Color Green => new Color(0, 255, 0);
        public static Color Blue => new Color(0, 0, 255);
    }
}
