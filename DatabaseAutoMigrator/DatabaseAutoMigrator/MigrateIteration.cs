using System.Reflection;

namespace DatabaseAutoMigrator
{
    public class MigrateIteration
    {
        public MethodInfo Method { get; set; }
        public string Id { get; set; }
        public IMigrationFile File { get; set; }
    }
}
