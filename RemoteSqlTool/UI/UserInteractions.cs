using RemoteSqlTool.Connector;
using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteSqlTool.UI
{
    public static class UserInteractions
    {
        public static string Startup()
        {
            var databaseAuthorizationInputs = InitialUserPrompts();
            NpgConnector NConn = new NpgConnector(databaseAuthorizationInputs);
            var NConString = NConn.connString(NConn.authProps);
            return NConString;
        }
        public static AttestationCharacteristics InitialUserPrompts()
        {
            AttestationCharacteristics AC = new AttestationCharacteristics();
            List<string> inputs = new List<string>();
            var index = 0;
            foreach(var ac in AttestationCharacteristics.ListOfAttestationCharacteristics)
            {
                Console.WriteLine("Enter " + ac);
                inputs.Add(Console.ReadLine());
                Console.WriteLine();
            }
            if (inputs[0] != "")
            {
                AC.Host = inputs[0];
            }
            AC.Username = inputs[1];
            AC.Password = inputs[2];
            AC.Database = inputs[3];
            int port;
            bool portIsInt = Int32.TryParse(inputs[4], out port);
            if (portIsInt)
            {
                AC.Port = port;
            }
            else
            {
                Console.WriteLine("Invalid Port");
            }
                       
            if (AC.Host == "HATCH" || AC.Host == "hatch" || AC.Host == "Hatch")
            {
                Console.WriteLine("http://www.enterthehatch.com/");
            }
            return AC;
        }

        public static string ProcessSqlInput()
        {
            Console.WriteLine("Enter a SQL Query or press q to quit" + System.Environment.NewLine);
            return Console.ReadLine();
        }



    }
}
