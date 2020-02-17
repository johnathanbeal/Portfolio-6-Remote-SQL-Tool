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
using RemoteSqlTool.Entities;

namespace RemoteSqlTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Interaction inter = new Interaction();///Commented out temporarily

            //AttestationCharacteristics authInputs = inter.InitialUserPrompts();///Commented out temporarily

            ////NpgConnector NConn = new NpgConnector(authInputs);///Commented out temporarily
            //var NConString = NConn.connString(NConn.authProps);///Commented out temporarily

            PeopleRepo people = new PeopleRepo();
            List<PeopleEntity> fakePeople = new List<PeopleEntity>()
            {
            new PeopleEntity()
            {
                Id = 1,
                Firstname = "Johnathan",
                Lastname = "Locke",
                Email = "johnathan.locke@protonmail.com",
                CreatedDate = DateTime.Now
            },
            new PeopleEntity()
            {
                Id = 2,
                Firstname = "Christopher",
                Lastname = "Locke",
                Email = "christopher.locke@protonmail.com",
                CreatedDate = DateTime.Now
            }

            };

            ///var selectResult = await people.SelectFromPeopleTable(NConString);///Commented out temporarily
            //people.InsertIntoAwsRdsInstance();///Commented out temporarily

            #region CodeThatDidntWork
            //foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(people))
            //{
            //    Console.WriteLine(descriptor.Name);
            //}
            //var debug1 = typeof(PeopleRepo);
            //var debug2 = people.GetType();
            //var debug3 = debug1.GetTypeInfo();
            //var debugFields = debug3.GetFields();

            //foreach (var debug in debugFields)
            //{
            //    Console.WriteLine(debug);
            //    Console.WriteLine(debug.Name);
            //    Console.WriteLine(debug.FieldType);
            //    Console.WriteLine(debug.ToString());
            //}

            //PropertyInfo[] propertyInfos = typeof(PeopleRepo).GetProperties();
            //foreach (var propInfo in propertyInfos)
            //{
            //    Console.WriteLine(propInfo.Name);
            //}

            //Type type = typeof(PeopleRepo);
            //FieldInfo[] fields = type.GetFields();
            //string headerColumn;
            //for (int i = 0; i < fields.Length; i++)
            //{
            //    var id = fields[0].ToString().PadRight(3).PadLeft(2) + "|";
            //    var fn = fields[1].ToString().PadRight(10).PadLeft(2) + "|";
            //    var ln = fields[2].ToString().PadRight(10).PadLeft(2) + "|";
            //    var em = fields[3].ToString().PadRight(25).PadLeft(2) + "|";
            //    var cd = fields[4].ToString().PadRight(5).PadLeft(2);
            //    headerColumn = id + fn + ln + em + cd;
            //    Console.WriteLine(headerColumn);

            //}

            //Type type2 = people.GetType();
            //PropertyInfo[] properties = type.GetProperties();

            //foreach (PropertyInfo property in properties)
            //{
            //    Console.WriteLine("Name: " + property.Name + ", Value: " + property.GetValue(type2, null));
            //}

            //foreach (var peopleRecord in selectResult)
            //{
            //    Console.WriteLine(peopleRecord.Id.GetType());

            //}
            #endregion

            //foreach (var peopleRecord in selectResult)///Temporarily commented out
            //{
            //    //Console.WriteLine(peopleRecord.GetType());
            //    var id = peopleRecord.Id.ToString().PadRight(3).PadLeft(2) + "|";
            //    var fn = peopleRecord.Firstname.PadRight(10).PadLeft(2) + "|";
            //    var ln = peopleRecord.Lastname.PadRight(10).PadLeft(2) + "|";
            //    var em = peopleRecord.Email.PadRight(25).PadLeft(2) + "|";
            //    var cd = peopleRecord.CreatedDate.ToString().PadRight(5).PadLeft(2);
            //    Console.WriteLine(id + fn + ln + em + cd);
            //}
            StringBuilder sb = new StringBuilder();

            PropertyInfo[] properties = fakePeople[0].GetType().GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                sb.Append(
                    string.Format("Name: {0} | Value: {1}",
                            pi.Name,
                            pi.GetValue(fakePeople[0], null)
                        )
                );
            }
            var PropStringBuilder = new StringBuilder();
            foreach (PropertyInfo prop in properties)
            {
                PropStringBuilder.Append(prop.Name.PadRight(Util.PadSpace(prop.Name)).PadLeft(2) + "|");
            }
            Console.WriteLine(PropStringBuilder);
            foreach (var peopleRecord in fakePeople)
            {
                PropertyInfo[] props = peopleRecord.GetType().GetProperties();
                var printRecord = new StringBuilder();
                foreach(PropertyInfo prop in props)
                {
                    printRecord.Append(prop.GetValue(peopleRecord, null).ToString().PadRight(Util.PadSpace(prop.Name)).PadLeft(2) + "|");
                }
                Console.WriteLine(printRecord);
                //Console.WriteLine(nameof(peopleRecord.Id));
                //Console.WriteLine(peopleRecord.GetType());
                //var id = peopleRecord.Id.ToString().PadRight(20).PadLeft(5) + "|";
                //var fn = peopleRecord.Firstname.PadRight(20).PadLeft(5) + "|";
                //var ln = peopleRecord.Lastname.PadRight(20).PadLeft(5) + "|";
                //var em = peopleRecord.Email.PadRight(20).PadLeft(5) + "|";
                //var cd = peopleRecord.CreatedDate.ToString().PadRight(20).PadLeft(5);
                //Console.WriteLine(id + fn + ln + em + cd);
            }
        }
    }
}
