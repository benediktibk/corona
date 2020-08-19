using ScalableVectorGraphic;
using System;
using System.Collections.Generic;

namespace Backend.Service {
    public class GraphService : IGraphService {
        private const int _graphWidth = 1000;
        private const int _graphHeight = 500;
        private const int _barGraphHeight = 1000;
        private readonly NumericOperationsDateTimeForDatesOnly _numericOperationsDates;
        private readonly NumericOperationsDouble _numericOperationsDouble;
        private readonly IAxis<DateTime> _dateAxis;
        private readonly IAxis<double> _linearPersonAxis;
        private readonly IAxis<double> _linearPersonPerPopulationAxis;
        private readonly IAxis<double> _logarithmicPersonAxis;
        private readonly IAxis<double> _logarithmicPersonPerPopulationAxis;
        private readonly ILabelGenerator<CountryType> _countryLabelGenerator;
        private readonly bool _compressed;
        private readonly IDataSeriesService _dataSeriesService;

        public GraphService(ISettings settings, IDataSeriesService dataSeriesService) {
            _dataSeriesService = dataSeriesService;
            _numericOperationsDates = new NumericOperationsDateTimeForDatesOnly(new DateTime(2020, 1, 1));
            _numericOperationsDouble = new NumericOperationsDouble();
            _dateAxis = new LinearAxisDateTime(_numericOperationsDates, "Date");
            _linearPersonAxis = new LinearAxisDouble(_numericOperationsDouble, "Persons", "F0");
            _linearPersonPerPopulationAxis = new LinearAxisDouble(_numericOperationsDouble, "Persons [%]", "P5");
            _logarithmicPersonAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Persons", "F0");
            _logarithmicPersonPerPopulationAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Persons [%]", "P5");
            _countryLabelGenerator = new LabelGenerator<CountryType>();
            _compressed = settings.SvgCompressed;
        }

        public string CreateDeathsPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateDeathsPerPopulationLogarithmic(unitOfWork, countries, out var referenceLines);
            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries, referenceLines, true, true, new Point(0.4, 0.8));
            return ConvertGraphToSvg(graph);
        }

        public string CreateDeaths(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateDeaths(unitOfWork, countries);
            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _linearPersonAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        public string CreateInfectedAbsoluteLinear(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateInfectedAbsoluteLinear(unitOfWork, countries);
            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _linearPersonAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        public string CreateInfectedAbsoluteLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateInfectedAbsoluteLogarithmic(unitOfWork, countries);
            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        public string CreateInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateInfectedPerPopulationLogarithmic(unitOfWork, countries);
            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        public string CreateStillInfectedPerPopulationLogarithmic(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateStillInfectedPerPopulationLogarithmic(unitOfWork, countries);
            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        public string CreateStillInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateStillInfected(unitOfWork, countries);
            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _linearPersonAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        public string CreateInfectedGrowthPerTotalInfected(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateInfectedGrowthPerTotalInfected(unitOfWork, countries);
            var xAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Persons Total", "F0");
            var yAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Persons Growth", "F0");
            var graph = new XYGraph<double, double>(_graphWidth, _graphHeight, xAxis, yAxis, allDataSeries, true, true, new Point(0.2, 0.6));
            return ConvertGraphToSvg(graph);
        }

        public string CreateInfectedGrowthPerTotalInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateInfectedGrowthPerTotalInfectedPerPopulation(unitOfWork, countries);
            var xAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Population Total [%]", "P5");
            var yAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Infected Population Growth [%]", "P5");
            var graph = new XYGraph<double, double>(_graphWidth, _graphHeight, xAxis, yAxis, allDataSeries, true, true, new Point(0.2, 0.6));
            return ConvertGraphToSvg(graph);
        }

        public string CreateEstimatedActualNewInfectedPersons(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateEstimatedActualNewInfectedPersons(unitOfWork, countries);
            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _linearPersonAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        public string CreateTopCountriesByNewDeaths(IUnitOfWork unitOfWork) {
            var dataSeries = _dataSeriesService.CreateHighestAverageDeathsPerPopulationRecently(unitOfWork, 10);
            var graph = new HorizontalBarGraph<CountryType, double>(_graphWidth, _barGraphHeight, _countryLabelGenerator, _linearPersonPerPopulationAxis, dataSeries);
            return ConvertGraphToSvg(graph);
        }

        public string CreateTopCountriesByNewInfections(IUnitOfWork unitOfWork) {
            var dataSeries = _dataSeriesService.CreateHighestAverageNewInfectionsPerPopulationRecently(unitOfWork, 10);
            var graph = new HorizontalBarGraph<CountryType, double>(_graphWidth, _barGraphHeight, _countryLabelGenerator, _linearPersonPerPopulationAxis, dataSeries);
            return ConvertGraphToSvg(graph);
        }

        private string ConvertGraphToSvg(IGraph graph) {
            if (_compressed) {
                return graph.ToSvgCompressed();
            }
            else {
                return graph.ToSvg();
            }
        }
    }
}
