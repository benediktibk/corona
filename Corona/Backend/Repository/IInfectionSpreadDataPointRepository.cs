namespace Backend.Repository
{
    public interface IInfectionSpreadDataPointRepository
    {
        void DeleteAll(IUnitOfWork unitOfWork);
        void Insert(IUnitOfWork unitOfWork, InfectionSpreadDataPointDao dataPoint);
    }
}