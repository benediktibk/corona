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

        public void FindRangeOfXValues(IGenericNumericOperations<X> numericOperations, out X minimum, out X maximum) {
            var allValues = _dataPoints.Select(dataPoint => dataPoint.XValue).ToList();
            minimum = numericOperations.FindSmallest(allValues);
            maximum = numericOperations.FindBiggest(allValues);
        }

        public void FindRangeOfYValues(IGenericNumericOperations<Y> numericOperations, out Y minimum, out Y maximum) {
            var allValues = _dataPoints.Select(dataPoint => dataPoint.YValue).ToList();
            minimum = numericOperations.FindSmallest(allValues);
            maximum = numericOperations.FindBiggest(allValues);
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
