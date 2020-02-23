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
            Interaction StartUpSequence = new Interaction();///Commented out temporarily

            AttestationCharacteristics databaseAuthorizationInputs = StartUpSequence.InitialUserPrompts();///Commented out temporarily

            NpgConnector NConn = new NpgConnector(databaseAuthorizationInputs);///Commented out temporarily
            var NConString = NConn.connString(NConn.authProps);///Commented out temporarily
            List<ListDictionary> queryResult = new List<ListDictionary>();
            PeopleRepo people = new PeopleRepo();

            var processAQuery = true;
            while (processAQuery)
            {
                Console.WriteLine("Enter a SQL Query: ");
                var sqlQuery = Console.ReadLine();
                Npgsql.Schema.NpgsqlDbColumn columns;
                if (sqlQuery.ToLower().Contains("select"))
                {
                    try
                    {
                        queryResult = await people.SelectFromPeopleTable(NConString, sqlQuery);
                        processAQuery = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("There may be an error with your SQL Query");
                        Console.WriteLine("The error reads: " + ex.Message);
                    }
                }
            }
            
            //people.InsertIntoAwsRdsInstance();///Commented out temporarily

            Console.WriteLine("The number of records is :" + queryResult.Count);
            var headerRowStringBuilder = new StringBuilder();
            var rowsOfRecordsStringBuilder = new StringBuilder();
            //var debugStringBuilder = new StringBuilder();

            var firstDictionary = queryResult[0];
          
            //GroupBy(de => de.Keys).Select(columnName => columnName.First()).ToList();
            foreach (var columnName in firstDictionary.Keys)
            {
                
                    headerRowStringBuilder.Append(columnName.ToString().ToUpper().PadRight(Util.PadSpace(columnName.ToString())).PadLeft(2) + "|");               
            }
            Console.WriteLine(headerRowStringBuilder);

            foreach (var row in queryResult)
                {
                var debugStringBuilder = new StringBuilder();
                    foreach (DictionaryEntry column in row)
                    {
                    int index;
                    int length = column.Value.ToString().Length;
                    if (Util.TruncateValue(column.Key.ToString()) > column.Value.ToString().Length)
                    {
                        index = length;
                    }
                    else
                    {
                        index = Util.TruncateValue(column.Key.ToString());
                    }
                    var val = column.Value.ToString();
                    var ki = column.Key.ToString();
                        val = val.Substring(0, index);
                        debugStringBuilder.Append(val.PadRight(Util.PadSpace(ki)).PadLeft(2) + "|");
                    }
                Console.WriteLine(debugStringBuilder);
                }
            //Console.WriteLine(rowsOfRecordsStringBuilder);
        }
    }
}
