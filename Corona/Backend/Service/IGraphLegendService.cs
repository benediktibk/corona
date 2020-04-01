using System.Collections.Generic;

namespace Backend.Service
{
    public interface IGraphLegendService
    {
        string CreateLegend(IReadOnlyList<CountryType> countries);
    }
}