using DatabaseAutoMigrator;
using DatabaseAutoMigrator.Models;

namespace ConsoleTest
{
    public class MigrationFileTest1:BaseMigrationFile
    {
        public string Migrate_1(IDatabaseContext db)
        {
            var table=Helper.CreateTable("razvan")
                .Column("Id",DbType.Int64,false)
                .Column("Name",DbType.String,400,false);

            db.CreateTable(table);

            return "First migration";
        }
        public string Migrate_2(IDatabaseContext db)
        {
            db.DropTable("razvan");
            return "drop razvan";
        }

        public string Migrate_3(IDatabaseContext db)
        {
            var table = Helper.CreateTable("Lucian")
                .AutoIncrementColumn("Id", DbType.Int64);
            db.CreateTable(table);

            var table1=Helper.AlterTable("Lucian")
                .AddColumn("Razvan",DbType.String,423,true);
            db.AlterTable(table1);
            return "added razvan column";
        }

        public string Migrate_4(IDatabaseContext db)
        {
            var table = Helper.AlterTable("Lucian")
                .AddColumn("Razvan1", DbType.String, 23, true)
                .AddColumn("Ok", DbType.Int64, false)
                .DeleteColumn("Razvan");
                
            db.AlterTable(table);
            return "added razvan column";
        }

        public string Migrate_5(IDatabaseContext db)
        {
            var row = Helper.InsertRow("Lucian")
                .Parameter("ok", 4)
                .Parameter("Razvan1","acuma");
            db.InsertRow(row);
            return "insert row";
        }

        public string Migrate_6(IDatabaseContext db)
        {
            var c=Helper.CreateTable("Marius")
                .AutoIncrementColumn("Id", DbType.Int64)
                .Column("Name", DbType.String, false)
                .Column("Name1", DbType.String, false)
                .Timestamp();
            db.CreateTable(c);
            return "some tests";
        }
    }
}
