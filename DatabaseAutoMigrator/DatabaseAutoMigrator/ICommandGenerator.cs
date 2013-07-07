using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Models;
using DatabaseAutoMigrator.Models.Expressions;

namespace DatabaseAutoMigrator
{
    public interface ICommandGenerator
    {
        DatabaseCommand GenerateCreateTable(CreateTableExpression model);
        DatabaseCommand GenerateDropTable(string tableName);
        DatabaseCommand GenerateAlterTable(AlterTableExpression model);
        DatabaseCommand GenerateRawCommand(RawCommandExpression model);
        DatabaseCommand GenerateRawCommand(string commandText);
        DatabaseCommand GenerateInsert(InsertExpression model);
        DatabaseCommand GenerateRenameTable(string oldName, string newName);
        DatabaseCommand GenerateAddColumn(string tableName, string columnName, Models.DbType type, bool allowNull);
        DatabaseCommand GenerateAddColumn(string tableName, string columnName, Models.DbType type, object defaultValue, bool allowNull);
        DatabaseCommand GenerateAddColumn(string tableName, string columnName, Models.DbType type, int length, bool allowNull);
        DatabaseCommand GenerateAddColumn(string tableName, string columnName, Models.DbType type, int length, object defaultValue, bool allowNull);
        DatabaseCommand GenerateDropColumn(string tableName, string columnName);
        DatabaseCommand GenerateAlterColumn(string tableName, string columnName, Models.DbType type, bool allowNull);
        DatabaseCommand GenerateAlterColumn(string tableName, string columnName, DbType type, object defaultValue, bool allowNull);
        DatabaseCommand GenerateAlterColumn(string tableName, string columnName, DbType type, int length, object defaultValue, bool allowNull);
        DatabaseCommand GenerateAlterColumn(string tableName, string columnName, Models.DbType type, int length, bool allowNull);
        DatabaseCommand GenerateRenameColumn(string tableName, string oldName, string newName);
        DatabaseCommand GenerateTableExists(string tableName);

        DatabaseCommand GenerateCreatePrimaryKey(ConstraintDefinition model);
        DatabaseCommand GenerateCreateForeignKey(ForeignKeyDefinition model);
        DatabaseCommand GenerateCreateUniqueConstraint(ConstraintDefinition model);
        DatabaseCommand GenerateDropConstraint(string table, string constraintName);
        DatabaseCommand GenerateDropDefaultValue(string table, string columnName);
    }
}
