namespace Backend.Repository {
    public interface IGitRepository {
        bool Clone(string repoUrl, string destinationPath);
        bool CheckIfDirectoryExists(string path);
        bool Pull(string path);
        string GetLatestCommitHash(string path);
    }
}