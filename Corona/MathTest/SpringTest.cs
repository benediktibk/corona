using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTest
{
    [TestClass]
    public class SpringTest
    {
        [TestMethod]
        public void CalculateForce_InitialPositionCalculateOnFirst_0() {
            var pointOne = new FixedPoint(new Vector(3, 5));
            var pointTwo = new FixedPoint(new Vector(3, 6));
            var spring = new Spring(1, 5, pointOne, pointTwo);

            var result = spring.CalculateForce(pointOne);

            result.Norm.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateForce_InitialPositionCalculateOnSecond_0() {
            var pointOne = new FixedPoint(new Vector(3, 5));
            var pointTwo = new FixedPoint(new Vector(3, 6));
            var spring = new Spring(1, 5, pointOne, pointTwo);

            var result = spring.CalculateForce(pointTwo);

            result.Norm.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void CalculateForce_StretchedAndCalculateOnFirst_CorrectValue() {
            var pointOne = new FixedPoint(new Vector(3, 5));
            var pointTwo = new FixedPoint(new Vector(3, 6));
            var spring = new Spring(0.1, 5, pointOne, pointTwo);

            var result = spring.CalculateForce(pointOne);

            result.X.Should().BeApproximately(0, 1e-5);
            result.Y.Should().BeApproximately(4.5, 1e-5);
        }

        [TestMethod]
        public void CalculateForce_StretchedAndCalculateOnSecond_CorrectValue() {
            var pointOne = new FixedPoint(new Vector(3, 5));
            var pointTwo = new FixedPoint(new Vector(3, 6));
            var spring = new Spring(0.1, 5, pointOne, pointTwo);

            var result = spring.CalculateForce(pointTwo);

            result.X.Should().BeApproximately(0, 1e-5);
            result.Y.Should().BeApproximately(-4.5, 1e-5);
        }
    }
}
