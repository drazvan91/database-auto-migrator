using DatabaseAutoMigrator.Models;
using System;

namespace DatabaseAutoMigrator.Tests.TestFiles
{
    public class MigrationFile_08_TableExists:BaseMigrationFile
    {
        public string Migrate_08_01(IDatabaseContext db)
        {
            if (!db.TableExists("Table1"))
                throw new Exception("Table1 should exist");
            if (db.TableExists("StupidName"))
                throw new Exception("Stupidname should not exist");
            if (!db.TableExists("Table2"))
                throw new Exception("Table2 should exist");
            return "801";
        }
    }
}
