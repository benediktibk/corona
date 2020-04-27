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
        private const double _boxMiddlePositionLeftLowerX = 0.5 - _boxWidth / 2;
        private const double _boxMiddlePositionLeftLowerY = 0.5 - _boxHeight / 2;
        private Rectangle _rectangle;
        private List<Point> _points;
        private OverlapMinimizer _overlapMinimzer;

        [TestInitialize]
        public void Setup() {
            _rectangle = new Rectangle("blub", new Point(0, _boxHeight), new Point(_boxWidth, 0), Color.Black, Color.Black, 0.001);
            _points = new List<Point>();
            _overlapMinimzer = new OverlapMinimizer();
        }

        [TestMethod]
        public void PlaceRectangleOverPoints_NoPointsOnlyBorders_Middle() {
            var result = _overlapMinimzer.PlaceRectangleOverPoints(_rectangle, _points);

            result.X.Should().BeApproximately(_boxMiddlePositionLeftLowerX, 1e-5);
            result.Y.Should().BeApproximately(_boxMiddlePositionLeftLowerY, 1e-5);
        }

        [TestMethod]
        public void PlaceRectangleOverPoints_NoPointsOnlyBordersAndOnlyOneSpringPerBorder_Middle() {
            _overlapMinimzer = new OverlapMinimizer(1);

            var result = _overlapMinimzer.PlaceRectangleOverPoints(_rectangle, _points);

            result.X.Should().BeApproximately(_boxMiddlePositionLeftLowerX, 1e-5);
            result.Y.Should().BeApproximately(_boxMiddlePositionLeftLowerY, 1e-5);
        }

        [TestMethod]
        public void PlaceRectangleOverPoints_NoPointsOnlyBordersAndOnlyTwoSpringsPerBorder_Middle() {
            _overlapMinimzer = new OverlapMinimizer(2);

            var result = _overlapMinimzer.PlaceRectangleOverPoints(_rectangle, _points);

            result.X.Should().BeApproximately(_boxMiddlePositionLeftLowerX, 1e-5);
            result.Y.Should().BeApproximately(_boxMiddlePositionLeftLowerY, 1e-5);
        }

        [TestMethod]
        public void PlaceRectangleOverPoints_NoPointsOnlyBordersAndOnlyFourSpringsPerBorder_Middle() {
            _overlapMinimzer = new OverlapMinimizer(4);

            var result = _overlapMinimzer.PlaceRectangleOverPoints(_rectangle, _points);

            result.X.Should().BeApproximately(_boxMiddlePositionLeftLowerX, 1e-5);
            result.Y.Should().BeApproximately(_boxMiddlePositionLeftLowerY, 1e-5);
        }

        [TestMethod]
        public void PlaceRectangleOverPoints_OnePointInTheMiddle_NotTheMiddle() {
            _points.Add(new Point(0.51, 0.5));

            var result = _overlapMinimzer.PlaceRectangleOverPoints(_rectangle, _points);

            var distance = (result.ToVector() - new Math.Vector(_boxMiddlePositionLeftLowerX, _boxMiddlePositionLeftLowerY)).Norm;
            distance.Should().BeGreaterThan(0.1);
        }
    }
}
