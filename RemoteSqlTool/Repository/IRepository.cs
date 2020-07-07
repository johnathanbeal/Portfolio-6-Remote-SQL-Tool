using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace RemoteSqlTool.Repository
{
    public interface IRepository
    {
        Task<List<ListDictionary>> Command(string connString, string _sqlQuery);
    }
}
