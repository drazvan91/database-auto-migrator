using DatabaseAutoMigrator.DatabaseAccess;
using System.Data.SqlClient;

namespace DatabaseAutoMigrator.Providers.Sql.DataAccess
{
    public class SqlDatabaseReader:IDatabaseReader
    {
        protected SqlDataReader dataReader { get; set; }

        public SqlDatabaseReader(SqlDataReader dataReader)
        {
            this.dataReader=dataReader;
        }

        public bool Read()
        {
            return dataReader.Read();
        }

        public object Get(int index)
        {
            return dataReader[index];
        }

        public object Get(string columnName)
        {
            return dataReader[columnName];
        }
        public void Close()
        {
            dataReader.Close();
        }

        public void Dispose()
        {
            dataReader.Dispose();
        }
    }
}
