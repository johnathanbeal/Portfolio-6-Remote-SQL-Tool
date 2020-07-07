using RemoteSqlTool.Connector;
using System;
using System.Collections.Generic;

namespace RemoteSqlTool.UI
{
    public static class UserInteractions
    {
        public static string AskUserForAuthenticationInformation()
        {
            InitialUserPrompts();
            var sqlToolAuthenticationInfo = new SqlToolAuthenticationInformation();
            return sqlToolAuthenticationInfo.GetPostgresConnectionString();
        }
        public static SqlToolAuthenticationInformation InitialUserPrompts()
        {
            var userCredentials = new SqlToolAuthenticationInformation();
            var inputs = new List<string>();

            foreach(var ac in SqlToolAuthenticationInformation.ListOfAttestationCharacteristics)
            {
                Console.WriteLine("Enter " + ac);
                inputs.Add(Console.ReadLine());
                Console.WriteLine();
            }
            if (inputs[0] != "")
            {
                userCredentials.Host = inputs[0];
            }
            userCredentials.Username = inputs[1];
            userCredentials.Password = inputs[2];
            userCredentials.Database = inputs[3];
            int port;
            bool portIsInt = Int32.TryParse(inputs[4], out port);
            if (portIsInt)
            {
                userCredentials.Port = port;
            }
            else
            {
                Console.WriteLine("Invalid Port");
            }
                       
            if (userCredentials.Host == "HATCH" || userCredentials.Host == "hatch" || userCredentials.Host == "Hatch")
            {
                Console.WriteLine("http://www.enterthehatch.com/");
            }
            return userCredentials;
        }

        public static string PromptUserForSqlCommand()
        {
            Console.WriteLine("Enter a SQL Query or press q to quit" + System.Environment.NewLine);
            return Console.ReadLine();
        }
    }
}
