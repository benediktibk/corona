using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScalableVectorGraphic;
using System.Collections.Generic;

namespace ScalableVectorGraphicTest
{
    [TestClass]
    public class AxisBaseTest
    {
        [TestMethod]
        public void CalculateVerticalLabelPosition_1Tick_05() {
            var tickPositions = new List<double> {
                2
            };

            var labelPosition = AxisBase<double>.CalculateVerticalLabelPosition(tickPositions);

            labelPosition.Should().BeApproximately(0.5, 1e-5);
        }

        [TestMethod]
        public void CalculateVerticalLabelPosition_2Ticks_05() {
            var tickPositions = new List<double> {
                0,
                1
            };

            var labelPosition = AxisBase<double>.CalculateVerticalLabelPosition(tickPositions);

            labelPosition.Should().BeApproximately(0.5, 1e-5);
        }

        [TestMethod]
        public void CalculateVerticalLabelPosition_3Ticks_075() {
            var tickPositions = new List<double> {
                0,
                0.5,
                1
            };

            var labelPosition = AxisBase<double>.CalculateVerticalLabelPosition(tickPositions);

            labelPosition.Should().BeApproximately(0.75, 1e-5);
        }

        [TestMethod]
        public void CalculateVerticalLabelPosition_4Ticks_05() {
            var tickPositions = new List<double> {
                0,
                1.0/3,
                2.0/3,
                1
            };

            var labelPosition = AxisBase<double>.CalculateVerticalLabelPosition(tickPositions);

            labelPosition.Should().BeApproximately(0.5, 1e-5);
        }

        [TestMethod]
        public void CalculateVerticalLabelPosition_5Ticks_0625() {
            var tickPositions = new List<double> {
                0,
                1.0/4,
                2.0/4,
                3.0/4,
                1
            };

            var labelPosition = AxisBase<double>.CalculateVerticalLabelPosition(tickPositions);

            labelPosition.Should().BeApproximately(0.625, 1e-5);
        }
    }
}
