namespace DatabaseAutoMigrator.Models.Expressions
{
    public class RawCommandExpression : BaseMigrationExpression
    {
        internal string CommandText { get; set; }

        public RawCommandExpression(string commandText)
        {
            this.CommandText = commandText;
        }
    }
}
