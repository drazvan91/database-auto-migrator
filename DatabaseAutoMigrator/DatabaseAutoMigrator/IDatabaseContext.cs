using DatabaseAutoMigrator.Models.Expressions;
using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Models;

namespace DatabaseAutoMigrator
{
    public interface IDatabaseContext
    {
        IDatabaseProvider DatabaseProvider { get; }

        void CreateTable(CreateTableExpression model);
        bool TableExists(string tableName);
        void DropTable(string tableName);
        void RenameTable(string oldName, string newName);
        
        void AlterTable(AlterTableExpression model);
        void AddColumn(string tableName, string columnName, DbType type,bool allowNull=true);
        void AddColumn(string tableName, string columnName, DbType type, int length, bool allowNull = true);
        void DropColumn(string tableName, string columnName);
        void AlterColumn(string tableName, string columnName, DbType type, bool allowNull = true);
        void AlterColumn(string tableName, string columnName, DbType type, int length, bool allowNull = true);
        void RenameColumn(string tableName, string oldName, string newName);

        int ExecuteQuery(string text);
        int ExecuteQuery(RawCommandExpression model);

        IDatabaseReader ExecuteReader(string text);
        IDatabaseReader ExecuteReader(RawCommandExpression model);

        int InsertRow(InsertExpression model);
    }
}
