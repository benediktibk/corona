using System.Collections.Generic;

namespace Backend.Repository {
    public interface IInfectionSpreadDataPointRepository {
        void DeleteAll(IUnitOfWork unitOfWork);
        void Insert(IUnitOfWork unitOfWork, IReadOnlyList<InfectionSpreadDataPointDao> dataPoints);
        List<InfectionSpreadDataPointDao> GetAllForCountryOrderedByDate(IUnitOfWork unitOfWork, CountryType country);
        List<CountryType> FindCountriesWithHighestNewInfectionsDuringLastDays(IUnitOfWork unitOfWork, int days, int topCount);
        List<CountryType> FindCountriesWithHighestNewDeathsDuringLastDays(IUnitOfWork unitOfWork, int days, int topCount);
    }
}