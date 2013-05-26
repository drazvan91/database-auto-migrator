namespace DatabaseAutoMigrator
{
    public abstract class BaseMigrationFile:IMigrationFile
    {
        public CommandHelper Helper { get; set; }
        
        public BaseMigrationFile()
        {
            this.Helper = new CommandHelper();
        }
    }
}
