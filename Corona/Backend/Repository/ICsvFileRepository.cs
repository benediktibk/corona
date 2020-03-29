using System.Collections.Generic;

namespace Backend.Repository
{
    public interface ICsvFileRepository
    {
        List<Dictionary<string, string>> ReadFile(string path);
        List<string> ListAllCsvFilesIn(string path);
    }
}