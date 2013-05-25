using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseAutoMigrator.Providers.Sql;

namespace DatabaseAutoMigrator.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string connection = @"Server=.\SQLEXPRESS;Database=MigTest1;Trusted_Connection=True;";
            SqlMigrator migrator = new SqlMigrator(connection);
            TestFiles.MigrationFileTest1 file1 = new TestFiles.MigrationFileTest1();
            long lastId=migrator.Migrate(file1);
            Assert.IsTrue(lastId > 0);
        }
    }
}
