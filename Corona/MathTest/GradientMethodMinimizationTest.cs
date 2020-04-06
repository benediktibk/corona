using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MathTest
{
    [TestClass]
    public class GradientMethodMinimizationTest
    {
        [TestMethod]
        public void Minimize_PointPenaltyFunctionAndStartNearbyPoint_ResultIsFarAwayFromPoint() {
            var penaltyPoint = new Vector(3, 5);
            var penaltyFunction = new PointExponentialDistancePenaltyFunction(penaltyPoint, 5, 1e10);

            var result = GradientMethodMinimization.Minimize(penaltyPoint + new Vector(0.1, 0), penaltyFunction, 10, 1e-5);

            var distance = (result - penaltyPoint).Norm;
            distance.Should().BeGreaterThan(100);
        }

        [TestMethod]
        public void Minimize_PointPenaltyFunctionAndStartOnlyChangeInX_ResultHasStillTheSameYCoordinate() {
            var penaltyPoint = new Vector(3, 5);
            var penaltyFunction = new PointExponentialDistancePenaltyFunction(penaltyPoint, 5, 1e10);

            var result = GradientMethodMinimization.Minimize(penaltyPoint + new Vector(0.1, 0), penaltyFunction, 10, 1e-5);

            result.Y.Should().BeApproximately(penaltyPoint.Y, 1e-5);
        }

        [TestMethod]
        public void Minimize_PointPenaltyFunctionAndStartOnlyChangeInY_ResultHasStillTheSameXCoordinate() {
            var penaltyPoint = new Vector(3, 5);
            var penaltyFunction = new PointExponentialDistancePenaltyFunction(penaltyPoint, 5, 1e10);

            var result = GradientMethodMinimization.Minimize(penaltyPoint + new Vector(0, 0.1), penaltyFunction, 10, 1e-5);

            var distance = (result - penaltyPoint).Norm;
            distance.Should().BeGreaterThan(100);
            result.X.Should().BeApproximately(penaltyPoint.X, 1e-5);
        }

        [TestMethod]
        public void Minimize_PointPenaltyFunctionAndStartChangedInXAndY_ResultHasDifferentXAndYCoordinate() {
            var penaltyPoint = new Vector(3, 5);
            var penaltyFunction = new PointExponentialDistancePenaltyFunction(penaltyPoint, 5, 1e10);

            var result = GradientMethodMinimization.Minimize(penaltyPoint + new Vector(0.01, 0.1), penaltyFunction, 10, 1e-5);

            var distance = (result - penaltyPoint).Norm;
            distance.Should().BeGreaterThan(100);
            result.X.Should().NotBeApproximately(penaltyPoint.X, 1e-5);
            result.Y.Should().NotBeApproximately(penaltyPoint.Y, 1e-5);
        }
    }
}
