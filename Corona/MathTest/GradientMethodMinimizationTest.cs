﻿using FluentAssertions;
using Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MathTest {
    [TestClass]
    public class GradientMethodMinimizationTest {
        [TestMethod]
        public void Minimize_PointPenaltyFunctionAndStartNearbyPoint_ResultIsFarAwayFromPoint() {
            var penaltyPoint = new Vector(3, 5);
            var penaltyFunction = new PointExponentialDistancePenaltyFunction(penaltyPoint, 5, 1e10);

            var result = GradientMethodMinimization.Minimize(penaltyPoint + new Vector(0.1, 0), penaltyFunction, 10, 1e-5);

            var distance = (result - penaltyPoint).Norm;
            distance.Should().BeGreaterThan(100);
        }

        [TestMethod]
        public void Minimize_PointPenaltyFunctionAndStartOnlyChangeInX_ResultHasStillTheSameYCoordinate() {
            var penaltyPoint = new Vector(3, 5);
            var penaltyFunction = new PointExponentialDistancePenaltyFunction(penaltyPoint, 5, 1e10);

            var result = GradientMethodMinimization.Minimize(penaltyPoint + new Vector(0.1, 0), penaltyFunction, 10, 1e-5);

            result.Y.Should().BeApproximately(penaltyPoint.Y, 1e-5);
        }

        [TestMethod]
        public void Minimize_PointPenaltyFunctionAndStartOnlyChangeInY_ResultHasStillTheSameXCoordinate() {
            var penaltyPoint = new Vector(3, 5);
            var penaltyFunction = new PointExponentialDistancePenaltyFunction(penaltyPoint, 5, 1e10);

            var result = GradientMethodMinimization.Minimize(penaltyPoint + new Vector(0, 0.1), penaltyFunction, 10, 1e-5);

            var distance = (result - penaltyPoint).Norm;
            distance.Should().BeGreaterThan(100);
            result.X.Should().BeApproximately(penaltyPoint.X, 1e-5);
        }

        [TestMethod]
        public void Minimize_PointPenaltyFunctionAndStartChangedInXAndY_ResultHasDifferentXAndYCoordinate() {
            var penaltyPoint = new Vector(3, 5);
            var penaltyFunction = new PointExponentialDistancePenaltyFunction(penaltyPoint, 5, 1e10);

            var result = GradientMethodMinimization.Minimize(penaltyPoint + new Vector(0.01, 0.1), penaltyFunction, 10, 1e-5);

            var distance = (result - penaltyPoint).Norm;
            distance.Should().BeGreaterThan(100);
            result.X.Should().NotBeApproximately(penaltyPoint.X, 1e-5);
            result.Y.Should().NotBeApproximately(penaltyPoint.Y, 1e-5);
        }

        [TestMethod]
        public void Minimize_BoxWithStartCloseToCorner_CenterOfBox() {
            const int exponentialBase = 2;
            const double maximumValue = 1e5;
            var bottom = new LineExponentialDistancePenaltyFunction(new Vector(0, 0), new Vector(1, 0), exponentialBase, maximumValue, false, false);
            var top = new LineExponentialDistancePenaltyFunction(new Vector(0, 5), new Vector(1, 0), exponentialBase, maximumValue, false, false);
            var left = new LineExponentialDistancePenaltyFunction(new Vector(0, 0), new Vector(0, 1), exponentialBase, maximumValue, false, false);
            var right = new LineExponentialDistancePenaltyFunction(new Vector(10, 0), new Vector(0, 1), exponentialBase, maximumValue, false, false);
            var penaltyFunction = new PenaltyFunctionWeightedSum(new List<IPenaltyFunction> {
                bottom,
                top,
                left,
                right
            });
            var start = new Vector(0.01, 0.1);

            var result = GradientMethodMinimization.Minimize(start, penaltyFunction, 10000, 1e-15);

            result.X.Should().BeApproximately(5, 1e-5);
            result.Y.Should().BeApproximately(2.5, 1e-5);
        }

        [TestMethod]
        public void Minimize_BoxWithStartCloseToCenter_CenterOfBox() {
            const int exponentialBase = 2;
            const double maximumValue = 1e5;
            var bottom = new LineExponentialDistancePenaltyFunction(new Vector(0, 0), new Vector(1, 0), exponentialBase, maximumValue, false, false);
            var top = new LineExponentialDistancePenaltyFunction(new Vector(0, 5), new Vector(1, 0), exponentialBase, maximumValue, false, false);
            var left = new LineExponentialDistancePenaltyFunction(new Vector(0, 0), new Vector(0, 1), exponentialBase, maximumValue, false, false);
            var right = new LineExponentialDistancePenaltyFunction(new Vector(10, 0), new Vector(0, 1), exponentialBase, maximumValue, false, false);
            var penaltyFunction = new PenaltyFunctionWeightedSum(new List<IPenaltyFunction> {
                bottom,
                top,
                left,
                right
            });
            var start = new Vector(2, 3);

            var result = GradientMethodMinimization.Minimize(start, penaltyFunction, 10000, 1e-15);

            result.X.Should().BeApproximately(5, 1e-5);
            result.Y.Should().BeApproximately(2.5, 1e-5);
        }

        [TestMethod]
        public void Minimize_CompleteBoxWithStartCloseToCenter_CenterOfBox() {
            const double exponentialBase = 10;
            const double maximumValue = 1e5;
            var bottom = new LineExponentialDistancePenaltyFunction(new Vector(0, 0), new Vector(1, 0), exponentialBase, maximumValue, false, true);
            var top = new LineExponentialDistancePenaltyFunction(new Vector(0, 5), new Vector(1, 0), exponentialBase, maximumValue, true, false);
            var left = new LineExponentialDistancePenaltyFunction(new Vector(0, 0), new Vector(0, 1), exponentialBase, maximumValue, true, false);
            var right = new LineExponentialDistancePenaltyFunction(new Vector(10, 0), new Vector(0, 1), exponentialBase, maximumValue, false, true);
            var penaltyFunction = new PenaltyFunctionWeightedSum(new List<IPenaltyFunction> {
                bottom,
                top,
                left,
                right
            });
            var start = new Vector(2, 3);

            var result = GradientMethodMinimization.Minimize(start, penaltyFunction, 10000, 1e-15);

            result.X.Should().BeApproximately(5, 1e-5);
            result.Y.Should().BeApproximately(2.5, 1e-5);
        }

        [TestMethod]
        public void Minimize_CompleteBoxWithStartAlreadyCorrectInX_CenterOfBox() {
            const double exponentialBase = 10;
            const double maximumValue = 1e5;
            var bottom = new LineExponentialDistancePenaltyFunction(new Vector(0, 0), new Vector(1, 0), exponentialBase, maximumValue, false, true);
            var top = new LineExponentialDistancePenaltyFunction(new Vector(0, 5), new Vector(1, 0), exponentialBase, maximumValue, true, false);
            var left = new LineExponentialDistancePenaltyFunction(new Vector(0, 0), new Vector(0, 1), exponentialBase, maximumValue, true, false);
            var right = new LineExponentialDistancePenaltyFunction(new Vector(10, 0), new Vector(0, 1), exponentialBase, maximumValue, false, true);
            var penaltyFunction = new PenaltyFunctionWeightedSum(new List<IPenaltyFunction> {
                bottom,
                top,
                left,
                right
            });
            var start = new Vector(5, 3);

            var result = GradientMethodMinimization.Minimize(start, penaltyFunction, 10, 1e-5);

            result.X.Should().BeApproximately(5, 1e-5);
            result.Y.Should().BeApproximately(2.5, 1e-5);
        }

        [TestMethod]
        public void Minimize_TwoParallelLinesAndStartInMiddle_CenterOfLines() {
            const double exponentialBase = 10;
            const double maximumValue = 1e5;
            var left = new LineExponentialDistancePenaltyFunction(new Vector(0, 0), new Vector(0, 1), exponentialBase, maximumValue, true, false);
            var right = new LineExponentialDistancePenaltyFunction(new Vector(10, 0), new Vector(0, 1), exponentialBase, maximumValue, false, true);
            var penaltyFunction = new PenaltyFunctionWeightedSum(new List<IPenaltyFunction> {
                left,
                right
            });
            var start = new Vector(5, 0);

            var result = GradientMethodMinimization.Minimize(start, penaltyFunction, 10, 1e-5);

            result.X.Should().BeApproximately(5, 1e-5);
            result.Y.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void Minimize_TwoParallelLinesAndStartSomewhere_CenterOfLines() {
            const double exponentialBase = 10;
            const double maximumValue = 1e5;
            var left = new LineExponentialDistancePenaltyFunction(new Vector(0, 0), new Vector(0, 1), exponentialBase, maximumValue, true, false);
            var right = new LineExponentialDistancePenaltyFunction(new Vector(10, 0), new Vector(0, 1), exponentialBase, maximumValue, false, true);
            var penaltyFunction = new PenaltyFunctionWeightedSum(new List<IPenaltyFunction> {
                left,
                right
            });
            var start = new Vector(2, 0);

            var result = GradientMethodMinimization.Minimize(start, penaltyFunction, 10, 1e-8);

            result.X.Should().BeApproximately(5, 1e-5);
            result.Y.Should().BeApproximately(0, 1e-5);
        }

        [TestMethod]
        public void Minimize_CompleteLinearPenaltyBox_CenterOfBox() {
            const double width = 10;
            const double height = 5;
            const double maximumValue = 10;
            const double horziontalGradient = maximumValue / width * 2;
            const double verticalGradient = maximumValue / height * 2;
            var bottom = new LineLinearDistancePenaltyFunction(new Vector(0, 0), new Vector(1, 0), verticalGradient, maximumValue, false, true);
            var top = new LineLinearDistancePenaltyFunction(new Vector(0, height), new Vector(1, 0), verticalGradient, maximumValue, true, false);
            var left = new LineLinearDistancePenaltyFunction(new Vector(0, 0), new Vector(0, 1), horziontalGradient, maximumValue, true, false);
            var right = new LineLinearDistancePenaltyFunction(new Vector(width, 0), new Vector(0, 1), horziontalGradient, maximumValue, false, true);
            var penaltyFunction = new PenaltyFunctionSum(new List<IPenaltyFunction> {
                bottom,
                top,
                left,
                right
            });
            var start = new Vector(0.01, 0.01);

            var result = GradientMethodMinimization.Minimize(start, penaltyFunction, 200, 1e-10);

            result.X.Should().BeApproximately(width / 2, 1e-5);
            result.Y.Should().BeApproximately(height / 2, 1e-5);
        }

        [TestMethod]
        public void Minimize_CompleteLinearPenaltyBoxForRectangle_CenterOfBox() {
            const double width = 10;
            const double height = 5;
            const double maximumValue = 10;
            const double horziontalGradient = maximumValue / width * 2;
            const double verticalGradient = maximumValue / height * 2;
            const double boxWidth = 2;
            const double boxHeight = 1;
            var bottom = new LineLinearDistancePenaltyFunction(new Vector(0, 0), new Vector(1, 0), verticalGradient, maximumValue, false, true);
            var top = new LineLinearDistancePenaltyFunction(new Vector(0, height), new Vector(1, 0), verticalGradient, maximumValue, true, false);
            var left = new LineLinearDistancePenaltyFunction(new Vector(0, 0), new Vector(0, 1), horziontalGradient, maximumValue, true, false);
            var right = new LineLinearDistancePenaltyFunction(new Vector(width, 0), new Vector(0, 1), horziontalGradient, maximumValue, false, true);
            var penaltyFunction = new RectanglePenaltySum(new List<IPenaltyFunction> {
                bottom,
                top,
                left,
                right
            }, boxWidth, boxHeight, 0.01, 1);
            var start = new Vector(0.01, 0.01);

            var result = GradientMethodMinimization.Minimize(start, penaltyFunction, 200, 1e-10);

            result.X.Should().BeApproximately(width / 2 - boxWidth / 2, 1e-2);
            result.Y.Should().BeApproximately(height / 2 - boxHeight / 2, 1e-2);
        }
    }
}
