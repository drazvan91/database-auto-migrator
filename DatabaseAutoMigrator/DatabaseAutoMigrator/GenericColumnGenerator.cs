using DatabaseAutoMigrator.Models;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseAutoMigrator
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
            string ret = string.Format("{0} {1} {2} {3}",
                Dialect.QuoteColumnName(column.Name),
                type,
                column.AllowNull ? Dialect.Null : Dialect.NotNull,
                column.AutoIncrement ? Dialect.Identity : string.Empty
                );
            return ret;
        }

        public virtual string Generate(IEnumerable<ColumnDefinition> columns)
        {
            return string.Join(",", columns.Select(c => Generate(c)));
        }


        public string Generate(string columnName, DbType type, bool allowNull)
        {
            string t = TypeMapper.MapDataType(type);
            string ret = string.Format("{0} {1} {2}",
                Dialect.QuoteColumnName(columnName),
                t,
                allowNull ? Dialect.Null : Dialect.NotNull
                );
            return ret;
        }

        public string Generate(string columnName, DbType type, int length, bool allowNull)
        {
            string t = TypeMapper.MapDataType(type, length);
            string ret = string.Format("{0} {1} {2}",
                Dialect.QuoteColumnName(columnName),
                t,
                allowNull ? Dialect.Null : Dialect.NotNull
                );
            return ret;
        }
    }
}
