namespace Backend.Repository {
    public interface IImportedCommitHistoryRepository {
        ImportedCommitHistoryDao GetLatest(IUnitOfWork unitOfWork);
        void Insert(IUnitOfWork unitOfWork, ImportedCommitHistoryDao dao);
    }
}