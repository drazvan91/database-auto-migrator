using DatabaseAutoMigrator.DatabaseAccess;

namespace DatabaseAutoMigrator.Commands.Factories
{
    public interface IGenericCommandFactory<TCommand> where TCommand:IDatabaseCommand
    {
        TCommand GenerateGenericCommand(ICommandModel model);
        TCommand GenerateCreateTableCommand(CreateTableCommandModel model);
        TCommand GenerateDropTableCommand(DropTableCommandModel model);
        TCommand GenerateRawCommand(RawCommandModel model);
        TCommand GenerateInsertCommand(InsertCommandModel model);
    }
}
