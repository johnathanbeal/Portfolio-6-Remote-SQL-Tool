using RemoteSqlTool.Repository;
using RemoteSqlTool.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;


namespace RemoteSqlTool.UI
{
    public static class QueryWorkflow
    {
        public static async Task<List<ListDictionary>> EnterQueryAndRun(string NConString, List<ListDictionary> queryResult, bool _keepRunning)       
        {

            while (_keepRunning)
            {
                var sqlQuery = UserInteractions.ProcessSqlInput();
                var afterWhere = sqlQuery.ToLower().After("where");
                if (afterWhere.ToLower().Contains("select"))
                {
                    Console.WriteLine("This application is unable to process statements that have a select in their where clause");
                }
                IRepo repo = Util.GetRepoType(sqlQuery);
                
                try
                {
                    if (sqlQuery.ToLower().Contains("select"))
                    {
                        queryResult = await repo.Command(NConString, sqlQuery);
                        DisplayResults.WriteSelectResultsToConsole(queryResult);
                    }
                    else if (sqlQuery.ToLower().Contains("insert"))
                    {
                        queryResult = await repo.Command(NConString, sqlQuery);
                    }
                    else if (sqlQuery.ToLower().Contains("delete"))
                    {
                        queryResult = await repo.Command(NConString, sqlQuery);
                    }
                    else if (sqlQuery.ToLower().Contains("update"))
                    {
                        queryResult = await repo.Command(NConString, sqlQuery);
                    }
                    else if (sqlQuery.ToLower().Contains("q"))
                    {
                        Console.WriteLine("Are you sure you want to quit?");
                        var quitter = Console.ReadLine();
                        if (quitter.ToLower().Contains("y") || quitter.ToLower().Contains("yes"))
                        _keepRunning = false;
                    }
                    else
                    {                     
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There may be an error with your SQL Query." + System.Environment.NewLine + "The error reads: " + ex.Message);
                    if (ex.Message.Contains("No such host is known") || ex.Message.Contains("Couldn't set port (Parameter 'port')"))
                    {
                        _keepRunning = false;
                        var ConnString = UserInteractions.Startup();
                        await EnterQueryAndRun(ConnString, queryResult, true);
                    }
                }
            }
            return queryResult;
        }
    }
}
