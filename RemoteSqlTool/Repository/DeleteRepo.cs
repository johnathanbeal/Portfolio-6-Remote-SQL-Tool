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

                    int recordAffected = cmd.ExecuteNonQuery();

                    if (Convert.ToBoolean(recordAffected))
                    {
                        Console.WriteLine("Data successfully deleted");
                    }
                    

                    conn.Dispose();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Message.Contains("update or delete on table") && ex.Message.Contains(" violates foreign key constraint"))
                    {
                        Console.WriteLine("Delete the related record in the address table before deleting from the people table");
                    }
            }
            
        }
    }
}
