using RemoteSqlTool.UI;
using System.Threading.Tasks;

namespace RemoteSqlTool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var queryWorkflow = new QueryWorkflow();
            var displayResults = new DisplayResults();

            var connectionString = UserInteractions.AskUserForAuthenticationInformation();
            var sqlCommand = UserInteractions.PromptUserForSqlCommand();

            var results = await queryWorkflow.ExecuteQuery(connectionString, sqlCommand);

            displayResults.WriteSelectResultsToConsole(results);
        }
    }
}
