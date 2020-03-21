using Npgsql;
using RemoteSqlTool.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSqlTool.Repository
{
    public class InsertRepo : IRepo
    {
        public async Task<List<ListDictionary>> Command(string connString, string _sqlQuery)
        {
            var insertDictionary = new List<ListDictionary>();
            try
            {
                await using NpgsqlConnection conn = new NpgsqlConnection(connString);

                await conn.OpenAsync();


                await using (var cmd = new NpgsqlCommand(_sqlQuery, conn))
                {
                    cmd.Connection = conn;

                    var lld = ParseSQL.parseInsertSQLReturnColumnsAndValues(_sqlQuery);

                    foreach (var listDictionary in lld)
                    {
                        ListDictionary localListDictionary = new ListDictionary();

                        foreach (DictionaryEntry entry in listDictionary)
                        {
                            var value = TemporalUtility.ConvertCurrentTimestampStringToDateTimeNowString(entry.Value.ToString());

                            cmd.Parameters.AddWithValue(value, entry.Key.ToString());

                            localListDictionary.Add(entry.Key.ToString(), value);
                        }
                        insertDictionary.Add(localListDictionary);
                    }

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Your insert statement was processed");
                }
            }
            catch (Exception x)
            {
                Debug.WriteLine(x);
            }
            return insertDictionary;
        }
    }
}
