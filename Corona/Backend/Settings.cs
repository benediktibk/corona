using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using NLog;

namespace Backend {
    public class Settings : ISettings {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public Settings(IConfiguration configuration) {
            var databaseHost = configuration.GetSection("DBHOST").Value;
            var databaseUser = configuration.GetSection("DBUSER").Value;
            var databasePassword = configuration.GetSection("DBPASSWORD").Value;
            var databaseName = configuration.GetSection("DBNAME").Value;
            _logger.Info($"creating database connection string for host {databaseHost} and database {databaseName}");

            DatabaseConnectionString = $"Server={databaseHost};Database={databaseName};User Id={databaseUser};Password={databasePassword}";
            GitRepo = configuration.GetSection("GITREPO").Value;
            LocalPath = configuration.GetSection("LOCALTEMPPATH").Value;
            SvgCompressed = bool.Parse(configuration.GetSection("SVGCOMPRESSED").Value);
        }

        public string DatabaseConnectionString { get; private set; }
        public string GitRepo { get; private set; }
        public string LocalPath { get; private set; }
        public bool SvgCompressed { get; private set; }
    }
}
