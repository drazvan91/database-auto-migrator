using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseAutoMigrator.Providers.Sql;
using DatabaseAutoMigrator.Tests.TestFiles;
using System.Diagnostics;
using System.Data.SqlClient;

namespace DatabaseAutoMigrator.Tests
{
    [TestClass]
    public class Sql2008MigrationTest
    {
        static string connectionString = @"Server=.\SQLEXPRESS;Database=UnitTest1;Trusted_Connection=True;";

        static Sql2008MigrationTest()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("EXEC sp_msforeachtable 'DROP TABLE ?'");
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            Debug.WriteLine("Here - "+DateTime.Now.ToLongTimeString());
        }

        [TestMethod]
        public void TestCreateTable()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s=migrator.Migrate(new MigrationFile_01_CreateTable());
            Assert.AreEqual( "01_03_CreateTable3",s);
            s = migrator.Migrate(new MigrationFile_01_CreateTable());
            Assert.AreEqual("01_03_CreateTable3",s);
            migrator.Dispose();
        }

        [TestMethod]
        public void TestAlterTable()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_02_AlterTable());
            Assert.AreEqual( "02_04_AlterTable_Combined",s);
            s = migrator.Migrate(new MigrationFile_01_CreateTable());
            Assert.AreEqual("02_04_AlterTable_Combined", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void TestDropTable()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_03_DropTable());
            Assert.AreEqual("03_02_DropTable2", s);
            s = migrator.Migrate(new MigrationFile_01_CreateTable());
            Assert.AreEqual("03_02_DropTable2", s);
            migrator.Dispose();
        }
        
        [TestMethod]
        public void TestAddColumn()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_04_AddColumn());
            Assert.AreEqual("04_02_AddColumn2", s);
            s = migrator.Migrate(new MigrationFile_04_AddColumn());
            Assert.AreEqual("04_02_AddColumn2", s);
            migrator.Dispose();
        }
        [TestMethod]
        public void TestAlterColumn()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_05_AlterColumn());
            Assert.AreEqual("05_02_AlterColumn2", s);
            s = migrator.Migrate(new MigrationFile_05_AlterColumn());
            Assert.AreEqual("05_02_AlterColumn2", s);
            migrator.Dispose();
        }
        [TestMethod]
        public void TestDropColumn()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_06_DropColumn());
            Assert.AreEqual("06_02_DropColumn2", s);
            s = migrator.Migrate(new MigrationFile_06_DropColumn());
            Assert.AreEqual("06_02_DropColumn2", s);
            migrator.Dispose();
        }
        [TestMethod]
        public void TestRenameColumn()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_07_RenameColumn());
            Assert.AreEqual("07_02_RenameColumn", s);
            s = migrator.Migrate(new MigrationFile_07_RenameColumn());
            Assert.AreEqual("07_02_RenameColumn", s);
            migrator.Dispose();
        }
        [TestMethod]
        public void TestTableExists()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_08_TableExists());
            Assert.AreEqual("08_01", s);
            s = migrator.Migrate(new MigrationFile_08_TableExists());
            Assert.AreEqual("08_01", s);
            migrator.Dispose();
        }
        [TestMethod]
        public void TestRenameTable()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_09_RenameTable());
            Assert.AreEqual("09_02", s);
            s = migrator.Migrate(new MigrationFile_09_RenameTable());
            Assert.AreEqual("09_02", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void TestCreatePrimaryKey()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_10_CreatePrimaryKey());
            Assert.AreEqual("10_02", s);
            s = migrator.Migrate(new MigrationFile_10_CreatePrimaryKey());
            Assert.AreEqual("10_02", s);
            migrator.Dispose();
        }

        [TestMethod]
        public void TestCreateForeignKey()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_11_CreateForeignKey());
            Assert.AreEqual("11_02", s);
            s = migrator.Migrate(new MigrationFile_11_CreateForeignKey());
            Assert.AreEqual("11_02", s);
            migrator.Dispose();
        }
        [TestMethod]
        public void TestCreateUnique()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_12_CreateUnique());
            Assert.AreEqual("12_02", s);
            s = migrator.Migrate(new MigrationFile_12_CreateUnique());
            Assert.AreEqual("12_02", s);
            migrator.Dispose();
        }
        [TestMethod]
        public void TestDropConstraint()
        {
            Sql2008Migrator migrator = new Sql2008Migrator(connectionString);
            string s = migrator.Migrate(new MigrationFile_13_DropConstraint());
            Assert.AreEqual("13_01", s);
            s = migrator.Migrate(new MigrationFile_13_DropConstraint());
            Assert.AreEqual("13_01", s);
            migrator.Dispose();
        }
    }
}
