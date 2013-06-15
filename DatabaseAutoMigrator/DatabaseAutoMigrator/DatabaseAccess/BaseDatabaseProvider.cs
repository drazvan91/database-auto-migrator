
namespace DatabaseAutoMigrator.DatabaseAccess
{
    public abstract class BaseDatabaseProvider : IDatabaseProvider
    {
        public abstract int ExecuteCommand(DatabaseCommand command);
        public abstract IDatabaseReader ExecuteReaderCommand(DatabaseCommand command);
        public abstract int ExecuteCommand(string command);
        public abstract IDatabaseReader ExecuteReaderCommand(string command);

        public abstract void StartTransaction(string transactionName);
        public abstract void CommitTransaction();
        public abstract void RollbackTransaction();

        public abstract void Dispose();

    }
}
