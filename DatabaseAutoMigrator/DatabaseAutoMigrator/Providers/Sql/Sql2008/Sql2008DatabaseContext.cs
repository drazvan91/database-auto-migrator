using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Providers.Sql.DataAccess;

namespace DatabaseAutoMigrator.Providers.Sql.Sql2008
{
    public class Sql2008DatabaseContext:BaseDatabaseContext
    {
        public Sql2008DatabaseContext(IDatabaseProvider provider):
            base(provider,new Sql2008CommandGenerator())
        {
        }
    }
}
