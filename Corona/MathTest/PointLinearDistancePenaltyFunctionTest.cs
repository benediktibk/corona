using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTest
{
    [TestClass]
    public class PointLinearDistancePenaltyFunctionTest
    {
        [TestMethod]
        public void CalculateValue_AtOwnPosition_MaximumValue() {
            var penaltyFunction = new PointLinearDistancePenaltyFunction(new Vector(3, 6), 4, 10);

            var result = penaltyFunction.CalculateValue(new Vector(3, 6));

            result.Should().BeApproximately(10, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_AtOwnPosition_0() {
            var penaltyFunction = new PointLinearDistancePenaltyFunction(new Vector(3, 6), 4, 10);

            var result = penaltyFunction.CalculateGradient(new Vector(3, 6));

            result.Norm.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_Nearby_CorrectValue() {
            var penaltyFunction = new PointLinearDistancePenaltyFunction(new Vector(3, 6), 4, 10);

            var result = penaltyFunction.CalculateValue(new Vector(3, 7));

            result.Should().BeApproximately(6, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_NearbyInY_CorrectValue() {
            var penaltyFunction = new PointLinearDistancePenaltyFunction(new Vector(3, 6), 4, 10);

            var result = penaltyFunction.CalculateGradient(new Vector(3, 7));

            result.X.Should().BeApproximately(0, 1e-5);
            result.Y.Should().BeApproximately(-4, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_NearbyInX_CorrectValue() {
            var penaltyFunction = new PointLinearDistancePenaltyFunction(new Vector(3, 6), 4, 10);

            var result = penaltyFunction.CalculateGradient(new Vector(2, 6));

            result.X.Should().BeApproximately(4, 1e-5);
            result.Y.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_Farway_0() {
            var penaltyFunction = new PointLinearDistancePenaltyFunction(new Vector(3, 6), 4, 10);

            var result = penaltyFunction.CalculateValue(new Vector(30, 700));

            result.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_Farway_CorrectDirection() {
            var penaltyFunction = new PointLinearDistancePenaltyFunction(new Vector(3, 6), 4, 10);

            var result = penaltyFunction.CalculateGradient(new Vector(3, 700));

            result.X.Should().BeApproximately(0, 1e-5);
            result.Y.Should().BeApproximately(-4, 1e-5);
        }
    }
}
