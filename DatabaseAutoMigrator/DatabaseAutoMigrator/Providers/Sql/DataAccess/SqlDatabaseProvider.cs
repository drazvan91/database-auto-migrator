using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Exceptions;
using DatabaseAutoMigrator.Logging;
using DatabaseAutoMigrator.Providers.Sql.DataAccess;
using System;
using System.Data.SqlClient;

namespace DatabaseAutoMigrator.Providers.Sql.DataAccess
{
    public class SqlDatabaseProvider:BaseDatabaseProvider
    {
        private bool handleSqlConnection = false;
        private SqlConnection sqlConnection;
        private string connectionString;
        private ILogger logger;

        private SqlTransaction currentTransaction { get; set; }

        public SqlDatabaseProvider(string connectionString)
        {
            logger = LoggerFactory.Current.Get("SqlDatabaseProvider");
            this.connectionString = connectionString;
            sqlConnection = new SqlConnection(connectionString);
            try
            {
                logger.Log("opening connection for connection string: "+connectionString, "init");
                this.sqlConnection.Open();
                this.handleSqlConnection = true;
                logger.Log("connection successful", "init");
            }
            catch
            {
                throw new WrongConnectionStringException();
            }
            init(sqlConnection);
        }
        public SqlDatabaseProvider(SqlConnection sqlConnection)
        {
            logger = LoggerFactory.Current.Get("SqlDatabaseProvider");
            init(sqlConnection);
        }
        private void init(SqlConnection connection)
        {
            this.sqlConnection = connection;
        }

        public override int ExecuteCommand(DatabaseCommand command)
        {
            SqlCommand cmd = new SqlCommand(command.CommandText, this.sqlConnection);
            cmd.Transaction = this.currentTransaction;
            foreach (var p in command.Parameters)
            {
                cmd.Parameters.Add(new SqlParameter(p.Key, p.Value));
            }
            logger.Log("executing - \r\n" + command.CommandText, "execute command");
            int rez= cmd.ExecuteNonQuery();
            logger.Log("result:" + rez, "execute command");
            return rez;
        }
        public override int ExecuteCommand(string command)
        {
            SqlCommand cmd = new SqlCommand(command, this.sqlConnection);
            cmd.Transaction = this.currentTransaction;
            logger.Log("executing - \r\n" + command, "execute command");
            int rez = cmd.ExecuteNonQuery();
            logger.Log("result:" + rez, "execute command");
            return rez;
        }

        public override IDatabaseReader ExecuteReaderCommand(DatabaseCommand command)
        {
            SqlCommand cmd = new SqlCommand(command.CommandText, this.sqlConnection);
            cmd.Transaction = this.currentTransaction;
            foreach (var p in command.Parameters)
            {
                cmd.Parameters.Add(new SqlParameter(p.Key, p.Value));
            }
            logger.Log("executing reader - \r\n" + command.CommandText, "execute reader");
            var dr= cmd.ExecuteReader(System.Data.CommandBehavior.Default);
            return new SqlDatabaseReader(dr);
        }
        public override IDatabaseReader ExecuteReaderCommand(string command)
        {
            SqlCommand cmd = new SqlCommand(command, this.sqlConnection);
            cmd.Transaction = this.currentTransaction;
            logger.Log("executing reader - \r\n" + command, "execute reader");
            var dr = cmd.ExecuteReader(System.Data.CommandBehavior.Default);
            return new SqlDatabaseReader(dr);
        }

        public override void Dispose()
        {
            if (this.handleSqlConnection)
            {
                this.sqlConnection.Close();
                this.sqlConnection.Dispose();
            }
        }

        public override void StartTransaction(string transactionName)
        {
            this.currentTransaction=sqlConnection.BeginTransaction(transactionName);
        }

        public override void CommitTransaction()
        {
            if (this.currentTransaction != null)
            {
                this.currentTransaction.Commit();
                this.currentTransaction = null;
            }
        }

        public override void RollbackTransaction()
        {
            if (this.currentTransaction != null)
            {
                this.currentTransaction.Rollback();
                this.currentTransaction = null;
            }
        }
    }
}
