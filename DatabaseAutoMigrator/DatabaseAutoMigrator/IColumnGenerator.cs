using DatabaseAutoMigrator.Models;
using System.Collections.Generic;

namespace DatabaseAutoMigrator
{
    public interface IColumnGenerator
    {
        string Generate(ColumnDefinition column);
        string Generate(IEnumerable<ColumnDefinition> columns);
    }
}
