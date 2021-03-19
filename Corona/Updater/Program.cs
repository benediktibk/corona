using Backend;
using Backend.DependencyInjection;
using Backend.Repository;
using Backend.Service;
using Microsoft.Extensions.Configuration;
using NLog;
using StructureMap;
using System;
using System.Diagnostics;
using System.IO;

namespace Updater {
    class Program {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        static void Main() {
            try {
                _logger.Info("build backend");
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                    .AddJsonFile("appsettings.json", false)
                    .Build();
                var settings = new Settings(configuration);
                var container = new Container(new DependencyInjectionRegistry(settings));
                var database = container.GetInstance<IDatabase>();
                var dataReimportService = container.GetInstance<IDataReimportService>();
                var unitOfWorkFactory = container.GetInstance<IUnitOfWorkFactory>();
                var stopWatch = new Stopwatch();

                using (var unitOfWork = unitOfWorkFactory.Create()) {
                    unitOfWork.BeginDatabaseTransaction();

                    _logger.Info("initializing database");
                    database.Initialize(unitOfWork);

                    _logger.Info("importing current data");
                    stopWatch.Start();
                    var result = dataReimportService.ReimportAll(unitOfWork);
                    stopWatch.Stop();
                    _logger.Info($"import took {stopWatch.Elapsed.TotalSeconds} seconds");

                    if (!result) {
                        return;
                    }

                    unitOfWork.CommitDatabaseTransaction();
                    _logger.Info("successfully initialized the database");
                }
            }
            catch (Exception e) {
                _logger.Error(e);
            }

#if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
#endif
        }
    }
}
