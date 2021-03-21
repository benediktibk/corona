using NLog;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

namespace Backend.Repository {
    public class GitRepository : IGitRepository {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public void Clone(string repoUrl, string destinationPath) {
            ExecuteGitCommand($"clone {repoUrl} {destinationPath}", null);
        }

        public bool CheckIfDirectoryExists(string path) {
            var directory = new DirectoryInfo(path);
            return directory.Exists;
        }

        public void Pull(string path) {
            ExecuteGitCommand("pull", path);
        }

        public string GetLatestCommitHash(string path) {
            return ExecuteGitCommand("rev-parse HEAD", path);
        }

        public string ExecuteGitCommand(string arguments, string workingDirectory) {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.FileName = "git";

            Process process = new Process();
            processStartInfo.Arguments = arguments;
            processStartInfo.WorkingDirectory = workingDirectory;

            process.StartInfo = processStartInfo;
            process.Start();

            string stdout = process.StandardOutput.ReadToEnd();

            process.WaitForExit(1000 * 1000);
            process.Close();
            return stdout;
        }
    }
}
