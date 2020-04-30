using System.Collections.Generic;

namespace Backend.Repository {
    public interface IInfectionSpreadDataPointRepository {
        void DeleteAll(IUnitOfWork unitOfWork);
        void Insert(IUnitOfWork unitOfWork, InfectionSpreadDataPointDao dataPoint);
        void Insert(IUnitOfWork unitOfWork, IReadOnlyList<InfectionSpreadDataPointDao> dataPoints);
        List<InfectionSpreadDataPointDao> GetAllForCountry(IUnitOfWork unitOfWork, CountryType country);
    }
}