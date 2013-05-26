using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Models.Commands;

namespace DatabaseAutoMigrator
{
    public abstract class BaseDatabaseContext<TCommand,TReader>:IDatabaseContext
        where TCommand: IDatabaseCommand
        where TReader: IDatabaseReader
    {
        protected IDatabaseProvider<TCommand, TReader> Provider { get; private set; }
        protected ICommandFactory<TCommand> Factory { get; set; }

        public BaseDatabaseContext(IDatabaseProvider<TCommand, TReader> provider)
        {
            this.Provider = provider;
        }

        public virtual void CreateTable(CreateTableCommandModel model)
        {
            var command = Factory.CreateTable(model);
            Provider.ExecuteCommand(command);
        }

        public virtual bool TableExists(string tableName)
        {
            throw new System.NotImplementedException();
        }

        public virtual void DropTable(string tableName, bool ifExists)
        {
            Provider.ExecuteCommand(Factory.DropTable(tableName, ifExists));
        }

        public virtual void AlterTable(AlterTableCommandModel model)
        {
            Provider.ExecuteCommand(Factory.AlterTable(model));
        }

        public virtual int ExecuteQuery(string text)
        {
            return Provider.ExecuteCommand(Factory.RawCommand(text));
        }

        public virtual int ExecuteQuery(RawCommandModel model)
        {
            return Provider.ExecuteCommand(Factory.RawCommand(model));
        }

        public virtual IDatabaseReader ExecuteReader(string text)
        {
            return Provider.ExecuteReaderCommand(text);
        }

        public virtual IDatabaseReader ExecuteReader(RawCommandModel model)
        {
            return Provider.ExecuteReaderCommand(Factory.RawCommand(model));
        }

        public virtual int InsertRow(InsertCommandModel model)
        {
            return Provider.ExecuteCommand(Factory.Insert(model));
        }
    }
}
