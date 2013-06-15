
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_10_CreateForeignKey:BaseMigrationFile
    {
        public string Migrate_10_01(IDatabaseContext db)
        {
            db.AddColumn("Table2", "Table1ID", DbType.Int64, false);
            var fk=Helper.CreateForeignKey("FK1","Table2","Table1")
                .MapColumn("Table1ID","Id");
            db.CreateForeignKey(fk);
            return "1001";
        }
        public string Migrate_10_02(IDatabaseContext db)
        {
            db.AddColumn("Table3", "Table11ID", DbType.Int64, false);
            db.AddColumn("Table3", "Table12ID", DbType.Int64, false);
            var fk = Helper.CreateForeignKey("FK1", "Table3", "Table1")
                .MapColumn("Table11ID", "Id")
                .MapColumn("Table12ID", "Id");
            db.CreateForeignKey(fk);
            return "1002";
        }
    }
}