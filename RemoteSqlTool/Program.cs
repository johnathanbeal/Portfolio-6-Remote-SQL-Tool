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
            ListDictionary queryResult = new ListDictionary();
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
            //List<PeopleEntity> fakePeople = new List<PeopleEntity>()
            //{
            //new PeopleEntity()
            //{
            //    Id = 1,
            //    Firstname = "Johnathan",
            //    Lastname = "Locke",
            //    Email = "johnathan.locke@protonmail.com",
            //    CreatedDate = DateTime.Now
            //},
            //new PeopleEntity()
            //{
            //    Id = 2,
            //    Firstname = "Christopher",
            //    Lastname = "Locke",
            //    Email = "christopher.locke@protonmail.com",
            //    CreatedDate = DateTime.Now
            //}

            //};

            //people.InsertIntoAwsRdsInstance();///Commented out temporarily




            //foreach (var peopleRecord in queryResult)///Temporarily commented out
            //{
            //    //Console.WriteLine(peopleRecord.GetType());
            //    var id = peopleRecord.Id.ToString().PadRight(3).PadLeft(2) + "|";
            //    var fn = peopleRecord.Firstname.PadRight(10).PadLeft(2) + "|";
            //    var ln = peopleRecord.Lastname.PadRight(10).PadLeft(2) + "|";
            //    var em = peopleRecord.Email.PadRight(25).PadLeft(2) + "|";
            //    var cd = peopleRecord.AddressCreatedDate.ToString().PadRight(5).PadLeft(2);
            //    Console.WriteLine(id + fn + ln + em + cd);
            //}
            //StringBuilder sb = new StringBuilder();

            //PropertyInfo[] properties = fakePeople[0].GetType().GetProperties();

            //var PropStringBuilder = new StringBuilder();
            //foreach (PropertyInfo prop in properties)
            //{
            //    PropStringBuilder.Append(prop.Name.PadRight(Util.PadSpace(prop.Name)).PadLeft(2) + "|");
            //}
            //Console.WriteLine(PropStringBuilder);
            //foreach (var peopleRecord in fakePeople)
            //{
            //    PropertyInfo[] props = peopleRecord.GetType().GetProperties();
            //    var printRecord = new StringBuilder();
            //    foreach(PropertyInfo prop in props)
            //    {
            //        printRecord.Append(prop.GetValue(peopleRecord, null).ToString().PadRight(Util.PadSpace(prop.Name)).PadLeft(2) + "|");
            //    }
            //    Console.WriteLine(printRecord);

            //}
            
            Console.WriteLine("The number of records is :" + queryResult.Count);
            var QueryResultStringBuilder = new StringBuilder();
            try
            {
                foreach (Dictionary<string, string> rowOfColumns in queryResult)
                {
                    try
                    {
                        foreach (string column in rowOfColumns.Keys)
                        {
                            QueryResultStringBuilder.Append(column.ToString().PadRight(Util.PadSpace(column.ToString())).PadLeft(2) + "|");
                        }
                        foreach (Dictionary<string, string> record in queryResult.Values)
                        {
                            QueryResultStringBuilder.Append(record.ToString().PadRight(Util.PadSpace(record.ToString())).PadLeft(2) + "|");
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine(QueryResultStringBuilder);
        }
    }
}
