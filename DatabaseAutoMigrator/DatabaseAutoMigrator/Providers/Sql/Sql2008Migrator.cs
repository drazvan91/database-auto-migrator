using DatabaseAutoMigrator.Providers.Sql.DataAccess;
using DatabaseAutoMigrator.Providers.Sql.Sql2008;
using System.Data.SqlClient;

namespace DatabaseAutoMigrator.Providers.Sql
{
    public class Sql2008Migrator:BaseSqlMigrator
    {
        public Sql2008Migrator(string connectionString):
            base(
            new Sql2008DatabaseContext(
                new SqlDatabaseProvider(connectionString))
                )
        {
        }
        public Sql2008Migrator(SqlConnection sqlConnection):
            base(
            new Sql2008DatabaseContext(
                new SqlDatabaseProvider(sqlConnection))
                )
        {
            
        }
    }
}
