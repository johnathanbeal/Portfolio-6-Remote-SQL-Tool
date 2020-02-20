using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Npgsql.Schema;
using RemoteSqlTool.Entities;
using RemoteSqlTool.Indexer;

namespace RemoteSqlTool.Repository
{
    public class PeopleRepo
    {
        public async Task<List<PeopleAddressEntity>> SelectFromPeopleTable(string connString, string _sqlQuery)
        {
            List<PeopleAddressEntity> personWithAddress = new List<PeopleAddressEntity>();

            await using NpgsqlConnection conn = new NpgsqlConnection(connString);

            try
            {
                await conn.OpenAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await using (var cmd = new NpgsqlCommand(_sqlQuery))
            {
                cmd.Connection = conn;

                int headCount = 0;
                
                var format = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(GMT Daylight Time)'";

                await using (var reader = await cmd.ExecuteReaderAsync())
                {                    
                    try
                    {
                        while (await reader.ReadAsync())
                        {
                            PeopleAddressIndexer PeopleAddressIndexer = Util.assignReaderValueToPropertyByNpgsqlDbColumn(reader);

                            
                                Console.WriteLine(reader.FieldCount);
                                //Console.WriteLine(reader.VisibleFieldCount);
                                //Console.WriteLine(columnValue.ToString());
                                var sqlStatement = reader.Statements;
                                
                            try
                            {
                                //var debug = reader[0];
                                personWithAddress.Add(new PeopleAddressEntity()
                                {
                                    Id = Int32.Parse(reader[PeopleAddressIndexer.Id].ToString()),
                                    Firstname = reader[PeopleAddressIndexer.Firstname].ToString(),
                                    Lastname = reader[PeopleAddressIndexer.Lastname].ToString(),
                                    Email = reader[PeopleAddressIndexer.Email].ToString(),
                                    PeopleCreatedOn = DateTime.Parse(reader[PeopleAddressIndexer.CreatedOn].ToString()),
                                    AddressId = Int32.Parse(reader[PeopleAddressIndexer.AddressId].ToString()),
                                    PersonId = Int32.Parse(reader[PeopleAddressIndexer.PersonId].ToString()),
                                    City = reader[PeopleAddressIndexer.City].ToString(),
                                    State = reader[PeopleAddressIndexer.State].ToString(),
                                    Zip = reader[PeopleAddressIndexer.Zip].ToString(),
                                    AddressCreatedDate = reader[PeopleAddressIndexer.CreatedDate].ToString()
                                });
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                
                                
                            }
                            
                        }
                    }
                    catch(Exception x)
                    {
                        Console.WriteLine("Something went wrong at or after the call to ExecuteReaderAsync():" + x.Message);
                    }
                    await cmd.DisposeAsync();
                }
                return personWithAddress;
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
