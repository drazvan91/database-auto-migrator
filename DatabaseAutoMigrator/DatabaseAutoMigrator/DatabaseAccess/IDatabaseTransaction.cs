using System;

namespace DatabaseAutoMigrator.DatabaseAccess
{
    public interface IDatabaseTransaction:IDisposable
    {
        void Commit();
    }
}
