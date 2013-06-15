using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DatabaseAutoMigrator.Models.Expressions
{
    public class AlterTableExpression:BaseMigrationExpression
    {
        internal string TableName { get; private set; }
        internal ICollection<ColumnDefinition> AddColumns { get; private set; }
        internal ICollection<ColumnDefinition> AlterColumns { get; private set; }
        internal ICollection<string> DropColumns { get; private set; }

        public AlterTableExpression(string tableName)
        {
            this.TableName = tableName;
            this.AddColumns = new Collection<ColumnDefinition>();
            this.AlterColumns = new Collection<ColumnDefinition>();
            this.DropColumns = new Collection<string>();
        }

        public AlterTableExpression AddColumn(string name, DbType type, bool allowNull = true)
        {
            var c=new ColumnDefinition()
            {
                AllowNull = allowNull,
                Name = name,
                Type = type
            };
            this.AddColumns.Add(c);
            return this;
        }
        public AlterTableExpression AddColumn(string name, DbType type, int length = 0, bool allowNull = true)
        {
            ColumnDefinition c = new ColumnDefinition()
            {
                AllowNull = allowNull,
                Name = name,
                Type = type,
                Size = length
            };
            this.AddColumns.Add(c);
            return this;
        }
        public AlterTableExpression AddAutoIncrementColumn(string name, DbType type)
        {
            var c=new ColumnDefinition()
            {
                Name = name,
                Type = type,
                AllowNull = false,
                AutoIncrement = true
            };
            this.AddColumns.Add(c);
            return this;
        }

        public AlterTableExpression AlterColumn(string name, DbType type, bool allowNull = true)
        {
            var c=new ColumnDefinition()
            {
                Name = name,
                Type = type,
                AllowNull = allowNull
            };
            this.AlterColumns.Add(c);
            return this;
        }
        public AlterTableExpression AlterColumn(string name, DbType type, int length, bool allowNull = true)
        {
            var c = new ColumnDefinition()
            {
                Name = name,
                Type = type,
                AllowNull = allowNull,
                Size=length
            };
            this.AlterColumns.Add(c);
            return this;
        }

        public AlterTableExpression DeleteColumn(string columnName)
        {
            this.DropColumns.Add(columnName);
            return this;
        }
    }
}
