﻿using Math;
using System.Diagnostics;

namespace ScalableVectorGraphic {
    [DebuggerDisplay("Point ({X}, {Y})")]
    public class Point {
        public Point(double x, double y) {
            X = x;
            Y = y;
        }

        public Point(Vector vector) :
            this(vector.X, vector.Y) {
        }

        public double X { get; }
        public double Y { get; }

        public Point Apply(IAxisTransformation xAxisTransformation, IAxisTransformation yAxisTransformation) {
            return new Point(
                xAxisTransformation.Apply(X),
                yAxisTransformation.Apply(Y)
                );
        }

        public Vector ToVector() {
            return new Vector(X, Y);
        }
    }
}
