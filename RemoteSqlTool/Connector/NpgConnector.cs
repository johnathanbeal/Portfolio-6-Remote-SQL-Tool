using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteSqlTool.Connector
{
    public class NpgConnector
    {
        public AttestationCharacteristics authProps = new AttestationCharacteristics();

        public NpgConnector(AttestationCharacteristics atChar)
        {
            authProps = atChar;
        }

        public NpgConnector()
        {
            authProps = null;
        }

        public async Task<NpgsqlConnection> connString(string? _host, string? _username, string? _password, string? _database, int _port)
        {
            var hookHost = _host ?? "blackbook.c9mrseu2nxwi.us-east-1.rds.amazonaws.com";  // "rolodex.c9mrseu2nxwi.us-east-1.rds.amazonaws.com";  

            var ConnString = "Server=" + hookHost + ";Username=" + _username + ";Password=" + _password + ";Database=" + _database + ";Port=" + _port;
            
            var ConString = "Server = blackbook.c9mrseu2nxwi.us-east-1.rds.amazonaws.com; Port = 5432; Database = rolodex; User Id = postgres; Password = postgres";


            var NpgConnString = new NpgsqlConnectionStringBuilder(ConString);
                
            await using NpgsqlConnection conn = new NpgsqlConnection(NpgConnString.ConnectionString);

            try
            {
                await conn.OpenAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return conn;
        }

        public string connString(AttestationCharacteristics ac)
        {
            var hookHost = ac.Host ?? "blackbook.c9mrseu2nxwi.us-east-1.rds.amazonaws.com";

            var ConnString = "Server=" + hookHost + ";Username=" + ac.Username + ";Password=" + ac.Password + ";Database=" + ac.Database + ";Port=" + ac.Port;

            //var NpgConnString = new NpgsqlConnectionStringBuilder(ConnString);

            return ConnString;
        }
    }
}
