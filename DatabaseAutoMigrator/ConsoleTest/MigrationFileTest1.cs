
using DatabaseAutoMigrator;
using DatabaseAutoMigrator.Models;
namespace ConsoleTest
{
    public class MigrationFileTest1:IMigrationFile
    {
        public MigrateIteration Migrate_1()
        {
            var ret = new MigrateIteration("first migration");
            ret.CreateTable("Razvan1")
                .AutoIncrementColumn("Id", ColumnDataType.Int64)
                .Column("Name", ColumnDataType.String, 50, false)
                .Timestamp();
            return ret;
        }
        public MigrateIteration Migrate_2()
        {
            var ret = new MigrateIteration("sec 2");
            ret.DropTable("Razvan1");
            return ret;
        }
        public MigrateIteration Migrate_3()
        {
            var ret = new MigrateIteration("migrate");
            ret.CreateTable("Test3")
                .AutoIncrementColumn("Id", ColumnDataType.Int32)
                .Column("Name", ColumnDataType.String, 1000)
                .Timestamp();
            return ret;
        }
    }
}
