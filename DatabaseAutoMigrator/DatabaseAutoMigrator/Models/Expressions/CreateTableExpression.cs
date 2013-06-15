using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DatabaseAutoMigrator.Models.Expressions
{
    public class CreateTableExpression : BaseMigrationExpression
    {
        internal string TableName{get;private set;}
        internal string TimestampColumnName { get;private  set; }
        internal ICollection<ColumnDefinition> Columns{get;  private set;}

        public CreateTableExpression(string tableName)
        {
            this.TableName = tableName;
            this.Columns = new Collection<ColumnDefinition>();
        }

        public CreateTableExpression AutoIncrementColumn(string name, DbType type)
        {
            ColumnDefinition c = new ColumnDefinition()
            {
                AllowNull = false,
                Name = name,
                Type = type,
                AutoIncrement = true
            };
            this.Columns.Add(c);
            return this;
        }

        public CreateTableExpression Column(string name, DbType type, bool allowNull = true)
        {
            ColumnDefinition c = new ColumnDefinition()
            {
                AllowNull=allowNull,
                Name=name,
                Type=type
            };
            this.Columns.Add(c);
            return this;
        }
        public CreateTableExpression Column(string name, DbType type, int length = 0, bool allowNull = true)
        {
            ColumnDefinition c = new ColumnDefinition()
            {
                AllowNull = allowNull,
                Name = name,
                Type = type,
                Size=length
            };
            this.Columns.Add(c);
            return this;
        }

        public CreateTableExpression Timestamp()
        {
            return this.Timestamp("Timestamp");
        }
        public CreateTableExpression Timestamp(string columnName)
        {
            return this.Column(columnName, DbType.Timestamp, false);
        }
    }
}
