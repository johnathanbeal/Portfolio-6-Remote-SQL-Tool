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
            RoloRepo people = new RoloRepo();

            var processAQuery = true;
            while (processAQuery)
            {
                Console.WriteLine("Enter a SQL Query" + System.Environment.NewLine);
                var sqlQuery = Console.ReadLine();
                Console.WriteLine();
                Npgsql.Schema.NpgsqlDbColumn columns;
                try
                {
                    if (sqlQuery.ToLower().Contains("select"))
                    {                        
                            queryResult = await people.SelectFromRolodex(NConString, sqlQuery);
                            DisplayResults display = new DisplayResults();
                            display.WriteSelectResultsToConsole(queryResult);
                            processAQuery = false;                      
                    }
                    else if (sqlQuery.ToLower().Contains("insert"))
                    {                       
                            people.InsertIntoRolodex(sqlQuery);                       
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There may be an error with your SQL Query");
                    Console.WriteLine("The error reads: " + ex.Message);
                    processAQuery = true;
                }                
            }           
        }
    }
}
