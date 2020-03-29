namespace Backend.Repository
{
    public interface IGitRepository
    {
        void FetchLatestCommit(string repoUrl, string destinationPath);
    }
}