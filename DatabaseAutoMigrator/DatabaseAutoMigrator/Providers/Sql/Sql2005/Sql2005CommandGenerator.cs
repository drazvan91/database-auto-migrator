using DatabaseAutoMigrator.Models;
using DatabaseAutoMigrator.Providers.Sql.DataAccess;
using DatabaseAutoMigrator.Providers.Sql.Sql2000;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseAutoMigrator.Providers.Sql.Sql2005
{
    public class Sql2005CommandGenerator:Sql2000CommandGenerator
    {
        public Sql2005CommandGenerator():
            this(
                new Sql2005TypeMapper()
            )
        {
        }
        protected Sql2005CommandGenerator(ITypeMapper mapper)
            :base(mapper)
        {

        }
    }
}
