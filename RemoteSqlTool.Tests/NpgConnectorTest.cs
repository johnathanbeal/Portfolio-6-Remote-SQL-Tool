using NUnit.Framework;
using RemoteSqlTool.Connector;
using System.Threading.Tasks;
using RemoteSqlTool.Repository;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections;
using RemoteSqlTool.ignormee;

namespace RemoteSqlTool.Tests
{
    [TestFixture]
    public class NpgConnectorTest
    {
        string? Host { get; set; }
        string? Username { get; set; }
        string? Password { get; set; }
        string? Database { get; set; }
        int Port { get; set; }

        SqlToolAuthenticationInformation LoginInfo;

        string connString;

        IRepository deleteRepo;
        IRepository updateRepo;
        IRepository selectRepo;
        IRepository insertRepo;

        IRepository verifySelectRepo;

        IRepository setupInsertRepo;
        IRepository setupDeleteRepo;
        IRepository teardownDeleteRepo;

        List<string> selectKeysList;
        List<string> selectValuesList;
        List<string> verifyUpdateValuesList;

        string deleteRecordMessage;

        [SetUp]
        public async Task Setup()
        {
            Host = "databank.c9mrseu2nxwi.us-east-1.rds.amazonaws.com";
            var ignore = new Ignore();
            Username = ignore.Username;
            Password = ignore.Password;
            Database = ignore.Database;
            Port = 5432;

            LoginInfo = new SqlToolAuthenticationInformation(Host, Username, Password, Database, Port);
            var sqlToolAuthenticationInformation = new SqlToolAuthenticationInformation();
            var connectionString = sqlToolAuthenticationInformation.GetPostgresConnectionString();

            setupInsertRepo = new InsertRepository();
            selectKeysList = new List<string>(new string[] { "firstname", "lastname", "email", "created_date", "address", "city", "state", "zip" });
            selectValuesList = new List<string>(new string[] { "Tyler", "Durden", "jack@gmail.com", "420 Paper St.", "Wilmington", "DE", "19886" });
            verifyUpdateValuesList = new List<string>(new string[] { "Brad", "Pitt", "brad.pitt@protonmail.com", "8/18/1980 12:00:00 AM" });

            await setupInsertRepo.Command(connString, "Insert into people (firstname, lastname, email, created_date) values ('Robert', 'Paulson', 'hisnameisrobertpaulson@gmail.com', current_timestamp)");
            await setupInsertRepo.Command(connString, "Insert into people (firstname, lastname, email, created_date) values ('Tyler', 'Durden', 'jack@gmail.com', current_timestamp)");
            await setupInsertRepo.Command(connString, "Insert into address (pid, address, city, state, zip, created_on) " +
                                                            " values ((select id from people where email = 'jack@gmail.com'), '420 Paper St.', 'Wilmington', 'DE', '19886', current_timestamp)");
            await setupInsertRepo.Command(connString, "Insert into people (firstname, lastname, email, created_date) values ('Edward', 'Norton', 'edward.norton@gmail.com', current_timestamp)");
        }

        [TearDown]
        public async Task Teardown()
        {
            setupDeleteRepo = new DeleteRepository();
            await setupDeleteRepo.Command(connString, "Delete from address where address = '420 Paper St.'");
            await setupDeleteRepo.Command(connString, "Delete from people where email = 'jack@gmail.com'");

            teardownDeleteRepo = new DeleteRepository();
            await teardownDeleteRepo.Command(connString, "Delete from people where email = 'Chuck.Palahniuk@gmail.com'");
            await teardownDeleteRepo.Command(connString, "Delete from address where address = '506 SW Mill Street, Suite 750'");

            await teardownDeleteRepo.Command(connString, "Delete from people where email = 'edward.norton@gmail.com'");
            await teardownDeleteRepo.Command(connString, "Delete from people where email = 'brad.pitt@protonmail.com'");
            await teardownDeleteRepo.Command(connString, "Delete from people where email = 'hisnameisrobertpaulson@gmail.com'");
        }

        [Test]
        public async Task UpdateCommandUpdatesRecord()
        {
            var assertCount = 0;
            var expectedCount = 4;

            updateRepo = new UpdateRepository();
            await updateRepo.Command(connString, "Update people Set firstname = 'Brad', lastname = 'Pitt', email = 'brad.pitt@protonmail.com', created_date = '08/18/1980' where email = 'edward.norton@gmail.com'");

            verifySelectRepo = new SelectRepository();
            var updateSelectResult = await verifySelectRepo.Command(connString, "Select firstname, lastname, email, created_date from people where email = 'brad.pitt@protonmail.com'");

            foreach (ListDictionary selectListDictionary in updateSelectResult)
            {
                int i = 0;
                foreach (DictionaryEntry dictionaryEntry in selectListDictionary)
                {
                    if (dictionaryEntry.Key.ToString() == selectKeysList[i]) 
                    {
                        if (dictionaryEntry.Value.ToString() == verifyUpdateValuesList[i])
                        {
                            Assert.AreEqual(dictionaryEntry.Value.ToString(), verifyUpdateValuesList[i]);
                            assertCount++;
                        }
                    }
                    i++;
                }
            }
            Assert.AreEqual(expectedCount, assertCount);
        }

        [Test]
        public async Task SelectCommandReturnsListOfPeople()
        {
            var assertCount = 0;
            var expectedCount = 3;

            selectRepo = new SelectRepository();
            var selectResult = await selectRepo.Command(connString, "Select firstname, lastname, email from people");

            Assert.IsInstanceOf<List<ListDictionary>>(selectResult);

            foreach (ListDictionary selectListDictionary in selectResult)
            {
                int i = 0;
                foreach (DictionaryEntry dictionaryEntry in selectListDictionary)
                {
                    if (dictionaryEntry.Key.ToString() == selectKeysList[i]);
                    {
                        if (dictionaryEntry.Value.ToString() == selectValuesList[i])
                        {
                            Assert.AreEqual(dictionaryEntry.Value.ToString(), selectValuesList[i]);
                            assertCount++;
                        }
                    }
                    i++;
                }
            }
            Assert.AreEqual(expectedCount, assertCount);
        }

        [Test]
        public async Task SelectCommandReturnsListOfAddresses()
        {
            var assertCount = 0;
            var expectedCount = 4;

            selectRepo = new SelectRepository();
            var selectResult = await selectRepo.Command(connString, "Select address, city, state, zip from address where zip = '19886'");

            Assert.IsInstanceOf<List<ListDictionary>>(selectResult);

            foreach (ListDictionary selectListDictionary in selectResult)
            {
                int i = 4;
                int v = 3;
                foreach (DictionaryEntry dictionaryEntry in selectListDictionary)
                {
                    if (dictionaryEntry.Key.ToString() == selectKeysList[i]) 
                    {
                        if (dictionaryEntry.Value.ToString() == selectValuesList[v])
                        {
                            Assert.AreEqual(dictionaryEntry.Value.ToString(), selectValuesList[v]);
                            assertCount++;
                        }
                    }
                    i++;
                    v++;
                }
            }
            Assert.AreEqual(expectedCount, assertCount);
        }

        [Test]
        public async Task DeleteCommandReturnsListOfListDictionary()
        {         
            deleteRepo = new DeleteRepository();

            var result = await deleteRepo.Command(connString, "Delete from people where email = 'hisnameisrobertpaulson@gmail.com'");

            Assert.IsInstanceOf<List<ListDictionary>>(result);

            ListDictionary firstRecord = result[0];

            foreach (DictionaryEntry commandResult in result[0])
            {
                deleteRecordMessage = commandResult.Value.ToString();
                break;
            }
           
            Assert.AreEqual(deleteRecordMessage, "Data successfully deleted"); //
        }

        [Test]
        public async Task InsertCommandIntoAddressTable()
        {
            var addressAssertCount = 0;
            var addressExpectedCount = 4;

            insertRepo = new InsertRepository();
            var selectAddressValuesList = new List<string>(new string[] { "506 SW Mill Street, Suite 750", "Portland", "OR", "97201" });
            
            await insertRepo.Command(connString, "Insert into address (pid, address, city, state, zip, created_on) values ((select id from people where id not in (select pid from address) order by id desc limit 1), '506 SW Mill Street, Suite 750', 'Portland', 'OR', '97201', current_timestamp)");
            //

            selectRepo = new SelectRepository();
            var selectAddressResult = await selectRepo.Command(connString, "Select address, city, state, zip from address where address = '506 SW Mill Street, Suite 750'");

            foreach (ListDictionary selectListDictionary in selectAddressResult)
            {
                int i = 4;
                int v = 0;
                foreach (DictionaryEntry dictionaryEntry in selectListDictionary)
                {
                    if (dictionaryEntry.Key.ToString() == selectKeysList[i])
                    {
                        if (dictionaryEntry.Value.ToString() == selectAddressValuesList[v])
                        {
                            Assert.AreEqual(dictionaryEntry.Value.ToString(), selectAddressValuesList[v]);
                            addressAssertCount++;
                        }
                    }
                    i++;
                    v++;
                }
            }
            Assert.AreEqual(addressExpectedCount, addressAssertCount);

            //deleteRepo = new DeleteRepo();

            //await deleteRepo.Command(connString, "Delete from address where address = '506 SW Mill Street, Suite 750'");
        }

        [Test]
        public async Task InsertCommandIntoPeopleTable()
        {
            insertRepo = new InsertRepository();

            var peopleAssertCount = 0;
            var peopleExpectedCount = 3;

            var selectPeopleValuesList = new List<string>(new string[] { "Chuck", "Palahniuk", "Chuck.Palahniuk@gmail.com" });

            await insertRepo.Command(connString, "Insert into people (firstname, lastname, email, created_date) values ('Chuck', 'Palahniuk', 'Chuck.Palahniuk@gmail.com', current_timestamp)");
            
            selectRepo = new SelectRepository();
            var selectPeopleResult = await selectRepo.Command(connString, "Select firstname, lastname, email from people where email = 'Chuck.Palahniuk@gmail.com'");

            foreach (ListDictionary selectListDictionary in selectPeopleResult)
            {
                int i = 0;
                foreach (DictionaryEntry dictionaryEntry in selectListDictionary)
                {
                    if (dictionaryEntry.Key.ToString() == selectKeysList[i]) 
                    {
                        if (dictionaryEntry.Value.ToString() == selectPeopleValuesList[i])
                        {
                            Assert.AreEqual(dictionaryEntry.Value.ToString(), selectPeopleValuesList[i]);
                            peopleAssertCount++;
                        }
                    }
                    i++;
                }
            }
            Assert.AreEqual(peopleExpectedCount, peopleAssertCount);
           
            //deleteRepo = new DeleteRepo();

            //await deleteRepo.Command(connString, "Delete from people where email = 'Chuck.Palahniuk@gmail.com'");
        }
    }
}