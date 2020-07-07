using Npgsql;
using RemoteSqlTool.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace RemoteSqlTool.Repository
{
    public class SelectRepository : IRepository
    {
        public async Task<List<ListDictionary>> Command(string connString, string sqlQuery)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();

            await using (var cmd = new NpgsqlCommand(sqlQuery))
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
