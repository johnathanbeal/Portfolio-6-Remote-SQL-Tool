using System;

namespace RemoteSqlTool.Exceptions
{
    public static class ErrorMessages
    {
        public static void MessageWhenUpdateValuesDoesNotHaveParentheses(Exception exception)
        {
            Console.WriteLine(exception.Message);
            if (exception.Message.Contains("does not exist") && exception.Message.Contains("column"))
            {
                Console.WriteLine("Try wrapping your update values in quotes");
            }
        }

        public static void MessageWhenDeleteStatementHasForeignKeyContstraing(Exception exception)
        {
            Console.WriteLine(exception.Message);
            if (exception.Message.Contains("update or delete on table") && exception.Message.Contains(" violates foreign key constraint"))
            {
                Console.WriteLine("Delete the related record in the address table before deleting from the people table");
            }
        }

        public static void MessageWhenSelectStatementHasAnException(Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
