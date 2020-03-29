using System;

namespace Backend
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginDatabaseTransaction();
        void CommitDatabaseTransaction();
        void RollbackDatabaseTransaction();
        void ExecuteDatabaseCommand(string command);
        void QueryDatabase(string command, object param);
    }
}