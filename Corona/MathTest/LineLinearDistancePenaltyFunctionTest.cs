using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTest {
    [TestClass]
    public class LineLinearDistancePenaltyFunctionTest {
        [TestMethod]
        public void CalculateValue_AtLine_MaximumValue() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, false);

            var result = penaltyFunction.CalculateValue(new Vector(5, 3));

            result.Should().BeApproximately(10, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_LeftOfLine_CorrectValue() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, false);

            var result = penaltyFunction.CalculateValue(new Vector(4, 3));

            result.Should().BeApproximately(6, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_RightOfLine_CorrectValue() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, false);

            var result = penaltyFunction.CalculateValue(new Vector(6, 3));

            result.Should().BeApproximately(6, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_LeftFarAwayOfLine_0() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, false);

            var result = penaltyFunction.CalculateValue(new Vector(-999, 3));

            result.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_RightFarAwayOfLine_0() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, false);

            var result = penaltyFunction.CalculateValue(new Vector(699, 3));

            result.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_LeftFarAwayOfLineWithMaximumValueLeft_MaximumValue() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, true, false);

            var result = penaltyFunction.CalculateValue(new Vector(-999, 3));

            result.Should().BeApproximately(10, 1e-5);
        }

        [TestMethod]
        public void CalculateValue_RightFarAwayOfLineWithMaximumValueRight_MaximumValue() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, true);

            var result = penaltyFunction.CalculateValue(new Vector(699, 3));

            result.Should().BeApproximately(10, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_AtLine_0() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, false);

            var result = penaltyFunction.CalculateGradient(new Vector(5, 3));

            result.Norm.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_AtLineAndLeftMaximum_0() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, true, false);

            var result = penaltyFunction.CalculateGradient(new Vector(5, 3));

            result.Norm.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_AtLineAndRightMaximum_0() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, true);

            var result = penaltyFunction.CalculateGradient(new Vector(5, 3));

            result.Norm.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_LeftOfLine_CorrectValue() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, false);

            var result = penaltyFunction.CalculateGradient(new Vector(4, 3));

            result.X.Should().BeApproximately(4, 1e-5);
            result.Y.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_RightOfLine_CorrectValue() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, false);

            var result = penaltyFunction.CalculateGradient(new Vector(6, 3));

            result.X.Should().BeApproximately(-4, 1e-5);
            result.Y.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_LeftFarAwayOfLine_0() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, false);

            var result = penaltyFunction.CalculateGradient(new Vector(-999, 3));

            result.Norm.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculatGradient_RightFarAwayOfLine_0() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, false);

            var result = penaltyFunction.CalculateGradient(new Vector(699, 3));

            result.Norm.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_LeftFarAwayOfLineWithMaximumValueLeft_CorrectValue() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, true, false);

            var result = penaltyFunction.CalculateGradient(new Vector(-999, 3));

            result.X.Should().BeApproximately(-1004, 1e-5);
            result.Y.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateGradient_RightFarAwayOfLineWithMaximumValueRight_CorrectValue() {
            var penaltyFunction = new LineLinearDistancePenaltyFunction(new Vector(5, 3), new Vector(0, 1), 4, 10, false, true);

            var result = penaltyFunction.CalculateGradient(new Vector(699, 3));

            result.X.Should().BeApproximately(694, 1e-5);
            result.Y.Should().BeApproximately(0, 1e-5);
        }
    }
}
