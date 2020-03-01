using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSqlTool.Repository
{
    public class UpdateRepo
    {
        public async Task UpdateRecord(string connString, string _sqlQuery)
        {
            try
            {
                await using NpgsqlConnection conn = new NpgsqlConnection(connString);

                await conn.OpenAsync();


                //NpgsqlCommand cmd = new NpgsqlCommand("update info set \"Fname\" = :FirstName, \"Lname\" = :LastName, \"Address\" = :Address," +
                //        "\"City\" = :City, \"State\" = State, \"Zip\" = :Zip," +
                //        "\"PhoneNumber\" = :PhoneNumber where \"LicenceNumber\" = '" + LicenseID + "' ;", conn);

                //var _sqlQuery2 = "update people set \"firstname\" = :Joh

                await using (var cmd = new NpgsqlCommand(_sqlQuery, conn))
                {
                    cmd.Connection = conn;

                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        ;


                        //"Update people SET firstname = a, lastname = b, email = c, created_on = d Where id = 1";

                        var afterSetString = _sqlQuery.ToLower().After("set");
                        string setString;
                        if (afterSetString.Contains("where"))
                        {
                            setString = afterSetString.Before("where");
                        }
                        else
                        {
                            setString = afterSetString;
                        }
                        //setString = setString.Replace(" =", "=").Replace("= ", "");

                        //string[] equalSplit = setString.Split('=');
                        string[] commaSplit = setString.Split(',');
                        Dictionary<string, string> columnsAndValues = new Dictionary<string, string>();
                        foreach(var columnAndValue in commaSplit)
                        {
                            columnsAndValues.Add((columnAndValue.Before("=").Trim()), columnAndValue.After("=").Trim());
                        }
                       

                        var table = _sqlQuery.ToLower().Between("update", "set").Trim();//ADD TRIM

                        //var valuesString = _sqlQuery.ToLower().Between("values (", ")");//i think this works

                        //var _columns = part1.ToLower().Between(table + " (", ")");//doesn't work
                        //if (_columns == "")
                        //{
                        //    Console.WriteLine("Please include columns in your insert statement");
                        //}

                        //string[] columns = _columns.Replace(" ", "").Split(',');
                        //string[] values = valuesString.Replace(" ", "").Split(',');

                        //write code to split and replace update statement and pass values to values array
                        //string[] values;

                        //while (await reader.ReadAsync())
                        //{
                        //    var columnSchema = reader.GetColumnSchema();

                            //for (int i = 0; i < reader.FieldCount; i++)
                            //{
                            //    cmd.Parameters.Add(columnsAndValues.Values[, Util.GetTypeFromColumn(columnSchema[i].ColumnName.ToString().ToLower()));
                            //}

                            foreach(var key in columnsAndValues.Keys)
                            {
                                cmd.Parameters.Add(columnsAndValues[key], Util.GetTypeFromColumn(key));
                            }

                        //}
                        cmd.Prepare();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Message.Contains("does not exist") && ex.Message.Contains("column"))
                {
                    Console.WriteLine("Try wrapping your update values in quotes");
                }
            }
        
            }
        }
    }

