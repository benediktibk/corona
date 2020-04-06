using System.Collections.Generic;

namespace Math
{
    public class PhysicalRectangle : ISpringConnection, IPhysicalObject
    {
        private readonly HashSet<ISpring> _springsConnectedLeft;
        private readonly HashSet<ISpring> _springsConnectedRight;
        private readonly HashSet<ISpring> _springsConnectedTop;
        private readonly HashSet<ISpring> _springsConnectedBottom;
        private readonly HashSet<ISpring> _springsConnectedCenter;
        private readonly List<ISpring> _allSprings;
        private readonly double _weight;
        private readonly double _width;
        private readonly double _height;
        private readonly double _damping;
        private Vector _positionOfCenter;
        private Vector _speed;

        public Vector PositionOfCenter => _positionOfCenter;

        public PhysicalRectangle(double weight, double width, double height, Vector positionOfCenter, double damping) {
            _weight = weight;
            _width = width;
            _height = height;
            _damping = damping;
            _positionOfCenter = positionOfCenter;
            _speed = new Vector(0, 0);
            _springsConnectedBottom = new HashSet<ISpring>();
            _springsConnectedLeft = new HashSet<ISpring>();
            _springsConnectedRight = new HashSet<ISpring>();
            _springsConnectedTop = new HashSet<ISpring>();
            _springsConnectedCenter = new HashSet<ISpring>();
            _allSprings = new List<ISpring>();
        }

        public void AddSpringCenter(ISpring spring) {
            _springsConnectedCenter.Add(spring);
            _allSprings.Add(spring);
        }

        public void AddSpringLeft(ISpring spring) {
            _springsConnectedLeft.Add(spring);
            _allSprings.Add(spring);
        }

        public void AddSpringRight(ISpring spring) {
            _springsConnectedRight.Add(spring);
            _allSprings.Add(spring);
        }

        public void AddSpringTop(ISpring spring) {
            _springsConnectedTop.Add(spring);
            _allSprings.Add(spring);
        }

        public void AddSpringBottom(ISpring spring) {
            _springsConnectedBottom.Add(spring);
            _allSprings.Add(spring);
        }

        public void ApplyForces(double timeStep) {
            var allForces = new Vector(0, 0);

            foreach (var spring in _allSprings) {
                allForces = allForces + spring.CalculateForce(this);
            }

            var acceleration = 1 / _weight * allForces;
            _speed = (1 - _damping * timeStep) * (_speed + timeStep * acceleration);
            _positionOfCenter = _positionOfCenter + timeStep * _speed;
        }

        public Vector GetPosition(ISpring spring) {
            if (_springsConnectedLeft.Contains(spring)) {
                return _positionOfCenter + new Vector((-1) * _width / 2, 0);
            }

            if (_springsConnectedRight.Contains(spring)) {
                return _positionOfCenter + new Vector(_width / 2, 0);
            }

            if (_springsConnectedTop.Contains(spring)) {
                return _positionOfCenter + new Vector(0, _height / 2);
            }

            if (_springsConnectedBottom.Contains(spring)) {
                return _positionOfCenter + new Vector(0, (-1) * _height / 2);
            }

            if (_springsConnectedCenter.Contains(spring)) {
                return _positionOfCenter;
            }

            throw new KeyNotFoundException();
        }
    }
}
