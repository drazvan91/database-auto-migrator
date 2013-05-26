using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Logging;
using DatabaseAutoMigrator.Models;
using DatabaseAutoMigrator.Models.Commands;

namespace DatabaseAutoMigrator
{
    public abstract class BaseMigrator<TCommand,TReader> : IMigrator
        where TCommand: IDatabaseCommand
        where TReader:IDatabaseReader
    {
        public string MigrationTableName = "DatabaseAutoMigrator";

        protected BaseDatabaseContext<TCommand,TReader> DatabaseContext { get; set; }
        protected IDatabaseProvider<TCommand,TReader> DatabaseProvider {get;set;}

        private ILogger logger { get; set; }

        public BaseMigrator()
        {
            logger = LoggerFactory.Current.Get("BaseMigrator");
        }

        public string Migrate(IMigrationFile migrationFile)
        {
            logger.Log("starting migration from single file", "migrate");
            
            var methods = migrationFile.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var allMigrations = (from m in methods
                                 where m.Name.StartsWith("Migrate_")
                                 select new MigrateIteration()
                                 {
                                     File=migrationFile,
                                     Id = m.Name.Remove(0, "Migrate_".Length),
                                     Method=m
                                 });
            return migrateMethods(allMigrations);
            
        }
        private string migrateMethods(IEnumerable<MigrateIteration> methods)
        {
            logger.Log("getting last migration id", "migrate");
            string lastMigrationId = this.GetLastMigrationID();
            logger.Log("last id=" + lastMigrationId, "migrate");

            var nextMigrations = (from m in methods
                                  where string.Compare(m.Id ,lastMigrationId)>0
                                  orderby m.Id
                                  select m);
            logger.Log("number of valid migration methods: " + nextMigrations.Count(), "migrate");

            var currentMigrationId = lastMigrationId;
            foreach (var iteration in nextMigrations)
            {
                logger.EmptyLine();
                logger.Log("starting execute migration #" + iteration.Id, "migrate");
                var result = this.ExecuteMigrateIteration(iteration, currentMigrationId);
                if (result.Success == false)
                {
                    logger.EmptyLine();
                    logger.Log("Error at migration #" + iteration.Id + ". " + result.Error.Message, "migrate");
                    break;
                }
                else
                {
                    currentMigrationId = iteration.Id;
                }
                logger.EmptyLine();
            }
            logger.Log("migration finished with id: " + currentMigrationId);
            return currentMigrationId;
        }
        public string Migrate(IEnumerable<IMigrationFile> migrationFiles)
        {
            return "";    
        }
        public string Migrate(Assembly assembly, string nameSpace)
        {
            return "";
        }

        public ExecuteIterationResult ExecuteMigrateIteration(MigrateIteration iteration, string currentId)
        {
            if (iteration != null)
            {
                try
                {
                    this.DatabaseProvider.StartTransaction("automigratetransaction");
                    string description=(string)iteration.Method.Invoke(iteration.File,new object[]{this.DatabaseContext});
                    this.InsertMigrationFingerPrint(iteration.Id, description);
                    this.DatabaseProvider.CommitTransaction();
                    return new ExecuteIterationResult();
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        return new ExecuteIterationResult(ex.InnerException);
                    return new ExecuteIterationResult(ex);
                }
            }
            return new ExecuteIterationResult();
        }

        #region Migration Table
        public virtual string GetLastMigrationID()
        {
            string cmd = string.Format(@"select Id from {0} order by Id desc", this.MigrationTableName);
            var reader=this.DatabaseContext.ExecuteReader(cmd);
            string id = string.Empty;
            if (reader.Read())
            {
                id = reader.Get("Id").ToString();
            }
            reader.Close();
            reader.Dispose();
            return id;
        }

        public virtual void CreateMigrationTable()
        {
            CreateTableCommandModel table = new CreateTableCommandModel(this.MigrationTableName)
                .Column("Id", ColumnDataType.String,20,false)
                .Column("Executed", ColumnDataType.DateTime, false)
                .Column("Description", ColumnDataType.String, 500)
                .Timestamp();
            this.DatabaseContext.CreateTable(table);
        }

        public virtual bool IsMigrationTableCreated()
        {
            string cmd = string.Format(@"SELECT * FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = '{0}'", this.MigrationTableName);

            var reader = this.DatabaseContext.ExecuteReader(cmd);
            bool exists = false;
            if (reader.Read())
                exists = true;
            reader.Close();
            reader.Dispose();
            return exists;
        }

        public virtual void InsertMigrationFingerPrint(string id, string description)
        {
            InsertCommandModel cmd = new InsertCommandModel(this.MigrationTableName)
                .Parameter("Id", id)
                .Parameter("Description", description)
                .FunctionParameter("Executed", "GETDATE()");
            this.DatabaseContext.InsertRow(cmd);
        }
        #endregion
        public abstract void Dispose();

    }
}
