using System.Collections.Generic;

namespace Backend.Service {
    public interface IGraphService {
        string CreateInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateDeathsPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateDeaths(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateStillInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateInfectedGrowthPerTotalInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateInfectedGrowthPerTotalInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateEstimatedActualInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
    }
}