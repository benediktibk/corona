using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic
{
    public class DataSeries<X, Y>
    {
        private readonly List<DataPoint<X, Y>> _dataPoints;
        private readonly Color _color;
        private const double _radius = 0.1;
        private const double _lineWidth = 0.05;

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
            var dataPointsConvertedAndOrderd = dataPointsConverted.OrderBy(dataPoint => dataPoint.X).ToList();

            foreach (var dataPoint in dataPointsConvertedAndOrderd) {
                result.Add(new Circle($"data point ({dataPoint.X},{dataPoint.Y})", _radius, _color, dataPoint));
            }

            for (var i = 1; i < dataPointsConvertedAndOrderd.Count(); ++i) {
                var previous = dataPointsConvertedAndOrderd[i - 1];
                var current = dataPointsConvertedAndOrderd[i];

                result.Add(new Line($"data point connection from ({previous.X},{previous.Y}) to ({current.X},{current.Y})", previous, current, _color, _lineWidth));
            }

            return result;
        }
    }
}
