using DatabaseAutoMigrator.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DatabaseAutoMigrator
{
    public class MigrateIteration
    {
        internal string Description { get; set; }
        internal ICollection<ICommandModel> Commands { get; private set; }

        internal MigrateIteration()
        {
            this.Commands = new Collection<ICommandModel>();
        }
        internal MigrateIteration(string description)
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

        public DropTableCommandModel DropTable(string tableName,bool ifExists=false)
        {
            var table = new DropTableCommandModel(tableName,ifExists);
            this.Commands.Add(table);
            return table;
        }

        public RawCommandModel RawCommand(string commandText)
        {
            var raw = new RawCommandModel(commandText);
            this.Commands.Add(raw);
            return raw;
        }

        public InsertCommandModel InsertRow(string tableName)
        {
            var insert = new InsertCommandModel(tableName);
            this.Commands.Add(insert);
            return insert;
        }

        public AlterTableCommandModel AlterTable(string tableName)
        {
            var alter = new AlterTableCommandModel(tableName);
            this.Commands.Add(alter);
            return alter;
        }

    }
}
