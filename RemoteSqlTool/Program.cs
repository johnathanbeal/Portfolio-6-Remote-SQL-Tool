using RemoteSqlTool.Connector;
using RemoteSqlTool.Repository;
using RemoteSqlTool.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;


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

            var selectResult = await people.SelectFromPeopleTable(NConString);
            //people.InsertIntoAwsRdsInstance();
           foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(people))
           {
                Console.WriteLine(descriptor.Name);
           }

            PropertyInfo[] propertyInfos = typeof(PeopleRepo).GetProperties();
           foreach(var propInfo in propertyInfos)
           {
                Console.WriteLine(propInfo);
           }
            
           foreach(var peopleRecord in selectResult)
           {
                //Console.WriteLine(peopleRecord.GetType());
                var id = peopleRecord.Id.ToString().PadRight(5) + "|";
                var fn = peopleRecord.Firstname.PadRight(15) + "|";
                var ln = peopleRecord.Lastname.PadRight(15) + "|";
                var em = peopleRecord.Email.PadRight(30) + "|";
                var cd = peopleRecord.CreatedDate.ToString().PadRight(15);
                Console.WriteLine(id + fn + ln + em + cd);
           }
        }
    }
}
