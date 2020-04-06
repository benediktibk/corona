using System.Diagnostics;

namespace Math
{
    [DebuggerDisplay("Vector ({X}, {Y})")]
    public class Vector
    {
        public Vector(double x, double y) {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }
        public double Norm => System.Math.Sqrt(X * X + Y * Y);

        public static Vector operator +(Vector a, Vector b) {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b) {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator *(double a, Vector b) {
            return new Vector(a * b.X, a * b.Y);
        }

        public static double operator *(Vector a, Vector b) {
            return a.X * b.X + a.Y * b.Y;
        }

        public static bool IsLeftOfLine(Vector offset, Vector direction, Vector point) {
            return (point.X - offset.X) * direction.Y - (point.Y - offset.Y) * direction.X < 0;
        }
    }
}
