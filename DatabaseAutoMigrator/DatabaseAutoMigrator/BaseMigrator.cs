using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Logging;
using DatabaseAutoMigrator.Models;
using DatabaseAutoMigrator.Models.Expressions;

namespace DatabaseAutoMigrator
{
    public abstract class BaseMigrator : IMigrator
    {
        public string MigrationTableName = "DatabaseAutoMigrator";

        protected IDatabaseContext DatabaseContext { get; private set; }
        protected IDatabaseProvider DatabaseProvider { get { return this.DatabaseContext.DatabaseProvider; } }
        private ILogger Logger { get; set; }

        public BaseMigrator(IDatabaseContext databaseContext)
        {
            Logger = LoggerFactory.Current.Get("BaseMigrator");
            this.DatabaseContext = databaseContext;
            CreateMigrationTable();
        }

        public string Migrate(IMigrationFile migrationFile)
        {
            Logger.Log("starting migration from single file", "migrate");
            
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
            Logger.Log("getting last migration id", "migrate");
            string lastMigrationId = this.GetLastMigrationID();
            Logger.Log("last id=" + lastMigrationId, "migrate");

            var nextMigrations = (from m in methods
                                  where string.Compare(m.Id ,lastMigrationId)>0
                                  orderby m.Id
                                  select m);
            Logger.Log("number of valid migration methods: " + nextMigrations.Count(), "migrate");

            var currentMigrationId = lastMigrationId;
            foreach (var iteration in nextMigrations)
            {
                Logger.EmptyLine();
                Logger.Log("starting execute migration #" + iteration.Id, "migrate");
                var result = this.ExecuteMigrateIteration(iteration, currentMigrationId);
                if (result.Success == false)
                {
                    Logger.EmptyLine();
                    Logger.Log("Error at migration #" + iteration.Id + ". " + result.Error.Message, "migrate");
                    break;
                }
                else
                {
                    currentMigrationId = iteration.Id;
                }
                Logger.EmptyLine();
            }
            Logger.Log("migration finished with id: " + currentMigrationId);
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
            if (!this.IsMigrationTableCreated())
            {
                var table = new CreateTableExpression(this.MigrationTableName)
                    .Column("Id", DbType.String, 20, false)
                    .Column("Executed", DbType.DateTime, false)
                    .Column("Description", DbType.String, 500)
                    .Timestamp();
                this.DatabaseContext.CreateTable(table);
            }
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
            var cmd = new InsertExpression(this.MigrationTableName)
                .Parameter("Id", id)
                .Parameter("Description", description)
                .FunctionParameter("Executed", FunctionType.CurrentDateTime);
            this.DatabaseContext.InsertRow(cmd);
        }
        #endregion
        public abstract void Dispose();

    }
}
