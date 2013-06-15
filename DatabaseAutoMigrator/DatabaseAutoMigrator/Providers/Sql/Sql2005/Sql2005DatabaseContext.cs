using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Providers.Sql.DataAccess;

namespace DatabaseAutoMigrator.Providers.Sql.Sql2005
{
    public class Sql2005DatabaseContext:BaseDatabaseContext
    {
        public Sql2005DatabaseContext(IDatabaseProvider provider):
            base(provider,new Sql2005CommandGenerator())
        {
        }
    }
}
