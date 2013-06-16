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

        public ConstraintDefinition PrimaryKey(string tableName)
        {
            return new ConstraintDefinition(tableName,"PK_"+tableName);
        }
        public ConstraintDefinition PrimaryKey(string tableName,string constraintName)
        {
            return new ConstraintDefinition(tableName,constraintName);
        }

        public ForeignKeyDefinition ForeignKey(string primaryTable, string foreignTable)
        {
            return new ForeignKeyDefinition(primaryTable, foreignTable);
        }
        public ForeignKeyDefinition ForeignKey(string primaryTable, string foreignTable, string name)
        {
            return new ForeignKeyDefinition(primaryTable, foreignTable,name);
        }

        public ConstraintDefinition Unique(string tableName)
        {
            return new ConstraintDefinition(tableName,"UK_"+tableName);
        }
        public ConstraintDefinition Unique(string tableName, string constraintName)
        {
            return new ConstraintDefinition(tableName, constraintName);
        }

    }
}
