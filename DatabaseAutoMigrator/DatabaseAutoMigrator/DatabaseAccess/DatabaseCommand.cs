using System.Collections.Generic;

namespace DatabaseAutoMigrator.DatabaseAccess
{
    public class DatabaseCommand
    {
        public string CommandText { get; set; }
        public IDictionary<string, object> Parameters { get; set; }

        public DatabaseCommand(string cmd)
        {
            this.CommandText = cmd;
            this.Parameters = new Dictionary<string, object>();
        }
    }
}
