using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Backend.Repository
{
    public class GitRepository : IGitRepository
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public bool FetchLatestCommit(string repoUrl, string destinationPath) {
            var result = DeletePath(destinationPath);

            if (!result) {
                _logger.Error("was not able to delete the destination path, skipping the fetch phase");
                return false;
            }

            return FetchShallowCopy(repoUrl, destinationPath);
        }

        private bool FetchShallowCopy(string repoUrl, string destinationPath) {
            var process = new Process();
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.FileName = "cmd.exe";
            var command = $"git clone --depth 1 {repoUrl} {destinationPath}";
            processStartInfo.Arguments = $"/C {command}";
            process.StartInfo = processStartInfo;

            _logger.Info($"executing command {command}");
            var result = process.Start();

            _logger.Info($"waiting for command to finish");
            result = result && process.WaitForExit(100 * 1000);

            if (result) {
                _logger.Info("successfully finished fetch of latest commit");
            }
            else {
                _logger.Error("was not able to fetch the latest commit");
            }

            return result;
        }

        private bool DeletePath(string destinationPath) {
            var directory = new DirectoryInfo(destinationPath);

            if (!directory.Exists) {
                _logger.Debug("destination path does not even exist, nothing to do yet");
                return true;
            }

            try {
                var files = directory.GetFiles().ToList();
                var directories = directory.GetDirectories().ToList();

                _logger.Debug($"must delete {files.Count()} files");
                foreach (var file in files) {
                    _logger.Debug($"deleting {file.FullName}");
                    file.Delete();
                }

                _logger.Debug($"must delete {directories.Count()} directories");
                foreach (var subdirectory in directories) {
                    _logger.Debug($"deleting {subdirectory.FullName}");
                    subdirectory.Delete(true);
                }

                directory.Delete(true);
            }
            catch (Exception e) {
                _logger.Error(e, $"unable to delete directory {destinationPath}");
                return false;
            }

            return true;
        }
    }
}
