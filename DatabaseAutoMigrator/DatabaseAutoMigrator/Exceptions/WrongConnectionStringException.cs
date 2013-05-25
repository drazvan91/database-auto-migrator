using System;

namespace DatabaseAutoMigrator.Exceptions
{
    public class WrongConnectionStringException:Exception
    {
        public WrongConnectionStringException() :
            base("Wrong connection string")
        {

        }
    }
}
