using DatabaseAutoMigrator.DatabaseAccess;
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

        DatabaseCommand GenerateAddColumn(string tableName, string columnName, Models.DbType type, int length, bool allowNull);

        DatabaseCommand GenerateDropColumn(string tableName, string columnName);

        DatabaseCommand GenerateAlterColumn(string tableName, string columnName, Models.DbType type, bool allowNull);

        DatabaseCommand GenerateAlterColumn(string tableName, string columnName, Models.DbType type, int length, bool allowNull);

        DatabaseCommand GenerateRenameColumn(string tableName, string oldName, string newName);
    }
}
