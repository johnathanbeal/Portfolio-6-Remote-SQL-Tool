using NUnit.Framework;
using Npgsql;
using RemoteSqlTool.Connector;
using System.Threading.Tasks;
using RemoteSqlTool.Repository;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;

namespace RemoteSqlTool.Tests
{
    public class NpgConnectorTest
    {
        public string? Host { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Database { get; set; }
        public int Port { get; set; }

        public AttestationCharacteristics LoginInfo;
        public NpgConnector npgConn;

        public string connString;

        [SetUp]
        public async Task Setup()
        {
            Host = "blackbook.c9mrseu2nxwi.us-east-1.rds.amazonaws.com";
            Username = "postgres";
            Password = "postgres";
            Database = "rolodex";
            Port = 5432;

            LoginInfo = new AttestationCharacteristics(Host, Username, Password, Database, Port);
            npgConn = new NpgConnector();
            connString = npgConn.connString(LoginInfo);

            IRepo ir = new InsertRepo();

            var insertResult = await ir.Command(connString, "Insert into people (firstname, lastname, email, created_date) values ('Robert', 'Paulson', 'hisnameisrobertpaulson@gmail.com', current_timestamp)");

        }

        [Test]
        public async System.Threading.Tasks.Task ConnectToAwsRdsInstanceAsync()
        {          
            NpgConnector npgConn = new NpgConnector();
            System.Data.ConnectionState connectionState = await npgConn.connString(Host, Username, Password, Database, Port);
            Assert.AreEqual(connectionState, System.Data.ConnectionState.Open);
        }

        [Test]
        public async Task DeleteCommandReturnsListOfListDictionary()
        {         
            IRepo dr = new DeleteRepo();

            var result = await dr.Command(connString, "Delete from people where email = 'hisnameisrobertpaulson@gmail.com'");

            Assert.IsInstanceOf<List<ListDictionary>>(result);

            ListDictionary firstRecord = result[0];

            string debug = "";
            string debug2 = "";

            foreach (DictionaryEntry commandResult in result[0])
            {
                debug = commandResult.Key.ToString();
                debug2 = commandResult.Value.ToString();
                break;
            }
           
            Assert.AreEqual(debug2, "Data successfully deleted"); //
        }
    }
}