
using DatabaseAutoMigrator.Models.Commands;
namespace DatabaseAutoMigrator
{
    public class CommandHelper
    {
        public CreateTableCommandModel CreateTable(string tableName)
        {
            var table = new CreateTableCommandModel(tableName);
            return table;
        }

        public RawCommandModel RawCommand(string commandText)
        {
            var raw = new RawCommandModel(commandText);
            return raw;
        }

        public InsertCommandModel InsertRow(string tableName)
        {
            var insert = new InsertCommandModel(tableName);
            return insert;
        }

        public AlterTableCommandModel AlterTable(string tableName)
        {
            var alter = new AlterTableCommandModel(tableName);
            return alter;
        }
    }
}
