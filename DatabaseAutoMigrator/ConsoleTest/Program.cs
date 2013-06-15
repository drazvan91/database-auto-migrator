using DatabaseAutoMigrator.Providers.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connection = @"Server=.\SQLEXPRESS;Database=MigTest1;Trusted_Connection=True;";
            Sql2008Migrator migrator = new Sql2008Migrator(connection);
            MigrationFileTest1 file1 = new MigrationFileTest1();
            string lastId = migrator.Migrate(file1);
            Console.ReadKey();
        }
    }
}
