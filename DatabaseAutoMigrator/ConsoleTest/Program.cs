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
        class dada
        {
            public string Name { get; set; }
            public int Dada { get; set; }
            public DateTime Ahaaa { get; set; }
        }
        static void Main(string[] args)
        {
            cuul(new dada()
            {
                Ahaaa=DateTime.Now
            });
            cuul(new
            {
                Id=3,
                Razvan="rser",
                Fain=DateTime.Now,
                Tare="ahahaaa"
            });
            string connection = @"Server=.\SQLEXPRESS;Database=MigTest1;Trusted_Connection=True;";
            SqlMigrator migrator = new SqlMigrator(connection);
            MigrationFileTest1 file1 = new MigrationFileTest1();
            long lastId = migrator.Migrate(file1);
            Console.ReadKey();
        }
        static void cuul(object a)
        {
            var ab = a.GetType();

        }
    }
}
