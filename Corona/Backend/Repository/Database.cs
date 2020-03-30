using System.Collections.Generic;

namespace Backend.Repository
{
    public class Database : IDatabase 
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICountryDetailedRepository _countryDetailedRepository;

        public Database(ICountryRepository countryRepository, ICountryDetailedRepository countryDetailedRepository) {
            _countryRepository = countryRepository;
            _countryDetailedRepository = countryDetailedRepository;
        }

        public void Initialize(IUnitOfWork unitOfWork) {
            DeleteAllTables(unitOfWork);
            CreateTables(unitOfWork);
            InsertCountries(unitOfWork);
        }

        private void InsertCountries(IUnitOfWork unitOfWork) {
            var countries = new List<CountryDao> {
                new CountryDao {
                    Id = CountryType.Afghanistan,
                    Name = "Afghanistan"
                },
                new CountryDao {
                    Id = CountryType.Albania,
                    Name = "Albania"
                },
                new CountryDao {
                    Id = CountryType.Algeria,
                    Name = "Algeria"
                },
                new CountryDao {
                    Id = CountryType.Andorra,
                    Name = "Andorra"
                },
                new CountryDao {
                    Id = CountryType.Angola,
                    Name = "Angola"
                },
                new CountryDao {
                    Id = CountryType.AntiguaAndBarbuda,
                    Name = "Antigua and Barbuda"
                },
                new CountryDao {
                    Id = CountryType.Argentina,
                    Name = "Argentina"
                },
                new CountryDao {
                    Id = CountryType.Armenia,
                    Name = "Armenia"
                },
                new CountryDao {
                    Id = CountryType.Australia,
                    Name = "Australia"
                },
                new CountryDao {
                    Id = CountryType.Austria,
                    Name = "Austria"
                },
                new CountryDao {
                    Id = CountryType.Azerbaijan,
                    Name = "Azerbaijan"
                },
                new CountryDao {
                    Id = CountryType.Bahamas,
                    Name = "Bahamas"
                },
                new CountryDao {
                    Id = CountryType.Bahrain,
                    Name = "Bahrain"
                },
                new CountryDao {
                    Id = CountryType.Bangladesh,
                    Name = "Bangladesh"
                },
                new CountryDao {
                    Id = CountryType.Barbados,
                    Name = "Barbados"
                },
                new CountryDao {
                    Id = CountryType.Belarus,
                    Name = "Belarus"
                },
                new CountryDao {
                    Id = CountryType.Belgium,
                    Name = "Belgium"
                },
                new CountryDao {
                    Id = CountryType.Belize,
                    Name = "Belize"
                },
                new CountryDao {
                    Id = CountryType.Benin,
                    Name = "Benin"
                },
                new CountryDao {
                    Id = CountryType.Bhutan,
                    Name = "Bhutan"
                },
                new CountryDao {
                    Id = CountryType.Bolivia,
                    Name = "Bolivia"
                },
                new CountryDao {
                    Id = CountryType.BosniaAndHerzegovina,
                    Name = "Bosnia and Herzegovina"
                },
                new CountryDao {
                    Id = CountryType.Brazil,
                    Name = "Brazil"
                },
                new CountryDao {
                    Id = CountryType.Brunei,
                    Name = "Brunei"
                },
                new CountryDao {
                    Id = CountryType.Bulgaria,
                    Name = "Bulgaria"
                },
                new CountryDao {
                    Id = CountryType.BurkinaFaso,
                    Name = "Faso"
                },
                new CountryDao {
                    Id = CountryType.Burma,
                    Name = "Burma"
                },
                new CountryDao {
                    Id = CountryType.CaboVerde,
                    Name = "CaboVerde"
                },
                new CountryDao {
                    Id = CountryType.Cambodia,
                    Name = "Cambodia"
                },
                new CountryDao {
                    Id = CountryType.Cameroon,
                    Name = "Cameroon"
                },
                new CountryDao {
                    Id = CountryType.Canada,
                    Name = "Canada"
                },
                new CountryDao {
                    Id = CountryType.CentralAfricanRepublic,
                    Name = "Central African Republic"
                },
                new CountryDao {
                    Id = CountryType.Chad,
                    Name = "Chad"
                },
                new CountryDao {
                    Id = CountryType.Chile,
                    Name = "Chile"
                },
                new CountryDao {
                    Id = CountryType.China,
                    Name = "China"
                },
                new CountryDao {
                    Id = CountryType.Colombia,
                    Name = "Colombia"
                },
                new CountryDao {
                    Id = CountryType.CongoBrazzaville,
                    Name = "Congo (Brazzaville)"
                },
                new CountryDao {
                    Id = CountryType.CongoKinshasa,
                    Name = "Congo (Kinshasa)"
                },
                new CountryDao {
                    Id = CountryType.CostaRica,
                    Name = "Costa Rica"
                },
                new CountryDao {
                    Id = CountryType.CotedIvoire,
                    Name = "Cote d'Ivoire"
                },
                new CountryDao {
                    Id = CountryType.Croatia,
                    Name = "Croatia"
                },
                new CountryDao {
                    Id = CountryType.Cuba,
                    Name = "Cuba"
                },                
                new CountryDao {
                    Id = CountryType.Cyprus,
                    Name = "Cyprus"
                },
                new CountryDao {
                    Id = CountryType.Czechia,
                    Name = "Czechia"
                },
                new CountryDao {
                    Id = CountryType.Denmark,
                    Name = "Denmark"
                },
                new CountryDao {
                    Id = CountryType.Others,
                    Name = "Others"
                },
                new CountryDao {
                    Id = CountryType.Djibouti,
                    Name = "Djibouti"
                },
                new CountryDao {
                    Id = CountryType.Dominica,
                    Name = "Dominica"
                },
                new CountryDao {
                    Id = CountryType.DominicanRepublic,
                    Name = "Dominican Republic"
                },
                new CountryDao {
                    Id = CountryType.Ecuador,
                    Name = "Ecuador"
                },
                new CountryDao {
                    Id = CountryType.Egypt,
                    Name = "Egypt"
                },
                new CountryDao {
                    Id = CountryType.ElSalvador,
                    Name = "El Salvador"
                },
                new CountryDao {
                    Id = CountryType.EquatorialGuinea,
                    Name = "Equatorial Guinea"
                },
                new CountryDao {
                    Id = CountryType.Eritrea,
                    Name = "Eritrea"
                },
                new CountryDao {
                    Id = CountryType.Estonia,
                    Name = "Estonia"
                },
                new CountryDao {
                    Id = CountryType.Eswatini,
                    Name = "Eswatini"
                },
                new CountryDao {
                    Id = CountryType.Ethiopia,
                    Name = "Ethiopia"
                },
                new CountryDao {
                    Id = CountryType.Fiji,
                    Name = "Fiji"
                },                
                new CountryDao {
                    Id = CountryType.Finland,
                    Name = "Finland"
                },
                new CountryDao {
                    Id = CountryType.France,
                    Name = "France"
                },
                new CountryDao {
                    Id = CountryType.Gabon,
                    Name = "Gabon"
                },
                new CountryDao {
                    Id = CountryType.Gambia,
                    Name = "Gambia"
                },
                new CountryDao {
                    Id = CountryType.Georgia,
                    Name = "Georgia"
                },
                new CountryDao {
                    Id = CountryType.Germany,
                    Name = "Germany"
                },
                new CountryDao {
                    Id = CountryType.Ghana,
                    Name = "Ghana"
                },
                new CountryDao {
                    Id = CountryType.Greece,
                    Name = "Greece"
                },
                new CountryDao {
                    Id = CountryType.Grenada,
                    Name = "Grenada"
                },
                new CountryDao {
                    Id = CountryType.Guatemala,
                    Name = "Guatemala"
                },
                new CountryDao {
                    Id = CountryType.Guinea,
                    Name = "Guinea"
                },
                new CountryDao {
                    Id = CountryType.GuineaBissau,
                    Name = "Guinea-Bissau"
                },
                new CountryDao {
                    Id = CountryType.Guyana,
                    Name = "Guyana"
                },
                new CountryDao {
                    Id = CountryType.Haiti,
                    Name = "Haiti"
                },
                new CountryDao {
                    Id = CountryType.HolySee,
                    Name = "Holy See"
                },
                new CountryDao {
                    Id = CountryType.Honduras,
                    Name = "Honduras"
                },
                new CountryDao {
                    Id = CountryType.Hungary,
                    Name = "Hungary"
                },
                new CountryDao {
                    Id = CountryType.Iceland,
                    Name = "Iceland"
                },
                new CountryDao {
                    Id = CountryType.India,
                    Name = "India"
                },
                new CountryDao {
                    Id = CountryType.Indonesia,
                    Name = "Indonesia"
                },
                new CountryDao {
                    Id = CountryType.Iran,
                    Name = "Iran"
                },
                new CountryDao {
                    Id = CountryType.Iraq,
                    Name = "Iraq"
                },
                new CountryDao {
                    Id = CountryType.Ireland,
                    Name = "Ireland"
                },
                new CountryDao {
                    Id = CountryType.Israel,
                    Name = "Israel"
                },
                new CountryDao {
                    Id = CountryType.Italy,
                    Name = "Italy"
                },
                new CountryDao {
                    Id = CountryType.Jamaica,
                    Name = "Jamaica"
                },
                new CountryDao {
                    Id = CountryType.Japan,
                    Name = "Japan"
                },
                new CountryDao {
                    Id = CountryType.Jordan,
                    Name = "Jordan"
                },
                new CountryDao {
                    Id = CountryType.Kazakhstan,
                    Name = "Kazakhstan"
                },
                new CountryDao {
                    Id = CountryType.Kenya,
                    Name = "Kenya"
                },
                new CountryDao {
                    Id = CountryType.SouthKorea,
	                Name = "South Korea"
                },
                new CountryDao {
	                Id = CountryType.Kosovo,
	                Name = "Kosovo"
                },
                new CountryDao {
	                Id = CountryType.Kuwait,
	                Name = "Kuwait"
                },
                new CountryDao {
	                Id = CountryType.Kyrgyzstan,
	                Name = "Kyrgyzstan"
                },
                new CountryDao {
                    Id = CountryType.Laos,
                    Name = "Laos"
                },                
                new CountryDao {
	                Id = CountryType.Latvia,
	                Name = "Latvia"
                },
                new CountryDao {
	                Id = CountryType.Lebanon,
	                Name = "Lebanon"
                },
                new CountryDao {
	                Id = CountryType.Liberia,
	                Name = "Liberia"
                },
                new CountryDao {
	                Id = CountryType.Libya,
	                Name = "Libya"
                },
                new CountryDao {
	                Id = CountryType.Liechtenstein,
	                Name = "Liechtenstein"
                },
                new CountryDao {
	                Id = CountryType.Lithuania,
	                Name = "Lithuania"
                },
                new CountryDao {
	                Id = CountryType.Luxembourg,
	                Name = "Luxembourg"
                },
                new CountryDao {
	                Id = CountryType.Madagascar,
	                Name = "Madagascar"
                },
                new CountryDao {
	                Id = CountryType.Malaysia,
	                Name = "Malaysia"
                },
                new CountryDao {
	                Id = CountryType.Maldives,
	                Name = "Maldives"
                },
                new CountryDao {
                    Id = CountryType.Mali,
                    Name = "Mali"
                },                
                new CountryDao {
	                Id = CountryType.Malta,
	                Name = "Malta"
                },
                new CountryDao {
	                Id = CountryType.Mauritania,
	                Name = "Mauritania"
                },
                new CountryDao {
	                Id = CountryType.Mauritius,
	                Name = "Mauritius"
                },
                new CountryDao {
	                Id = CountryType.Mexico,
	                Name = "Mexico"
                },
                new CountryDao {
	                Id = CountryType.Moldova,
	                Name = "Moldova"
                },
                new CountryDao {
	                Id = CountryType.Monaco,
	                Name = "Monaco"
                },
                new CountryDao {
	                Id = CountryType.Mongolia,
	                Name = "Mongolia"
                },
                new CountryDao {
	                Id = CountryType.Montenegro,
	                Name = "Montenegro"
                },
                new CountryDao {
	                Id = CountryType.Morocco,
	                Name = "Morocco"
                },
                new CountryDao {
	                Id = CountryType.Mozambique,
	                Name = "Mozambique"
                },
                new CountryDao {
	                Id = CountryType.Namibia,
	                Name = "Namibia"
                },
                new CountryDao {
	                Id = CountryType.Nepal,
	                Name = "Nepal"
                },
                new CountryDao {
	                Id = CountryType.Netherlands,
	                Name = "Netherlands"
                },
                new CountryDao {
	                Id = CountryType.NewZealand,
                    Name = "New Zealand"
                },
                new CountryDao {
	                Id = CountryType.Nicaragua,
	                Name = "Nicaragua"
                },
                new CountryDao {
	                Id = CountryType.Niger,
	                Name = "Niger"
                },
                new CountryDao {
	                Id = CountryType.Nigeria,
	                Name = "Nigeria"
                },
                new CountryDao {
	                Id = CountryType.NorthMacedonia,
                    Name = "North Macedonia"
                },
                new CountryDao {
	                Id = CountryType.Norway,
	                Name = "Norway"
                },
                new CountryDao {
                    Id = CountryType.Oman,
                    Name = "Oman"
                },
                new CountryDao {
	                Id = CountryType.Pakistan,
	                Name = "Pakistan"
                },
                new CountryDao {
	                Id = CountryType.Panama,
	                Name = "Panama"
                },
                new CountryDao {
	                Id = CountryType.PapuaNewGuinea,
	                Name = "Papua New Guinea"
                },
                new CountryDao {
	                Id = CountryType.Paraguay,
	                Name = "Paraguay"
                },
                new CountryDao {
                    Id = CountryType.Peru,
                    Name = "Peru"
                },                
                new CountryDao {
	                Id = CountryType.Philippines,
	                Name = "Philippines"
                },
                new CountryDao {
	                Id = CountryType.Poland,
	                Name = "Poland"
                },
                new CountryDao {
	                Id = CountryType.Portugal,
	                Name = "Portugal"
                },
                new CountryDao {
	                Id = CountryType.Qatar,
	                Name = "Qatar"
                },
                new CountryDao {
	                Id = CountryType.Romania,
	                Name = "Romania"
                },
                new CountryDao {
	                Id = CountryType.Russia,
	                Name = "Russia"
                },
                new CountryDao {
	                Id = CountryType.Rwanda,
	                Name = "Rwanda"
                },
                new CountryDao {
	                Id = CountryType.SaintKittsAndNevis,
                    Name = "Saint Kitts and Nevis"
                },
                new CountryDao {
	                Id = CountryType.SaintLucia,
                    Name = "Saitn Lucia"
                },
                new CountryDao {
	                Id = CountryType.SaintVincentAndTheGrenadines,
	                Name = "Saint Vincent and the Grenadines"
                },
                new CountryDao {
	                Id = CountryType.SanMarino,
                    Name = "San Marino"
                },
                new CountryDao {
	                Id = CountryType.SaudiArabia,
                    Name = "Saudi Arabia"
                },
                new CountryDao {
	                Id = CountryType.Senegal,
	                Name = "Senegal"
                },
                new CountryDao {
	                Id = CountryType.Serbia,
	                Name = "Serbia"
                },
                new CountryDao {
	                Id = CountryType.Seychelles,
	                Name = "Seychelles"
                },
                new CountryDao {
	                Id = CountryType.Singapore,
	                Name = "Singapore"
                },
                new CountryDao {
	                Id = CountryType.Slovakia,
	                Name = "Slovakia"
                },
                new CountryDao {
	                Id = CountryType.Slovenia,
	                Name = "Slovenia"
                },
                new CountryDao {
	                Id = CountryType.Somalia,
	                Name = "Somalia"
                },
                new CountryDao {
	                Id = CountryType.SouthAfrica,
                    Name = "South Africa"
                },
                new CountryDao {
	                Id = CountryType.Spain,
	                Name = "Spain"
                },
                new CountryDao {
	                Id = CountryType.SriLanka,
                    Name = "Sri Lanka"
                },
                new CountryDao {
	                Id = CountryType.Sudan,
	                Name = "Sudan"
                },
                new CountryDao {
	                Id = CountryType.Suriname,
	                Name = "Suriname"
                },
                new CountryDao {
	                Id = CountryType.Sweden,
	                Name = "Sweden"
                },
                new CountryDao {
	                Id = CountryType.Switzerland,
	                Name = "Switzerland"
                },
                new CountryDao {
	                Id = CountryType.Syria,
	                Name = "Syria"
                },
                new CountryDao {
	                Id = CountryType.Taiwan,
	                Name = "Taiwan"
                },
                new CountryDao {
	                Id = CountryType.Tanzania,
	                Name = "Tanzania"
                },
                new CountryDao {
	                Id = CountryType.Thailand,
	                Name = "Thailand"
                },
                new CountryDao {
	                Id = CountryType.TimorLeste,
	                Name = "Timor-Leste"
                },
                new CountryDao {
                    Id = CountryType.Togo,
                    Name = "Togo"
                },                
                new CountryDao {
	                Id = CountryType.TrinidadAndTobago,
	                Name = "Trinidad and Tobago"
                },
                new CountryDao {
	                Id = CountryType.Tunisia,
	                Name = "Tunisia"
                },
                new CountryDao {
	                Id = CountryType.Turkey,
	                Name = "Turkey"
                },
                new CountryDao {
                    Id = CountryType.Usa,
                    Name = "USA"
                },
                new CountryDao {
	                Id = CountryType.Uganda,
	                Name = "Uganda"
                },
                new CountryDao {
	                Id = CountryType.Ukraine,
	                Name = "Ukraine"
                },
                new CountryDao {
	                Id = CountryType.UnitedArabEmirates,
	                Name = "United Arab Emirates"
                },
                new CountryDao {
	                Id = CountryType.UnitedKingdom,
                    Name = "United Kingdom"
                },
                new CountryDao {
	                Id = CountryType.Uruguay,
	                Name = "Uruguay"
                },
                new CountryDao {
	                Id = CountryType.Uzbekistan,
	                Name = "Uzbekistan"
                },
                new CountryDao {
	                Id = CountryType.Venezuela,
	                Name = "Venezuela"
                },
                new CountryDao {
	                Id = CountryType.Vietnam,
	                Name = "Vietnam"
                },
                new CountryDao {
	                Id = CountryType.WestBankAndGaza,
                    Name = "West Bank and Gaza"
                },
                new CountryDao {
	                Id = CountryType.Zambia,
	                Name = "Zambia"
                },
                new CountryDao {
	                Id = CountryType.Zimbabwe,
	                Name = "Zimbabwe"
                },
                new CountryDao {
                    Id = CountryType.Gibraltar,
                    Name = "Gibraltar"
                },
                new CountryDao {
                    Id = CountryType.Vatican,
                    Name = "Vatican"
                },
                new CountryDao {
                    Id = CountryType.FaroeIslands,
                    Name = "Faroe Islands"
                },
                new CountryDao {
                    Id = CountryType.FrenchGuiana,
                    Name = "French Guiana"
                },
                new CountryDao {
                    Id = CountryType.SaintBarthelemy,
                    Name = "Saint Barthelemy"
                },
                new CountryDao {
                    Id = CountryType.Martinique,
                    Name = "Martinique"
                },
                new CountryDao {
                    Id = CountryType.SaintMartin,
                    Name = "Saint Martin"
                },
                new CountryDao {
                    Id = CountryType.ChannelIslands,
                    Name = "Channel Islands"
                },
                new CountryDao {
                    Id = CountryType.CaymanIslands,
                    Name = "Cayman Islands"
                },
                new CountryDao {
                    Id = CountryType.Guadeloupe,
                    Name = "Guadeloupe"
                },
                new CountryDao {
                    Id = CountryType.Aruba,
                    Name = "Aruba"
                },
                new CountryDao {
                    Id = CountryType.RepublicOfCongo,
                    Name = "Republic of Congo"
                },
                new CountryDao {
                    Id = CountryType.Mayotte,
                    Name = "Mayotte"
                },
                new CountryDao {
                    Id = CountryType.Reunion,
                    Name = "Reunion"
                },
                new CountryDao {
                    Id = CountryType.Greenland,
                    Name = "Greenland"
                },
                new CountryDao {
                    Id = CountryType.Guernsey,
                    Name = "Guernsey"
                },
                new CountryDao {
                    Id = CountryType.Curacao,
                    Name = "Curacao"
                }
            };

            var countriesDetailed = new List<CountryDetailedDao> {
                new CountryDetailedDao {
                    CountryId = CountryType.Austria,
                    Inhabitants = 8822000,
                    IcuBeds = 1923,
                    MoratilityRatePerOneMillionPerDay = 2.71
                },
                new CountryDetailedDao {
                    CountryId = CountryType.Italy,
                    Inhabitants = 60480000,
                    IcuBeds = 7560,
                    MoratilityRatePerOneMillionPerDay = 2.93
                },
                new CountryDetailedDao {
                    CountryId = CountryType.Germany,
                    Inhabitants = 82790000,
                    IcuBeds = 24175,
                    MoratilityRatePerOneMillionPerDay = 3.09
                },
                new CountryDetailedDao {
                    CountryId = CountryType.UnitedKingdom,
                    Inhabitants = 66440000,
                    IcuBeds = 4385,
                    MoratilityRatePerOneMillionPerDay = 2.57
                }
            };

            unitOfWork.ExecuteDatabaseCommand(@"SET IDENTITY_INSERT Country ON");

            foreach (var country in countries) {
                _countryRepository.Insert(unitOfWork, country);
            }

            unitOfWork.ExecuteDatabaseCommand(@"SET IDENTITY_INSERT Country OFF");

            foreach (var country in countriesDetailed) {
                _countryDetailedRepository.Insert(unitOfWork, country);
            }
        }

        private void CreateTables(IUnitOfWork unitOfWork) {
            unitOfWork.ExecuteDatabaseCommand(@"
                CREATE TABLE Country 
                (
                    Id INT PRIMARY KEY IDENTITY(1, 1),
                    Name NVARCHAR(100) NOT NULL
                )
");
            unitOfWork.ExecuteDatabaseCommand(@"
                CREATE TABLE CountryDetailed 
                (
                    Id INT PRIMARY KEY IDENTITY(1, 1),
                    CountryId INT NOT NULL
                        CONSTRAINT FK_CountryDetailed_Country_Id
                        REFERENCES Country(Id),
                    Inhabitants INT NOT NULL,
                    IcuBeds INT NOT NULL,
                    MoratilityRatePerOneMillionPerDay FLOAT NOT NULL
                )
");
            unitOfWork.ExecuteDatabaseCommand(@"
                CREATE TABLE InfectionSpreadDataPoint 
                (
                    Id INT PRIMARY KEY NOT NULL IDENTITY,
                    [Date] DATETIME NOT NULL,
                    CountryId INT NOT NULL
                        CONSTRAINT FK_InfectionSpreadDataPoint_Country_Id
                        REFERENCES Country(Id),
                    InfectedTotal INT NOT NULL,
                    DeathsTotal INT NOT NULL,
                    RecoveredTotal INT NOT NULL
                )
");
        }

        private void DeleteAllTables(IUnitOfWork unitOfWork) {
            var tables = unitOfWork.QueryDatabase<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo'");
            var foreignKeyConstraints = unitOfWork.QueryDatabase<ForeignKeyConstraintAndTable>("SELECT fk.name as [ForeignKeyConstraint], t.name as [Table] FROM sys.foreign_keys fk JOIN sys.tables t on t.object_id = fk.parent_object_id");

            foreach (var foreignKeyConstraint in foreignKeyConstraints) {
                unitOfWork.ExecuteDatabaseCommand($"ALTER TABLE \"{foreignKeyConstraint.Table}\" DROP CONSTRAINT \"{foreignKeyConstraint.ForeignKeyConstraint}\"");
            }

            foreach (var table in tables) {
                unitOfWork.ExecuteDatabaseCommand($"DROP TABLE {table}");
            }
        }
    }
}
