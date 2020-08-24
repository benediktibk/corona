using System.Linq;

namespace Backend.Repository {
    public class ImportedCommitHistoryRepository : IImportedCommitHistoryRepository {
        public void Insert(IUnitOfWork unitOfWork, ImportedCommitHistoryDao dao) {
            unitOfWork.ExecuteDatabaseCommand(@"
                INSERT ImportedCommitHistory
                (
                    ImportTimestamp,
                    CommitHash
                )
                VALUES
                (
                    @ImportTimestamp,
                    @CommitHash
                )", dao);
        }

        public ImportedCommitHistoryDao GetLatest(IUnitOfWork unitOfWork) {
            return unitOfWork.QueryDatabase<ImportedCommitHistoryDao>(@"SELECT TOP 1 * FROM ImportedCommitHistory ORDER BY ImportTimestamp").FirstOrDefault();
        }
    }
}
