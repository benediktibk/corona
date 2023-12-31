﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Math {
    public class Matrix {
        private readonly double[,] _values;

        public Matrix(double m11, double m22) :
            this(m11, 0, 0, m22) {
        }

        public Matrix(double m11, double m12, double m21, double m22) {
            _values = new double[2, 2];
            this[0, 0] = m11;
            this[1, 0] = m21;
            this[0, 1] = m12;
            this[1, 1] = m22;
            Determinant = this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
        }

        public double Determinant { get; }

        public double EstimateRotationInDegrees() {
            var possibilities = new List<double>();

            var m11 = this[0, 0] / Determinant;
            if (m11 >= -1 && m11 <= 1) {
                possibilities.Add(System.Math.Acos(m11));
            }

            var m22 = this[1, 1] / Determinant;
            if (m22 >= -1 && m22 <= 1) {
                possibilities.Add(System.Math.Acos(m22));
            }

            var m12 = this[0, 1] / Determinant * (-1);
            if (m12 >= -1 && m12 <= 1) {
                possibilities.Add(System.Math.Asin(m12));
            }

            var m21 = this[1, 0] / Determinant;
            if (m21 >= -1 && m21 <= 1) {
                possibilities.Add(System.Math.Asin(m21));
            }

            if (possibilities.Count == 0) {
                return 0;
            }

            return possibilities.Sum() / possibilities.Count * 180 / System.Math.PI;
        }

        public static Vector operator *(Matrix matrix, Vector vector) {
            return new Vector(
                x: matrix[0, 0] * vector.X + matrix[0, 1] * vector.Y,
                y: matrix[1, 0] * vector.X + matrix[1, 1] * vector.Y);
        }

        public double this[int row, int column] {
            get {
                if (column < 0 || column > 2) {
                    throw new IndexOutOfRangeException("column is out of range");
                }

                if (row < 0 || row > 2) {
                    throw new IndexOutOfRangeException("row is out of range");
                }

                return _values[row, column];
            }
            set {
                if (column < 0 || column > 2) {
                    throw new IndexOutOfRangeException("column is out of range");
                }

                if (row < 0 || row > 2) {
                    throw new IndexOutOfRangeException("row is out of range");
                }

                _values[row, column] = value;
            }
        }
    }
}
