using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Providers.Sql.DataAccess;

namespace DatabaseAutoMigrator.Providers.Sql.Sql2000
{
    public class Sql2000DatabaseContext:BaseDatabaseContext
    {
        public Sql2000DatabaseContext(IDatabaseProvider provider):
            base(provider,new Sql2000CommandGenerator())
        {
        }
    }
}
