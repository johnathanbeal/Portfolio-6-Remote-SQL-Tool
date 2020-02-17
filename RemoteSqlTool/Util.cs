using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteSqlTool
{
    public static class Util
    {
        public static int PadSpace(string _propertyName)
        {
            switch(_propertyName.ToLower())
            {
                case "Id":
                    return 3;
                case "firstname":
                case "lastname":
                    return 20;
                case "email":
                    return 35;
                case "createddate":
                    return 25;
                default:
                    return 10;   
            }
        }
    }
}
