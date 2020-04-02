using Dapper;
using System.Collections.Generic;

namespace Backend.Repository
{
    public class CountryDetailedRepository : ICountryDetailedRepository
    {
        public void Insert(IUnitOfWork unitOfWork, CountryDetailedDao country) {
            unitOfWork.ExecuteDatabaseCommand(@"
                INSERT CountryDetailed
                (
                    CountryId,
                    Inhabitants,
                    IcuBeds,
                    MoratilityRatePerOneMillionPerDay
                )
                VALUES
                (
                    @CountryId,
                    @Inhabitants,
                    @IcuBeds,
                    @MoratilityRatePerOneMillionPerDay
                )", country);
        }

        public List<CountryDetailedDao> GetAllAvailable(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            return unitOfWork.QueryDatabase<CountryDetailedDao>(@"SELECT * FROM CountryDetailed WHERE CountryId IN @countries", new { countries });
        }
    }
}
