using Backend.Repository;
using Math;
using ScalableVectorGraphic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Service
{
    public class GraphService : IGraphService
    {
        private const int _graphWidth = 1000;
        private const int _graphHeight = 500;
        private readonly NumericOperationsDateTimeForDatesOnly _numericOperationsDates;
        private readonly NumericOperationsDouble _numericOperationsDouble;
        private readonly IAxis<DateTime> _dateAxis;
        private readonly IAxis<double> _linearPersonAxis;
        private readonly IAxis<double> _logarithmicPersonAxis;
        private readonly IAxis<double> _logarithmicPersonPerPopulationAxis;
        private readonly bool _compressed;
        private readonly IDataSeriesService _dataSeriesService;

        public GraphService(ISettings settings, IDataSeriesService dataSeriesService) {
            _dataSeriesService = dataSeriesService;
            _numericOperationsDates = new NumericOperationsDateTimeForDatesOnly(new DateTime(2020, 1, 1));
            _numericOperationsDouble = new NumericOperationsDouble();
            _dateAxis = new LinearAxisDateTime(_numericOperationsDates, "Date");
            _linearPersonAxis = new LinearAxisDouble(_numericOperationsDouble, "Persons", "F0");
            _logarithmicPersonAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Persons", "F0");
            _logarithmicPersonPerPopulationAxis = new LogarithmicAxis<double>(_numericOperationsDouble, "Persons [%]", "P5");
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

        public string CreateEstimatedActualInfectedPerPopulation(IUnitOfWork unitOfWork, IReadOnlyList<CountryType> countries) {
            var allDataSeries = _dataSeriesService.CreateEstimatedActualInfectedPerPopulation(unitOfWork, countries);
            var graph = new XYGraph<DateTime, double>(_graphWidth, _graphHeight, _dateAxis, _logarithmicPersonPerPopulationAxis, allDataSeries, true, true, new Point(0.2, 0.8));
            return ConvertGraphToSvg(graph);
        }

        private string ConvertGraphToSvg<X, Y>(XYGraph<X, Y> graph) {
            if (_compressed) {
                return graph.ToSvgCompressed();
            }
            else {
                return graph.ToSvg();
            }
        }
    }
}
