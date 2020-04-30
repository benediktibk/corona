namespace Math {
    public class FixedPoint : ISpringConnection {
        private readonly Vector _position;

        public FixedPoint(Vector position) {
            _position = position;
        }

        public Vector GetPosition(ISpring spring) {
            return _position;
        }
    }
}
