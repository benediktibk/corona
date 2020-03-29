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
                    Id = CountryType.Austria,
                    Name = "Austria",
                },
                new CountryDao {
                    Id = CountryType.Italy,
                    Name = "Italy",
                },
                new CountryDao {
                    Id = CountryType.Germany,
                    Name = "Germany",
                },
                new CountryDao {
                    Id = CountryType.UnitedKingdom,
                    Name = "United Kingdom",
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
