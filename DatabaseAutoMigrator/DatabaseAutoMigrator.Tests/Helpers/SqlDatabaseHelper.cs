using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DatabaseAutoMigrator.Tests.Helpers
{
    public static class SqlDatabaseHelper
    {
        public static string DefaultConnectionString = @"Server=.\SQLEXPRESS;Database=UnitTest1;Trusted_Connection=True;";

        public static void ClearDatabase()
        {
            ClearDatabase(DefaultConnectionString);
        }
        public static void ClearDatabase(string connString)
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand cmd = new SqlCommand("EXEC sp_msforeachtable 'DROP TABLE ?'");
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            Debug.WriteLine("Database cleared - " + DateTime.Now.ToLongTimeString());
        }
    }
}
