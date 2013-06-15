using DatabaseAutoMigrator.Models;
using System;
using System.Collections.Generic;

namespace DatabaseAutoMigrator
{
    public abstract class BaseTypeMapper:ITypeMapper
    {
        private readonly Dictionary<DbType, SortedList<int, string>> _dbTypesTemplates = new Dictionary<DbType, SortedList<int, string>>();
        private readonly Dictionary<FunctionType, string> _fnTypesTemplates = new Dictionary<FunctionType, string>();
        private const string SizePlaceholder = "$size";
        protected const string PrecisionPlaceholder = "$precision";

        public BaseTypeMapper()
        {
            this.SetupTypeMaps();
        }

        protected abstract void SetupTypeMaps();
        protected void SetTypeMap(DbType type, string template)
        {
            EnsureHasList(type);
            _dbTypesTemplates[type][0] = template;
        }
        protected void SetTypeMap(DbType type, string template, int maxSize)
        {
            EnsureHasList(type);
            _dbTypesTemplates[type][maxSize] = template;
        }
        protected void SetTypeMap(FunctionType type, string template)
        {
            _fnTypesTemplates[type] = template;
        }

        public virtual string MapDataType(DbType type)
        {
            return MapDataType(type, 0, 0);
        }
        public virtual string MapDataType(DbType type, int size)
        {
            return MapDataType(type, size, 0);
        }
        public virtual string MapDataType(DbType type, int size, int precision)
        {
            if (!_dbTypesTemplates.ContainsKey(type))
                throw new NotSupportedException(String.Format("Unsupported DbType '{0}'", type));

            if (size == 0)
                return ReplacePlaceholders(_dbTypesTemplates[type][0], size, precision);

            foreach (KeyValuePair<int, string> entry in _dbTypesTemplates[type])
            {
                int capacity = entry.Key;
                string template = entry.Value;

                if (size <= capacity)
                    return ReplacePlaceholders(template, size, precision);
            }

            throw new NotSupportedException(String.Format("Unsupported DbType '{0}'", type));
        }
        public virtual string MapFunctionType(FunctionType type)
        {
            if (!_fnTypesTemplates.ContainsKey(type))
                throw new NotSupportedException(String.Format("Unsupported FunctionType '{0}'", type));

            return _fnTypesTemplates[type] + "()";
        }

        private void EnsureHasList(DbType type)
        {
            if (!_dbTypesTemplates.ContainsKey(type))
                _dbTypesTemplates.Add(type, new SortedList<int, string>());
        }

        private string ReplacePlaceholders(string value, int size, int precision)
        {
            return value.Replace(SizePlaceholder, size.ToString())
                .Replace(PrecisionPlaceholder, precision.ToString());
        }

    }
}
