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
                        var commandResults = new ListDictionary();
                        Console.WriteLine("Data successfully deleted");
                        commandResults.Add("Delete Command Result", "Data successfully deleted");
                        deleteDictionary.Add(commandResults);
                    }
                    else
                    {
                        var commandResults = new ListDictionary();
                        Console.WriteLine("Data was not deleted");
                        commandResults.Add("Delete Command Result", "Data was not deleted");
                        deleteDictionary.Add(commandResults);
                    }
                    
                    conn.Dispose();
                }
            }
            catch(Exception ex)
            {
                var commandResults = new ListDictionary();
                ErrorMessages.MessageWhenDeleteStatementHasForeignKeyContstraing(ex);
                commandResults.Add("Error Log", "Data was not deleted");
                deleteDictionary.Add(commandResults);
            }
            return deleteDictionary;
        }
    }
}
