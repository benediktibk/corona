using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathTest
{
    [TestClass]
    public class VectorTest
    {
        [TestMethod]
        public void Add_ValidValues_CorrectResult() {
            var a = new Vector(5, 3);
            var b = new Vector(-4, 10);

            var result = a + b;

            result.X.Should().BeApproximately(1, 1e-5);
            result.Y.Should().BeApproximately(13, 1e-5);
        }

        [TestMethod]
        public void ScalarProduct_ValidValues_CorrectResult() {
            var a = new Vector(5, -1);
            var b = new Vector(6, 9);

            var result = a * b;

            result.Should().BeApproximately(21, 1e-5);
        }

        [TestMethod]
        public void IsLeftOfLine_HorizontalLineAndUnderneath_False() {
            var result = Vector.IsLeftOfLine(new Vector(5, 7), new Vector(1, 0), new Vector(-4, 2));

            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsLeftOfLine_HorizontalLineAndAbove_True() {
            var result = Vector.IsLeftOfLine(new Vector(5, 7), new Vector(1, 0), new Vector(-4, 20));

            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsLeftOfLine_VerticalLineAnLeft_True() {
            var result = Vector.IsLeftOfLine(new Vector(5, 7), new Vector(0, 1), new Vector(-4, 2));

            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsLeftOfLine_VerticalLineAnRight_True() {
            var result = Vector.IsLeftOfLine(new Vector(5, 7), new Vector(0, 1), new Vector(40, 2));

            result.Should().BeFalse();
        }
    }
}
