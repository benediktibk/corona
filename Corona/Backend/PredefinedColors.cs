using ScalableVectorGraphic;
using System.Collections.Generic;
using System.Linq;

namespace Backend {
    public static class PredefinedColors {
        private static List<Color> _colors;

        static PredefinedColors() {
            _colors = new List<Color> {
                new Color(179, 0, 0),
                new Color(179, 119, 0),
                new Color(0, 128, 21),
                new Color(0, 21, 128),
                new Color(102, 102, 0),
                new Color(0, 128, 128),
                new Color(255, 85, 0),
                new Color(0, 204, 0),
                new Color(106, 77, 255),
                new Color(68, 0, 102),
            };
        }

        public static Color GetFor(int index) {
            var indexInRange = index % _colors.Count();
            return _colors[indexInRange];
        }
    }
}
