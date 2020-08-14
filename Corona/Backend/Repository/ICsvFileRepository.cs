using System.Collections.Generic;

namespace Backend.Repository {
    public interface ICsvFileRepository {
        CsvFile ReadFile(string path);
        List<string> ListAllCsvFilesIn(string path);
    }
}