namespace DatabaseAutoMigrator.Commands
{
    public class RawCommandModel:ICommandModel
    {
        internal string CommandText { get; set; }

        public RawCommandModel(string commandText)
        {
            this.CommandText = commandText;
        }
    }
}
