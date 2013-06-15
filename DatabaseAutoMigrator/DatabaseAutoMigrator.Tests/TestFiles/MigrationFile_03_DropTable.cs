
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_03_DropTable:BaseMigrationFile
    {
        public string Migrate_03_01_DropTable1(IDatabaseContext db)
        {
            db.DropTable("Table3");
            return "301";
        }
        public string Migrate_03_02_DropTable2(IDatabaseContext db)
        {
            var table = Helper.CreateTable("Table3")
               .AutoIncrementColumn("Id", DbType.Int64)
               .Column("Name", DbType.String, 100, true)
               .Column("Date", DbType.DateTime, false)
               .Timestamp();
            db.CreateTable(table);
            return "302";
        }
    }
}
