using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSqlTool.Repository
{
    public class DeleteRepo
    {
        public void deleteRecord(string connectionString, string _sqlQuery)
        {
            try
            {
                using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
                conn.Open();

                using (var cmd = new NpgsqlCommand(_sqlQuery))
                {
                    cmd.Connection = conn;

                    cmd.ExecuteNonQuery();

                    conn.Dispose();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
