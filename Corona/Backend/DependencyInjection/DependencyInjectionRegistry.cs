using Backend.Repository;
using Backend.Service;
using StructureMap;
using Microsoft.Extensions.Configuration;

namespace Backend.DependencyInjection {
    public class DependencyInjectionRegistry : Registry {
        public DependencyInjectionRegistry(IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("Database");
            var gitRepo = configuration.GetSection("GitRepo").Value;
            var localPath = configuration.GetSection("LocalPath").Value;
            var adminUsers = configuration.GetSection("AdminUsers").Value;
            var svgCompressed = bool.Parse(configuration.GetSection("SvgCompressed").Value);

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
