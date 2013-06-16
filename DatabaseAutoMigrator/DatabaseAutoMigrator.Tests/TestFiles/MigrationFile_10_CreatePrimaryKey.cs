
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_10_CreatePrimaryKey:BaseMigrationFile
    {
        public string Migrate_10_01(IDatabaseContext db)
        {
            var pk = Helper.PrimaryKey("Table1").AddColumn("Id");
            db.CreatePrimaryKey(pk);
            return "1001";
        }
        public string Migrate_10_02(IDatabaseContext db)
        {
            var pk = Helper.PrimaryKey("Table2")
                .AddColumn("Id")
                .AddColumn("AddColumn1");
            db.CreatePrimaryKey(pk);
            
            pk = Helper.PrimaryKey("Table3")
                .AddColumn("Id");
            db.CreatePrimaryKey(pk);
            return "1002";
        }
    }
}