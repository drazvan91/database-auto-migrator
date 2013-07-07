using DatabaseAutoMigrator.Models;
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
