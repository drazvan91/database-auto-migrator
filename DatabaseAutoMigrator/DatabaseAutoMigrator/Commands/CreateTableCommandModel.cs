using DatabaseAutoMigrator.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DatabaseAutoMigrator.Commands
{
    public class CreateTableCommandModel:ICommandModel
    {
        internal string TableName{get;private set;}
        internal string TimestampColumnName { get;private  set; }
        internal ICollection<Column> Columns{get;  private set;}

        public CreateTableCommandModel(string tableName)
        {
            this.TableName = tableName;
            this.Columns = new Collection<Column>();
        }

        public CreateTableCommandModel AutoIncrementColumn(string name, ColumnDataType type)
        {
            Column c = new Column()
            {
                AllowNull = false,
                Name = name,
                Type = type,
                AutoIncrement = true
            };
            this.Columns.Add(c);
            return this;
        }

        public CreateTableCommandModel Column(string name, ColumnDataType type, bool allowNull = true)
        {
            Column c = new Column()
            {
                AllowNull=allowNull,
                Name=name,
                Type=type,
                Length=0
            };
            this.Columns.Add(c);
            return this;
        }
        public CreateTableCommandModel Column(string name, ColumnDataType type, int length = 0, bool allowNull = true)
        {
            Column c = new Column()
            {
                AllowNull = allowNull,
                Name = name,
                Type = type,
                Length=length
            };
            this.Columns.Add(c);
            return this;
        }

        public CreateTableCommandModel Timestamp()
        {
            return this.Timestamp("Timestamp");
        }
        public CreateTableCommandModel Timestamp(string columnName)
        {
            this.TimestampColumnName = columnName;
            return this;
        }
    }
}
