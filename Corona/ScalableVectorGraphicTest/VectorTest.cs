using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScalableVectorGraphic;

namespace ScalableVectorGraphicTest
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
    }
}
