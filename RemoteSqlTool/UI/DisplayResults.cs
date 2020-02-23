using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace RemoteSqlTool.UI
{
    public class DisplayResults
    {
        public void WriteSelectResultsToConsole(List<ListDictionary> _queryResult)
        {
            Console.WriteLine("The number of records is :" + _queryResult.Count + System.Environment.NewLine);
            var headerRowStringBuilder = new StringBuilder();
            var rowsOfRecordsStringBuilder = new StringBuilder();
            //var debugStringBuilder = new StringBuilder();

            var firstDictionary = _queryResult[0];
            var columnCount = firstDictionary.Keys.Count;
            if (columnCount > 8)
            {
                Console.WriteLine("Maximize console window to see results more clearly" + System.Environment.NewLine);
            }
            //GroupBy(de => de.Keys).Select(columnName => columnName.First()).ToList();
            foreach (var columnName in firstDictionary.Keys)
            {

                headerRowStringBuilder.Append(columnName.ToString().ToUpper().PadRight(Util.PadSpace(columnName.ToString())).PadLeft(2) + "|");
            }
            Console.WriteLine(headerRowStringBuilder);

            foreach (var row in _queryResult)
            {
                var debugStringBuilder = new StringBuilder();
                foreach (DictionaryEntry column in row)
                {
                    int index;
                    int length = column.Value.ToString().Length;
                    if (Util.TruncateValue(column.Key.ToString()) > column.Value.ToString().Length)
                    {
                        index = length;
                    }
                    else
                    {
                        index = Util.TruncateValue(column.Key.ToString());
                    }
                    var val = column.Value.ToString();
                    var ki = column.Key.ToString();
                    val = val.Substring(0, index);
                    debugStringBuilder.Append(val.PadRight(Util.PadSpace(ki)).PadLeft(2) + "|");
                }
                Console.WriteLine(debugStringBuilder);
            }
        }
    }
}
