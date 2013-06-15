using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Models.Expressions;

namespace DatabaseAutoMigrator
{
    public abstract class BaseDatabaseContext:IDatabaseContext
    {
        public IDatabaseProvider DatabaseProvider { get; private set; }
        protected ICommandGenerator Factory { get; set; }

        public BaseDatabaseContext(IDatabaseProvider provider,ICommandGenerator factory)
        {
            this.DatabaseProvider = provider;
            this.Factory = factory;
        }
        protected int exec(DatabaseCommand databaseCommand)
        {
            return DatabaseProvider.ExecuteCommand(databaseCommand);
        }
        private IDatabaseReader execReader(DatabaseCommand databaseCommand)
        {
            return DatabaseProvider.ExecuteReaderCommand(databaseCommand);
        }

        public virtual void CreateTable(CreateTableExpression model)
        {
            var command = Factory.GenerateCreateTable(model);
            exec(command);
        }

        public virtual bool TableExists(string tableName)
        {
            var command = Factory.GenerateTableExists(tableName);
            using (var reader = execReader(command))
            {
                return reader.Read();
            }
        }

        public virtual void DropTable(string tableName)
        {
            exec(Factory.GenerateDropTable(tableName));
        }

        public virtual void AlterTable(AlterTableExpression model)
        {
            exec(Factory.GenerateAlterTable(model));
        }

        public virtual int ExecuteQuery(string text)
        {
            return exec(Factory.GenerateRawCommand(text));
        }

        public virtual int ExecuteQuery(RawCommandExpression model)
        {
            return exec(Factory.GenerateRawCommand(model));
        }

        public virtual IDatabaseReader ExecuteReader(string text)
        {
            return DatabaseProvider.ExecuteReaderCommand(text);
        }

        public virtual IDatabaseReader ExecuteReader(RawCommandExpression model)
        {
            return execReader(Factory.GenerateRawCommand(model));
        }

        public virtual int InsertRow(InsertExpression model)
        {
            return exec(Factory.GenerateInsert(model));
        }

        public void RenameTable(string oldName, string newName)
        {
            exec(Factory.GenerateRenameTable(oldName, newName));
        }

        public void AddColumn(string tableName, string columnName, Models.DbType type, bool allowNull = true)
        {
            exec(Factory.GenerateAddColumn(tableName, columnName, type, allowNull));
        }

        public void AddColumn(string tableName, string columnName, Models.DbType type, int length, bool allowNull = true)
        {
            exec(Factory.GenerateAddColumn(tableName, columnName, type, length, allowNull));
        }

        public void DropColumn(string tableName, string columnName)
        {
            exec(Factory.GenerateDropColumn(tableName, columnName));
        }

        public void AlterColumn(string tableName, string columnName, Models.DbType type, bool allowNull = true)
        {
            exec(Factory.GenerateAlterColumn(tableName, columnName, type, allowNull));
        }

        public void AlterColumn(string tableName, string columnName, Models.DbType type, int length, bool allowNull = true)
        {
            exec(Factory.GenerateAlterColumn(tableName, columnName, type, length, allowNull));
        }

        public void RenameColumn(string tableName, string oldName, string newName)
        {
            exec(Factory.GenerateRenameColumn(tableName, oldName, newName));
        }
    }
}
