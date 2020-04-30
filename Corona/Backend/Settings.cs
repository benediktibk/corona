using System.Collections.Generic;
using System.Linq;

namespace Backend {
    public class Settings : ISettings {
        public Settings(string databaseConnectionString, string gitRepo, string localPath, string adminUsers, bool svgCompressed) {
            DatabaseConnectionString = databaseConnectionString;
            GitRepo = gitRepo;
            LocalPath = localPath;
            AdminUsers = adminUsers.Split(';').ToList();
            SvgCompressed = svgCompressed;
        }

        public string DatabaseConnectionString { get; private set; }
        public string GitRepo { get; private set; }
        public string LocalPath { get; private set; }
        public IReadOnlyList<string> AdminUsers { get; private set; }

        public bool SvgCompressed { get; private set; }
    }
}
