using RemoteSqlTool.Connector;
using RemoteSqlTool.Repository;
using RemoteSqlTool.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace RemoteSqlTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Interaction inter = new Interaction();
            AttestationCharacteristics authInputs = inter.InitialUserPrompts();

            NpgConnector NConn = new NpgConnector(authInputs);
            var NConString = NConn.connString(NConn.authProps);

            PeopleRepo people = new PeopleRepo();
            AddressRepo address = new AddressRepo();
            var notAsyncConnString = await NConString;
            var selectResult = await people.SelectFromPeopleTable(notAsyncConnString);
            //Task results = 
                //pr.InsertIntoAwsRdsInstance();
            Console.WriteLine("Hello World!");
        }
    }
}
