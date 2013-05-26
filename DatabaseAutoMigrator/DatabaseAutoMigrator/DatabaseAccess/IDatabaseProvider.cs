using System;

namespace DatabaseAutoMigrator.DatabaseAccess
{
    public interface IDatabaseProvider<TCommand, TReader> : IDisposable
        where TCommand: IDatabaseCommand
        where TReader: IDatabaseReader
    {
        int ExecuteCommand(TCommand command);
        int ExecuteCommand(string text);

        TReader ExecuteReaderCommand(TCommand command);
        TReader ExecuteReaderCommand(string text);

        void StartTransaction(string transactionName);
        void CommitTransaction();
        void RollbackTransaction();
    }
}
