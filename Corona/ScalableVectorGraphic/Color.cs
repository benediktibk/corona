﻿namespace ScalableVectorGraphic
{
    public class Color
    {
        public Color(int red, int green, int blue) {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }

        public string ToSvg() {
            return $"rgb({Red},{Green},{Blue})";
        }
    }
}
