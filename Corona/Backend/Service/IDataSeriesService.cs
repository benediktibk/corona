using ScalableVectorGraphic;
using System;
using System.Collections.Generic;

namespace Backend.Service {
    public interface IDataSeriesService {
        List<DataSeries<DateTime, double>> CreateDeathsPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries, out List<ReferenceLine<double>> referenceLines);
        List<DataSeries<DateTime, double>> CreateDeaths(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeries<DateTime, double>> CreateInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeries<DateTime, double>> CreateEstimatedActualInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeries<double, double>> CreateInfectedGrowthPerTotalInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeries<double, double>> CreateInfectedGrowthPerTotalInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeries<DateTime, double>> CreateStillInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeries<DateTime, double>> CreateInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeries<DateTime, double>> CreateInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
    }
}