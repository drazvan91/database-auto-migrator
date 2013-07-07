
namespace DatabaseAutoMigrator.Models
{
    public class ColumnDefinition
    {
        public string Name { get; set; }
        public DbType Type { get; set; }
        public bool AllowNull { get; set; }
        public bool AutoIncrement { get; set; }
        public int? Size { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }

        public virtual object DefaultValue { get; set; }
    }
}
