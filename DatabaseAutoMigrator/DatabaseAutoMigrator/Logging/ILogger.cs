using System;
using System.Linq;
using System.Text;
namespace DatabaseAutoMigrator.Logging
{
    public interface ILogger
    {
        string Name { get; set; }

        void Log(string message, string method=null);
        void Warn(string message, string method=null);
        void Error(string message, string method=null);
        void EmptyLine();
    }
}
