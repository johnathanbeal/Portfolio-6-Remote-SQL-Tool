﻿using Npgsql;
using RemoteSqlTool.Exceptions;
using RemoteSqlTool.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSqlTool.Repository
{
    public class UpdateRepo : IRepo
    {
        public async Task<List<ListDictionary>> Command(string connString, string _sqlQuery)
        {
            var updateDictionary = new List<ListDictionary>();

            try
            {
                await using NpgsqlConnection conn = new NpgsqlConnection(connString);

                await conn.OpenAsync();

                await using (var cmd = new NpgsqlCommand(_sqlQuery, conn))
                {
                    cmd.Connection = conn;

                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var colVal = ParseSQL.parseUpdateSQLReturnColumnsAndValues(_sqlQuery);

                        int i = 0;
                        
                        foreach (var key in colVal.Keys)
                        {
                            var ld = new ListDictionary(); 
                            var value = TemporalUtility.ConvertCurrentTimestampStringToDateTimeNowString(colVal[key].ToLower());                       
                            cmd.Parameters.Add(value, Util.GetTypeFromColumn(key));
                            ld.Add(key, value);
                            updateDictionary.Add(ld);
                            i++;
                        }
                    }
                }
                Console.WriteLine("The statement " + _sqlQuery + " ran without throwing an error");
            }
            catch (Exception ex)
            {
                ErrorMessages.MessageWhenUpdateValuesDoesNotHaveParentheses(ex);
            }
            return updateDictionary;
        }
    }
}

