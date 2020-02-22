using System;
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
        public async Task<ListDictionary> SelectFromPeopleTable(string connString, string _sqlQuery)
        {
            List<PeopleAddressEntity> personWithAddress = new List<PeopleAddressEntity>();
            

            ListDictionary rowsOfRecords = new ListDictionary();
            
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
                            Dictionary<string, string> row = new Dictionary<string, string>();
                            var ColumnSchema = reader.GetColumnSchema();

                            try
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    
                                    row.Add(ColumnSchema[i].ColumnName.ToString().ToLower(), reader[i].ToString());
                                }
                                rowsOfRecords.Add(index, row);
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

                return rowsOfRecords;
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
