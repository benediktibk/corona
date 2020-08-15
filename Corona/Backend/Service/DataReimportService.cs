using Backend.Repository;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Service {
    public class DataReimportService : IDataReimportService {
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
            ISettings settings) {
            _csvFileRepository = csvFileRepository;
            _sourceFilePath = settings.LocalPath;
            _gitRepoUrl = settings.GitRepo;
            _infectionSpreadDataPointRepository = infectionSpreadDataPointRepository;
            _gitRepository = gitRepository;
        }

        public bool ReimportAll(IUnitOfWork unitOfWork) {
            var result = _gitRepository.FetchLatestCommit(_gitRepoUrl, _sourceFilePath);

            if (!result) {
                _logger.Error("skipping the update process, could not fetch the latest data");
                return false;
            }

            _logger.Info("search for daily reports");
            var files = _csvFileRepository.ListAllCsvFilesIn($"{_sourceFilePath}/csse_covid_19_data/csse_covid_19_daily_reports");

            _logger.Info($"found {files.Count} daily reports, starting to import them");
            var dataPoints = new ConcurrentBag<List<InfectionSpreadDataPointDao>>();
            Parallel.ForEach(files, file => {
                _logger.Info($"importing {file}");
                dataPoints.Add(ParseFileContent(unitOfWork, file));
            });
            var dataPointsAggregated = dataPoints.Aggregate(seed: new List<InfectionSpreadDataPointDao>(), func: (aggregationResult, item) => {
                aggregationResult.AddRange(item);
                return aggregationResult;
            });

            _logger.Info("reimporting data points");
            _infectionSpreadDataPointRepository.DeleteAll(unitOfWork);
            _infectionSpreadDataPointRepository.Insert(unitOfWork, dataPointsAggregated);

            _logger.Info("successfully executed the reimport");
            return true;
        }

        private List<InfectionSpreadDataPointDao> ParseFileContent(IUnitOfWork unitOfWork, string file) {
            var content = _csvFileRepository.ReadFile(file);
            var result = new Dictionary<CountryType, InfectionSpreadDataPointDao>();

            _logger.Info($"parsing content of file {file}");
            foreach (var line in content.Lines) {
                var success = true;
                var dataPoint = new InfectionSpreadDataPointDao();

                if (!TryParseCountryFromLine(content, line, out var country)) {
                    success = false;
                } 

                if (!TryParseConfirmedFromLine(content, line, out var confirmed)) {
                    success = false;
                }

                if (!TryParseDeathsFromLine(content, line, out var deaths)) {
                    success = false;
                }

                if (!TryParseRecoveredFromLine(content, line, out var recovered)) {
                    success = false;
                } 

                if (!TryParseLastUpdatedFromLine(content, line, out var lastUpdated)) {
                    success = false;
                } 

                if (!success) {
                    _logger.Warn($"unable to parse one line in file {file}, skipping it");
                    continue;
                }

                dataPoint.CountryId = country;
                dataPoint.InfectedTotal = confirmed;
                dataPoint.DeathsTotal = deaths;
                dataPoint.RecoveredTotal = recovered;
                dataPoint.Date = lastUpdated;

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

            return result.Select(x => x.Value).ToList();
        }

        private bool TryParseConfirmedFromLine(CsvFile file, CsvFileLine line, out int confirmed) {
            if (!file.TryGetColumnIndexOfHeader("Confirmed", out var headerIndex)) {
                _logger.Warn("failed to parse confirmed from line");
                confirmed = 0;
                return false;
            }

            var value = line.GetValue(headerIndex);

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

        private bool TryParseDeathsFromLine(CsvFile file, CsvFileLine line, out int deaths) {
            if (!file.TryGetColumnIndexOfHeader("Deaths", out var headerIndex)) {
                _logger.Warn("failed to parse deaths from line");
                deaths = 0;
                return false;
            }

            var value = line.GetValue(headerIndex);

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

        private bool TryParseRecoveredFromLine(CsvFile file, CsvFileLine line, out int recovered) {
            if (!file.TryGetColumnIndexOfHeader("Recovered", out var headerIndex)) {
                _logger.Warn("failed to parse recovered from line");
                recovered = 0;
                return false;
            }

            var value = line.GetValue(headerIndex);

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

        private bool TryParseLastUpdatedFromLine(CsvFile file, CsvFileLine line, out DateTime lastUpdated) {
            var success = file.TryGetColumnIndexOfHeader("Last Update", out var headerIndex);

            if (!success) {
                success = file.TryGetColumnIndexOfHeader("Last_Update", out headerIndex);
            }

            if (!success) {
                _logger.Warn("failed to parse last updated from line");
                lastUpdated = new DateTime();
                return false;
            }

            var valueAsString = line.GetValue(headerIndex);

            if (!DateTime.TryParse(valueAsString, CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.None, out lastUpdated)) {
                _logger.Warn($"unable to parse {valueAsString} to a DateTime");
                return false;
            }

            return true;
        }

        private bool TryParseCountryFromLine(CsvFile file, CsvFileLine line, out CountryType country) {

            var success = file.TryGetColumnIndexOfHeader("Country/Region", out var headerIndex);
            if (!success) {
                success = file.TryGetColumnIndexOfHeader("Country_Region", out headerIndex);
            }

            if (!success) {
                _logger.Warn("failed to parse country from line");
                country = CountryType.Invalid;
                return false;
            }

            var countryString = line.GetValue(headerIndex);

            switch (countryString) {
                case "Hong Kong":
                case "Hong Kong SAR":
                case "Macau":
                case "Macao SAR":
                case "Mainland China":
                case "China":
                    country = CountryType.China;
                    break;
                case "Taiwan":
                case "Taiwan*":
                case "Taipei and environs":
                    country = CountryType.Taiwan;
                    break;
                case "US":
                case "NE":
                case "NE (from Diamond Princess)":
                case " NE (From Diamond Princess)":
                case "TX":
                case "TX (from Diamond Princess)":
                case " TX (From Diamond Princess)":
                case "CA":
                case "CA (from Diamond Princess)":
                case " CA (From Diamond Princess)":
                case "Jersey":
                case "Puerto Rico":
                case "Guam":
                    country = CountryType.Usa;
                    break;
                case "Japan":
                    country = CountryType.Japan;
                    break;
                case "Thailand":
                    country = CountryType.Thailand;
                    break;
                case "South Korea":
                case "Korea South":
                case "Republic of Korea":
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
                case " Azerbaijan":
                    country = CountryType.Azerbaijan;
                    break;
                case "Bahamas":
                case "The Bahamas":
                case "Bahamas The":
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
                case "Aruba":
                    country = CountryType.Aruba;
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
                case "Cape Verde":
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
                case "Republic of Congo":
                case "Republic of the Congo":
                    country = CountryType.RepublicOfCongo;
                    break;
                case "Costa Rica":
                    country = CountryType.CostaRica;
                    break;
                case "Cote d'Ivoire":
                case "Ivory Coast":
                    country = CountryType.CotedIvoire;
                    break;
                case "Croatia":
                    country = CountryType.Croatia;
                    break;
                case "Cuba":
                    country = CountryType.Cuba;
                    break;
                case "Mayotte":
                    country = CountryType.Mayotte;
                    break;
                case "Cyprus":
                    country = CountryType.Cyprus;
                    break;
                case "Czechia":
                case "Czech Republic":
                    country = CountryType.Czechia;
                    break;
                case "Denmark":
                    country = CountryType.Denmark;
                    break;
                case "Diamond Princess":
                case "Others":
                case "Cruise Ship":
                case "MS Zaandam":
                    country = CountryType.Others;
                    break;
                case "Djibouti":
                    country = CountryType.Djibouti;
                    break;
                case "Cayman Islands":
                    country = CountryType.CaymanIslands;
                    break;
                case "Guadeloupe":
                    country = CountryType.Guadeloupe;
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
                case "Saint Barthelemy":
                    country = CountryType.SaintBarthelemy;
                    break;
                case "Martinique":
                    country = CountryType.Martinique;
                    break;
                case "St. Martin":
                case "Saint Martin":
                    country = CountryType.SaintMartin;
                    break;
                case "French Guiana":
                    country = CountryType.FrenchGuiana;
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
                case "Gambia The":
                case "The Gambia":
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
                case "Iran (Islamic Republic of)":
                    country = CountryType.Iran;
                    break;
                case "Iraq":
                    country = CountryType.Iraq;
                    break;
                case "Ireland":
                case "Republic of Ireland":
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
                case "Republic of Moldova":
                    country = CountryType.Moldova;
                    break;
                case "Monaco":
                    country = CountryType.Monaco;
                    break;
                case "Mongolia":
                    country = CountryType.Mongolia;
                    break;
                case "Channel Islands":
                    country = CountryType.ChannelIslands;
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
                case "Russian Federation":
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
                case "East Timor":
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
                case "Gibraltar":
                    country = CountryType.Gibraltar;
                    break;
                case "United Kingdom":
                case "UK":
                case "North Ireland":
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
                case "Viet Nam":
                    country = CountryType.Vietnam;
                    break;
                case "West Bank and Gaza":
                case "Palestine":
                case "Palestinian territory":
                case "occupied Palestinian territory":
                    country = CountryType.WestBankAndGaza;
                    break;
                case "Zambia":
                    country = CountryType.Zambia;
                    break;
                case "Reunion":
                    country = CountryType.Reunion;
                    break;
                case "Zimbabwe":
                    country = CountryType.Zimbabwe;
                    break;
                case "Vatican City":
                case "Vatican":
                    country = CountryType.Vatican;
                    break;
                case "Faroe Islands":
                    country = CountryType.FaroeIslands;
                    break;
                case "Greenland":
                    country = CountryType.Greenland;
                    break;
                case "Guernsey":
                    country = CountryType.Guernsey;
                    break;
                case "Curacao":
                    country = CountryType.Curacao;
                    break;
                case "Burundi":
                    country = CountryType.Burundi;
                    break;
                case "Botswana":
                    country = CountryType.Botswana;
                    break;
                case "Sierra Leone":
                    country = CountryType.SierraLeone;
                    break;
                case "Malawi":
                    country = CountryType.Malawi;
                    break;
                case "Sao Tome and Principe":
                    country = CountryType.SaoTomeAndPrincipe;
                    break;
                case "South Sudan":
                    country = CountryType.SouthSudan;
                    break;
                case "Western Sahara":
                    country = CountryType.WesternSahara;
                    break;
                case "Yemen":
                    country = CountryType.Yemen;
                    break;
                case "Comoros":
                    country = CountryType.Comoros;
                    break;
                case "Tajikistan":
                    country = CountryType.Tajikistan;
                    break;
                case "Lesotho":
                    country = CountryType.Lesotho;
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
