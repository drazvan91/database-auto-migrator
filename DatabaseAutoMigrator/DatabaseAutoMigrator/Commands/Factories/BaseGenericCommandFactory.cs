using DatabaseAutoMigrator.DatabaseAccess;
using System;

namespace DatabaseAutoMigrator.Commands.Factories
{
    public abstract class BaseGenericCommandFactory<TCommand>:IGenericCommandFactory<TCommand>
        where TCommand : IDatabaseCommand
    {
        protected IDialect Dialect { get; set; }
        public TCommand GenericCommand(ICommandModel model)
        {
            if (model is CreateTableCommandModel)
                return this.CreateTable(model as CreateTableCommandModel);
            else if (model is DropTableCommandModel)
                return this.DropTable(model as DropTableCommandModel);
            else if (model is RawCommandModel)
                return this.RawCommand(model as RawCommandModel);
            else if (model is InsertCommandModel)
                return this.Insert(model as InsertCommandModel);
            throw new Exception("Unknown command");
        }

        public abstract TCommand CreateTable(CreateTableCommandModel model);
        public abstract TCommand DropTable(DropTableCommandModel model);
        public abstract TCommand RawCommand(RawCommandModel model);
        public abstract TCommand Insert(InsertCommandModel model);
    }
}
