namespace DatabaseAutoMigrator.Commands
{
    public class DropTableCommandModel:ICommandModel
    {
        internal string TableName { get; private set; }
        internal bool IfExists { get; private set; }

        public DropTableCommandModel(string tableName,bool ifExists=false)
        {
            this.TableName = tableName;
            this.IfExists = ifExists;
        }
    }
}
