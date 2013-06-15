
using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_02_AlterTable:BaseMigrationFile
    {
        public string Migrate_02_01_AlterTable_AddColumn(IDatabaseContext db)
        {
            var table = Helper.AlterTable("Table1")
                .AddColumn("Muciacia",DbType.Byte,true)
                .AddColumn("Senior",DbType.Boolean,false);
            db.AlterTable(table);

            table=Helper.AlterTable("Table1")
                .AddAutoIncrementColumn("Id1",DbType.Int64);
            db.AlterTable(table);

            return "201 - Alter table add column";
        }
        public string Migrate_02_02_AlterTable_AlterColumn(IDatabaseContext db)
        {
            var table = Helper.AlterTable("Table1")
                .AlterColumn("Name",DbType.Int32,true);
            table = Helper.AlterTable("Table3")
                .AlterColumn("Name", DbType.String, 80, false);
            return "202 - Alter table alter column";
        }

        public string Migrate_02_03_AlterTable_DropColumn(IDatabaseContext db)
        {
            var table = Helper.AlterTable("Table3")
                .DeleteColumn("Date")
                .DeleteColumn("Timestamp");
            db.AlterTable(table);
            table = Helper.AlterTable("Table1")
                .DeleteColumn("Muciacia");
            db.AlterTable(table);
            return "203 - Alter table drop column";
        }

        public string Migrate_02_04_AlterTable_Combined(IDatabaseContext db)
        {
            var table = Helper.AlterTable("Table3")
                .AddColumn("Date",DbType.Date,true)
                .AddColumn("Razvan",DbType.Double,false)
                .DeleteColumn("Id");
            db.AlterTable(table);

            table = Helper.AlterTable("Table1")
                .DeleteColumn("Senior")
                .AddColumn("Bunica", DbType.Decimal, false)
                .AddColumn("Ion", DbType.StringFixedLength, 10, false)
                .AlterColumn("Name", DbType.String, 500, true);
                

            db.AlterTable(table);
            return "203 - Alter table combined";
        }
    }
}
