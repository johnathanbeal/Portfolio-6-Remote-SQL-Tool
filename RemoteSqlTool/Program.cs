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
using System.Collections.Specialized;
using System.Collections;
using System.Linq;

namespace RemoteSqlTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AttestationCharacteristics databaseAuthorizationInputs = UserInteractions.InitialUserPrompts();

            NpgConnector NConn = new NpgConnector(databaseAuthorizationInputs);
            var NConString = NConn.connString(NConn.authProps);

            var results = await QueryWorkflow.EnterQueryAndRun(NConString, new List<ListDictionary>(), true);
        }
    }
}
