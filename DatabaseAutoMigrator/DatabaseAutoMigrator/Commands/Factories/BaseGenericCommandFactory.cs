using DatabaseAutoMigrator.DatabaseAccess;
using System;

namespace DatabaseAutoMigrator.Commands.Factories
{
    public abstract class BaseGenericCommandFactory<TCommand>:IGenericCommandFactory<TCommand>
        where TCommand : IDatabaseCommand
    {
        protected IDialect Dialect { get; set; }
        protected ICommandFactory<TCommand, CreateTableCommandModel> CreateTableFactory { get; set; }
        protected ICommandFactory<TCommand, DropTableCommandModel> DropTableFactory { get; set; }
        protected ICommandFactory<TCommand, RawCommandModel> RawCommandFactory { get; set; }
        protected ICommandFactory<TCommand, InsertCommandModel> InsertCommandFactory { get; set; }

        public TCommand GenerateGenericCommand(ICommandModel model)
        {
            if (model is CreateTableCommandModel)
                return this.GenerateCreateTableCommand(model as CreateTableCommandModel);
            else if (model is DropTableCommandModel)
                return this.GenerateDropTableCommand(model as DropTableCommandModel);
            else if (model is RawCommandModel)
                return this.GenerateRawCommand(model as RawCommandModel);
            else if (model is InsertCommandModel)
                return this.GenerateInsertCommand(model as InsertCommandModel);
            throw new Exception("Unknown command");
        }

        public TCommand GenerateCreateTableCommand(CreateTableCommandModel model)
        {
            return CreateTableFactory.GenerateCommand(model,Dialect);
        }
        public TCommand GenerateDropTableCommand(DropTableCommandModel model)
        {
            return DropTableFactory.GenerateCommand(model, Dialect);
        }
        public TCommand GenerateRawCommand(RawCommandModel model)
        {
            return RawCommandFactory.GenerateCommand(model, Dialect);
        }
        public TCommand GenerateInsertCommand(InsertCommandModel model)
        {
            return InsertCommandFactory.GenerateCommand(model, Dialect);
        }
    }
}
