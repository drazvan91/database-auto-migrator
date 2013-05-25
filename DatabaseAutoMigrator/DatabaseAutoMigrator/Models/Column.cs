
namespace DatabaseAutoMigrator.Models
{
    public class Column
    {
        public string Name { get; set; }
        public ColumnDataType Type { get; set; }
        public bool AllowNull { get; set; }
        public bool AutoIncrement { get; set; }
        public int? Length { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }

    }
}
