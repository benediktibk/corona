using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Backend.Repository {
    public class GitRepository : IGitRepository {
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
                DeleteContentOfPathRecursively(directory);
            }
            catch (Exception e) {
                _logger.Error(e, $"unable to delete directory {destinationPath}");
                return false;
            }

            return true;
        }

        private void DeleteContentOfPathRecursively(DirectoryInfo directoryInfo) {
            _logger.Debug($"deleting directory {directoryInfo.FullName}");

            var files = directoryInfo.GetFiles().ToList();
            var directories = directoryInfo.GetDirectories().ToList();

            foreach (var file in files) {
                _logger.Debug($"removing readonly attribute of {file.FullName}");
                File.SetAttributes(file.FullName, file.Attributes & ~FileAttributes.ReadOnly);
                _logger.Debug($"deleting {file.FullName}");
                file.Delete();
            }

            foreach (var subdirectory in directories) {
                DeleteContentOfPathRecursively(subdirectory);
            }

            directoryInfo.Delete();
        }
    }
}
