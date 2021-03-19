using Backend.Repository;
using Backend.Service;
using Microsoft.Extensions.DependencyInjection;
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

        public static void ConfigureServices(IServiceCollection services, Settings settings) {
            services.AddSingleton<ISettings>(settings);
            services.AddTransient<IServerSideCache, ServerSideCache>();
            services.AddTransient<IDatabase, Database>();
            services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICountryInhabitantsRepository, CountryInhabitantsRepository>();
            services.AddTransient<IGitRepository, GitRepository>();
            services.AddTransient<ICsvFileRepository, CsvFileRepository>();
            services.AddTransient<IInfectionSpreadDataPointRepository, InfectionSpreadDataPointRepository>();
            services.AddTransient<IImportedCommitHistoryRepository, ImportedCommitHistoryRepository>();
            services.AddTransient<IDataReimportService, DataReimportService>();
            services.AddTransient<IGraphService, GraphService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IDataSeriesService, DataSeriesService>();
        }
    }
}
