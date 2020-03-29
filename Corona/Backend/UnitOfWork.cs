﻿using Dapper;
using NLog;
using System;
using System.Data.SqlClient;

namespace Backend
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly SqlConnection _databaseConnection;
        private SqlTransaction _transaction;

        public UnitOfWork(string databaseConnectionString) {
            _databaseConnection = new SqlConnection(databaseConnectionString);
            _databaseConnection.Open();
        }

        public void BeginDatabaseTransaction() {
            if (_transaction != null) {
                throw new InvalidOperationException("transaction is already open");
            }

            _transaction = _databaseConnection.BeginTransaction();
        }

        public void RollbackDatabaseTransaction() {
            if (_transaction == null) {
                throw new InvalidOperationException("transaction is not open");
            }

            _transaction.Rollback();
            _transaction = null;
        }

        public void CommitDatabaseTransaction() {
            if (_transaction == null) {
                throw new InvalidOperationException("transaction is not open");
            }

            _transaction.Commit();
            _transaction = null;
        }

        public void ExecuteDatabaseCommand(string command) {
            if (_transaction == null) {
                throw new InvalidOperationException("no transaction is open");
            }

            _databaseConnection.Execute(sql: command, transaction: _transaction);
        }

        public void QueryDatabase(string command, object param) {
            if (_transaction == null) {
                throw new InvalidOperationException("no transaction is open");
            }

            _databaseConnection.Query(sql: command, param: param, transaction: _transaction);
        }

        public void Dispose() {
            if (_transaction != null) {
                _logger.Warn("transaction is still open, rolling it back");
                RollbackDatabaseTransaction();
            }

            _databaseConnection.Dispose();
        }
    }
}
