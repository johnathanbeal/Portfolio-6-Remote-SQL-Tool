using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using RemoteSqlTool.Entities;

namespace RemoteSqlTool.Repository
{
    public class PeopleRepo
    {
        public async Task<List<PeopleEntity>> SelectFromPeopleTable(string connString)
        {
            List<PeopleEntity> person = new List<PeopleEntity>();

            await using NpgsqlConnection conn = new NpgsqlConnection(connString);

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

                int headCount = 0;
                
                var format = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(GMT Daylight Time)'";

                //List<PeopleEntity> person = new List<PeopleEntity>();

                await using (var reader = await cmd.ExecuteReaderAsync())
                {

                    try
                    {
                        while (await reader.ReadAsync())
                        {
                            var debug = reader[0];
                            person.Add(new PeopleEntity()
                            {


                                Firstname = reader[0].ToString(),
                                Lastname = reader[1].ToString(),
                                Email = reader[2].ToString(),
                                CreatedDate = DateTime.Parse(reader[3].ToString()),
                                Id = Int32.Parse(reader[4].ToString()),
                            });
                        }
                    }
                    catch(Exception x)
                    {
                        Console.WriteLine("Something went wrong at or after the call to ExecuteReaderAsync():" + x.Message);
                    }
                    await cmd.DisposeAsync();
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
                using (var cmd = new NpgsqlCommand("INSERT INTO people (firstname, lastname, email, created_on) VALUES (@firstname, @lastname, @email, @created_on)", conn))
                {
                    cmd.Connection = conn;
                    //cmd.Parameters.AddWithValue("id", 2);
                    cmd.Parameters.AddWithValue("firstname", "Nina");
                    cmd.Parameters.AddWithValue("lastname", "Locke");
                    cmd.Parameters.AddWithValue("email", "nina.locke@fakeemail.com");
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
