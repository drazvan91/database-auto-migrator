using DatabaseAutoMigrator.Models;
using System;
using System.Linq;
using System.Text;
namespace DatabaseAutoMigrator.Providers.Sql
{
    public class SqlDialect:IDialect
    {
        public string QuoteColumnName(string name)
        {
            return string.Format("[{0}]", name);
        }

        public string QuoteTableName(string name)
        {
            return string.Format("[{0}]", name);
        }

        public string QuoteContraintName(string name)
        {
            return string.Format("[{0}]", name);
        }

        public string Null
        {
            get { return "NULL"; }
        }

        public string NotNull
        {
            get { return "NOT NULL"; }
        }

        public string Unique
        {
            get { return "UNIQUE"; }
        }

        public string PrimaryKey
        {
            get { return "PRIMARY KEY"; }
        }

        public string Identity
        {
            get { return "IDENTITY(1,1)"; }
        }
        public string IfExists
        {
            get { return "IF EXISTS"; }
        }

        public string Default(object defaultvalue)
        {
            return string.Format("DEFAULT {0}", defaultvalue);
        }
    }
}
