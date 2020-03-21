using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace RemoteSqlTool.UI
{
    public static class DisplayResults
    {
        public static void WriteSelectResultsToConsole(List<ListDictionary> _queryResult)
        {
            Console.WriteLine("The number of records is :" + _queryResult.Count + System.Environment.NewLine);

            var firstDictionary = _queryResult[0];
            DisplayMessageIfCountIsTooBig(firstDictionary, 8);

            string direction;
            int starterIndex = 0;
            int numberOfRecordsToDisplay = 10;
            do
            {
                DisplayHeaders(firstDictionary);
                DisplayRecords(_queryResult, starterIndex, numberOfRecordsToDisplay);
                
                Console.WriteLine("Press N to see the next results. Press P to see the Previous results");
                direction = Console.ReadLine();
                var x = _queryResult.Count;
                if (direction.ToLower().Contains("n"))
                {
                    if (starterIndex + 10 < _queryResult.Count)
                    {
                        starterIndex += 10;
                        numberOfRecordsToDisplay += 10;
                    }
                    else
                    {
                        Console.WriteLine("There are no next results");
                        break;
                    }
                }
                else if (direction.ToLower().Contains("p"))
                {
                    if (starterIndex >= 10)
                    {
                        starterIndex -= 10;
                        numberOfRecordsToDisplay -= 10;
                    }
                    else
                    {
                        Console.WriteLine("There are no previous results");
                        break;
                    }                  
                }
                else
                {
                    Console.WriteLine("Exiting");
                    direction = "q";
                }


            } while (((direction.ToLower().Contains("n") || direction.ToLower().Contains("p"))) && !direction.ToLower().Contains("q") && starterIndex < _queryResult.Count);
            
        }

        public static void DisplayMessageIfCountIsTooBig(ListDictionary _firstDictionary, int threshold)
        {
            var columnCount = _firstDictionary.Keys.Count;
            if (columnCount > threshold)
            {
                Console.WriteLine("Maximize console window to see results more clearly" + System.Environment.NewLine);
            }

        }

        public static void DisplayRecords(List<ListDictionary> _queryResult, int startIndex, int displayCount)
        {
                for(int row = startIndex; row < displayCount && row < _queryResult.Count; row++)
                {               
                    var debugStringBuilder = new StringBuilder();
                    foreach (DictionaryEntry column in _queryResult[row])
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
            //Console.WriteLine(System.Environment.NewLine);
        }

        public static void DisplayHeaders(ListDictionary _firstDictionary)
        {
            var headerRowStringBuilder = new StringBuilder();

            foreach (var columnName in _firstDictionary.Keys)
            {
                headerRowStringBuilder.Append(columnName.ToString().ToUpper().PadRight(Util.PadSpace(columnName.ToString())).PadLeft(2) + "|");
            }
            Console.WriteLine(headerRowStringBuilder);
        }

        public static List<ListDictionary> SplitList(List<ListDictionary> _queryResult, int nSize = 10)
        {
            var list = new List<ListDictionary>();

            for (int i = 0; i < _queryResult.Count; i += nSize)
            {
                list.Add(_queryResult[i]);
            }

            return list;
        }
    }
}
