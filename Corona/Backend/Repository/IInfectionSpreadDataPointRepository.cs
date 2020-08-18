using System.Collections.Generic;

namespace Backend.Repository {
    public interface IInfectionSpreadDataPointRepository {
        void DeleteAll(IUnitOfWork unitOfWork);
        void Insert(IUnitOfWork unitOfWork, IReadOnlyList<InfectionSpreadDataPointDao> dataPoints);
        List<InfectionSpreadDataPointDao> GetAllForCountryOrderedByDate(IUnitOfWork unitOfWork, CountryType country);
        List<InfectionSpreadDataPointDao> GetLastForAllCountriesOrderedByDate(IUnitOfWork unitOfWork, int days);
    }
}