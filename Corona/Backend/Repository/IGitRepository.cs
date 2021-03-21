namespace Backend.Repository {
    public interface IGitRepository {
        void Clone(string repoUrl, string destinationPath);
        bool CheckIfDirectoryExists(string path);
        void Pull(string path);
        string GetLatestCommitHash(string path);
    }
}