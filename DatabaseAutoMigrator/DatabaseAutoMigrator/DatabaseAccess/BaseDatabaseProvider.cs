using DatabaseAutoMigrator.Commands.Factories;

namespace DatabaseAutoMigrator.DatabaseAccess
{
    public abstract class BaseDatabaseProvider<TCommand, TTransaction, TReader> : IDatabaseProvider<TCommand, TTransaction, TReader>
        where TCommand : IDatabaseCommand
        where TTransaction : IDatabaseTransaction
        where TReader : IDatabaseReader
    {
        public abstract IGenericCommandFactory<TCommand> CommandFactory { get; protected set; }

        public abstract TTransaction CreateTransaction(string transactionName);

        public abstract int ExecuteCommand(TCommand command);
        public abstract int ExecuteCommand(TCommand command, TTransaction transaction);
        public abstract TReader ExecuteReaderCommand(TCommand command);
        public abstract TReader ExecuteReaderCommand(TCommand command, TTransaction transaction);
        public abstract void Dispose();


        public virtual int ExecuteCommand(ICommandModel command)
        {
            var cmd = this.CommandFactory.GenerateGenericCommand(command);
            return ExecuteCommand(cmd);
        }
        public virtual int ExecuteCommand(ICommandModel command, TTransaction transaction)
        {
            var cmd = this.CommandFactory.GenerateGenericCommand(command);
            return ExecuteCommand(cmd, transaction);
        }
        public virtual TReader ExecuteReaderCommand(ICommandModel command)
        {
            var cmd = this.CommandFactory.GenerateGenericCommand(command);
            return ExecuteReaderCommand(cmd);
        }
        public virtual TReader ExecuteReaderCommand(ICommandModel command, TTransaction transaction)
        {
            var cmd = this.CommandFactory.GenerateGenericCommand(command);
            return ExecuteReaderCommand(cmd, transaction);
        }

    }
}
