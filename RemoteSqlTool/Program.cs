using RemoteSqlTool.Connector;
using RemoteSqlTool.Repository;
using RemoteSqlTool.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Text;

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
            //foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(people))
            //{
            //     Console.WriteLine(descriptor.Name);
            //}

            
            PropertyInfo[] propertyInfos = typeof(PeopleRepo).GetProperties();
            foreach (var propInfo in propertyInfos)
            {
                Console.WriteLine(propInfo.Name);
            }

            Type type = typeof(PeopleRepo);
            FieldInfo[] fields = type.GetFields();
            string headerColumn;
            for (int i = 0; i < fields.Length; i++)
            {
                var id = fields[0].ToString().PadRight(3).PadLeft(2) + "|";
                var fn = fields[1].ToString().PadRight(10).PadLeft(2) + "|";
                var ln = fields[2].ToString().PadRight(10).PadLeft(2) + "|";
                var em = fields[3].ToString().PadRight(25).PadLeft(2) + "|";
                var cd = fields[4].ToString().PadRight(5).PadLeft(2);
                headerColumn = id + fn + ln + em + cd;
                Console.WriteLine(headerColumn);

            }


            foreach (var peopleRecord in selectResult)
            {
                Console.WriteLine(peopleRecord.GetType());
                
            }

            foreach (var peopleRecord in selectResult)
            {
                //Console.WriteLine(peopleRecord.GetType());
                var id = peopleRecord.Id.ToString().PadRight(3).PadLeft(2) + "|";
                var fn = peopleRecord.Firstname.PadRight(10).PadLeft(2) + "|";
                var ln = peopleRecord.Lastname.PadRight(10).PadLeft(2) + "|";
                var em = peopleRecord.Email.PadRight(25).PadLeft(2) + "|";
                var cd = peopleRecord.CreatedDate.ToString().PadRight(5).PadLeft(2);
                Console.WriteLine(id + fn + ln + em + cd);
            }
        }
    }
}
