namespace ScalableVectorGraphic {
    public class Color {
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

        public Color ReduceBy(uint reduction) {
            return new Color(System.Math.Max(0, (int)(RedComponent - reduction)), System.Math.Max(0, (int)(GreenComponent - reduction)), System.Math.Max(0, (int)(BlueComponent - reduction)));
        }

        public Color IncreaseBy(uint increase) {
            return new Color(System.Math.Min(255, (int)(RedComponent + increase)), System.Math.Min(255, (int)(GreenComponent + increase)), System.Math.Min(255, (int)(BlueComponent + increase)));
        }

        public static Color White => new Color(255, 255, 255);
        public static Color Black => new Color(0, 0, 0);
        public static Color Red => new Color(255, 0, 0);
        public static Color Green => new Color(0, 255, 0);
        public static Color Blue => new Color(0, 0, 255);
    }
}
