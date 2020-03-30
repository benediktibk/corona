using Backend.Repository;
using Backend.Service;

namespace Backend.DependencyInjection
{
    public class Container
    {
        private readonly StructureMap.Container _container;

        public Container() {
            _container = new StructureMap.Container(x => {
                x.For<Settings>().Use(() => new Settings {
                    DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Database"].ConnectionString,
                    GitRepo = System.Configuration.ConfigurationManager.AppSettings["GitRepo"],
                    LocalPath = System.Configuration.ConfigurationManager.AppSettings["LocalPath"]
                });

                x.For<IDatabase>().Use<Database>();
                x.For<IUnitOfWork>().Use<UnitOfWork>();
                x.For<ICountryRepository>().Use<CountryRepository>();
                x.For<ICountryDetailedRepository>().Use<CountryDetailedRepository>();
                x.For<IGitRepository>().Use<GitRepository>();
                x.For<ICsvFileRepository>().Use<CsvFileRepository>();
                x.For<IInfectionSpreadDataPointRepository>().Use<InfectionSpreadDataPointRepository>();
                x.For<IDataReimportService>().Use<DataReimportService>();
            });
        }

        public T GetInstance<T>() {
            return _container.GetInstance<T>();
        }
    }
}
