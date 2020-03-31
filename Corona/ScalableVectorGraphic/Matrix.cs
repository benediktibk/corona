using System;
using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class Matrix
    {
        private readonly double[,] _values;

        public Matrix(Vector column1, Vector column2) {
            _values = new double[2, 2];
            this[0, 0] = column1.X;
            this[1, 0] = column1.Y;
            this[0, 1] = column2.X;
            this[1, 1] = column2.Y;
            Determinant = this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
        }

        public double Determinant { get; }

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
