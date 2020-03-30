using System.Collections.Generic;
using System.Linq;

namespace Backend
{
    public class Settings : ISettings
    {
        public Settings(string databaseConnectionString, string gitRepo, string localPath, string adminUsers) {
            DatabaseConnectionString = databaseConnectionString;
            GitRepo = gitRepo;
            LocalPath = localPath;
            AdminUsers = adminUsers.Split(';').ToList();
        }

        public string DatabaseConnectionString { get; private set; }
        public string GitRepo { get; private set; }
        public string LocalPath { get; private set; }
        public IReadOnlyList<string> AdminUsers { get; private set; }
    }
}
