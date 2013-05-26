using DatabaseAutoMigrator.DatabaseAccess;

namespace DatabaseAutoMigrator.Commands.Factories
{
    public interface IGenericCommandFactory<TCommand> where TCommand:IDatabaseCommand
    {
        TCommand GenericCommand(ICommandModel model);
        TCommand CreateTable(CreateTableCommandModel model);
        TCommand DropTable(DropTableCommandModel model);
        TCommand RawCommand(RawCommandModel model);
        TCommand Insert(InsertCommandModel model);
        TCommand AlterTable(AlterTableCommandModel model);
    }
}
