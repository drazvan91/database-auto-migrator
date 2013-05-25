namespace DatabaseAutoMigrator.Commands
{
    public class DropTableCommandModel:ICommandModel
    {
        internal string TableName { get; set; }

        public DropTableCommandModel(string tableName)
        {
            this.TableName = tableName;
        }
    }
}
