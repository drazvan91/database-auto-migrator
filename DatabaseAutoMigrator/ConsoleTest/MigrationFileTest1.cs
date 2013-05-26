
using DatabaseAutoMigrator;
using DatabaseAutoMigrator.Models;
namespace ConsoleTest
{
    public class MigrationFileTest1:IMigrationFile
    {
        public string Migrate_1(MigrateIteration e)
        {
            e.CreateTable("Razvan1")
                .AutoIncrementColumn("Id", ColumnDataType.Int64)
                .Column("Name", ColumnDataType.String, 50, false)
                .Timestamp();
            return "First migration";
        }
        public string Migrate_2(MigrateIteration e)
        {
            e.DropTable("Razvan1");
            return "second migration";
        }
        public string Migrate_3(MigrateIteration e)
        {
            e.CreateTable("Test3")
                .AutoIncrementColumn("Id", ColumnDataType.Int32)
                .Column("Name", ColumnDataType.String, 1000)
                .Timestamp();
            return "added test3 table";
        }

        public string Migrate_4(MigrateIteration e)
        {
            e.DropTable("Iancu");
            return "deleted iancu table";
        }

        public string Migrate_5(MigrateIteration e)
        {
            e.AlterTable("Lucian")
                .AddColumn("Add", ColumnDataType.Int16, false)
                .DeleteColumn("Adaugata")
                .AlterColumn("Name", ColumnDataType.String, 500, false);
            e.DropTable("Table1");
            return "yey";

        }
        public string Migrate_6(MigrateIteration e)
        {
            e.AlterTable("Razvan1")
                .AddColumn("Adaugata1", ColumnDataType.Int32, false)
                .AddColumn("Adaugata2", ColumnDataType.Int64, true);
            return "yes6";
        }
        public string Migrate_7(MigrateIteration e)
        {
            e.CreateTable("test")
                .AutoIncrementColumn("Id", ColumnDataType.Int64);

            return "yes6";
        }
        public string Migrate_8(MigrateIteration e)
        {
            e.AlterTable("test")
                .AlterColumn("Id", ColumnDataType.Int64, false);
            return "da";
        }
    }
}
