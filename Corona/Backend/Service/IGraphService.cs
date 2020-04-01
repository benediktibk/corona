using System.Collections.Generic;

namespace Backend.Service
{
    public interface IGraphService
    {
        string CreateGraphInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        string CreateGraphInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
    }
}