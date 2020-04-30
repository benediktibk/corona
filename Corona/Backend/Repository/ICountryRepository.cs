namespace Backend.Repository {
    public interface ICountryRepository {
        void Insert(IUnitOfWork unitOfWork, CountryDao country);
    }
}