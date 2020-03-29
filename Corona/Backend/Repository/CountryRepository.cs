using Dapper;

namespace Backend.Repository
{
    public class CountryRepository : ICountryRepository
    {
        public void Insert(IUnitOfWork unitOfWork, CountryDao country) {
            unitOfWork.ExecuteDatabaseCommand(@"
                INSERT Country 
                (
                    Id,
                    Name
                )
                VALUES
                (
                    @Id,
                    @Name
                )", country);
        }
    }
}
