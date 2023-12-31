﻿using Backend.Repository;
using Backend.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Backend {
    public static class DependencyInjectionRegistry {
        public static void ConfigureServices(IServiceCollection services, Settings settings) {
            services.AddSingleton<ISettings>(settings);
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
