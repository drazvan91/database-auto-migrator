using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Models.Commands;

namespace DatabaseAutoMigrator
{
    public interface ICommandFactory<TCommand> where TCommand:IDatabaseCommand
    {
        IDialect Dialect { get; set; }
        
        TCommand CreateTable(CreateTableCommandModel model);
        TCommand DropTable(string tableName,bool ifExists=false);
        TCommand RawCommand(RawCommandModel model);
        TCommand RawCommand(string commandText);
        TCommand Insert(InsertCommandModel model);
        TCommand AlterTable(AlterTableCommandModel model);
    }
}
