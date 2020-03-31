namespace ScalableVectorGraphic
{
    public class Vector
    {
        public Vector(double x, double y) {
            X = x;
            Y = y;
        }

        public Vector(Point point) :
            this(point.X, point.Y) {
        }

        public double X { get; }
        public double Y { get; }

        public static Vector operator +(Vector a, Vector b) {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }
    }
}
