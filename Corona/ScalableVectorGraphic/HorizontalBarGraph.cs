using Math;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScalableVectorGraphic {
    public class HorizontalBarGraph {
        private readonly Image _image;
        private const double _ratioXAxisLengthToImageSize = 0.85;
        private const double _yAxisOffsetForLabels = 0.05;

        public HorizontalBarGraph(int width, int height, IAxis<double> horizontalAxis, List<DataPoint<string, double>> values, Point legendPosition) {
            var minimumValue = FindDataSeriesMinimum(values);
            var maximumValue = FindDataSeriesMaximum(values);
            var elements = CreateGraphicElements(horizontalAxis, values, legendPosition);
            elements = TransformElements(width, height, elements);
            _image = new Image(width, height, elements);
        }

        private List<IGraphicElement> TransformElements(int width, int height, List<IGraphicElement> elements) {
            var originOffset = new Vector((1 - _ratioXAxisLengthToImageSize) * 0.7 * width, ((1 - _ratioYAxisLengthToImageSize) / 2 + _yAxisOffsetForLabels) * height * 0.7);
            var transformGraphToImageSize = new Transformation(new Matrix(_ratioXAxisLengthToImageSize * width, _ratioYAxisLengthToImageSize * height), originOffset);
            elements = transformGraphToImageSize.Apply(elements);
            var transformImageToSvgCoordinates = new Transformation(new Matrix(1, -1), new Vector(0, height));
            elements = transformImageToSvgCoordinates.Apply(elements);
            return elements;
        }

        private List<IGraphicElement> CreateGraphicElements(IAxis<double> horizontalAxis, List<DataPoint<string, double>> values, Point legendPosition) {
            throw new NotImplementedException();
        }

        private double FindDataSeriesMinimum(List<DataPoint<string, double>> values) {
            return values.Select(x => x.YValue).Min();
        }

        private double FindDataSeriesMaximum(List<DataPoint<string, double>> values) {
            return values.Select(x => x.YValue).Max();
        }
    }
}
