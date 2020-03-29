using Backend;
using Backend.DependencyInjection;
using Backend.Repository;
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

            using (var unitOfWork = container.GetInstance<UnitOfWork>()) {
                unitOfWork.BeginDatabaseTransaction();

                _logger.Info("initializing database");
                database.Initialize(unitOfWork);

                unitOfWork.CommitDatabaseTransaction();
                _logger.Info("successfully initialized the database");
            }
        }
    }
}
