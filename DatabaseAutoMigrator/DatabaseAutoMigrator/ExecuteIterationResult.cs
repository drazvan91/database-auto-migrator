using System;

namespace DatabaseAutoMigrator
{
    public class ExecuteIterationResult
    {
        public bool Success { get; set; }
        public Exception Error { get; set; }

        public ExecuteIterationResult()
        {
            this.Success = true;
            this.Error = null;
        }
        public ExecuteIterationResult(Exception ex)
        {
            this.Success = false;
            this.Error = ex;
        }
    }
}
