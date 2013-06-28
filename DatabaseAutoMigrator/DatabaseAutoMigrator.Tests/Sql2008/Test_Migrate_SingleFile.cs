using DatabaseAutoMigrator.Providers.Sql;
using DatabaseAutoMigrator.Tests.Helpers;
using DatabaseAutoMigrator.Tests.TestFiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseAutoMigrator.Tests.Sql2008
{
    [TestClass]
    public class Test_Migrate_SingleFile
    {
        [TestMethod]
        public void Test_01_CreateTable()
        {
            SqlDatabaseHelper.ClearDatabase();
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            
            string s = migrator.Migrate(new MigrationFile_01_CreateTable()).EndId;
            Assert.AreEqual( "01_03_CreateTable3",s);
            s = migrator.Migrate(new MigrationFile_01_CreateTable()).EndId;
            Assert.AreEqual("01_03_CreateTable3",s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_02_AlterTable()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            
            string s = migrator.Migrate(new MigrationFile_02_AlterTable()).EndId;
            Assert.AreEqual( "02_04_AlterTable_Combined",s);
            s = migrator.Migrate(new MigrationFile_01_CreateTable()).EndId;
            Assert.AreEqual("02_04_AlterTable_Combined", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_03_DropTable()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            
            string s = migrator.Migrate(new MigrationFile_03_DropTable()).EndId;
            Assert.AreEqual("03_02_DropTable2", s);
            s = migrator.Migrate(new MigrationFile_01_CreateTable()).EndId;
            Assert.AreEqual("03_02_DropTable2", s);
            migrator.Dispose();
        }
        
        [TestMethod]
        public void Test_04_AddColumn()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            string s = migrator.Migrate(new MigrationFile_04_AddColumn()).EndId;
            Assert.AreEqual("04_02_AddColumn2", s);
            s = migrator.Migrate(new MigrationFile_04_AddColumn()).EndId;
            Assert.AreEqual("04_02_AddColumn2", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_05_AlterColumn()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            string s = migrator.Migrate(new MigrationFile_05_AlterColumn()).EndId;
            Assert.AreEqual("05_02_AlterColumn2", s);
            s = migrator.Migrate(new MigrationFile_05_AlterColumn()).EndId;
            Assert.AreEqual("05_02_AlterColumn2", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_06_DropColumn()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            string s = migrator.Migrate(new MigrationFile_06_DropColumn()).EndId;
            Assert.AreEqual("06_02_DropColumn2", s);
            s = migrator.Migrate(new MigrationFile_06_DropColumn()).EndId;
            Assert.AreEqual("06_02_DropColumn2", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_07_RenameColumn()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            string s = migrator.Migrate(new MigrationFile_07_RenameColumn()).EndId;
            Assert.AreEqual("07_02_RenameColumn", s);
            s = migrator.Migrate(new MigrationFile_07_RenameColumn()).EndId;
            Assert.AreEqual("07_02_RenameColumn", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_08_TableExists()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            string s = migrator.Migrate(new MigrationFile_08_TableExists()).EndId;
            Assert.AreEqual("08_01", s);
            s = migrator.Migrate(new MigrationFile_08_TableExists()).EndId;
            Assert.AreEqual("08_01", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_09_RenameTable()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            string s = migrator.Migrate(new MigrationFile_09_RenameTable()).EndId;
            Assert.AreEqual("09_02", s);
            s = migrator.Migrate(new MigrationFile_09_RenameTable()).EndId;
            Assert.AreEqual("09_02", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_10_CreatePrimaryKey()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            string s = migrator.Migrate(new MigrationFile_10_CreatePrimaryKey()).EndId;
            Assert.AreEqual("10_02", s);
            s = migrator.Migrate(new MigrationFile_10_CreatePrimaryKey()).EndId;
            Assert.AreEqual("10_02", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_11_CreateForeignKey()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            string s = migrator.Migrate(new MigrationFile_11_CreateForeignKey()).EndId;
            Assert.AreEqual("11_02", s);
            s = migrator.Migrate(new MigrationFile_11_CreateForeignKey()).EndId;
            Assert.AreEqual("11_02", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_12_CreateUnique()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            string s = migrator.Migrate(new MigrationFile_12_CreateUnique()).EndId;
            Assert.AreEqual("12_02", s);
            s = migrator.Migrate(new MigrationFile_12_CreateUnique()).EndId;
            Assert.AreEqual("12_02", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void Test_13_DropConstraint()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            string s = migrator.Migrate(new MigrationFile_13_DropConstraint()).EndId;
            Assert.AreEqual("13_01", s);
            s = migrator.Migrate(new MigrationFile_13_DropConstraint()).EndId;
            Assert.AreEqual("13_01", s);
            migrator.Dispose();
        }

    }
}
