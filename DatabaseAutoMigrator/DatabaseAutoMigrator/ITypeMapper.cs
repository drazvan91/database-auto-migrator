using DatabaseAutoMigrator.Models;
using System;
using System.Linq;
using System.Text;
namespace DatabaseAutoMigrator
{
    public interface ITypeMapper
    {
        string MapDataType(DbType type);
        string MapDataType(DbType type, int length);
        string MapDataType(DbType type, int length, int scale);

        string MapFunctionType(FunctionType type);
    }
}
