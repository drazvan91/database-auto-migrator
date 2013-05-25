using System;

namespace DatabaseAutoMigrator.Logging.Impl
{
    public class ConsoleLogger : BaseLogger
    {
        public ConsoleLogger(string name)
            :base(name)
        {

        }

        public override void Log(string message, string method = null)
        {
            handleLog("Log", message, method);
        }

        public override void Warn(string message, string method = null)
        {
            handleLog("WARN", message, method);
        }

        public override void Error(string message, string method = null)
        {
            handleLog("ERROR", message, method);
        }

        private void handleLog(string type, string message, string method)
        {
            Console.WriteLine(string.Format("[{0}] {1}: ({2} {3}) - {4}", System.DateTime.Now.ToLongTimeString(), type, this.Name, method, message));
        }
    }
}
