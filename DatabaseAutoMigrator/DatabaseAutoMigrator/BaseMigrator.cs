using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Logging;
using DatabaseAutoMigrator.Commands;
using DatabaseAutoMigrator.Models;

namespace DatabaseAutoMigrator
{
    public abstract class BaseMigrator<TCommand,TTransaction,TReader> : IMigrator
        where TCommand: IDatabaseCommand
        where TTransaction: IDatabaseTransaction
        where TReader: IDatabaseReader
    {
        public string MigrationTableName = "DatabaseAutoMigrator";

        protected IDatabaseProvider<TCommand, TTransaction, TReader> databaseProvider { get; set; }
        private ILogger logger { get; set; }

        public BaseMigrator()
        {
            logger = LoggerFactory.Current.Get("BaseMigrator");
        }

        public long Migrate(IMigrationFile migrationFile)
        {
            logger.Log("starting migration from single file", "migrate");
            logger.Log("getting last migration id", "migrate");
            long lastMigrationId = this.GetLastMigrationID();
            logger.Log("last id=" + lastMigrationId, "migrate");
            var methods = migrationFile.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var allMigrations = (from m in methods
                                 where m.Name.StartsWith("Migrate_")
                                 select m);
            var nextMigrations = (from m in allMigrations
                                  let id = int.Parse(m.Name.Remove(0, "Migrate_".Length))
                                  where id > lastMigrationId
                                  orderby m.Name
                                  select new
                                  {
                                      Method = m,
                                      Id = id
                                  });
            logger.Log("number of valid migration methods: " + nextMigrations.Count(), "migrate");

            var currentMigrationId = lastMigrationId;
            foreach (var iteration in nextMigrations)
            {
                MigrateIteration rez = (MigrateIteration)iteration.Method.Invoke(migrationFile, new object[0]);
                logger.Log("starting execute migration #" + iteration.Id, "migrate");
                var result = this.ExecuteMigrateIteration(iteration.Id, rez, currentMigrationId);
                if (result.Success == false)
                {
                    logger.Log("Error at migration #" + iteration.Id+". "+result.Error.Message, "migrate");
                    break;
                }
                else
                {
                    currentMigrationId = iteration.Id;
                }
            }
            logger.Log("migration finished with id: " + currentMigrationId);
            return currentMigrationId;
        }
        public long Migrate(IEnumerable<IMigrationFile> migrationFiles)
        {
            return 0;
        }
        public long Migrate(Assembly assembly, string nameSpace)
        {
            return 0;
        }

        public ExecuteIterationResult ExecuteMigrateIteration(long migrationId, MigrateIteration iteration, long currentId)
        {
            if (iteration != null)
            {
                try
                {
                    using (TTransaction transaction = this.databaseProvider.CreateTransaction("auto_migrate_transaction"))
                    {
                        if (iteration.Commands != null && iteration.Commands.Count > 0)
                        {
                            foreach (var comm in iteration.Commands)
                            {
                                databaseProvider.ExecuteCommand(comm, transaction);
                            }
                        }
                        
                        this.InsertMigrationFingerPrint(migrationId, iteration.Description,transaction);
                        transaction.Commit();
                        return new ExecuteIterationResult();
                    }
                }
                catch (Exception ex)
                {
                    return new ExecuteIterationResult(ex);
                }
            }
            return new ExecuteIterationResult();
        }

        #region Migration Table
        public virtual long GetLastMigrationID()
        {
            string cmd = string.Format(@"select Id from {0} order by Id desc", this.MigrationTableName);
            RawCommandModel rawCommand = new RawCommandModel(cmd);
            var reader = this.databaseProvider.ExecuteReaderCommand(rawCommand);
            long id = 0;
            if (reader.Read())
            {
                id = int.Parse(reader.Get("Id").ToString());
            }
            reader.Close();
            reader.Dispose();
            return id;
        }

        public virtual void CreateMigrationTable()
        {
            CreateTableCommandModel table = new CreateTableCommandModel(this.MigrationTableName)
                .Column("Id", ColumnDataType.Int64,false)
                .Column("Executed", ColumnDataType.DateTime, false)
                .Column("Description", ColumnDataType.String, 500)
                .Timestamp();
            this.databaseProvider.ExecuteCommand(table);
        }

        public virtual bool IsMigrationTableCreated()
        {
            string cmd = string.Format(@"SELECT * FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = '{0}'", this.MigrationTableName);

            RawCommandModel rawCommand = new RawCommandModel(cmd);
            var reader = this.databaseProvider.ExecuteReaderCommand(rawCommand);
            bool exists = false;
            if (reader.Read())
                exists = true;
            reader.Close();
            reader.Dispose();
            return exists;
        }

        public virtual void InsertMigrationFingerPrint(long id, string description, IDatabaseTransaction transaction)
        {
            InsertCommandModel cmd = new InsertCommandModel(this.MigrationTableName)
                .Parameter("Id", id)
                .Parameter("Description", description)
                .FunctionParameter("Executed", "GETDATE()");
            this.databaseProvider.ExecuteCommand(cmd,(TTransaction)transaction);
        }
        #endregion
        public abstract void Dispose();

    }
}
