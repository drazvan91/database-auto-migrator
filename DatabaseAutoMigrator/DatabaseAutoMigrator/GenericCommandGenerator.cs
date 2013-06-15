using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Models.Expressions;
using System;
using System.Linq;
using System.Text;

namespace DatabaseAutoMigrator
{
    public abstract class GenericCommandGenerator :ICommandGenerator
    {
        public IDialect Dialect { get; set; }
        public IColumnGenerator ColumnGenerator { get; set; }
        public ITypeMapper TypeMapper { get; set; }

        public GenericCommandGenerator(IDialect dialect,IColumnGenerator columnGenerator,ITypeMapper typeMapper)
        {
            this.Dialect = dialect;
            this.ColumnGenerator = columnGenerator;
            this.TypeMapper = typeMapper;
        }

        protected virtual string CreateTableFormat { get { return "CREATE TABLE {0} ({1})"; } }
        protected virtual string DropTableFormat { get { return "DROP TABLE {0}"; } }
        
        protected virtual string AddColumnFormat { get { return "ALTER TABLE {0} ADD COLUMN {1}"; } }
        protected virtual string DropColumnFormat { get { return "ALTER TABLE {0} DROP COLUMN {1}"; } }
        protected virtual string AlterColumnFormat { get { return "ALTER TABLE {0} ALTER COLUMN {1}"; } }
        protected virtual string RenameColumnFormat { get { return "ALTER TABLE {0} RENAME COLUMN {1} TO {2}"; } }
        
        protected virtual string RenameTableFormat { get { return "RENAME TABLE {0} TO {1}"; } }
        
        protected virtual string CreateSchemaFormat { get { return "CREATE SCHEMA {0}"; } }
        protected virtual string AlterSchemaFormat { get { return "ALTER SCHEMA {0} TRANSFER {1}.{2}"; } }
        protected virtual string DropSchemaFormat { get { return "DROP SCHEMA {0}"; } }
        
        protected virtual string CreateIndexFormat { get { return "CREATE {0}{1}INDEX {2} ON {3} ({4})"; } }
        protected virtual string DropIndexFormat { get { return "DROP INDEX {0}"; } }
        
        protected virtual string InsertDataFormat { get { return "INSERT INTO {0} ({1}) VALUES ({2})"; } }
        protected virtual string UpdateDataFormat { get { return "UPDATE {0} SET {1} WHERE {2}"; } }
        protected virtual string DeleteDataFormat { get { return "DELETE FROM {0} WHERE {1}"; } }
        
        protected virtual string CreateConstraintFormat { get { return "ALTER TABLE {0} ADD CONSTRAINT {1} {2} ({3})"; } }
        protected virtual string DeleteConstraintFormat { get { return "ALTER TABLE {0} DROP CONSTRAINT {1}"; } }
        protected virtual string CreateForeignKeyConstraintFormat { get { return "ALTER TABLE {0} ADD CONSTRAINT {1} FOREIGN KEY ({2}) REFERENCES {3} ({4}){5}{6}"; } }

        public virtual DatabaseCommand GenerateCreateTable(CreateTableExpression model)
        {
            string columns = ColumnGenerator.Generate(model.Columns);
            string cmd=string.Format(CreateTableFormat, Dialect.QuoteTableName(model.TableName), columns);
            return new DatabaseCommand(cmd);
        }

        public virtual DatabaseCommand GenerateDropTable(string tableName)
        {
            string cmd = string.Format(DropTableFormat, tableName);
            return new DatabaseCommand(cmd);
        }

        public virtual DatabaseCommand GenerateAlterTable(AlterTableExpression model)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var column in model.DropColumns)
            {
                sb.AppendFormat(DropColumnFormat, 
                    Dialect.QuoteTableName(model.TableName), 
                    Dialect.QuoteColumnName(column));
                sb.AppendLine(";");
            }
            foreach (var column in model.AlterColumns)
            {
                string columnDefinition = ColumnGenerator.Generate(column);
                sb.AppendFormat(AlterColumnFormat,
                    Dialect.QuoteTableName(model.TableName),
                    columnDefinition);
                sb.AppendLine(";");
            }
            foreach (var column in model.AddColumns)
            {
                string columnDefinition = ColumnGenerator.Generate(column);
                sb.AppendFormat(AddColumnFormat,
                    Dialect.QuoteTableName(model.TableName),
                    columnDefinition);
                sb.AppendLine(";");
            }
            return new DatabaseCommand(sb.ToString());
        }

        public virtual DatabaseCommand GenerateRawCommand(RawCommandExpression model)
        {
            throw new NotImplementedException();
        }

        public virtual DatabaseCommand GenerateRawCommand(string commandText)
        {
            throw new NotImplementedException();
        }

        public virtual DatabaseCommand GenerateInsert(InsertExpression model)
        {
            string names=string.Join(",",model.Parameters.Select(p=>p.Name));
            string values=string.Join(",",model.Parameters.Select(p=>
                !p.IsFunction?"@"+p.Name:this.TypeMapper.MapFunctionType(p.Function)));
            string cmd=string.Format(InsertDataFormat,
                model.TableName,
                names,
                values);
            DatabaseCommand ret = new DatabaseCommand(cmd);
            foreach (var p in model.Parameters)
            {
                if (!p.IsFunction)
                {
                    ret.Parameters.Add(p.Name, p.Value);
                }
            }
            return ret;
        }


        public DatabaseCommand GenerateRenameTable(string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public DatabaseCommand GenerateAddColumn(string tableName, string columnName, Models.DbType type, bool allowNull)
        {
            throw new NotImplementedException();
        }

        public DatabaseCommand GenerateAddColumn(string tableName, string columnName, Models.DbType type, int length, bool allowNull)
        {
            throw new NotImplementedException();
        }

        public DatabaseCommand GenerateDropColumn(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public DatabaseCommand GenerateAlterColumn(string tableName, string columnName, Models.DbType type, bool allowNull)
        {
            throw new NotImplementedException();
        }

        public DatabaseCommand GenerateAlterColumn(string tableName, string columnName, Models.DbType type, int length, bool allowNull)
        {
            throw new NotImplementedException();
        }

        public DatabaseCommand GenerateRenameColumn(string tableName, string oldName, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
