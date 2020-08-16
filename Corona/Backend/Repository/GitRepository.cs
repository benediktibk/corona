using NLog;
using System.Diagnostics;
using System.IO;

namespace Backend.Repository {
    public class GitRepository : IGitRepository {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public bool Clone(string repoUrl, string destinationPath) {
            var command = $"git clone {repoUrl} {destinationPath}";
            return ExecuteGitCommand(command, Directory.GetCurrentDirectory());
        }

        private bool ExecuteGitCommand(string command, string workingDirectory) {
            var processStartInfo = new ProcessStartInfo {WindowStyle = ProcessWindowStyle.Hidden, FileName = "cmd.exe", Arguments = $"/C {command}", WorkingDirectory = workingDirectory};
            var process = new Process {StartInfo = processStartInfo};

            _logger.Info($"executing command {command}");
            var result = process.Start();

            if (!result) {
                _logger.Error($"was not able to execute the command {command}");
                return false;
            }

            _logger.Info($"waiting for command {command} to finish");
            var processEnded = process.WaitForExit(1000 * 1000);

            if (processEnded) {
                _logger.Info($"command {command} did succeed in time");
            } else {
                _logger.Error($"command {command} did not finish in time");
                return false;
            }

            return true;
        }

        public bool CheckIfDirectoryExists(string path) {
            var directory = new DirectoryInfo(path);
            return directory.Exists;
        }

        public bool Pull(string path) {
            var command = $"git pull";
            return ExecuteGitCommand(command, path);
        }
    }
}
