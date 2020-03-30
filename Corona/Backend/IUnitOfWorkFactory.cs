namespace Backend
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}