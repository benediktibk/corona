using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic
{
    public class DataSeries<X, Y>
    {
        private readonly List<DataPoint<X, Y>> _dataPoints;
        private readonly Color _color;
        private const double _radius = 0.1;

        public DataSeries(IReadOnlyList<DataPoint<X, Y>> dataPoints, Color color) {
            _dataPoints = dataPoints.ToList();
            _color = color;
        }

        public void FindRangeOfXValuesAsDouble(IGenericNumericOperations<X> numericOperations, out double minimum, out double maximum) {
            var allValues = _dataPoints.Select(dataPoint => numericOperations.ConvertToDoubleEquivalent(dataPoint.XValue)).ToList();
            minimum = allValues.Min();
            maximum = allValues.Max();
        }

        public void FindRangeOfYValuesAsDouble(IGenericNumericOperations<Y> numericOperations, out double minimum, out double maximum) {
            var allValues = _dataPoints.Select(dataPoint => numericOperations.ConvertToDoubleEquivalent(dataPoint.YValue)).ToList();
            minimum = allValues.Min();
            maximum = allValues.Max();
        }

        public List<IGraphicElement> CreateGraphicElements(IGenericNumericOperations<X> numericOperationsX, IGenericNumericOperations<Y> numericOperationsY) {
            var result = new List<IGraphicElement>();

            var dataPointsConverted = _dataPoints.Select(dataPoint => new Point(numericOperationsX.ConvertToDoubleEquivalent(dataPoint.XValue), numericOperationsY.ConvertToDoubleEquivalent(dataPoint.YValue)));

            foreach (var dataPoint in dataPointsConverted) {
                result.Add(new Circle($"data point ({dataPoint.X},{dataPoint.Y})", _radius, _color, dataPoint));
            }

            return result;
        }
    }
}
