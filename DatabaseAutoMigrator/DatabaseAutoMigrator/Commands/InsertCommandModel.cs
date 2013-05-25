using DatabaseAutoMigrator.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace DatabaseAutoMigrator.Commands
{
    public class InsertCommandModel:ICommandModel
    {
        internal string TableName { get; private set; }
        internal ICollection<ColumnParameter> Parameters { get; private set; }

        public InsertCommandModel(string tableName)
        {
            this.Parameters = new Collection<ColumnParameter>();
            this.TableName=tableName;
        }

        public InsertCommandModel Parameter(string name, object value)
        {
            ColumnParameter p = new ColumnParameter()
            {
                Name=name,
                Value=value
            };
            this.Parameters.Add(p);
            return this;
        }
        public InsertCommandModel FunctionParameter(string name, string value)
        {
            ColumnParameter p = new ColumnParameter()
            {
                Name = name,
                IsFunction=true,
                Function=value
            };
            this.Parameters.Add(p);
            return this;
        }
    }
}
