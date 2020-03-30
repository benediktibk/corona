using Backend;
using Backend.DependencyInjection;
using Backend.Repository;
using Backend.Service;
using NConfig;
using NLog;

namespace Updater
{
    class Program
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        static void Main() 
        {
            NConfigurator.UsingFiles("Config\\Corona.config").SetAsSystemDefault();

            _logger.Info("build backend");
            var container = new Container();
            var database = container.GetInstance<IDatabase>();
            var dataReimportService = container.GetInstance<IDataReimportService>();

            using (var unitOfWork = container.GetInstance<UnitOfWork>()) {
                unitOfWork.BeginDatabaseTransaction();

                _logger.Info("initializing database");
                database.Initialize(unitOfWork);

                _logger.Info("importing current data");
                var result = dataReimportService.ReimportAll(unitOfWork);

                if (!result) {
                    return;
                }

                unitOfWork.CommitDatabaseTransaction();
                _logger.Info("successfully initialized the database");
            }
        }
    }
}
