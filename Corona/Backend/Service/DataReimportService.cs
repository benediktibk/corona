using System;
using System.Collections.Generic;
using System.Globalization;
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

                if (result.TryGetValue(dataPoint.CountryId, out var previousDataPoint)) {
                    previousDataPoint.InfectedTotal += dataPoint.InfectedTotal;
                    previousDataPoint.DeathsTotal += dataPoint.DeathsTotal;
                    previousDataPoint.RecoveredTotal += dataPoint.RecoveredTotal;

                    if (dataPoint.Date > previousDataPoint.Date) {
                        previousDataPoint.Date = dataPoint.Date;
                    }
                }
                else {
                    result.Add(dataPoint.CountryId, dataPoint);
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

            if (!TryParseLastUpdatedFromLine(line, out var lastUpdated)) {
                return false;
            }

            dataPoint.CountryId = country;
            dataPoint.InfectedTotal = confirmed;
            dataPoint.DeathsTotal = deaths;
            dataPoint.RecoveredTotal = recovered;
            dataPoint.Date = lastUpdated;

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

        private bool TryParseLastUpdatedFromLine(Dictionary<string, string> line, out DateTime lastUpdated) {
            var success = false;
            string valueAsString;

            success = line.TryGetValue("Last Update", out valueAsString);

            if (!success) {
                success = line.TryGetValue("Last_Update", out valueAsString);
            }

            if (!success) {
                _logger.Warn($"failed to parse last updated from line with headers {string.Join(";", line.Select(x => x.Key))}");
                lastUpdated = new DateTime();
                return false;
            }

            if (!DateTime.TryParse(valueAsString, CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.None, out lastUpdated)) {
                _logger.Warn($"unable to parse {valueAsString} to a DateTime");
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
                case "China":
                    country = CountryType.China;
                    break;
                case "Taiwan":
                case "Taiwan*":
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
                case "Austria":
                    country = CountryType.Austria;
                    break;
                case "Afghanistan":
                    country = CountryType.Afghanistan;
                    break;
                case "Albania":
                    country = CountryType.Albania;
                    break;
                case "Algeria":
                    country = CountryType.Algeria;
                    break;
                case "Andorra":
                    country = CountryType.Andorra;
                    break;
                case "Angola":
                    country = CountryType.Angola;
                    break;
                case "Antigua and Barbuda":
                    country = CountryType.AntiguaAndBarbuda;
                    break;
                case "Argentina":
                    country = CountryType.Argentina;
                    break;
                case "Armenia":
                    country = CountryType.Armenia;
                    break;
                case "Australia":
                    country = CountryType.Australia;
                    break;
                case "Azerbaijan":
                    country = CountryType.Azerbaijan;
                    break;
                case "Bahamas":
                    country = CountryType.Bahamas;
                    break;
                case "Bahrain":
                    country = CountryType.Bahrain;
                    break;
                case "Bangladesh":
                    country = CountryType.Bangladesh;
                    break;
                case "Barbados":
                    country = CountryType.Barbados;
                    break;
                case "Belarus":
                    country = CountryType.Belarus;
                    break;
                case "Belgium":
                    country = CountryType.Belgium;
                    break;
                case "Belize":
                    country = CountryType.Belize;
                    break;
                case "Benin":
                    country = CountryType.Benin;
                    break;
                case "Bhutan":
                    country = CountryType.Bhutan;
                    break;
                case "Bolivia":
                    country = CountryType.Bolivia;
                    break;
                case "Bosnia and Herzegovina":
                    country = CountryType.BosniaAndHerzegovina;
                    break;
                case "Brazil":
                    country = CountryType.Brazil;
                    break;
                case "Brunei":
                    country = CountryType.Brunei;
                    break;
                case "Bulgaria":
                    country = CountryType.Bulgaria;
                    break;
                case "Burkina Faso":
                    country = CountryType.BurkinaFaso;
                    break;
                case "Burma":
                    country = CountryType.Burma;
                    break;
                case "Cabo Verde":
                    country = CountryType.CaboVerde;
                    break;
                case "Cambodia":
                    country = CountryType.Cambodia;
                    break;
                case "Cameroon":
                    country = CountryType.Cameroon;
                    break;
                case "Canada":
                    country = CountryType.Canada;
                    break;
                case "Central African Republic":
                    country = CountryType.CentralAfricanRepublic;
                    break;
                case "Chad":
                    country = CountryType.Chad;
                    break;
                case "Chile":
                    country = CountryType.Chile;
                    break;
                case "Colombia":
                    country = CountryType.Colombia;
                    break;
                case "Congo (Brazzaville)":
                    country = CountryType.CongoBrazzaville;
                    break;
                case "Congo (Kinshasa)":
                    country = CountryType.CongoKinshasa;
                    break;
                case "Costa Rica":
                    country = CountryType.CostaRica;
                    break;
                case "Cote d'Ivoire":
                    country = CountryType.CotedIvoire;
                    break;
                case "Croatia":
                    country = CountryType.Croatia;
                    break;
                case "Cuba":
                    country = CountryType.Cuba;
                    break;
                case "Cyprus":
                    country = CountryType.Cyprus;
                    break;
                case "Czechia":
                    country = CountryType.Czechia;
                    break;
                case "Denmark":
                    country = CountryType.Denmark;
                    break;
                case "Diamond Princess":
                    country = CountryType.DiamondPrincess;
                    break;
                case "Djibouti":
                    country = CountryType.Djibouti;
                    break;
                case "Dominica":
                    country = CountryType.Dominica;
                    break;
                case "Dominican Republic":
                    country = CountryType.DominicanRepublic;
                    break;
                case "Ecuador":
                    country = CountryType.Ecuador;
                    break;
                case "Egypt":
                    country = CountryType.Egypt;
                    break;
                case "El Salvador":
                    country = CountryType.ElSalvador;
                    break;
                case "Equatorial Guinea":
                    country = CountryType.EquatorialGuinea;
                    break;
                case "Eritrea":
                    country = CountryType.Eritrea;
                    break;
                case "Estonia":
                    country = CountryType.Estonia;
                    break;
                case "Eswatini":
                    country = CountryType.Eswatini;
                    break;
                case "Ethiopia":
                    country = CountryType.Ethiopia;
                    break;
                case "Fiji":
                    country = CountryType.Fiji;
                    break;
                case "Finland":
                    country = CountryType.Finland;
                    break;
                case "France":
                    country = CountryType.France;
                    break;
                case "Gabon":
                    country = CountryType.Gabon;
                    break;
                case "Gambia":
                    country = CountryType.Gambia;
                    break;
                case "Georgia":
                    country = CountryType.Georgia;
                    break;
                case "Germany":
                    country = CountryType.Germany;
                    break;
                case "Ghana":
                    country = CountryType.Ghana;
                    break;
                case "Greece":
                    country = CountryType.Greece;
                    break;
                case "Grenada":
                    country = CountryType.Grenada;
                    break;
                case "Guatemala":
                    country = CountryType.Guatemala;
                    break;
                case "Guinea":
                    country = CountryType.Guinea;
                    break;
                case "Guinea-Bissau":
                    country = CountryType.GuineaBissau;
                    break;
                case "Guyana":
                    country = CountryType.Guyana;
                    break;
                case "Haiti":
                    country = CountryType.Haiti;
                    break;
                case "Holy See":
                    country = CountryType.HolySee;
                    break;
                case "Honduras":
                    country = CountryType.Honduras;
                    break;
                case "Hungary":
                    country = CountryType.Hungary;
                    break;
                case "Iceland":
                    country = CountryType.Iceland;
                    break;
                case "India":
                    country = CountryType.India;
                    break;
                case "Indonesia":
                    country = CountryType.Indonesia;
                    break;
                case "Iran":
                    country = CountryType.Iran;
                    break;
                case "Iraq":
                    country = CountryType.Iraq;
                    break;
                case "Ireland":
                    country = CountryType.Ireland;
                    break;
                case "Israel":
                    country = CountryType.Israel;
                    break;
                case "Italy":
                    country = CountryType.Italy;
                    break;
                case "Jamaica":
                    country = CountryType.Jamaica;
                    break;
                case "Jordan":
                    country = CountryType.Jordan;
                    break;
                case "Kazakhstan":
                    country = CountryType.Kazakhstan;
                    break;
                case "Kenya":
                    country = CountryType.Kenya;
                    break;
                case "Kosovo":
                    country = CountryType.Kosovo;
                    break;
                case "Kuwait":
                    country = CountryType.Kuwait;
                    break;
                case "Kyrgyzstan":
                    country = CountryType.Kyrgyzstan;
                    break;
                case "Laos":
                    country = CountryType.Laos;
                    break;
                case "Latvia":
                    country = CountryType.Latvia;
                    break;
                case "Lebanon":
                    country = CountryType.Lebanon;
                    break;
                case "Liberia":
                    country = CountryType.Liberia;
                    break;
                case "Libya":
                    country = CountryType.Libya;
                    break;
                case "Liechtenstein":
                    country = CountryType.Liechtenstein;
                    break;
                case "Lithuania":
                    country = CountryType.Lithuania;
                    break;
                case "Luxembourg":
                    country = CountryType.Luxembourg;
                    break;
                case "Madagascar":
                    country = CountryType.Madagascar;
                    break;
                case "Malaysia":
                    country = CountryType.Malaysia;
                    break;
                case "Maldives":
                    country = CountryType.Maldives;
                    break;
                case "Mali":
                    country = CountryType.Mali;
                    break;
                case "Malta":
                    country = CountryType.Malta;
                    break;
                case "Mauritania":
                    country = CountryType.Mauritania;
                    break;
                case "Mauritius":
                    country = CountryType.Mauritius;
                    break;
                case "Mexico":
                    country = CountryType.Mexico;
                    break;
                case "Moldova":
                    country = CountryType.Moldova;
                    break;
                case "Monaco":
                    country = CountryType.Monaco;
                    break;
                case "Mongolia":
                    country = CountryType.Mongolia;
                    break;
                case "Montenegro":
                    country = CountryType.Montenegro;
                    break;
                case "Morocco":
                    country = CountryType.Morocco;
                    break;
                case "Mozambique":
                    country = CountryType.Mozambique;
                    break;
                case "Namibia":
                    country = CountryType.Namibia;
                    break;
                case "Nepal":
                    country = CountryType.Nepal;
                    break;
                case "Netherlands":
                    country = CountryType.Netherlands;
                    break;
                case "New Zealand":
                    country = CountryType.NewZealand;
                    break;
                case "Nicaragua":
                    country = CountryType.Nicaragua;
                    break;
                case "Niger":
                    country = CountryType.Niger;
                    break;
                case "Nigeria":
                    country = CountryType.Nigeria;
                    break;
                case "North Macedonia":
                    country = CountryType.NorthMacedonia;
                    break;
                case "Norway":
                    country = CountryType.Norway;
                    break;
                case "Oman":
                    country = CountryType.Oman;
                    break;
                case "Pakistan":
                    country = CountryType.Pakistan;
                    break;
                case "Panama":
                    country = CountryType.Panama;
                    break;
                case "Papua New Guinea":
                    country = CountryType.PapuaNewGuinea;
                    break;
                case "Paraguay":
                    country = CountryType.Paraguay;
                    break;
                case "Peru":
                    country = CountryType.Peru;
                    break;
                case "Philippines":
                    country = CountryType.Philippines;
                    break;
                case "Poland":
                    country = CountryType.Poland;
                    break;
                case "Portugal":
                    country = CountryType.Portugal;
                    break;
                case "Qatar":
                    country = CountryType.Qatar;
                    break;
                case "Romania":
                    country = CountryType.Romania;
                    break;
                case "Russia":
                    country = CountryType.Russia;
                    break;
                case "Rwanda":
                    country = CountryType.Rwanda;
                    break;
                case "Saint Kitts and Nevis":
                    country = CountryType.SaintKittsAndNevis;
                    break;
                case "Saint Lucia":
                    country = CountryType.SaintLucia;
                    break;
                case "Saint Vincent and the Grenadines":
                    country = CountryType.SaintVincentAndTheGrenadines;
                    break;
                case "San Marino":
                    country = CountryType.SanMarino;
                    break;
                case "Saudi Arabia":
                    country = CountryType.SaudiArabia;
                    break;
                case "Senegal":
                    country = CountryType.Senegal;
                    break;
                case "Serbia":
                    country = CountryType.Serbia;
                    break;
                case "Seychelles":
                    country = CountryType.Seychelles;
                    break;
                case "Singapore":
                    country = CountryType.Singapore;
                    break;
                case "Slovakia":
                    country = CountryType.Slovakia;
                    break;
                case "Slovenia":
                    country = CountryType.Slovenia;
                    break;
                case "Somalia":
                    country = CountryType.Somalia;
                    break;
                case "South Africa":
                    country = CountryType.SouthAfrica;
                    break;
                case "Spain":
                    country = CountryType.Spain;
                    break;
                case "Sri Lanka":
                    country = CountryType.SriLanka;
                    break;
                case "Sudan":
                    country = CountryType.Sudan;
                    break;
                case "Suriname":
                    country = CountryType.Suriname;
                    break;
                case "Sweden":
                    country = CountryType.Sweden;
                    break;
                case "Switzerland":
                    country = CountryType.Switzerland;
                    break;
                case "Syria":
                    country = CountryType.Syria;
                    break;
                case "Tanzania":
                    country = CountryType.Tanzania;
                    break;
                case "Timor-Leste":
                    country = CountryType.TimorLeste;
                    break;
                case "Togo":
                    country = CountryType.Togo;
                    break;
                case "Trinidad and Tobago":
                    country = CountryType.TrinidadAndTobago;
                    break;
                case "Tunisia":
                    country = CountryType.Tunisia;
                    break;
                case "Turkey":
                    country = CountryType.Turkey;
                    break;
                case "Uganda":
                    country = CountryType.Uganda;
                    break;
                case "Ukraine":
                    country = CountryType.Ukraine;
                    break;
                case "United Arab Emirates":
                    country = CountryType.UnitedArabEmirates;
                    break;
                case "United Kingdom":
                    country = CountryType.UnitedKingdom;
                    break;
                case "Uruguay":
                    country = CountryType.Uruguay;
                    break;
                case "Uzbekistan":
                    country = CountryType.Uzbekistan;
                    break;
                case "Venezuela":
                    country = CountryType.Venezuela;
                    break;
                case "Vietnam":
                    country = CountryType.Vietnam;
                    break;
                case "West Bank and Gaza":
                    country = CountryType.WestBankAndGaza;
                    break;
                case "Zambia":
                    country = CountryType.Zambia;
                    break;
                case "Zimbabwe":
                    country = CountryType.Zimbabwe;
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
