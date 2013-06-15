using DatabaseAutoMigrator.Models;
using System.Collections.Generic;

namespace DatabaseAutoMigrator
{
    public interface IColumnGenerator
    {
        string Generate(ColumnDefinition column);
        string Generate(IEnumerable<ColumnDefinition> columns);

        string Generate(string columnName, DbType type, bool allowNull);
        string Generate(string columnName, DbType type, int length, bool allowNull);
    }
}
