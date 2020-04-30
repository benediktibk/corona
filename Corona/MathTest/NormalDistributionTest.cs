
using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTest {
    [TestClass]
    public class NormalDistributionTest {
        [TestMethod]
        public void CalculateSumTo_Expectation0StandardDeviation1AndX0_05() {
            var normalDistribution = new NormalDistribution(0, 1);

            var result = normalDistribution.CalculateSumTo(0);

            result.Should().BeApproximately(0.5, 1e-5);
        }

        [TestMethod]
        public void CalculateSumTo_Expectation0StandardDeviation1AndX196_0975() {
            var normalDistribution = new NormalDistribution(0, 1);

            var result = normalDistribution.CalculateSumTo(1.96);

            result.Should().BeApproximately(0.975, 1e-5);
        }

        [TestMethod]
        public void CalculateSumTo_Expectation0StandardDeviation1AndXNegative196_0025() {
            var normalDistribution = new NormalDistribution(0, 1);

            var result = normalDistribution.CalculateSumTo(-1.96);

            result.Should().BeApproximately(0.025, 1e-5);
        }

        [TestMethod]
        public void CalculateSumBetween_Expectation0StandardDeviation1AndNegative196AndPositive196_095() {
            var normalDistribution = new NormalDistribution(0, 1);

            var result = normalDistribution.CalculateSumBetween(-1.96, 1.96);

            result.Should().BeApproximately(0.95, 1e-5);
        }

        [TestMethod]
        public void CalculateSumTo_Expectation02StandardDeviation1AndX0_04207() {
            var normalDistribution = new NormalDistribution(0.2, 1);

            var result = normalDistribution.CalculateSumTo(0);

            result.Should().BeApproximately(0.4207, 1e-4);
        }

        [TestMethod]
        public void CalculateSumTo_Expectation02StandardDeviation2AndX0_04602() {
            var normalDistribution = new NormalDistribution(0.2, 2);

            var result = normalDistribution.CalculateSumTo(0);

            result.Should().BeApproximately(0.4602, 1e-4);
        }

        [TestMethod]
        public void CalculateSumTo_Expectation0StandardDeviation2AndXNegative196_01635() {
            var normalDistribution = new NormalDistribution(0, 2);

            var result = normalDistribution.CalculateSumTo(-1.96);

            result.Should().BeApproximately(0.1635, 1e-4);
        }

        [TestMethod]
        public void CalculateSumBetween_Expectation10StandardDeviation3And21to99999_00001() {
            var normalDistribution = new NormalDistribution(10, 3);

            var result = normalDistribution.CalculateSumBetween(21, 99999);

            result.Should().BeApproximately(0.0001, 1e-4);
        }

        [TestMethod]
        public void CalculateSumBetween_Expectation10StandardDeviation3And10to11_01306() {
            var normalDistribution = new NormalDistribution(10, 3);

            var result = normalDistribution.CalculateSumBetween(10, 11);

            result.Should().BeApproximately(0.1306, 1e-4);
        }

        [TestMethod]
        public void CalculateSumBetween_ExpectationMinus10StandardDeviation3AndMinus12toMinus11_01169() {
            var normalDistribution = new NormalDistribution(-10, 3);

            var result = normalDistribution.CalculateSumBetween(-12, -11);

            result.Should().BeApproximately(0.1169, 1e-4);
        }
    }
}
