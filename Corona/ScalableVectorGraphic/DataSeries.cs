using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic {
    public class DataSeries<X, Y> {
        private const double _radius = 0.005;
        private const double _lineWidth = 0.002;
        private readonly List<DataPoint<X, Y>> _dataPoints;
        private readonly bool _connectDots;

        public DataSeries(IReadOnlyList<DataPoint<X, Y>> dataPoints, Color color, bool connectDots, string label) {
            _dataPoints = dataPoints.ToList();
            Color = color;
            _connectDots = connectDots;
            Label = label;
        }

        public Color Color { get; }
        public string Label { get; }

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

        public List<IGraphicElement> CreateGraphicElements(IGenericNumericOperations<X> numericOperationsX, IGenericNumericOperations<Y> numericOperationsY, IAxisTransformation xAxisTransformation, IAxisTransformation yAxisTransformation, out List<Point> dataPoints) {
            var result = new List<IGraphicElement>();

            var dataPointsConverted = _dataPoints.Select(dataPoint => new Point(numericOperationsX.ConvertToDoubleEquivalent(dataPoint.XValue), numericOperationsY.ConvertToDoubleEquivalent(dataPoint.YValue)));
            var dataPointsScaled = dataPointsConverted.Select(dataPoint => dataPoint.Apply(xAxisTransformation, yAxisTransformation));
            var dataPointsConvertedAndOrderd = dataPointsScaled.OrderBy(dataPoint => dataPoint.X).ToList();

            for (var i = 0; i < dataPointsConvertedAndOrderd.Count(); ++i) {
                result.Add(new Circle($"data point ({_dataPoints[i].XValue},{_dataPoints[i].YValue})", _radius, Color, dataPointsConvertedAndOrderd[i]));
            }

            dataPoints = dataPointsConvertedAndOrderd;

            if (!_connectDots) {
                return result;
            }

            for (var i = 1; i < dataPointsConvertedAndOrderd.Count(); ++i) {
                var previous = dataPointsConvertedAndOrderd[i - 1];
                var current = dataPointsConvertedAndOrderd[i];

                result.Add(new Line($"data point connection from ({_dataPoints[i - 1].XValue},{_dataPoints[i - 1].YValue}) to ({_dataPoints[i].XValue},{_dataPoints[i].YValue})", previous, current, Color, _lineWidth));
            }

            return result;
        }
    }
}
