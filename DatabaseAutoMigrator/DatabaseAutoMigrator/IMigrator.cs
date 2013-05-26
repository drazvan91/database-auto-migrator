using DatabaseAutoMigrator.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DatabaseAutoMigrator
{
    public interface IMigrator:IDisposable
    {
        string Migrate(IMigrationFile migrationFile);
        string Migrate(IEnumerable<IMigrationFile> migrationFiles);
        string Migrate(Assembly assembly, string nameSpace);
        
        ExecuteIterationResult ExecuteMigrateIteration(string migrationId, MigrateIteration iteration, string currentId);

        string GetLastMigrationID();
        void CreateMigrationTable();
        bool IsMigrationTableCreated();
        void InsertMigrationFingerPrint(string id, string description,IDatabaseTransaction transaction);
        
    }
}
