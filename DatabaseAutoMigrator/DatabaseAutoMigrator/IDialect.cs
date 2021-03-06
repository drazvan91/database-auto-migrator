﻿using DatabaseAutoMigrator.Models;
namespace DatabaseAutoMigrator
{
    public interface IDialect
    {
        /*bool ColumnNameNeedsQuote { get; }
        bool TableNameNeedsQuote { get; }
        bool ConstraintNameNeedsQuote { get; }
        bool IdentityNeedsType { get; }*/

        string QuoteColumnName(string name);
        string QuoteTableName(string name);
        string QuoteConstraintName(string name);
        string QuoteValue(object value);

        string Null { get; }
        string NotNull { get; }
        string Unique { get; }
        string PrimaryKey { get; }
        string Identity { get; }
        string IfExists { get; }

        string Default(object defaultvalue);
    }
}
