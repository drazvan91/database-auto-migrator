
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_05_AlterColumn:BaseMigrationFile
    {
        public string Migrate_05_01_AlterColumn1(IDatabaseContext db)
        {
            db.AlterColumn("Table1", "AddColumn1", DbType.Int32, true);
            db.AlterColumn("Table1", "AddColumn2", DbType.String, true);
            db.AlterColumn("Table1", "AddColumn3", DbType.String,30, true);
            return "501";
        }
        public string Migrate_05_02_AlterColumn2(IDatabaseContext db)
        {
            db.AlterColumn("Table2", "AddColumn1", DbType.String, 50,false);
            db.AlterColumn("Table2", "AddColumn2", DbType.DateTime, false);
            db.AlterColumn("Table2", "AddColumn3", DbType.AnsiStringFixedLength,20, true);
            return "502";
        }
    }
}
