using Dapper;
using System.Collections.Generic;

namespace Backend.Repository
{
    public class CountryInhabitantsRepository : ICountryInhabitantsRepository
    {
        public void Insert(IUnitOfWork unitOfWork, CountryInhabitantsDao country) {
            unitOfWork.ExecuteDatabaseCommand(@"
                INSERT CountryInhabitants
                (
                    CountryId,
                    Inhabitants
                )
                VALUES
                (
                    @CountryId,
                    @Inhabitants
                )", country);
        }

        public List<CountryInhabitantsDao> GetAllAvailable(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            return unitOfWork.QueryDatabase<CountryInhabitantsDao>(@"SELECT * FROM CountryInhabitants WHERE CountryId IN @countries", new { countries });
        }
    }
}
