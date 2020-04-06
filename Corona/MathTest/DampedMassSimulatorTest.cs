using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MathTest
{
    [TestClass]
    public class DampedMassSimulatorTest
    {
        [TestMethod]
        public void RunSimulation_TwoObjectWithNoStretchInBetween_BothObjectsTillHaveInitialPosition() {
            var rectangleOne = new PhysicalRectangle(1, 2, 2, new Vector(3, 4));
            var rectangleTwo = new PhysicalRectangle(1, 2, 2, new Vector(13, 4));
            var spring = new Spring(10, 2, rectangleOne, rectangleTwo);
            var physicalObjects = new List<IPhysicalObject> {
                rectangleOne,
                rectangleTwo
            };

            DampedMassSimulator.RunSimulation(1, 1e-5, 1e-2, physicalObjects);

            rectangleOne.PositionOfCenter.X.Should().BeApproximately(3, 1e-5);
            rectangleOne.PositionOfCenter.Y.Should().BeApproximately(4, 1e-5);
            rectangleTwo.PositionOfCenter.X.Should().BeApproximately(13, 1e-5);
            rectangleTwo.PositionOfCenter.Y.Should().BeApproximately(4, 1e-5);
        }
    }
}
