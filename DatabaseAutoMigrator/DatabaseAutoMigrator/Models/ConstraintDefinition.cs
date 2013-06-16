using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DatabaseAutoMigrator.Models
{
    public class ConstraintDefinition
    {
        internal string Name { get; set; }
        internal string TableName { get; set; }
        internal ICollection<string> Columns{get;set;}
        internal bool IsClustered { get; set; }

        public ConstraintDefinition(string tableName,string constraintName)
        {
            this.Name = constraintName;
            this.TableName = tableName;
            IsClustered = false;
            this.Columns = new Collection<string>();
        }

        public ConstraintDefinition AddColumn(string columnName)
        {
            this.Columns.Add(columnName);
            return this;
        }
        public ConstraintDefinition Clustered(bool value)
        {
            this.IsClustered = value;
            return this;
        }
    }
}
