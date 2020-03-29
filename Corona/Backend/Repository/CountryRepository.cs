using Dapper;

namespace Backend.Repository
{
    public class CountryRepository : ICountryRepository
    {
        public void Insert(IUnitOfWork unitOfWork, CountryDao country) {
            unitOfWork.QueryDatabase(@"
                INSERT Country 
                (
                    Id,
                    Name,
                    Inhabitants,
                    IcuBeds,
                    MoratilityRatePerOneMillionPerDay
                )
                VALUES
                (
                    @Id,
                    @Name,
                    @Inhabitants,
                    @IcuBeds,
                    @MoratilityRatePerOneMillionPerDay
                )", country);
        }
    }
}
