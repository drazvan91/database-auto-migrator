using DatabaseAutoMigrator.Logging.Impl;

namespace DatabaseAutoMigrator.Logging
{
    public class LoggerFactory
    {
        private static LoggerFactory _current;
        static LoggerFactory()
        {
            _current = new LoggerFactory();
        }
        public static LoggerFactory Current
        {
            get
            {
                return _current;
            }
        }

        public ILogger Get(string name)
        {
            return new ConsoleLogger(name);
        }
    }
}
