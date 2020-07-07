using RemoteSqlTool.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;


namespace RemoteSqlTool.UI
{
    public class QueryWorkflow
    {
        private bool _keepRunning = true;
        public async Task<List<ListDictionary>> ExecuteQuery(string connectionString, string sqlCommand)       
        {
            List<ListDictionary> queryResult = new List<ListDictionary>();
            while (_keepRunning)
            {
                var afterWhere = sqlCommand.ToLower().After("where");
                if (afterWhere.ToLower().Contains("select"))
                {
                    Console.WriteLine("This application is unable to process statements that have a select in their where clause");
                }
                
                try
                {
                    if (sqlCommand.ToLower().Contains("select"))
                    {
                        IRepository repo = new SelectRepository();
                        return queryResult = await repo.Command(connectionString, sqlCommand);                                                
                    }
                    else if (sqlCommand.ToLower().Contains("insert"))
                    {
                        IRepository repo = new InsertRepository();
                        return queryResult = await repo.Command(connectionString, sqlCommand);
                    }
                    else if (sqlCommand.ToLower().Contains("delete"))
                    {
                        IRepository repo = new DeleteRepository();
                        return queryResult = await repo.Command(connectionString, sqlCommand);
                    }
                    else if (sqlCommand.ToLower().Contains("update"))
                    {
                        IRepository repo = new UpdateRepository();
                        return queryResult = await repo.Command(connectionString, sqlCommand);
                    }
                    else if (sqlCommand.ToLower().Contains("q"))
                    {
                        Console.WriteLine("Are you sure you want to quit?");
                        var quitter = Console.ReadLine();
                        if (quitter.ToLower().Contains("y") || quitter.ToLower().Contains("yes"))
                        _keepRunning = false;
                        return new List<ListDictionary>();
                    }
                    else
                    {
                        return new List<ListDictionary>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There may be an error with your SQL Query." + System.Environment.NewLine + "The error reads: " + ex.Message);
                    if (ex.Message.Contains("No such host is known") || ex.Message.Contains("Couldn't set port (Parameter 'port')"))
                    {
                        _keepRunning = false;
                        var ConnString = UserInteractions.AskUserForAuthenticationInformation();
                        await ExecuteQuery(ConnString, sqlCommand);
                    }
                }
            }
            return queryResult;
        }
    }
}
