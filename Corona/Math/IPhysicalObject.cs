namespace Math
{
    public interface IPhysicalObject
    {
        Vector PositionOfCenter { get; }

        void ApplyForces(double timeStep);
    }
}