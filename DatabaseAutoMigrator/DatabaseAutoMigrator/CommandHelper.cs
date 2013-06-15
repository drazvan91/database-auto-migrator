using DatabaseAutoMigrator.Models;
using DatabaseAutoMigrator.Models.Expressions;

namespace DatabaseAutoMigrator
{
    public class CommandHelper
    {
        public CreateTableExpression CreateTable(string tableName)
        {
            var table = new CreateTableExpression(tableName);
            return table;
        }

        public RawCommandExpression RawCommand(string commandText)
        {
            var raw = new RawCommandExpression(commandText);
            return raw;
        }

        public InsertExpression InsertRow(string tableName)
        {
            var insert = new InsertExpression(tableName);
            return insert;
        }

        public AlterTableExpression AlterTable(string tableName)
        {
            var alter = new AlterTableExpression(tableName);
            return alter;
        }

        public ForeignKeyDefinition CreateForeignKey(string name, string primaryTable, string foreignTable)
        {
            return new ForeignKeyDefinition(name, primaryTable, foreignTable);
        }
    }
}
