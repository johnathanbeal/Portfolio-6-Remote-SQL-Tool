using RemoteSqlTool.Connector;
using RemoteSqlTool.Repository;
using RemoteSqlTool.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Text;
using RemoteSqlTool.Entities;
using System.Collections.Specialized;
using System.Collections;
using System.Linq;

namespace RemoteSqlTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Interaction StartUpSequence = new Interaction();

            AttestationCharacteristics databaseAuthorizationInputs = StartUpSequence.InitialUserPrompts();

            NpgConnector NConn = new NpgConnector(databaseAuthorizationInputs);
            var NConString = NConn.connString(NConn.authProps);

            List<ListDictionary> queryResult = new List<ListDictionary>();
            SelectRepo select = new SelectRepo();
            InsertRepo insert = new InsertRepo();
            DeleteRepo delete = new DeleteRepo();
            UpdateRepo update = new UpdateRepo();

            var processAQuery = true;
            while (processAQuery)
            {
                Console.WriteLine("Enter a SQL Query or press q to quit" + System.Environment.NewLine);
                var sqlQuery = Console.ReadLine();
                Console.WriteLine();
                Npgsql.Schema.NpgsqlDbColumn columns;
                try
                {
                    if (sqlQuery.ToLower().Contains("select"))
                    {                        
                            queryResult = await select.SelectFromRolodex(NConString, sqlQuery);
                            DisplayResults display = new DisplayResults();
                            display.WriteSelectResultsToConsole(queryResult);
                    }
                    else if (sqlQuery.ToLower().Contains("insert"))
                    {
                            if (sqlQuery.ToLower().Contains("join"))
                            {
                                Console.WriteLine("This program will have a difficult time processing JOIN statements");
                            }
                            insert.InsertIntoRolodex(NConString, sqlQuery);
                        Console.WriteLine("Your insert statement was processes");
                    }
                    else if (sqlQuery.ToLower().Contains("delete"))
                    {
                        delete.deleteRecord(NConString, sqlQuery);
                    }
                    else if (sqlQuery.ToLower().Contains("update"))
                    {
                        await update.UpdateRecord(NConString, sqlQuery);
                    }
                    else
                    {                       
                        if (sqlQuery.ToLower().Contains("q"))
                        {
                            processAQuery = false;
                            Console.WriteLine("Closing Time...");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There may be an error with your SQL Query");
                    Console.WriteLine("The error reads: " + ex.Message);
                    if (ex.Message.Contains("does not exist") && ex.Message.Contains("The error reads") && ex.Message.Contains("column"))
                    {
                        Console.WriteLine("Try wrapping your update values in quotes");
                    }
                    processAQuery = true;
                }                
            }           
        }
    }
}
