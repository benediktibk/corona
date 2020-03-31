using System.Collections.Generic;

namespace ScalableVectorGraphic
{
    public class Matrix
    {
        private readonly List<Vector> _columns;

        public Matrix(Vector column1, Vector column2) {
            _columns = new List<Vector> {
                column1,
                column2
            };
        }

        public static Vector operator *(Matrix matrix, Vector vector) {
            return new Vector(
                x: matrix._columns[0].X * vector.X + matrix._columns[1].X * vector.Y,
                y: matrix._columns[0].Y * vector.X + matrix._columns[1].Y * vector.Y);
        }
    }
}
