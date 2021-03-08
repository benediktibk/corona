using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Backend {
    public interface IUnitOfWork : IDisposable {
        void BeginDatabaseTransaction();
        void CommitDatabaseTransaction();
        void RollbackDatabaseTransaction();
        void ExecuteDatabaseCommand(string command);
        void ExecuteDatabaseCommand(SqlCommand command);
        void ExecuteDatabaseCommand(string command, object param);
        List<T> QueryDatabase<T>(string command);
        List<T> QueryDatabase<T>(string command, object param);
    }
}