using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Backend {
    public class Settings : ISettings {
        public Settings(IConfiguration configuration) {
            DatabaseConnectionString = configuration.GetConnectionString("Database");
            GitRepo = configuration.GetSection("GitRepo").Value;
            LocalPath = configuration.GetSection("LocalPath").Value;
            AdminUsers = configuration.GetSection("AdminUsers").Value.Split(';').ToList();
            SvgCompressed = bool.Parse(configuration.GetSection("SvgCompressed").Value);
        }

        public string DatabaseConnectionString { get; private set; }
        public string GitRepo { get; private set; }
        public string LocalPath { get; private set; }
        public IReadOnlyList<string> AdminUsers { get; private set; }

        public bool SvgCompressed { get; private set; }
    }
}
