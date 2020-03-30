namespace Backend.Service
{
    public interface IDataReimportService
    {
        bool ReimportAll(IUnitOfWork unitOfWork);
    }
}