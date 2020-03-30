using Backend.Repository;
using Backend.Service;
using System;

namespace Backend.DependencyInjection
{
    public class Container
    {
        private readonly StructureMap.Container _container;

        public Container() {
            _container = new StructureMap.Container(x => {
                var connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings;
                var appSettings = System.Configuration.ConfigurationManager.AppSettings;
                var connectionString = connectionStrings["Database"].ConnectionString;
                var gitRepo = appSettings["GitRepo"];
                var localPath = appSettings["LocalPath"];

                x.For<Settings>().Use(() => 
                    new Settings {
                        DatabaseConnectionString = connectionString,
                        GitRepo = gitRepo,
                        LocalPath = localPath
                    });

                x.For<IDatabase>().Use<Database>();
                x.For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();

                x.For<ICountryRepository>().Use<CountryRepository>();
                x.For<ICountryDetailedRepository>().Use<CountryDetailedRepository>();
                x.For<IGitRepository>().Use<GitRepository>();
                x.For<ICsvFileRepository>().Use<CsvFileRepository>();
                x.For<IInfectionSpreadDataPointRepository>().Use<InfectionSpreadDataPointRepository>();

                x.For<IDataReimportService>().Use<DataReimportService>();
                x.For<IGraphService>().Use<GraphService>();
            });
        }

        public T GetInstance<T>() {
            return _container.GetInstance<T>();
        }

        public object GetInstance(Type type) {
            return _container.GetInstance(type);
        }
    }
}
