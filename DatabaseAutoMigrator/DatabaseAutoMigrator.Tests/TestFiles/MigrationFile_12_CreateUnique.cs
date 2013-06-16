
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_12_CreateUnique:BaseMigrationFile
    {
        public string Migrate_12_01(IDatabaseContext db)
        {
            var u = Helper.Unique("Table1")
                .AddColumn("Id1")
                .AddColumn("Bunica")
                .Clustered(true);
            db.CreateUniqueConstraint(u);

            u = Helper.Unique("Table1","someUK")
                .AddColumn("AddColumn1");
            db.CreateUniqueConstraint(u);

            return "1201";
        }
        public string Migrate_12_02(IDatabaseContext db)
        {
            var u = Helper.Unique("Table3")
                .AddColumn("Name");
            db.CreateUniqueConstraint(u);
            return "1202";
        }
    }
}