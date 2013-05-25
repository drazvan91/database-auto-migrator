using DatabaseAutoMigrator.DatabaseAccess;

namespace DatabaseAutoMigrator.Commands.Factories
{
    public interface ICommandFactory<TCommand,TCommandModel> 
        where TCommand: IDatabaseCommand
        where TCommandModel: ICommandModel
    {
        TCommand GenerateCommand(TCommandModel model,IDialect dialect);
    }
}
