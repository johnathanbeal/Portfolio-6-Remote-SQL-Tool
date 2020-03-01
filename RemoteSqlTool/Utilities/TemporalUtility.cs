using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteSqlTool.Utilities
{
    public static class TemporalUtility
    {
        public static string ConvertCurrentTimestampStringToDateTimeNowString(string dateString)
        {
            if(dateString == "current_timestamp")
            {
                return DateTime.Now.ToString();
            }
            else
            {
                return dateString;
            }
        }
    }
}
