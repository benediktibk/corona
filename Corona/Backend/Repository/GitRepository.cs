using NLog;
using System.Diagnostics;
using System.IO;

namespace Backend.Repository
{
    public class GitRepository : IGitRepository
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public bool FetchLatestCommit(string repoUrl, string destinationPath) {
            var directory = new DirectoryInfo(destinationPath);

            if (directory.Exists) {
                foreach (var file in directory.GetFiles()) {
                    file.Delete();
                }
                foreach (var subdirectory in directory.GetDirectories()) {
                    subdirectory.Delete(true);
                }

                directory.Delete(true);
            }

            var process = new Process();
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.FileName = "cmd.exe";
            processStartInfo.Arguments = $"/C git clone --depth 1 {repoUrl} {destinationPath}";
            process.StartInfo = processStartInfo;
            var result = process.Start();

            if (!result) {
                _logger.Error("was not able to fetch the latest commit");
            }

            return result;
        }
    }
}
