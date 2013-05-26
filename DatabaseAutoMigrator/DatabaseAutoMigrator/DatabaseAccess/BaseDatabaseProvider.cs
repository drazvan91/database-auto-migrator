
namespace DatabaseAutoMigrator.DatabaseAccess
{
    public abstract class BaseDatabaseProvider<TCommand, TReader> : IDatabaseProvider<TCommand,  TReader>
        where TCommand : IDatabaseCommand
        where TReader : IDatabaseReader
    {
        public abstract int ExecuteCommand(TCommand command);
        public abstract TReader ExecuteReaderCommand(TCommand command);
        public abstract int ExecuteCommand(string command);
        public abstract TReader ExecuteReaderCommand(string command);

        public abstract void StartTransaction(string transactionName);
        public abstract void CommitTransaction();
        public abstract void RollbackTransaction();

        public abstract void Dispose();

    }
}
