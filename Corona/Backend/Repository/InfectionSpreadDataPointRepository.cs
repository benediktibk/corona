using System;
using NLog;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Backend.Repository {
    public class InfectionSpreadDataPointRepository : IInfectionSpreadDataPointRepository {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private const int BATCH_SIZE = 400;

        public void DeleteAll(IUnitOfWork unitOfWork) {
            unitOfWork.ExecuteDatabaseCommand("TRUNCATE TABLE InfectionSpreadDataPoint");
        }

        public List<InfectionSpreadDataPointDao> GetAllForCountryOrderedByDate(IUnitOfWork unitOfWork, CountryType country) {
            _logger.Info($"fetching all infection spread data points for country {country}");
            return unitOfWork.QueryDatabase<InfectionSpreadDataPointDao>(@"SELECT * FROM InfectionSpreadDataPoint WHERE CountryId = @CountryId ORDER BY [Date]", new { CountryId = country });
        }

        public void Insert(IUnitOfWork unitOfWork, IReadOnlyList<InfectionSpreadDataPointDao> dataPoints) {
            _logger.Info($"adding {dataPoints.Count} data points to the database");
            var batches = CreateBatches(dataPoints);
            var count = 0;

            foreach (var batch in batches) {
                _logger.Info($"adding batch {count} of {batches.Count}");
                InsertBatch(unitOfWork, batch);
                count++;
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

        private void InsertBatch(IUnitOfWork unitOfWork, IReadOnlyList<InfectionSpreadDataPointDao> dataPoints) {
            var commandBuilder = new StringBuilder("INSERT InfectionSpreadDataPoint ([Date], CountryId, InfectedTotal, DeathsTotal, RecoveredTotal) VALUES");
            var sqlCommand = new SqlCommand();

            for (var currentIndex = 0; currentIndex < dataPoints.Count; ++currentIndex) {
                _logger.Trace($"processing dataPoint {currentIndex} of {dataPoints.Count} of current batch");
                var dataPoint = dataPoints[currentIndex];

                commandBuilder.Append(Environment.NewLine);
                commandBuilder.Append($"(@p_date_{currentIndex}, @p_countryId_{currentIndex}, @p_infectedTotal_{currentIndex}, @p_deathsTotal_{currentIndex}, @p_recoveredTotal_{currentIndex}),");
                
                sqlCommand.Parameters.AddWithValue($"p_date_{currentIndex}", dataPoint.Date);
                sqlCommand.Parameters.AddWithValue($"p_countryId_{currentIndex}", dataPoint.CountryId);
                sqlCommand.Parameters.AddWithValue($"p_infectedTotal_{currentIndex}", dataPoint.InfectedTotal);
                sqlCommand.Parameters.AddWithValue($"p_deathsTotal_{currentIndex}", dataPoint.DeathsTotal);
                sqlCommand.Parameters.AddWithValue($"p_recoveredTotal_{currentIndex}", dataPoint.RecoveredTotal);
            }

            commandBuilder.Remove(commandBuilder.Length - 1, 1);
            sqlCommand.CommandText = commandBuilder.ToString();
            _logger.Info($"command creation has finished, executing the insert statement with {sqlCommand.CommandText.Length} characters and {sqlCommand.Parameters.Count} parameters");
            _logger.Trace($"command: {sqlCommand.CommandText}");
            unitOfWork.ExecuteDatabaseCommand(sqlCommand);
        }

        private List<List<InfectionSpreadDataPointDao>> CreateBatches(IReadOnlyList<InfectionSpreadDataPointDao> entries) {
            var result = new List<List<InfectionSpreadDataPointDao>>();
            var position = 0;

            while (position < entries.Count) {
                var currentBatchSize = System.Math.Min(BATCH_SIZE, entries.Count - position);
                result.Add(entries.Skip(position).Take(currentBatchSize).ToList());
                position += currentBatchSize;
            }

            return result;
        }
    }
}
