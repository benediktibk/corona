using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic {
    public class DataSeriesBar<X, Y> {
        private readonly List<DataPoint<X, Y>> _dataPoints;

        public DataSeriesBar(IReadOnlyList<DataPoint<X, Y>> dataPoints) {
            _dataPoints = dataPoints.ToList();
        }

        public IReadOnlyList<DataPoint<X, Y>> DataPoints => _dataPoints;
    }
}
