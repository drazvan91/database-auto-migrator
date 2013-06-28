using System;
using System.Collections.Generic;

namespace DatabaseAutoMigrator
{
    public class MigrationResult
    {
        public string StartId { get; set; }
        public string EndId { get; set; }
        public TimeSpan Duration { get; set; }
        public List<ExecuteIterationResult> Executed { get; protected set; }
        public List<ExecuteIterationResult> NotExecuted { get; protected set; }

        public MigrationResult()
        {
            this.Executed = new List<ExecuteIterationResult>();
            this.NotExecuted = new List<ExecuteIterationResult>();
        }
    }
}
