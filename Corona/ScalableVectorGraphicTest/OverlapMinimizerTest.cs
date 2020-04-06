using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScalableVectorGraphic;
using System.Collections.Generic;

namespace ScalableVectorGraphicTest
{
    [TestClass]
    public class OverlapMinimizerTest
    {
        private const double _boxHeight = 0.05;
        private const double _boxWidth = 0.07;
        private Rectangle _rectangle;
        private List<Point> _points;

        [TestInitialize]
        public void Setup() {
            _rectangle = new Rectangle("blub", new Point(0, _boxHeight), new Point(_boxWidth, 0), Color.Black, Color.Black, 0.001);
            _points = new List<Point>();
        }

        [TestMethod]
        public void PlaceRectangleOverPoints_NoPointsOnlyBorders_Middle() {
            var result = OverlapMinimizer.PlaceRectangleOverPoints(_rectangle, _points);

            result.X.Should().BeApproximately(0.5 - _boxWidth / 2, 1e-5);
            result.Y.Should().BeApproximately(0.5 - _boxHeight / 2, 1e-5);
        }

        [TestMethod]
        public void PlaceRectangleOverPoints_OnePointInTheMiddle_NotTheMiddle() {
            var result = OverlapMinimizer.PlaceRectangleOverPoints(_rectangle, _points);

            var distance = (result.ToVector() - new Math.Vector(0.5, 0.5)).Norm;
            distance.Should().BeGreaterThan(0.1);
        }
    }
}
