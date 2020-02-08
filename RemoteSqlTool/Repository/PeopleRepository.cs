using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace RemoteSqlTool.Repository
{
    class PeopleRepository
    {
        public async Task InsertIntoAwsRdsInstance()
        {
            var connString = "Host=rolodex-2.cr4dat7cc46x.us-east-2.rds.amazonaws.com;Username=*;Password=*;Database=rolodex-2";
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            //    // Insert some data
            await using (var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES (@p)", conn))
            {
                NpgsqlParameter p = new NpgsqlParameter();
                p.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text;
                cmd.Parameters.AddWithValue("p", "Hello world");
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
