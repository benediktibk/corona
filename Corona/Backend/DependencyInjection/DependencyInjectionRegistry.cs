using Backend.Repository;
using Backend.Service;
using StructureMap;

namespace Backend.DependencyInjection {
    public class DependencyInjectionRegistry : Registry {
        public DependencyInjectionRegistry() {
            var connectionStrings = System.Configuration.ConfigurationManager.ConnectionStrings;
            var appSettings = System.Configuration.ConfigurationManager.AppSettings;
            var connectionString = connectionStrings["Database"].ConnectionString;
            var gitRepo = appSettings["GitRepo"];
            var localPath = appSettings["LocalPath"];
            var adminUsers = appSettings["AdminUsers"];
            var svgCompressed = bool.Parse(appSettings["SvgCompressed"]);

            For<ISettings>().Use(() => new Settings(connectionString, gitRepo, localPath, adminUsers, svgCompressed));
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
