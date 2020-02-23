using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSqlTool.Repository
{
    public class SelectRepo
    {
        public async Task<List<ListDictionary>> SelectFromRolodex(string connString, string _sqlQuery)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand(_sqlQuery))
            {
                cmd.Connection = conn;
                var rows = new List<ListDictionary>();

                await using (var reader = await cmd.ExecuteReaderAsync())
                {

                    while (await reader.ReadAsync())
                    {
                        var row = new ListDictionary();
                        var columnSchema = reader.GetColumnSchema();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(columnSchema[i].ColumnName.ToString().ToLower(), reader[i].ToString());
                        }
                        rows.Add(row);
                    }
                    await cmd.DisposeAsync();
                }
                return rows;
            }
        }
    }
}
