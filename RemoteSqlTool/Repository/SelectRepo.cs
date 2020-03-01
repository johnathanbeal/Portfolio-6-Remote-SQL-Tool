using Npgsql;
using RemoteSqlTool.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSqlTool.Repository
{
    public class SelectRepo : IRepo
    {
        public async Task<List<ListDictionary>> Command(string connString, string _sqlQuery)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand(_sqlQuery))
            {
                cmd.Connection = conn;
                var rows = new List<ListDictionary>();
                try
                {
                    await using (var reader = await cmd.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            var row = new ListDictionary();
                            var columnSchema = reader.GetColumnSchema();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var debugKey = columnSchema[i].ColumnName.ToString().ToLower();

                                var valueKey = reader[i].ToString();

                                row.Add(columnSchema[i].ColumnName.ToString().ToLower(), reader[i].ToString());
                            }
                            rows.Add(row);
                        }
                        await cmd.DisposeAsync();
                    }
                }
                catch(Exception ex)
                {
                    ErrorMessages.MessageWhenSelectStatementHasAnException(ex);
                }
                return rows;
            }
        }
    }
}
