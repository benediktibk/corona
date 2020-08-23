using System;

namespace ScalableVectorGraphic {
    public static class DominantBaseLineHelper {
        public static string ToSvg(this DominantBaseLine dominantBaseLine) {
            switch (dominantBaseLine) {
                case DominantBaseLine.BaseLine:
                    return "baseline";
                case DominantBaseLine.Hanging:
                    return "hanging";
                case DominantBaseLine.Middle:
                    return "middle";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
