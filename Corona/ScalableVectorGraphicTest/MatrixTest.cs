using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScalableVectorGraphic;

namespace ScalableVectorGraphicTest
{
    [TestClass]
    public class MatrixTest
    {
        [TestMethod]
        public void Multiply_ValidValues_CorrectResult() {
            var x = new Vector(5, 3);
            var matrix = new Matrix(2.5, 10, -3, 7);

            var y = matrix * x;

            y.X.Should().BeApproximately(42.5, 1e-5);
            y.Y.Should().BeApproximately(6, 1e-5);
        }

        [TestMethod]
        public void Determinant_FullMatrix_CorrectResult() {
            var matrix = new Matrix(2.5, 10, -3, 7);

            matrix.Determinant.Should().BeApproximately(47.5, 1e-5);
        }

        [TestMethod]
        public void EstimateRotationInDegrees_RealRotationMatrix_CorrectAngle() {
            var matrix = new Matrix(0.86602540378443864676372317075294, -0.5, 0.5, 0.86602540378443864676372317075294);

            var result = matrix.EstimateRotationInDegrees();

            result.Should().BeApproximately(30, 1e-5);
        }
    }
}
