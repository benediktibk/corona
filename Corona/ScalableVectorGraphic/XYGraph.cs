using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic
{
    public class XYGraph<X, Y>
    {
        private readonly Image _image;
        private const double _ratioAxisLengthToImageSize = 0.95;

        public XYGraph(int width, int height, IAxis<X> xAxis, IAxis<Y> yAxis, IReadOnlyList<DataPoint<X, Y>> dataPoints, IGenericNumericOperations<X> xAxisNumericOperations, IGenericNumericOperations<Y> yAxisNumericOperations, X tickMarkDistanceXAxis) {
            var elements = new List<IGraphicElement>();
            var allXValues = dataPoints.Select(dataPoint => dataPoint.XValue).ToList();
            var allYValues = dataPoints.Select(dataPoint => dataPoint.YValue).ToList();

            var elementsXAxis = xAxis.CreateGraphicElements(xAxisNumericOperations.FindSmallest(allXValues), xAxisNumericOperations.FindBiggest(allXValues), tickMarkDistanceXAxis);
            var transformXToGraph = new Transformation(new Matrix(new Vector(_ratioAxisLengthToImageSize * width, 0), new Vector(0, _ratioAxisLengthToImageSize * height)), new Vector(0, (1 - _ratioAxisLengthToImageSize)/2*height));
            elementsXAxis = transformXToGraph.Apply(elementsXAxis);
            elements.AddRange(elementsXAxis);

            var transformGraphToImage = new Transformation(new Matrix(new Vector(1, 0), new Vector(0, -1)), new Vector(0, height));
            _image = new Image(width, height, elements, transformGraphToImage);
        }

        public string ToSvg() {
            return _image.CreateXml();
        }
    }
}
