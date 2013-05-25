using System;
using System.Linq;
using System.Text;
namespace DatabaseAutoMigrator.Logging
{
    public abstract class BaseLogger:ILogger
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }

        public BaseLogger() { }
        public BaseLogger(string name)
        {
            this.Name = name;
        }

        public abstract void Log(string message, string method = null);
        public abstract void Warn(string message, string method = null);
        public abstract void Error(string message, string method = null);
    }
}
