using System.Collections.Generic;

namespace Backend.Service {
    public interface IGraphService {
        string CreateInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateDeathsPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateDeaths(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateStillInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateStillInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateInfectedGrowthPerTotalInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateInfectedGrowthPerTotalInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateEstimatedActualNewInfectedPersons(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries, int estimationPastInDays);
        string CreateTopCountriesByNewDeaths(IUnitOfWork unitOfWork, int topCountriesCount, int daysInPast);
        string CreateTopCountriesByNewInfections(IUnitOfWork unitOfWork, int topCountriesCount, int daysInPast);
        string CreateTopCountriesByDeathsPerPopulation(IUnitOfWork unitOfWork, int topCountriesCount);
        string CreateTopCountriesByInfectionsPerPopulation(IUnitOfWork unitOfWork, int topCountriesCount);
    }
}