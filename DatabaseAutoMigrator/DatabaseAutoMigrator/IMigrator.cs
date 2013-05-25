using DatabaseAutoMigrator.DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DatabaseAutoMigrator
{
    public interface IMigrator:IDisposable
    {
        long Migrate(IMigrationFile migrationFile);
        long Migrate(IEnumerable<IMigrationFile> migrationFiles);
        long Migrate(Assembly assembly, string nameSpace);
        
        ExecuteIterationResult ExecuteMigrateIteration(long migrationId, MigrateIteration iteration, long currentId);

        long GetLastMigrationID();
        void CreateMigrationTable();
        bool IsMigrationTableCreated();
        void InsertMigrationFingerPrint(long id, string description,IDatabaseTransaction transaction);
        
    }
}
