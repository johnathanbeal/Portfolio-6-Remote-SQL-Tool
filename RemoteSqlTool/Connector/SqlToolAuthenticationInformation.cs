using System.Collections.Generic;

namespace RemoteSqlTool.Connector
{
    public class SqlToolAuthenticationInformation
    {
        public string? Host { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Database { get; set; }

        public int Port { get; set; }

        public static List<string> ListOfAttestationCharacteristics = new List<string>() { "Host", "Username", "Password", "Database", "Port" };

        public SqlToolAuthenticationInformation()
        {

        }

        public string GetPostgresConnectionString()
        {
            return "Server=" + (Host ?? "databank.c9mrseu2nxwi.us-east-1.rds.amazonaws.com")  + ";Username=" + Username + ";Password=" + Password + ";Database=" + Database + ";Port=" + Port;
        }

        public SqlToolAuthenticationInformation(SqlToolAuthenticationInformation attestationCharacteristics)
        {
            Host = attestationCharacteristics.Host;
            Username = attestationCharacteristics.Username;
            Password = attestationCharacteristics.Password;
            Database = attestationCharacteristics.Database;
            Port = attestationCharacteristics.Port;
        }

        public SqlToolAuthenticationInformation(string host, string username, string password, string database, int port)
        {
            Host = host;
            Username = username;
            Password = password;
            Database = database;
            Port = port;
        }
    }
}
