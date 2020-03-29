using System.Collections.Generic;

namespace Backend.Repository
{
    public class Database : IDatabase 
    {
        private readonly ICountryRepository _countryRepository;

        public Database(ICountryRepository countryRepository) {
            _countryRepository = countryRepository;
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
                    Inhabitants = 8822000,
                    IcuBeds = 1923,
                    MoratilityRatePerOneMillionPerDay = 2.71
                },
                new CountryDao {
                    Id = CountryType.Italy,
                    Name = "Italy",
                    Inhabitants = 60480000,
                    IcuBeds = 7560,
                    MoratilityRatePerOneMillionPerDay = 2.93
                },
                new CountryDao {
                    Id = CountryType.Germany,
                    Name = "Germany",
                    Inhabitants = 82790000,
                    IcuBeds = 24175,
                    MoratilityRatePerOneMillionPerDay = 3.09
                },
                new CountryDao {
                    Id = CountryType.UnitedKingdom,
                    Name = "United Kingdom",
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
        }

        private void CreateTables(IUnitOfWork unitOfWork) {
            unitOfWork.ExecuteDatabaseCommand(@"
                CREATE TABLE Country 
                (
                    Id INT PRIMARY KEY IDENTITY(1, 1),
                    Name NVARCHAR(100) NOT NULL,
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
                    Country INT NOT NULL,
                    InfectedTotal INT NOT NULL
                        CONSTRAINT FK_InfectionSpreadDataPoint_Country_Id
                        REFERENCES Country(Id),
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
