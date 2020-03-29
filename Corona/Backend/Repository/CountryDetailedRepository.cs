using Dapper;

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
    }
}
