using DatabaseAutoMigrator.Models;
using DatabaseAutoMigrator.Providers.Sql.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
