
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_07_RenameColumn:BaseMigrationFile
    {
        public string Migrate_07_01_RenameColumn(IDatabaseContext db)
        {
            db.RenameColumn("Table1","AddColumn1","AddColumn10");
            db.RenameColumn("Table2", "AddColumn1", "AddColumn20");
            return "701";
        }
        public string Migrate_07_02_RenameColumn(IDatabaseContext db)
        {
            db.RenameColumn("Table1", "AddColumn10", "AddColumn1");
            db.RenameColumn("Table2", "AddColumn20", "AddColumn1");
            return "702";
        }
    }
}