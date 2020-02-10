using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using RemoteSqlTool.Entities;

namespace RemoteSqlTool.Repository
{
    public class PeopleRepository
    {
        public async Task<PeopleEntity> SelectFromPeopleTable()
        {
            NpgsqlConnectionStringBuilder NpgCString = new NpgsqlConnectionStringBuilder();
            NpgCString.Host = "rolodex2.cr4dat7cc46x.us-east-2.rds.amazonaws.com";
            NpgCString.Username = "postgres";
            NpgCString.Password = "postgres";
            NpgCString.Port = 5432;
            NpgCString.Database = "rolodex";

            await using NpgsqlConnection conn = new NpgsqlConnection(NpgCString.ConnectionString);

            try
            {
                await conn.OpenAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await using (var cmd = new NpgsqlCommand("SELECT * FROM people"))
            {
                cmd.Connection = conn;
                PeopleEntity person = new PeopleEntity();
                //int id;
                //string firstname;
                //string lastname;
                //string email;
                //DateTime cd;
                var format = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(GMT Daylight Time)'";

                //// Retrieve all rows
                //await using (var cmd = new NpgsqlCommand("SELECT some_field FROM data", conn))
                //await using (var reader = await cmd.ExecuteReaderAsync())
                //    while (await reader.ReadAsync())
                //        Console.WriteLine(reader.GetString(0));
                //}

                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    person.Id = Int32.Parse(reader[0].ToString());
                    person.Firstname = reader[1].ToString();
                    person.Lastname = reader[2].ToString();
                    person.Email = reader[3].ToString();
                    person.CreatedDate = DateTime.Parse(reader[4].ToString());
                }
                return person;
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
                using (var cmd = new NpgsqlCommand("INSERT INTO people (id, firstname, lastname, email, created_date) VALUES (@id, @firstname, @lastname, @email, @created_date)", conn))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("id", 2);
                    cmd.Parameters.AddWithValue("firstname", "Nina");
                    cmd.Parameters.AddWithValue("lastname", "Locke");
                    cmd.Parameters.AddWithValue("email", "nina.locke@fakeemail.com");
                    cmd.Parameters.AddWithValue("created_date", DateTime.Now);
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
