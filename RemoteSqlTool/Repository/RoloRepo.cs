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
    public class RoloRepo
    {
        public async Task<List<ListDictionary>> SelectFromRolodex(string connString, string _sqlQuery)
        {                       
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();
                       
            await using (var cmd = new NpgsqlCommand(_sqlQuery))
            {
                cmd.Connection = conn;
                var rows = new List<ListDictionary>();

                await using (var reader = await cmd.ExecuteReaderAsync())
                {                                      

                        while (await reader.ReadAsync())
                        {
                            var row = new ListDictionary();
                            var columnSchema = reader.GetColumnSchema();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {                         
                                    row.Add(columnSchema[i].ColumnName.ToString().ToLower(), reader[i].ToString());
                                }
                                rows.Add(row);
                        }                   
                    await cmd.DisposeAsync();
                }
                return rows;
            }
        }

        public void InsertIntoRolodex(string _sqlQuery)
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
                using (var cmd = new NpgsqlCommand(_sqlQuery, conn))
                {
                    cmd.Connection = conn;

                    var table = _sqlQuery.ToLower().Between("insert into", " (");//doesn't work
                    var columnsString = _sqlQuery.ToLower().Between(table + " (", ") values");//doesn't work
                    var valuesString = _sqlQuery.ToLower().Between("values (", ")");//i think this works

                    var _sqlQueryWithoutInsertIntoText = _sqlQuery.Replace("insert into", "").Trim();
                    var _sqlQueryWithoutValuesText = _sqlQueryWithoutInsertIntoText.Replace("values", "").Trim();
                    //var _sqlQueryTable = 

                    //string[] columns;
                    //string[] values;

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
