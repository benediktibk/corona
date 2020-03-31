using System.Collections.Generic;

namespace Backend.Service
{
    public interface IGraphService
    {
        string CreateGraph(IUnitOfWork unitOfWork, GraphType type, IReadOnlyList<CountryAndColor> countries);
    }
}