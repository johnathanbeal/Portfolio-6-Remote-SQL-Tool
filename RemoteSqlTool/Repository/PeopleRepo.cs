﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Npgsql.Schema;
using RemoteSqlTool.Entities;
using RemoteSqlTool.Indexer;

namespace RemoteSqlTool.Repository
{
    public class PeopleRepo
    {
        public async Task<Tuple<ListDictionary, ListDictionary>>SelectFromPeopleTable(string connString, string _sqlQuery)
        {
            List<PeopleAddressEntity> personWithAddress = new List<PeopleAddressEntity>();
            

            ListDictionary listOfRecordsByColumn = new ListDictionary();
            ListDictionary listOfRecordsByIndex = new ListDictionary();
            
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);

            try
            {
                await conn.OpenAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await using (var cmd = new NpgsqlCommand(_sqlQuery))
            {
                cmd.Connection = conn;

                await using (var reader = await cmd.ExecuteReaderAsync())
                {                    
                    try
                    {
                        int index = 0;
                        while (await reader.ReadAsync())
                        {
                            ListDictionary listOfColumnsAndValuesPerRecord = new ListDictionary();
                            ListDictionary listOfIndexesAndValuesPerRecord = new ListDictionary();
                            var ColumnSchema = reader.GetColumnSchema();

                            try
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    
                                    listOfColumnsAndValuesPerRecord.Add(ColumnSchema[i].ColumnName.ToString().ToLower(), reader[i].ToString());
                                    listOfIndexesAndValuesPerRecord.Add(ColumnSchema[i].ColumnOrdinal, reader[i].ToString());
                                }
                                listOfRecordsByColumn.Add(index, listOfColumnsAndValuesPerRecord);
                                listOfRecordsByIndex.Add(index, listOfIndexesAndValuesPerRecord);
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                
                                
                            }
                            index++;
                        }
                    }
                    catch(Exception x)
                    {
                        Console.WriteLine("Something went wrong at or after the call to ExecuteReaderAsync():" + x.Message);
                    }
                    await cmd.DisposeAsync();
                }
                var results = new Tuple<ListDictionary, ListDictionary>(listOfRecordsByColumn, listOfRecordsByIndex);

                return results;
            }
        }

        public void InsertIntoAwsRdsInstance()
        {
            var connString = "Server=rolodex2.cr4dat7cc46x.us-east-2.rds.amazonaws.com;Username=postgres;Password=postgres;Database=rolodex;Port=5432";

            using NpgsqlConnection conn = new NpgsqlConnection(connString);

            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                //await 
                using (var cmd = new NpgsqlCommand("INSERT INTO people (firstname, lastname, email, created_on) VALUES (@firstname, @lastname, @email, @created_on)", conn))
                {
                    cmd.Connection = conn;
                    //cmd.Parameters.AddWithValue("id", 2);
                    cmd.Parameters.AddWithValue("firstname", "Bode");
                    cmd.Parameters.AddWithValue("lastname", "Locke");
                    cmd.Parameters.AddWithValue("email", "bodey.locke@fakeemail.com");
                    cmd.Parameters.AddWithValue("created_on", DateTime.Now);
                    //await 
                    //cmd.ExecuteNonQueryAsync();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //    // Insert some data


        }
    }
}
