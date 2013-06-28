using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseAutoMigrator.Providers.Sql;
using DatabaseAutoMigrator.Tests.TestFiles;
using System.Diagnostics;
using System.Data.SqlClient;
using DatabaseAutoMigrator.Tests.Helpers;

namespace DatabaseAutoMigrator.Tests.Sql2008
{
    [TestClass]
    public class Test_Migrate_Assembly
    {
        
        [TestMethod]
        public void TestMigrateAssembly()
        {
            SqlDatabaseHelper.ClearDatabase();
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);
            
            var assembly=this.GetType().Assembly;
            string s = migrator.Migrate(assembly, "DatabaseAutoMigrator.Tests.TestFiles").EndId;
            Assert.AreEqual("13_01", s);

            s = migrator.Migrate(assembly, "DatabaseAutoMigrator.Tests.TestFiles").EndId;
            Assert.AreEqual("13_01", s);

            migrator.Dispose();
        }
        
    }
}
