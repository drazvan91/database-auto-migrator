
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_13_DropConstraint:BaseMigrationFile
    {
        public string Migrate_13_01(IDatabaseContext db)
        {
            db.DropConstraint("Table2", "FK_Table2_Table1");
            db.DropConstraint("Table3", "FK_Table3_Table1");
            return "1301";
        }
    }
}