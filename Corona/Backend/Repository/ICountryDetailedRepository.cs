using System.Collections.Generic;

namespace Backend.Repository
{
    public interface ICountryDetailedRepository
    {
        void Insert(IUnitOfWork unitOfWork, CountryDetailedDao country);
        List<CountryDetailedDao> GetAllAvailable(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
    }
}