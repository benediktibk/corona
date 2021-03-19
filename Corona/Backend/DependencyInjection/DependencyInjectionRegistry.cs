using Backend.Repository;
using Backend.Service;
using StructureMap;

namespace Backend.DependencyInjection {
    public class DependencyInjectionRegistry : Registry {
        public DependencyInjectionRegistry(Settings settings) {
            For<ISettings>().Use(() => settings);
            For<IServerSideCache>().Use<ServerSideCache>().Singleton();

            For<IDatabase>().Use<Database>();
            For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>();

            For<ICountryRepository>().Use<CountryRepository>();
            For<ICountryInhabitantsRepository>().Use<CountryInhabitantsRepository>();
            For<IGitRepository>().Use<GitRepository>();
            For<ICsvFileRepository>().Use<CsvFileRepository>();
            For<IInfectionSpreadDataPointRepository>().Use<InfectionSpreadDataPointRepository>();
            For<IImportedCommitHistoryRepository>().Use<ImportedCommitHistoryRepository>();

            For<IDataReimportService>().Use<DataReimportService>();
            For<IGraphService>().Use<GraphService>();
            For<IAuthorizationService>().Use<AuthorizationService>();
            For<IDataSeriesService>().Use<DataSeriesService>();
        }
    }
}
