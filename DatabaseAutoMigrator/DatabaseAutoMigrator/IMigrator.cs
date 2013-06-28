using DatabaseAutoMigrator.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DatabaseAutoMigrator
{
    public interface IMigrator:IDisposable
    {
        MigrationResult Migrate(IMigrationFile migrationFile);
        MigrationResult Migrate(IEnumerable<IMigrationFile> migrationFiles);
        MigrationResult Migrate(Assembly assembly, string nameSpace);
        
        ExecuteIterationResult ExecuteMigrateIteration(MigrateIteration iteration, string currentId);

        string GetLastMigrationID();
        void CreateMigrationTable();
        bool IsMigrationTableCreated();
        void InsertMigrationFingerPrint(string id, string description);
        
    }
}
