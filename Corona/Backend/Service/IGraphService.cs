using System.Collections.Generic;

namespace Backend.Service
{
    public interface IGraphService
    {
        string CreateGraphInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateGraphInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateGraphInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateGraphDeathsPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
    }
}