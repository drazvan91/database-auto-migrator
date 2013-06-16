using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseAutoMigrator.Providers.Sql;
using DatabaseAutoMigrator.Tests.TestFiles;
using System.Diagnostics;
using System.Data.SqlClient;
using DatabaseAutoMigrator.Tests.Helpers;

namespace DatabaseAutoMigrator.Tests
{
    [TestClass]
    public class Sql2008Migration_MultiFiles
    {
        
        [TestMethod]
        public void TestMigrateMultipleFiles1()
        {
            SqlDatabaseHelper.ClearDatabase();
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            
            var files = new IMigrationFile[]{
                new MigrationFile_01_CreateTable(),
                new MigrationFile_02_AlterTable(),
                new MigrationFile_03_DropTable()
            };
            string s=migrator.Migrate(files);
            Assert.AreEqual("03_02_DropTable2", s);

            files = new IMigrationFile[]{
                new MigrationFile_04_AddColumn(),
                new MigrationFile_05_AlterColumn(),
                new MigrationFile_06_DropColumn(),
                new MigrationFile_07_RenameColumn()
            };
            s = migrator.Migrate(files);
            Assert.AreEqual("07_02_RenameColumn", s);

            files = new IMigrationFile[]{
                new MigrationFile_08_TableExists(),
                new MigrationFile_09_RenameTable(),
                new MigrationFile_10_CreatePrimaryKey()
            };
            s = migrator.Migrate(files);
            Assert.AreEqual("10_02", s);

            files = new IMigrationFile[]{
                new MigrationFile_11_CreateForeignKey(),
                new MigrationFile_12_CreateUnique(),
                new MigrationFile_13_DropConstraint()
            };
            s = migrator.Migrate(files);
            Assert.AreEqual("13_01", s);

            migrator.Dispose();
        }

        [TestMethod]
        public void TestMigrateMultipleFiles2()
        {
            SqlDatabaseHelper.ClearDatabase();
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);

            var files = new IMigrationFile[]{
                new MigrationFile_03_DropTable(),
                new MigrationFile_01_CreateTable(),
                new MigrationFile_02_AlterTable()
            };
            string s = migrator.Migrate(files);
            Assert.AreEqual("03_02_DropTable2", s);

            files = new IMigrationFile[]{
                new MigrationFile_04_AddColumn(),
                new MigrationFile_07_RenameColumn(),
                new MigrationFile_05_AlterColumn(),
                new MigrationFile_06_DropColumn(),
            };
            s = migrator.Migrate(files);
            Assert.AreEqual("07_02_RenameColumn", s);

            files = new IMigrationFile[]{
                new MigrationFile_08_TableExists(),
                new MigrationFile_10_CreatePrimaryKey(),
                new MigrationFile_09_RenameTable()
            };
            s = migrator.Migrate(files);
            Assert.AreEqual("10_02", s);

            files = new IMigrationFile[]{
                new MigrationFile_13_DropConstraint(),
                new MigrationFile_11_CreateForeignKey(),
                new MigrationFile_12_CreateUnique()
            };
            s = migrator.Migrate(files);
            Assert.AreEqual("13_01", s);

            files = new IMigrationFile[]{
                new MigrationFile_03_DropTable(),
                new MigrationFile_01_CreateTable(),
                new MigrationFile_02_AlterTable(),
                new MigrationFile_04_AddColumn(),
                new MigrationFile_07_RenameColumn(),
                new MigrationFile_05_AlterColumn(),
                new MigrationFile_06_DropColumn(),
                new MigrationFile_13_DropConstraint(),
                new MigrationFile_11_CreateForeignKey(),
                new MigrationFile_12_CreateUnique(),
                new MigrationFile_08_TableExists(),
                new MigrationFile_10_CreatePrimaryKey(),
                new MigrationFile_09_RenameTable()
            };
            s = migrator.Migrate(files);
            Assert.AreEqual("13_01", s);

            migrator.Dispose();
        }
        [TestMethod]
        public void TestMigrateMultipleFiles3()
        {
            SqlDatabaseHelper.ClearDatabase();
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);

            var files = new IMigrationFile[]{
                new MigrationFile_03_DropTable(),
                new MigrationFile_01_CreateTable(),
                new MigrationFile_02_AlterTable(),
                new MigrationFile_04_AddColumn(),
                new MigrationFile_07_RenameColumn(),
                new MigrationFile_05_AlterColumn(),
                new MigrationFile_06_DropColumn(),
                new MigrationFile_08_TableExists(),
                new MigrationFile_10_CreatePrimaryKey(),
                new MigrationFile_09_RenameTable(),
                new MigrationFile_13_DropConstraint(),
                new MigrationFile_11_CreateForeignKey(),
                new MigrationFile_12_CreateUnique()
            };
            string s = migrator.Migrate(files);
            Assert.AreEqual("13_01", s);

            migrator.Dispose();
        }
        
    }
}
