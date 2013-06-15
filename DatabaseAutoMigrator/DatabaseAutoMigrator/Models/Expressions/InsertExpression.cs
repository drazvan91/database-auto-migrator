using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DatabaseAutoMigrator.Models.Expressions
{
    public class InsertExpression : BaseMigrationExpression
    {
        internal string TableName { get; private set; }
        internal ICollection<ColumnParameter> Parameters { get; private set; }

        public InsertExpression(string tableName)
        {
            this.Parameters = new Collection<ColumnParameter>();
            this.TableName=tableName;
        }

        public InsertExpression Parameter(string name, object value)
        {
            ColumnParameter p = new ColumnParameter()
            {
                Name=name,
                Value=value
            };
            this.Parameters.Add(p);
            return this;
        }
        public InsertExpression FunctionParameter(string name, FunctionType functionType)
        {
            ColumnParameter p = new ColumnParameter()
            {
                Name = name,
                IsFunction=true,
                Function=functionType
            };
            this.Parameters.Add(p);
            return this;
        }
    }
}
