
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_01_CreateTable:BaseMigrationFile
    {
        public string Migrate_01_01_CreateTable1(IDatabaseContext db)
        {
            var table = Helper.CreateTable("Table1")
                .Column("Id", DbType.Int64, false)
                .Column("Name", DbType.String, 400, false);

            db.CreateTable(table);

            return "101 - Table1 created";
        }
        public string Migrate_01_02_CreateTable2(IDatabaseContext db)
        {
            var table = Helper.CreateTable("Table2")
                .AutoIncrementColumn("Id", DbType.Int32);
            db.CreateTable(table);
            return "102 - Table2 created";
        }

        public string Migrate_01_03_CreateTable3(IDatabaseContext db)
        {
            var table = Helper.CreateTable("Table3")
                .AutoIncrementColumn("Id", DbType.Int64)
                .Column("Name", DbType.String, 100, true)
                .Column("Date", DbType.DateTime, false)
                .Timestamp();
            db.CreateTable(table);
            return "103 - Table3 created";
        }
    }
}
