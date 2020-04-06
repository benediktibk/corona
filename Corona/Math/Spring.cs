namespace Math
{
    public class Spring : ISpring
    {
        private readonly double _length;
        private readonly double _stiffness;
        private readonly ISpringConnection _connectionOne;
        private readonly ISpringConnection _connectionTwo;

        public Spring(double length, double stiffness, ISpringConnection connectionOne, ISpringConnection connectionTwo) {
            _length = length;
            _stiffness = stiffness;
            _connectionOne = connectionOne;
            _connectionTwo = connectionTwo;
        }

        public Vector CalculateForce(ISpringConnection connection) {
            var distance = _connectionOne.GetPosition(this) - _connectionTwo.GetPosition(this);
            var distanceNorm = distance.Norm;
            var force = (distanceNorm - _length) * _stiffness;
            var distanceNormed = 1 / distanceNorm * distance;

            if (connection == _connectionOne) {
                return (-1) * force * distanceNormed;
            }
            else if (connection == _connectionTwo) {
                return force * distanceNormed;
            }

            throw new System.ArgumentException("connection is invalid");
        }
    }
}
