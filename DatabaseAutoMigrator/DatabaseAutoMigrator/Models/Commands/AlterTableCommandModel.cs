using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace DatabaseAutoMigrator.Models.Commands
{
    public class AlterTableCommandModel:ICommandModel
    {
        internal string TableName { get; private set; }
        internal ICollection<Column> AddColumns { get; private set; }
        internal ICollection<Column> AlterColumns { get; private set; }
        internal ICollection<string> DropColumns { get; private set; }

        public AlterTableCommandModel(string tableName)
        {
            this.TableName = tableName;
            this.AddColumns = new Collection<Column>();
            this.AlterColumns = new Collection<Column>();
            this.DropColumns = new Collection<string>();
        }

        public AlterTableCommandModel AddColumn(string name, ColumnDataType type, bool allowNull = true)
        {
            var c=new Column()
            {
                AllowNull = allowNull,
                Name = name,
                Type = type
            };
            this.AddColumns.Add(c);
            return this;
        }
        public AlterTableCommandModel AddColumn(string name, ColumnDataType type, int length = 0, bool allowNull = true)
        {
            Column c = new Column()
            {
                AllowNull = allowNull,
                Name = name,
                Type = type,
                Length = length
            };
            this.AddColumns.Add(c);
            return this;
        }
        public AlterTableCommandModel AddAutoIncrementColumn(string name, ColumnDataType type)
        {
            var c=new Column()
            {
                Name = name,
                Type = type,
                AllowNull = false,
                AutoIncrement = true
            };
            this.AddColumns.Add(c);
            return this;
        }

        public AlterTableCommandModel AlterColumn(string name, ColumnDataType type, bool allowNull = true)
        {
            var c=new Column()
            {
                Name = name,
                Type = type,
                AllowNull = allowNull
            };
            this.AlterColumns.Add(c);
            return this;
        }
        public AlterTableCommandModel AlterColumn(string name, ColumnDataType type, int length, bool allowNull = true)
        {
            var c = new Column()
            {
                Name = name,
                Type = type,
                AllowNull = allowNull,
                Length=length
            };
            this.AlterColumns.Add(c);
            return this;
        }

        public AlterTableCommandModel DeleteColumn(string columnName)
        {
            this.DropColumns.Add(columnName);
            return this;
        }
    }
}
