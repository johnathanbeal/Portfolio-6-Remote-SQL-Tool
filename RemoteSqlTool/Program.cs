using RemoteSqlTool.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace RemoteSqlTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            PeopleRepository pr = new PeopleRepository();

            var selectResult = await pr.SelectFromPeopleTable();
            //Task results = 
                //pr.InsertIntoAwsRdsInstance();
            Console.WriteLine("Hello World!");
        }
    }
}
