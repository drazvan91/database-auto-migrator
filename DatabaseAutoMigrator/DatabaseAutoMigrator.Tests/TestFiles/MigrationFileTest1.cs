
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFileTest1:IMigrationFile
    {
        public MigrateIteration Migrate_1()
        {
            var ret = new MigrateIteration("first migration");
            ret.CreateTable("Razvan")
                .AutoIncrementColumn("Id", ColumnDataType.Int64)
                .Column("Name", ColumnDataType.String, 50, false)
                .Timestamp();
            return ret;
        }
    }
}
