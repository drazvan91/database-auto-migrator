
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_04_AddColumn:BaseMigrationFile
    {
        public string Migrate_04_01_AddColumn1(IDatabaseContext db)
        {
            db.AddColumn("Table1", "AddColumn1", DbType.Int32, false);
            db.AddColumn("Table1", "AddColumn2", DbType.DateTime, false);
            db.AddColumn("Table1", "AddColumn3", DbType.Currency, true);
            return "401";
        }
        public string Migrate_04_02_AddColumn2(IDatabaseContext db)
        {
            db.AddColumn("Table2", "AddColumn1", DbType.String, 30,false);
            db.AddColumn("Table2", "AddColumn2", DbType.Time, false);
            db.AddColumn("Table2", "AddColumn3", DbType.AnsiStringFixedLength,20, true);
            return "402";
        }
    }
}
