
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_09_RenameTable:BaseMigrationFile
    {
        public string Migrate_09_01(IDatabaseContext db)
        {
            db.RenameTable("Table1","Table10");
            db.RenameTable("Table2", "Table20");
            return "901";
        }
        public string Migrate_09_02(IDatabaseContext db)
        {
            db.RenameTable("Table10", "Table1");
            db.RenameTable("Table20", "Table2");
            return "902";
        }
    }
}