using NLog;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Backend.Repository {
    public class GitRepository : IGitRepository {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public bool Clone(string repoUrl, string destinationPath) {
            var command = $"git clone {repoUrl} {destinationPath}";
            return ExecuteGitCommand(command, Directory.GetCurrentDirectory(), out var output);
        }

        public bool CheckIfDirectoryExists(string path) {
            var directory = new DirectoryInfo(path);
            return directory.Exists;
        }

        public bool Pull(string path) {
            var command = "git pull";
            return ExecuteGitCommand(command, path, out var output);
        }

        public string GetLatestCommitHash(string path) {
            var command = "git rev-parse HEAD";
            ExecuteGitCommand(command, path, out var output);
            output = output.Substring(0, output.Length - System.Environment.NewLine.Length);
            return output;
        }

        private bool ExecuteGitCommand(string command, string workingDirectory, out string output) {
            output = string.Empty;
            var processStartInfo = new ProcessStartInfo {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var process = new Process { StartInfo = processStartInfo };
            var stringWriter = new StringBuilder();

            _logger.Info($"executing command {command}");
            var result = process.Start();

            if (!result) {
                _logger.Error($"was not able to execute the command {command}");
                return false;
            }

            while (!process.StandardOutput.EndOfStream) {
                var line = process.StandardOutput.ReadLine();
                stringWriter.AppendLine(line);
            }

            output = stringWriter.ToString();

            _logger.Info($"waiting for command {command} to finish");
            var processEnded = process.WaitForExit(1000 * 1000);

            if (processEnded) {
                _logger.Info($"command {command} did succeed in time");
            }
            else {
                _logger.Error($"command {command} did not finish in time");
                return false;
            }

            return true;
        }
    }
}
