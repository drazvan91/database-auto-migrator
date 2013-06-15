using System;

namespace DatabaseAutoMigrator.DatabaseAccess
{
    public interface IDatabaseProvider : IDisposable
    {
        int ExecuteCommand(DatabaseCommand command);
        int ExecuteCommand(string text);

        IDatabaseReader ExecuteReaderCommand(DatabaseCommand command);
        IDatabaseReader ExecuteReaderCommand(string text);

        void StartTransaction(string transactionName);
        void CommitTransaction();
        void RollbackTransaction();
    }
}
