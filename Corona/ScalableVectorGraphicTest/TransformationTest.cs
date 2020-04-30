using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScalableVectorGraphic;

namespace ScalableVectorGraphicTest {
    [TestClass]
    public class TransformationTest {
        [TestMethod]
        public void Apply_ValidValues_CorrectResult() {
            var x = new Point(5, 3);
            var transformation = new Transformation(new Matrix(2.5, 10, -3, 7), new Vector(-4, 9.3));

            var y = transformation.ApplyToPoint(x);

            y.X.Should().BeApproximately(42.5 - 4, 1e-5);
            y.Y.Should().BeApproximately(6 + 9.3, 1e-5);
        }
    }
}
