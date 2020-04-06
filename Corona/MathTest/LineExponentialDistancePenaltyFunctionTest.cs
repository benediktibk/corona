using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTest
{
    [TestClass]
    public class LineExponentialDistancePenaltyFunctionTest
    {
        [TestMethod]
        public void CalculateValue_HorizontalLineThroughPointComparison_SameValueAsPoint() {
            var point = new PointExponentialDistancePenaltyFunction(new Vector(4, 5), 3, 10);
            var line = new LineExponentialDistancePenaltyFunction(new Vector(3, 5), new Vector(10, 0), 3, 10, false, false);

            var result = line.CalculateValue(new Vector(4, 2));

            var resultShouldBe = point.CalculateValue(new Vector(4, 2));
            result.Should().BeApproximately(resultShouldBe, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_VerticalLineThroughPointComparison_SameValueAsPoint() {
            var point = new PointExponentialDistancePenaltyFunction(new Vector(4, 5), 3, 10);
            var line = new LineExponentialDistancePenaltyFunction(new Vector(4, -5), new Vector(0, 2), 3, 10, false, false);

            var result = line.CalculateValue(new Vector(2, 5));

            var resultShouldBe = point.CalculateValue(new Vector(2, 5));
            result.Should().BeApproximately(resultShouldBe, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_HorizontalLineThroughPointComparison_SameValueAsPoint() {
            var point = new PointExponentialDistancePenaltyFunction(new Vector(4, 5), 3, 10);
            var line = new LineExponentialDistancePenaltyFunction(new Vector(3, 5), new Vector(10, 0), 3, 10, false, false);

            var result = line.CalculateGradient(new Vector(4, 2));

            var resultShouldBe = point.CalculateGradient(new Vector(4, 2));
            result.X.Should().BeApproximately(resultShouldBe.X, 1e-5);
            result.Y.Should().BeApproximately(resultShouldBe.Y, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_VerticalLineThroughPointComparison_SameValueAsPoint() {
            var point = new PointExponentialDistancePenaltyFunction(new Vector(4, 5), 3, 10);
            var line = new LineExponentialDistancePenaltyFunction(new Vector(4, -5), new Vector(0, 2), 3, 10, false, false);

            var result = line.CalculateGradient(new Vector(2, 5));

            var resultShouldBe = point.CalculateGradient(new Vector(2, 5));
            result.X.Should().BeApproximately(resultShouldBe.X, 1e-5);
            result.Y.Should().BeApproximately(resultShouldBe.Y, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_VerticalLineWithLeftMaxAndPointLeft_MaxValue() {
            var line = new LineExponentialDistancePenaltyFunction(new Vector(4, -5), new Vector(0, 2), 3, 10, true, false);

            var result = line.CalculateValue(new Vector(2, 5));

            result.Should().BeApproximately(10, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_VerticalLineWithLeftMaxAndPointRight_SameValueAsPoint() {
            var point = new PointExponentialDistancePenaltyFunction(new Vector(4, 5), 3, 10);
            var line = new LineExponentialDistancePenaltyFunction(new Vector(4, -5), new Vector(0, 2), 3, 10, true, false);

            var result = line.CalculateValue(new Vector(7, 5));

            var resultShouldBe = point.CalculateValue(new Vector(7, 5));
            result.Should().BeApproximately(resultShouldBe, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_VerticalLineWithRightMaxAndPointRight_MaxValue() {
            var line = new LineExponentialDistancePenaltyFunction(new Vector(4, -5), new Vector(0, 2), 3, 10, false, true);

            var result = line.CalculateValue(new Vector(7, 5));

            result.Should().BeApproximately(10, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_VerticalLineWithRightMaxAndPointLeft_SameValueAsPoint() {
            var point = new PointExponentialDistancePenaltyFunction(new Vector(4, 5), 3, 10);
            var line = new LineExponentialDistancePenaltyFunction(new Vector(4, -5), new Vector(0, 2), 3, 10, false, true);

            var result = line.CalculateValue(new Vector(2, 5));

            var resultShouldBe = point.CalculateValue(new Vector(2, 5));
            result.Should().BeApproximately(resultShouldBe, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_VerticalLineWithLeftMaxAndPointLeft_MaxValue() {
            var line = new LineExponentialDistancePenaltyFunction(new Vector(4, -5), new Vector(0, 2), 3, 10, true, false);

            var result = line.CalculateGradient(new Vector(2, 5));

            result.X.Should().BeApproximately(-2, 1e-5);
            result.Y.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_VerticalLineWithLeftMaxAndPointRight_SameValueAsPoint() {
            var point = new PointExponentialDistancePenaltyFunction(new Vector(4, 5), 3, 10);
            var line = new LineExponentialDistancePenaltyFunction(new Vector(4, -5), new Vector(0, 2), 3, 10, true, false);

            var result = line.CalculateGradient(new Vector(7, 5));

            var resultShouldBe = point.CalculateGradient(new Vector(7, 5));
            result.X.Should().BeApproximately(resultShouldBe.X, 1e-5);
            result.Y.Should().BeApproximately(resultShouldBe.Y, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_VerticalLineWithRightMaxAndPointRight_MaxValue() {
            var line = new LineExponentialDistancePenaltyFunction(new Vector(4, -5), new Vector(0, 2), 3, 10, false, true);

            var result = line.CalculateGradient(new Vector(7, 5));

            result.X.Should().BeApproximately(3, 1e-5);
            result.Y.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_VerticalLineWithRightMaxAndPointLeft_SameValueAsPoint() {
            var point = new PointExponentialDistancePenaltyFunction(new Vector(4, 5), 3, 10);
            var line = new LineExponentialDistancePenaltyFunction(new Vector(4, -5), new Vector(0, 2), 3, 10, false, true);

            var result = line.CalculateGradient(new Vector(2, 5));

            var resultShouldBe = point.CalculateGradient(new Vector(2, 5));
            result.X.Should().BeApproximately(resultShouldBe.X, 1e-5);
            result.Y.Should().BeApproximately(resultShouldBe.Y, 1e-5);
        }
    }
}
