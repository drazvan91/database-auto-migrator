using DatabaseAutoMigrator.Models;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseAutoMigrator.Providers.Generic
{
    public class GenericColumnGenerator:IColumnGenerator
    {
        protected ITypeMapper TypeMapper { get; set; }
        protected IDialect Dialect { get; set; }

        public GenericColumnGenerator(ITypeMapper typeMapper,IDialect dialect)
        {
            this.TypeMapper = typeMapper;
            this.Dialect = dialect;
        }

        public virtual string Generate(ColumnDefinition column)
        {
            string type = "";
            if (column.Size.HasValue)
            {
                if (column.Precision.HasValue && column.Scale.HasValue)
                    type = TypeMapper.MapDataType(column.Type, column.Size.Value, column.Precision.Value);
                else
                    type = TypeMapper.MapDataType(column.Type, column.Size.Value);
            }
            else
            {
                type = TypeMapper.MapDataType(column.Type);
            }
            return combine(
                Dialect.QuoteColumnName(column.Name),
                type,
                column.AllowNull ? Dialect.Null : Dialect.NotNull,
                column.AutoIncrement ? Dialect.Identity : string.Empty,
                column.DefaultValue!=null?Dialect.Default(column.DefaultValue):null
                );
        }

        public virtual string Generate(IEnumerable<ColumnDefinition> columns)
        {
            return string.Join(",", columns.Select(c => Generate(c)));
        }

        public string Generate(string columnName, DbType type, bool allowNull)
        {
            return combine(
                Dialect.QuoteColumnName(columnName),
                TypeMapper.MapDataType(type),
                allowNull ? Dialect.Null : Dialect.NotNull
                );
        }
        public string Generate(string columnName, DbType type, object defaultValue, bool allowNull)
        {
            return combine(
                Dialect.QuoteColumnName(columnName),
                TypeMapper.MapDataType(type),
                allowNull ? Dialect.Null : Dialect.NotNull,
                Dialect.Default(defaultValue)
                );
        }

        public string Generate(string columnName, DbType type, int length, bool allowNull)
        {
            return combine(
                Dialect.QuoteColumnName(columnName),
                TypeMapper.MapDataType(type, length),
                allowNull ? Dialect.Null : Dialect.NotNull
                );
        }

        public string Generate(string columnName, DbType type, int length, object defaultValue, bool allowNull)
        {
            return combine(
                Dialect.QuoteColumnName(columnName),
                TypeMapper.MapDataType(type,length),
                allowNull ? Dialect.Null : Dialect.NotNull,
                Dialect.Default(defaultValue)
                );
        }

        private string combine(params string[] elements)
        {
            return string.Join(" ", elements.Where(e => !string.IsNullOrWhiteSpace(e)));
        }
    }
}
