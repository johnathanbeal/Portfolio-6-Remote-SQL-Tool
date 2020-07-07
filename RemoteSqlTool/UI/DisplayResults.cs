using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace RemoteSqlTool.UI
{
    public class DisplayResults
    {
        public void WriteSelectResultsToConsole(List<ListDictionary> queryResult)
        {
            Console.WriteLine("The number of records is :" + queryResult.Count + System.Environment.NewLine);

            var firstDictionary = queryResult[0];
            DisplayMessageIfCountIsTooBig(firstDictionary, 8);

            string direction;
            int starterIndex = 0;
            int numberOfRecordsToDisplay = 10;
            do
            {
                DisplayHeaders(firstDictionary);
                DisplayRecords(queryResult, starterIndex, numberOfRecordsToDisplay);


                Console.WriteLine("Press N to see the next results. Press P to see the Previous results");
                direction = Console.ReadLine();

                if (direction.ToLower().Contains("n"))
                {
                    if (starterIndex + 10 < queryResult.Count)
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

            } while (((direction.ToLower().Contains("n") || direction.ToLower().Contains("p"))) && !direction.ToLower().Contains("q") && starterIndex < queryResult.Count);

        }

        public void DisplayMessageIfCountIsTooBig(ListDictionary firstDictionary, int threshold)
        {
            var columnCount = firstDictionary.Keys.Count;
            if (columnCount > threshold)
            {
                Console.WriteLine("Maximize console window to see results more clearly" + System.Environment.NewLine);
            }

        }

        public void DisplayRecords(List<ListDictionary> queryResult, int startIndex, int displayCount)
        {
            for (int row = startIndex; row < displayCount && row < queryResult.Count; row++)
            {
                var debugStringBuilder = new StringBuilder();
                foreach (DictionaryEntry column in queryResult[row])
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

        public void DisplayHeaders(ListDictionary _firstDictionary)
        {
            var headerRowStringBuilder = new StringBuilder();

            foreach (var columnName in _firstDictionary.Keys)
            {
                headerRowStringBuilder.Append(columnName.ToString().ToUpper().PadRight(Util.PadSpace(columnName.ToString())).PadLeft(2) + "|");
            }
            Console.WriteLine(headerRowStringBuilder);
        }
    }
}
