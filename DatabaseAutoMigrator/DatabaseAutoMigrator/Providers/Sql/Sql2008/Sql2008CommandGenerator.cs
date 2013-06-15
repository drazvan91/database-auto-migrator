using DatabaseAutoMigrator.Models;
using DatabaseAutoMigrator.Providers.Sql.DataAccess;
using DatabaseAutoMigrator.Providers.Sql.Sql2005;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseAutoMigrator.Providers.Sql.Sql2008
{
    public class Sql2008CommandGenerator : Sql2005CommandGenerator
    {
        public Sql2008CommandGenerator():
            base(
            new Sql2008TypeMapper()
            )
        {
        }
    }
}
