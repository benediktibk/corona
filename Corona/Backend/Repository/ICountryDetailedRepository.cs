namespace Backend.Repository
{
    public interface ICountryDetailedRepository
    {
        void Insert(IUnitOfWork unitOfWork, CountryDetailedDao country);
    }
}