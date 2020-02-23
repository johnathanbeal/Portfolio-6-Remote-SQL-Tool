using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteSqlTool.Repository
{
    public class InsertRepo
    {
        public void InsertIntoRolodex(string connString, string _sqlQuery)
        {
            using NpgsqlConnection conn = new NpgsqlConnection(connString);

            conn.Open();
            
            try
            {
                using (var cmd = new NpgsqlCommand(_sqlQuery, conn))
                {
                    cmd.Connection = conn;
                    var part1 = _sqlQuery.ToLower().Between("insert into ", " values");

                    var table = part1.ToLower().Between("", " (");

                    var valuesString = _sqlQuery.ToLower().Between("values (", ")");//i think this works

                    var _columns = part1.ToLower().Between(table + " (", ")");//doesn't work
                    if (_columns == "")
                    {
                        Console.WriteLine("Please include columns in your insert statement");
                    }
                    
                    string[] columns = _columns.Replace(" ", "").Split(',');
                    string[] values = valuesString.Replace(" ", "").Split(',');

                    for (int i = 0; i < values.Length; i++)
                    {
                        if (values[i].ToLower() == "current_timestamp")
                        {
                            values[i] = DateTime.Now.ToString();
                        }

                        cmd.Parameters.AddWithValue(columns[i], values[i]);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error with your SQL Statement: " + e.Message);
            }
        }
    }
}
