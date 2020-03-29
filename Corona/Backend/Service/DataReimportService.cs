using System.Collections.Generic;
using System.Linq;
using Backend.Repository;
using NLog;

namespace Backend.Service
{
    public class DataReimportService
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly ICsvFileRepository _csvFileRepository;
        private readonly IInfectionSpreadDataPointRepository _infectionSpreadDataPointRepository;
        private readonly IGitRepository _gitRepository;
        private readonly string _sourceFilePath;
        private readonly string _gitRepoUrl;

        public DataReimportService(
            ICsvFileRepository csvFileRepository, 
            IInfectionSpreadDataPointRepository infectionSpreadDataPointRepository, 
            IGitRepository gitRepository,
            string sourceFilePath,
            string gitRepoUrl) {
            _csvFileRepository = csvFileRepository;
            _sourceFilePath = sourceFilePath;
            _gitRepoUrl = gitRepoUrl;
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
            _gitRepository = gitRepository;
        }

        public void ReimportAll(IUnitOfWork unitOfWork) {
            _gitRepository.FetchLatestCommit(_gitRepoUrl, _sourceFilePath);
            _infectionSpreadDataPointRepository.DeleteAll(unitOfWork);

            var files = _csvFileRepository.ListAllCsvFilesIn(_sourceFilePath);

            foreach (var file in files) {
                ReimportAll(unitOfWork, file);
            }
        }

        private void ReimportAll(IUnitOfWork unitOfWork, string file) {
            var content = _csvFileRepository.ReadFile(file);
            var result = new Dictionary<CountryType, InfectionSpreadDataPointDao>();

            foreach (var line in content) {
                InfectionSpreadDataPointDao dataPoint;
                if (!TryParseDataPointFromLine(line, out dataPoint)) {
                    _logger.Warn($"unable to parse one line in file {file}, skipping it");
                    continue;
                }

                if (result.TryGetValue(dataPoint.Country, out var previousDataPoint)) {
                    previousDataPoint.InfectedTotal += dataPoint.InfectedTotal;
                    previousDataPoint.DeathsTotal += dataPoint.DeathsTotal;
                    previousDataPoint.RecoveredTotal += dataPoint.RecoveredTotal;
                }
                else {
                    result.Add(dataPoint.Country, dataPoint);
                }
            }

            foreach (var dataPoint in result.Select(x => x.Value)) {
                _infectionSpreadDataPointRepository.Insert(unitOfWork, dataPoint);
            }
        }

        private bool TryParseDataPointFromLine(Dictionary<string, string> line, out InfectionSpreadDataPointDao dataPoint) {
            dataPoint = new InfectionSpreadDataPointDao();

            if (!TryParseCountryFromLine(line, out var country)) {
                return false;
            }

            if (!TryParseConfirmedFromLine(line, out var confirmed)) {
                return false;
            }

            if (!TryParseDeathsFromLine(line, out var deaths)) {
                return false;
            }

            if (!TryParseRecoveredFromLine(line, out var recovered)) {
                return false;
            }

            dataPoint.Country = country;
            dataPoint.InfectedTotal = confirmed;
            dataPoint.DeathsTotal = deaths;
            dataPoint.RecoveredTotal = recovered;

            return true;
        }

        private bool TryParseConfirmedFromLine(Dictionary<string, string> line, out int confirmed) {
            if (!line.TryGetValue("Confirmed", out var value)) {
                _logger.Warn($"failed to parse confirmed from line with headers {string.Join(";", line.Select(x => x.Key))}");
                confirmed = 0;
                return false;
            }

            if (string.IsNullOrEmpty(value)) {
                confirmed = 0;
                return true;
            }

            if (!int.TryParse(value, out confirmed)) {
                _logger.Warn($"unable to parse {value} to an integer");
                return false;
            }

            return true;
        }

        private bool TryParseDeathsFromLine(Dictionary<string, string> line, out int deaths) {
            if (!line.TryGetValue("Deaths", out var value)) {
                _logger.Warn($"failed to parse deaths from line with headers {string.Join(";", line.Select(x => x.Key))}");
                deaths = 0;
                return false;
            }

            if (string.IsNullOrEmpty(value)) {
                deaths = 0;
                return true;
            }

            if (!int.TryParse(value, out deaths)) {
                _logger.Warn($"unable to parse {value} to an integer");
                return false;
            }

            return true;
        }

        private bool TryParseRecoveredFromLine(Dictionary<string, string> line, out int recovered) {
            if (!line.TryGetValue("Recovered", out var value)) {
                _logger.Warn($"failed to parse recovered from line with headers {string.Join(";", line.Select(x => x.Key))}");
                recovered = 0;
                return false;
            }

            if (string.IsNullOrEmpty(value)) {
                recovered = 0;
                return true;
            }

            if (!int.TryParse(value, out recovered)) {
                _logger.Warn($"unable to parse {value} to an integer");
                return false;
            }

            return true;
        }

        private bool TryParseCountryFromLine(Dictionary<string, string> line, out CountryType country) {
            var success = false;
            string countryString;

            success = line.TryGetValue("Country/Region", out countryString);
            if (!success) {
                success = line.TryGetValue("Country_Region", out countryString);
            }

            if (!success) {
                _logger.Warn($"failed to parse country from line with headers {string.Join(";", line.Select(x => x.Key))}");
                country = CountryType.Invalid;
                return false;
            }

            switch (countryString) {
                case "Hong Kong":
                case "Macau":
                case "Mainland China":
                    country = CountryType.China;
                    break;
                case "Taiwan":
                    country = CountryType.Taiwan;
                    break;
                case "US":
                    country = CountryType.Usa;
                    break;
                case "Japan":
                    country = CountryType.Japan;
                    break;
                case "Thailand":
                    country = CountryType.Thailand;
                    break;
                case "South Korea":
                    country = CountryType.SouthKorea;
                    break;
                default:
                    _logger.Warn($"unable to parse the country {countryString}");
                    country = CountryType.Invalid;
                    return false;
            }

            return true;
        }
    }
}
