using NLog;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using LibGit2Sharp;

namespace Backend.Repository {
    public class GitRepository : IGitRepository {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public void Clone(string repoUrl, string destinationPath) {
            LibGit2Sharp.Repository.Clone(repoUrl, destinationPath);
        }

        public bool CheckIfDirectoryExists(string path) {
            var directory = new DirectoryInfo(path);
            return directory.Exists;
        }

        public void Pull(string path) {
            using (var repo = new LibGit2Sharp.Repository(path)) {
                Commands.Pull(repo, new Signature("nobody", "nobody@blub.at", DateTime.Now), new PullOptions { MergeOptions = new MergeOptions { FastForwardStrategy = FastForwardStrategy.Default }});
            }
        }

        public string GetLatestCommitHash(string path) {
            using (var repo = new LibGit2Sharp.Repository(path)) {
                return repo.Head.Tip.Id.ToString();
            }
        }
    }
}
