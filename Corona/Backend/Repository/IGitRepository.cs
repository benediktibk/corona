namespace Backend.Repository
{
    public interface IGitRepository
    {
        bool FetchLatestCommit(string repoUrl, string destinationPath);
    }
}