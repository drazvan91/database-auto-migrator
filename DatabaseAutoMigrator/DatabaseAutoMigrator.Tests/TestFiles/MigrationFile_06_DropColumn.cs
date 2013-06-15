
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_06_DropColumn:BaseMigrationFile
    {
        public string Migrate_06_01_DropColumn1(IDatabaseContext db)
        {
            db.DropColumn("Table1", "AddColumn2");
            db.DropColumn("Table1", "AddColumn3");
            return "601";
        }
        public string Migrate_06_02_DropColumn2(IDatabaseContext db)
        {
            db.DropColumn("Table2", "AddColumn2");
            db.DropColumn("Table2", "AddColumn3");
            return "602";
        }
    }
}