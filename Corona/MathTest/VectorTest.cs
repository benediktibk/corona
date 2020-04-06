﻿using FluentAssertions;
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
    }
}
