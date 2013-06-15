using DatabaseAutoMigrator.Models;
using DatabaseAutoMigrator.Providers.Sql.Sql2000;

namespace DatabaseAutoMigrator.Providers.Sql.Sql2005
{
    public class Sql2005TypeMapper:Sql2000TypeMapper
    {
        protected override void SetupTypeMaps()
        {
            base.SetupTypeMaps();

            SetTypeMap(DbType.String, "NVARCHAR(MAX)", UnicodeTextCapacity);
            SetTypeMap(DbType.AnsiString, "VARCHAR(MAX)", AnsiTextCapacity);
            SetTypeMap(DbType.Binary, "VARBINARY(MAX)", ImageCapacity);
        }
    }
}
