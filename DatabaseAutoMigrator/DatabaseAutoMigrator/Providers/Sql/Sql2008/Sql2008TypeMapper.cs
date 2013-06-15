using DatabaseAutoMigrator.Models;
using DatabaseAutoMigrator.Providers.Sql.Sql2005;

namespace DatabaseAutoMigrator.Providers.Sql.Sql2008
{
    public class Sql2008TypeMapper:Sql2005TypeMapper
    {
        protected override void SetupTypeMaps()
        {
            base.SetupTypeMaps();

            SetTypeMap(DbType.DateTime2, "DATETIME2");
            SetTypeMap(DbType.DateTimeOffset, "DATETIMEOFFSET");
            SetTypeMap(DbType.Date, "DATE");
            SetTypeMap(DbType.Time, "TIME");
        }
    }
}
