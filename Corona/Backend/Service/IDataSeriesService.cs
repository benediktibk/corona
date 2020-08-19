using ScalableVectorGraphic;
using System;
using System.Collections.Generic;

namespace Backend.Service {
    public interface IDataSeriesService {
        List<DataSeriesXY<DateTime, double>> CreateDeathsPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries, out List<ReferenceLine<double>> referenceLines);
        List<DataSeriesXY<DateTime, double>> CreateDeaths(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeriesXY<DateTime, double>> CreateInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeriesXY<DateTime, double>> CreateEstimatedActualNewInfectedPersons(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries, int estimationPastInDays);
        List<DataSeriesXY<double, double>> CreateInfectedGrowthPerTotalInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeriesXY<double, double>> CreateInfectedGrowthPerTotalInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeriesXY<DateTime, double>> CreateStillInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeriesXY<DateTime, double>> CreateStillInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeriesXY<DateTime, double>> CreateInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        List<DataSeriesXY<DateTime, double>> CreateInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries);
        DataSeriesBar<CountryType, double> CreateHighestAverageDeathsPerPopulationRecently(IUnitOfWork unitOfWork, int topCountriesCount, int daysInPast);
        DataSeriesBar<CountryType, double> CreateHighestAverageNewInfectionsPerPopulationRecently(IUnitOfWork unitOfWork, int topCountriesCount, int daysInPast);
    }
}