using System;

namespace ScalableVectorGraphic {
    public static class TextAnchorHelper {
        public static string ToSvg(this TextAnchor textAnchor) {
            switch (textAnchor) {
                case TextAnchor.Start:
                    return "start";
                case TextAnchor.Middle:
                    return "middle";
                case TextAnchor.End:
                    return "end";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
