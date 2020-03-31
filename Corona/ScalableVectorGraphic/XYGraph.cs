using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic
{
    public class XYGraph<X, Y>
    {
        private readonly Image _image;
        private const double _ratioAxisLengthToImageSize = 0.95;

        public XYGraph(int width, int height, IAxis<X> xAxis, IAxis<Y> yAxis, IReadOnlyList<DataPoint<X, Y>> dataPoints, X tickMarkDistanceXAxis, Y tickMarkDistanceYAxis) {
            var elements = new List<IGraphicElement>();
            var allXValues = dataPoints.Select(dataPoint => dataPoint.XValue).ToList();
            var allYValues = dataPoints.Select(dataPoint => dataPoint.YValue).ToList();
            var originOffset = new Vector((1 - _ratioAxisLengthToImageSize) / 2 * width, (1 - _ratioAxisLengthToImageSize) / 2 * height);

            var elementsXAxis = xAxis.CreateGraphicElements(xAxis.NumericOperations.FindSmallest(allXValues), xAxis.NumericOperations.FindBiggest(allXValues), tickMarkDistanceXAxis);
            var transformXToGraph = new Transformation(new Matrix(new Vector(_ratioAxisLengthToImageSize * width, 0), new Vector(0, _ratioAxisLengthToImageSize * height)), originOffset);
            elementsXAxis = transformXToGraph.Apply(elementsXAxis);
            elements.AddRange(elementsXAxis);

            var elementsYAxis = yAxis.CreateGraphicElements(yAxis.NumericOperations.FindSmallest(allYValues), yAxis.NumericOperations.FindBiggest(allYValues), tickMarkDistanceYAxis);
            var transformYToGraph = new Transformation(new Matrix(new Vector(0, _ratioAxisLengthToImageSize * height), new Vector(_ratioAxisLengthToImageSize * width, 0)), originOffset);
            elementsYAxis = transformYToGraph.Apply(elementsYAxis);
            elements.AddRange(elementsYAxis);

            var transformGraphToImage = new Transformation(new Matrix(new Vector(1, 0), new Vector(0, -1)), new Vector(0, height));
            elements = transformGraphToImage.Apply(elements);
            _image = new Image(width, height, elements);
        }

        public string ToSvg() {
            return _image.CreateXml();
        }
    }
}
