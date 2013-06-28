using DatabaseAutoMigrator.Providers.Sql;
using DatabaseAutoMigrator.Tests.Helpers;
using DatabaseAutoMigrator.Tests.TestFiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace DatabaseAutoMigrator.Tests.Sql2008
{
    [TestClass]
    public class Test_MigrateResult
    {
        [TestMethod]
        public void TestMigrateResult()
        {
            SqlDatabaseHelper.ClearDatabase();
            Sql2008Migrator migrator = new Sql2008Migrator(SqlDatabaseHelper.DefaultConnectionString);

            var result=migrator.Migrate(new MigrationFile_MigrateResult());
            Assert.AreEqual("", result.StartId);
            Assert.AreEqual("99_Error2", result.EndId);
            Assert.IsTrue(result.Duration.Ticks > 0);
            Assert.AreEqual(3, result.Executed.Count);
            Assert.AreEqual(1, result.NotExecuted.Count);

            Assert.IsTrue(result.Executed[0].Duration.Ticks > 0);
            Assert.IsTrue(result.Executed[0].Success);
            Assert.IsNull(result.Executed[0].Error);
            Assert.AreEqual("DatabaseAutoMigrator.Tests.TestFiles.MigrationFile_MigrateResult",result.Executed[0].File);
            Assert.AreEqual("Migrate_99_Error1", result.Executed[0].MethodName);

            Assert.IsTrue(result.Executed[2].Duration.Ticks > 0);
            Assert.IsFalse(result.Executed[2].Success);
            Assert.IsNotNull(result.Executed[2].Error);
            Assert.AreEqual("DatabaseAutoMigrator.Tests.TestFiles.MigrationFile_MigrateResult", result.Executed[2].File);
            Assert.AreEqual("Migrate_99_Error3", result.Executed[2].MethodName);

            Assert.IsFalse(result.NotExecuted[0].Success);
            Assert.IsNull(result.NotExecuted[0].Error);
            Assert.AreEqual("DatabaseAutoMigrator.Tests.TestFiles.MigrationFile_MigrateResult", result.NotExecuted[0].File);
            Assert.AreEqual("Migrate_99_Error4", result.NotExecuted[0].MethodName);

            result = migrator.Migrate(new MigrationFile_MigrateResult());
            Assert.AreEqual("99_Error2", result.StartId);
            Assert.AreEqual("99_Error2", result.EndId);
            Assert.IsTrue(result.Duration.Ticks > 0);
            Assert.AreEqual(1, result.Executed.Count);
            Assert.AreEqual(1, result.NotExecuted.Count);

            migrator.Dispose();
        }
    }
}
