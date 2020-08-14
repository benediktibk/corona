using NLog;
using System.Collections.Generic;

namespace Backend.Repository {
    public class InfectionSpreadDataPointRepository : IInfectionSpreadDataPointRepository {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public void DeleteAll(IUnitOfWork unitOfWork) {
            unitOfWork.ExecuteDatabaseCommand("TRUNCATE TABLE InfectionSpreadDataPoint");
        }

        public List<InfectionSpreadDataPointDao> GetAllForCountry(IUnitOfWork unitOfWork, CountryType country) {
            _logger.Info($"fetching all infection spread data points for country {country}");
            return unitOfWork.QueryDatabase<InfectionSpreadDataPointDao>(@"SELECT * FROM InfectionSpreadDataPoint WHERE CountryId = @CountryId", new { CountryId = country });
        }

        public void Insert(IUnitOfWork unitOfWork, IReadOnlyList<InfectionSpreadDataPointDao> dataPoints) {
            _logger.Info($"adding {dataPoints.Count} data points to the database");
            foreach (var dataPoint in dataPoints) {
                Insert(unitOfWork, dataPoint);
            }
        }

        public void Insert(IUnitOfWork unitOfWork, InfectionSpreadDataPointDao dataPoint) {
            _logger.Trace($"adding data point for country {dataPoint.CountryId}");
            unitOfWork.ExecuteDatabaseCommand(@"
                INSERT InfectionSpreadDataPoint
                (
                    [Date],
                    CountryId,
                    InfectedTotal,
                    DeathsTotal,
                    RecoveredTotal
                )
                VALUES
                (
                    @Date,
                    @CountryId,
                    @InfectedTotal,
                    @DeathsTotal,
                    @RecoveredTotal
                )", dataPoint);
        }
    }
}
