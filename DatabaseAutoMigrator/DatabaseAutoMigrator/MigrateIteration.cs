using DatabaseAutoMigrator.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DatabaseAutoMigrator
{
    public class MigrateIteration
    {
        public string Description { get; protected set; }
        public ICollection<ICommandModel> Commands { get; protected set; }

        public MigrateIteration(string description)
        {
            this.Description = description;
            this.Commands = new Collection<ICommandModel>();
        }

        public CreateTableCommandModel CreateTable(string tableName)
        {
            var table = new CreateTableCommandModel(tableName);
            this.Commands.Add(table);
            return table;
        }

        public DropTableCommandModel DropTable(string tableName)
        {
            var table = new DropTableCommandModel(tableName);
            this.Commands.Add(table);
            return table;
        }

        public RawCommandModel RawCommand(string commandText)
        {
            var raw = new RawCommandModel(commandText);
            this.Commands.Add(raw);
            return raw;
        }
    }
}
