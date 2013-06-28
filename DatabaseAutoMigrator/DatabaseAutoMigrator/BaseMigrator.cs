using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Logging;
using DatabaseAutoMigrator.Models;
using DatabaseAutoMigrator.Models.Expressions;
using System.Diagnostics;

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

        public MigrationResult Migrate(IMigrationFile migrationFile)
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
        private MigrationResult migrateMethods(IEnumerable<MigrateIteration> methods)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            MigrationResult result = new MigrationResult();
            Logger.Log("getting last migration id", "migrate");
            string lastMigrationId = this.GetLastMigrationID();
            result.StartId = lastMigrationId;
            Logger.Log("last id=" + lastMigrationId, "migrate");

            var nextMigrations = (from m in methods
                                  where string.Compare(m.Id ,lastMigrationId)>0
                                  orderby m.Id
                                  select m);
            Logger.Log("number of valid migration methods: " + nextMigrations.Count(), "migrate");

            var currentMigrationId = lastMigrationId;
            bool failed = false;
            foreach (var iteration in nextMigrations)
            {
                if (!failed)
                {
                    Logger.EmptyLine();
                    Logger.Log("starting execute migration #" + iteration.Id, "migrate");
                    var iterationResult = this.ExecuteMigrateIteration(iteration, currentMigrationId);
                    if (iterationResult.Success == false)
                    {
                        Logger.EmptyLine();
                        Logger.Log("Error at migration #" + iteration.Id + ". " + iterationResult.Error.Message, "migrate");
                        failed = true;
                    }
                    else
                    {
                        currentMigrationId = iteration.Id;
                    }
                    Logger.EmptyLine();
                    result.Executed.Add(iterationResult);
                }
                else
                {
                    result.NotExecuted.Add(new ExecuteIterationResult()
                    {
                        Success=false,
                        Duration=new TimeSpan(0),
                        Error=null, 
                        File=iteration.File.GetType().FullName,
                        MethodName=iteration.Method.Name
                    });
                }
            }
            Logger.Log("migration finished with id: " + currentMigrationId);
            result.EndId=currentMigrationId;
            stopwatch.Stop();
            result.Duration = stopwatch.Elapsed;
            return result;
        }
        public MigrationResult Migrate(IEnumerable<IMigrationFile> migrationFiles)
        {
            Logger.Log("starting migration from single file", "migrate");

            var allMigrations = (from mFile in migrationFiles
                                 let methods = (from method in mFile.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                where method.Name.StartsWith("Migrate_")
                                                select method)
                                 select methods.Select(m =>
                                         new MigrateIteration()
                                         {
                                             File = mFile,
                                             Id = m.Name.Remove(0, "Migrate_".Length),
                                             Method = m
                                         })
                                 
                               ).SelectMany(i => i).ToList();
            return migrateMethods(allMigrations);
        }
        public MigrationResult Migrate(Assembly assembly, string nameSpace)
        {
            Type migrationFileType =typeof(IMigrationFile);
            var types = (from t in assembly.GetTypes()
                         where t.Namespace == nameSpace
                         select (IMigrationFile)Activator.CreateInstance(t));

            return Migrate(types);
        }

        public ExecuteIterationResult ExecuteMigrateIteration(MigrateIteration iteration, string currentId)
        {
            Stopwatch watch = Stopwatch.StartNew();
            ExecuteIterationResult result = new ExecuteIterationResult()
            {
                File=iteration.File.GetType().FullName,
                MethodName=iteration.Method.Name
            };
            try
            {
                this.DatabaseProvider.StartTransaction("automigratetransaction");
                string description = (string)iteration.Method.Invoke(iteration.File, new object[] { this.DatabaseContext });
                this.InsertMigrationFingerPrint(iteration.Id, description);
                this.DatabaseProvider.CommitTransaction();
                result.Success = true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    result.Error = ex.InnerException;
                }
                else
                {
                    result.Error = ex;
                }
                result.Success = false;
            }
            watch.Stop();
            result.Duration = watch.Elapsed;
            return result;
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
                    .Column("Id", DbType.String, 50, false)
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
