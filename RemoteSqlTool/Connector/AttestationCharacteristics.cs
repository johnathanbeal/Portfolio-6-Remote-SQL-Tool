using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteSqlTool.Connector
{
    public class AttestationCharacteristics
    {
        public string? Host { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Database { get; set; }

        public int Port { get; set; }

        public static List<string> ListOfAttestationCharacteristics = new List<string>() { "Host", "Username", "Password", "Database", "Port" };

    }
}
