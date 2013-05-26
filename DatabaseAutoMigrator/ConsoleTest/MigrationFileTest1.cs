
using DatabaseAutoMigrator;
using DatabaseAutoMigrator.Models;
namespace ConsoleTest
{
    public class MigrationFileTest1:BaseMigrationFile
    {
        
        
        public string Migrate_2013(IDatabaseContext db)
        {
            var table=Helper.CreateTable("razvan")
                .Column("Id",ColumnDataType.Int64,false)
                .Column("Name",ColumnDataType.String,400,false);

            db.CreateTable(table);

            return "First migration";
        }
        public string Migrate_2014(IDatabaseContext db)
        {
            db.DropTable("razvan",false);
            return "drop razvan";
        }

        public string Migrate_2015(IDatabaseContext db)
        {
            var table=Helper.AlterTable("Lucian")
                .AddColumn("Razvan",ColumnDataType.String,423,true);
            db.AlterTable(table);
            return "added razvan column";
        }

        public string Migrate_2016(IDatabaseContext db)
        {
            var table = Helper.AlterTable("Lucian")
                .AddColumn("Razvan1", ColumnDataType.String, 23, true)
                .AddColumn("Ok", ColumnDataType.Int64, false)
                .DeleteColumn("Razvan")
                .AlterColumn("Name", ColumnDataType.String, 123, true);
            db.AlterTable(table);
            return "added razvan column";
        }

        public string Migrate_2018(IDatabaseContext db)
        {
            var row = Helper.InsertRow("Lucian")
                .Parameter("Name", "yes")
                .Parameter("Add", 3)
                .Parameter("ok", 4)
                .Parameter("Razvan1","acuma");
            db.InsertRow(row);
            return "insert row";
        }
    }
}
