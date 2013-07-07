using DatabaseAutoMigrator.DatabaseAccess;
using DatabaseAutoMigrator.Providers.Generic;
using System;

namespace DatabaseAutoMigrator.Providers.Sql.Sql2000
{
    public class Sql2000CommandGenerator:GenericCommandGenerator
    {
        public Sql2000CommandGenerator()
            :this(
            new Sql2000TypeMapper()
            )
        {
        }
        protected Sql2000CommandGenerator(ITypeMapper typeMapper)
            :this(new SqlDialect(),typeMapper)
        {

        }
        private Sql2000CommandGenerator(IDialect dialect, ITypeMapper mapper)
            :base(
                dialect,
                new GenericColumnGenerator(mapper,dialect),
                mapper)
        {

        }

        protected override string RenameTableFormat { get { return "sp_rename '{0}', '{1}'"; } }
        protected override string RenameColumnFormat { get { return "sp_rename '{0}.{1}', '{2}'"; } }
        protected override string DropIndexFormat { get { return "DROP INDEX {1}.{0}"; } }
        protected override string AddColumnFormat { get { return "ALTER TABLE {0} ADD {1}"; } }
        protected virtual string IdentityInsertFormat { get { return "SET IDENTITY_INSERT {0} {1}"; } }
        protected override string CreateConstraintFormat { get { return "ALTER TABLE {0} ADD CONSTRAINT {1} {2}{3} ({4})"; } }

        public override DatabaseCommand GenerateCreatePrimaryKey(Models.ConstraintDefinition model)
        {
            string clustered = model.IsClustered ? " CLUSTERED" : " NONCLUSTERED";
            string columns = string.Join(",", model.Columns);
            string cmd = string.Format(
                CreateConstraintFormat,
                Dialect.QuoteTableName(model.TableName),
                Dialect.QuoteConstraintName(model.Name),
                Dialect.PrimaryKey,
                clustered,
                columns);
            return new DatabaseCommand(cmd);
        }

        public override DatabaseCommand GenerateCreateUniqueConstraint(Models.ConstraintDefinition model)
        {
            string clustered = model.IsClustered ? " CLUSTERED" : " NONCLUSTERED";
            string columns = string.Join(",", model.Columns);
            string cmd = string.Format(
                CreateConstraintFormat,
                Dialect.QuoteTableName(model.TableName),
                Dialect.QuoteConstraintName(model.Name),
                Dialect.Unique,
                clustered,
                columns);
            return new DatabaseCommand(cmd);
        }

        public override DatabaseCommand GenerateDropDefaultValue(string tableName, string columnName)
        {
            string sql =
                "DECLARE @default sysname, @sql nvarchar(4000);" + Environment.NewLine + Environment.NewLine +
                "-- get name of default constraint" + Environment.NewLine +
                "SELECT @default = name" + Environment.NewLine +
                "FROM sys.default_constraints" + Environment.NewLine +
                "WHERE parent_object_id = object_id('{0}')" + Environment.NewLine +
                "AND type = 'D'" + Environment.NewLine +
                "AND parent_column_id = (" + Environment.NewLine +
                "SELECT column_id" + Environment.NewLine +
                "FROM sys.columns" + Environment.NewLine +
                "WHERE object_id = object_id('{0}')" + Environment.NewLine +
                "AND name = '{1}'" + Environment.NewLine +
                ");" + Environment.NewLine + Environment.NewLine +
                "-- create alter table command to drop constraint as string and run it" + Environment.NewLine +
                "SET @sql = N'ALTER TABLE {0} DROP CONSTRAINT ' + @default;" + Environment.NewLine +
                "EXEC sp_executesql @sql;";

            return new DatabaseCommand(String.Format(sql, Dialect.QuoteTableName(tableName), columnName));
        }
    }
}
