using DatabaseAutoMigrator.Commands.Factories;
using System;

namespace DatabaseAutoMigrator.DatabaseAccess
{
    public interface IDatabaseProvider<TCommand, TTransaction, TReader> : IDisposable
        where TCommand: IDatabaseCommand
        where TTransaction: IDatabaseTransaction
        where TReader: IDatabaseReader
    {
        IGenericCommandFactory<TCommand> CommandFactory { get; }
        TTransaction CreateTransaction(string transactionName);
        
        int ExecuteCommand(ICommandModel command);
        int ExecuteCommand(ICommandModel command, TTransaction transaction);
        int ExecuteCommand(TCommand command);
        int ExecuteCommand(TCommand command, TTransaction transaction);

        TReader ExecuteReaderCommand(ICommandModel command);
        TReader ExecuteReaderCommand(ICommandModel command, TTransaction transaction);
        TReader ExecuteReaderCommand(TCommand command);
        TReader ExecuteReaderCommand(TCommand command, TTransaction transaction);
    }
}
