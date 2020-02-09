using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace RemoteSqlTool.Repository
{
    public class PeopleRepository
    {
        public async Task InsertIntoAwsRdsInstance()
        {
            NpgsqlConnectionStringBuilder NpgConnectionString = new NpgsqlConnectionStringBuilder();
            NpgConnectionString.Host = "rolodex-2.cr4dat7cc46x.us-east-2.rds.amazonaws.com";
            NpgConnectionString.Username = "postgres";
            NpgConnectionString.Password = "postgres";
            NpgConnectionString.Port = 5432;
            NpgConnectionString.Database = "rolodex";

            var connString = "Server=rolodex-2.cr4dat7cc46x.us-east-2.rds.amazonaws.com;Username=Postgres;Password=Postgres;Database=rolodex-2;Port=5432";
            await using NpgsqlConnection conn = new NpgsqlConnection(NpgConnectionString.ConnectionString);

            try
            {
                await conn.OpenAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //    // Insert some data
            await using (var cmd = new NpgsqlCommand("INSERT INTO people (firstname) VALUES (@p)", conn))
            {
                cmd.Parameters.AddWithValue("p", "Johnathan");
                await cmd.ExecuteNonQueryAsync();
            }

            //// Retrieve all rows
            //await using (var cmd = new NpgsqlCommand("SELECT some_field FROM data", conn))
            //await using (var reader = await cmd.ExecuteReaderAsync())
            //    while (await reader.ReadAsync())
            //        Console.WriteLine(reader.GetString(0));
        }
    }
}
