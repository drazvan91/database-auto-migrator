
namespace DatabaseAutoMigrator.Providers.Sql
{
    public abstract class BaseSqlMigrator:BaseMigrator
    {
        public BaseSqlMigrator(IDatabaseContext context)
            : base(context)
        {

        }
        public override void Dispose()
        {
            //todoss
        }
    }
}
