using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTest {
    [TestClass]
    public class PointExponentialDistancePenaltyFunctionTest {
        private PointExponentialDistancePenaltyFunction _penaltyFunction;

        [TestInitialize]
        public void Setup() {
            _penaltyFunction = new PointExponentialDistancePenaltyFunction(new Vector(4, 5), 3, 10);
        }

        [TestMethod]
        public void CalculateValue_AtOwnPosition_MaxValue() {
            var result = _penaltyFunction.CalculateValue(new Vector(4, 5));

            result.Should().BeApproximately(10, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_FarAway_AlmostZero() {
            var result = _penaltyFunction.CalculateValue(new Vector(4e10, 5));

            result.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_Nearby_CorrectValue() {
            var result = _penaltyFunction.CalculateValue(new Vector(5, 7));

            result.Should().BeApproximately(0.857283452480479652081247523203359410366624541725080390132, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_Nearby_CorrectValue() {
            var result = _penaltyFunction.CalculateGradient(new Vector(5, 7));

            result.X.Should().BeApproximately(-0.42119566365775522928882632958020862382656631894011144028, 1e-5);
            result.Y.Should().BeApproximately(-0.84239132731551045857765265916041724765313263788022288057, 1e-5);
        }
    }
}
