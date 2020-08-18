using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic {
    public class DataSeriesBar<X, Y> {
        private readonly List<DataPoint<X, Y>> _dataPoints;

        public DataSeriesBar(IReadOnlyList<DataPoint<X, Y>> dataPoints) {
            _dataPoints = dataPoints.ToList();
        }

        public IReadOnlyList<DataPoint<X, Y>> DataPoints => _dataPoints;

        public double FindMaximumValueAsDouble(IGenericNumericOperations<Y> numericOperations) {
            var allValues = _dataPoints.Select(dataPoint => numericOperations.ConvertToDoubleEquivalent(dataPoint.YValue)).ToList();
            return allValues.Max();
        }
    }
}
