using System.Collections.Generic;

namespace Backend.Repository
{
    public interface ICountryInhabitantsRepository
    {
        void Insert(IUnitOfWork unitOfWork, CountryInhabitantsDao country);
        List<CountryInhabitantsDao> GetAllAvailable(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
    }
}