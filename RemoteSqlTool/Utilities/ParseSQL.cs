using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace RemoteSqlTool.Utilities
{
    public static class ParseSQL
    {
        public static Dictionary<string, string> parseUpdateSQLReturnColumnsAndValues(string _sqlQuery)
        {
            var afterSetString = _sqlQuery.ToLower().After("set");
            string setString = afterSetString; ;
            if (afterSetString.Contains("where"))
            {
                setString = afterSetString.Before("where");
            }

            string[] commaSplit = setString.Split(',');
            Dictionary<string, string> columnsAndValues = new Dictionary<string, string>();
            foreach (var columnAndValue in commaSplit)
            {
                columnsAndValues.Add((columnAndValue.Before("=").Trim()), columnAndValue.After("=").Trim());
            }

            return columnsAndValues;
        }

        public static List<ListDictionary> parseInsertSQLReturnColumnsAndValues(string _sqlQuery)
        {
            var lld = new List<ListDictionary>();

            var tableAndColumns = _sqlQuery.ToLower().Between("insert into ", " values");

            var table = tableAndColumns.ToLower().Between("", " (");

            var valuesString = _sqlQuery.ToLower().Between("values (", ")");

            var _columns = tableAndColumns.ToLower().Between(table + " (", ")");
            if (_columns == "")
            {
                Console.WriteLine("Please include columns in your insert statement");
            }

            string[] columns = _columns.Replace(" ", "").Split(',');
            string[] values = valuesString.Replace(" ", "").Split(',');

            for (int i = 0; i < columns.Length; i++)
            {
                var ld = new ListDictionary();
                ld.Add(columns[i], values[i]);
                lld.Add(ld);
            }

            return lld;
        }
    }
}
