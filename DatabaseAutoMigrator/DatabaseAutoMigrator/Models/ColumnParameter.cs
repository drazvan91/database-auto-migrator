using System;
using System.Linq;
using System.Text;
namespace DatabaseAutoMigrator.Models
{
    public class ColumnParameter
    {
        internal string Name { get; set; }
        internal object Value { get; set; }
        internal bool IsFunction{get;set;}
        internal string Function { get; set; } //TODO: this should be an enum
    }
}
