using System.Diagnostics;

namespace ScalableVectorGraphic {
    [DebuggerDisplay("DataPoint ({XValue}, {YValue})")]
    public class DataPoint<X, Y> {
        public DataPoint(X x, Y y) {
            XValue = x;
            YValue = y;
        }

        public X XValue { get; }
        public Y YValue { get; }

    }
}
