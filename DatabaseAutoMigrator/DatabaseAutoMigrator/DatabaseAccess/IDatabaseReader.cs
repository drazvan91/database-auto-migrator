using System;

namespace DatabaseAutoMigrator.DatabaseAccess
{
    public interface IDatabaseReader:IDisposable
    {
        bool Read();
        object Get(int index);
        object Get(string columnName);
        void Close();
    }
}
