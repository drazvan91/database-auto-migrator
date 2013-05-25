﻿using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator
{
    public interface IDialect
    {
        string MapDataType(ColumnDataType type);
        string MapDataType(ColumnDataType type, int length);
        string MapDataType(ColumnDataType type, int length, int precision, int scale);

        /*bool ColumnNameNeedsQuote { get; }
        bool TableNameNeedsQuote { get; }
        bool ConstraintNameNeedsQuote { get; }
        bool IdentityNeedsType { get; }*/

        string QuoteColumnName(string name);
        string QuoteTableName(string name);
        string QuoteContraintName(string name);

        string Null { get; }
        string NotNull { get; }
        string Unique { get; }
        string PrimaryKey { get; }
        string Identity { get; }

        string Default(object defaultvalue);
    }
}
