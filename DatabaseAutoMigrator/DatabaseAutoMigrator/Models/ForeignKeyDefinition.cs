using System.Collections.Generic;

namespace DatabaseAutoMigrator.Models
{
    public class ForeignKeyDefinition
    {
        internal string Name { get; set; }
        internal string ForeignTable { get; set; }
        internal string PrimaryTable { get; set; }
        internal IDictionary<string, string> Columns { get; set; }

        
        public ForeignKeyDefinition(string foreignTable,string primaryTable):
            this(
                foreignTable,
                primaryTable,
                string.Format("FK_{0}_{1}",foreignTable,primaryTable)
            )
        {
        }
        
        /// <param name="name">ex: FK_table1_table2</param>
        /// <param name="foreignTable">ex: table2</param>
        /// <param name="primaryTable">ex: table1</param>
        public ForeignKeyDefinition( string foreignTable,string primaryTable,string name)
        {
            this.Name = name;
            this.PrimaryTable = primaryTable;
            this.ForeignTable = foreignTable;
            this.Columns = new Dictionary<string, string>();
        }
        /// <param name="foreignColumn">ex: Table1Id</param>
        /// <param name="primaryColumn">ex: Id</param>
        /// <returns></returns>
        public ForeignKeyDefinition MapColumn(string foreignColumn,string primaryColumn)
        {
            this.Columns[primaryColumn] = foreignColumn;
            return this;
        }
    }
}
