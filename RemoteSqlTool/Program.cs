using RemoteSqlTool.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace RemoteSqlTool
{
    class Program
    {
        static void Main(string[] args)
        {
            PeopleRepository pr = new PeopleRepository();
            var v = pr.InsertIntoAwsRdsInstance();
            Console.WriteLine("Hello World!");
        }
    }
}
