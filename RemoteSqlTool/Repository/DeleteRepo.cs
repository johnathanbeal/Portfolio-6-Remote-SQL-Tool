using Npgsql;
using RemoteSqlTool.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSqlTool.Repository
{
    public class DeleteRepo : IRepo
    {
        public async Task<List<ListDictionary>> Command(string connectionString, string _sqlQuery)
        {
            var deleteDictionary = new List<ListDictionary>();
            try
            {
                await using NpgsqlConnection conn = new NpgsqlConnection(connectionString);
                conn.Open();

                await using (var cmd = new NpgsqlCommand(_sqlQuery))
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
                ErrorMessages.MessageWhenDeleteStatementHasForeignKeyContstraing(ex);
            }
            return deleteDictionary;
        }
    }
}
