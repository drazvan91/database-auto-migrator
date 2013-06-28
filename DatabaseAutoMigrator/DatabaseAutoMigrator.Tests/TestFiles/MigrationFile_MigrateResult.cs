using DatabaseAutoMigrator.Models;

namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_MigrateResult:BaseMigrationFile
    {
        public string Migrate_99_Error1(IDatabaseContext db)
        {
            var table = Helper.CreateTable("Table1")
                .Column("Id", DbType.Int64, false)
                .Column("Name", DbType.String, 400, false);

            db.CreateTable(table);

            return "Error1";
        }
        public string Migrate_99_Error2(IDatabaseContext db)
        {
            var table = Helper.CreateTable("Table2")
                .AutoIncrementColumn("Id", DbType.Int32);
            db.CreateTable(table);
            return "Error2";
        }

        public string Migrate_99_Error3(IDatabaseContext db)
        {
            var table = Helper.CreateTable("Table1")
                .Column("Id", DbType.Int64, false)
                .Column("Name", DbType.String, 400, false);

            db.CreateTable(table);

            return "Error1";
        }
        public string Migrate_99_Error4(IDatabaseContext db)
        {
            var table = Helper.CreateTable("Table2")
                .AutoIncrementColumn("Id", DbType.Int32);
            db.CreateTable(table);
            return "Error2";
        }
    }
}
