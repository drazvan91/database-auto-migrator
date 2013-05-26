using DatabaseAutoMigrator.Models.Commands;
using DatabaseAutoMigrator.DatabaseAccess;

namespace DatabaseAutoMigrator
{
    public interface IDatabaseContext
    {
        void CreateTable(CreateTableCommandModel model);
        bool TableExists(string tableName);
        void DropTable(string tableName, bool ifExists);
        void AlterTable(AlterTableCommandModel model);
        
        int ExecuteQuery(string text);
        int ExecuteQuery(RawCommandModel model);

        IDatabaseReader ExecuteReader(string text);
        IDatabaseReader ExecuteReader(RawCommandModel model);

        int InsertRow(InsertCommandModel model);
    }
}
