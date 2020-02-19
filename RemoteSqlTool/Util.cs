using Npgsql;
using RemoteSqlTool.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteSqlTool
{
    public static class Util
    {
        public static int PadSpace(string _propertyName)
        {
            switch (_propertyName.ToLower())
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

        public static PeopleAddressEntity assignReaderValueToPropertyByNpgsqlDbColumn(NpgsqlDataReader _reamde)
        {
            PeopleAddressEntity peo = new PeopleAddressEntity();
            Dictionary<int, PeopleAddressEntity> peopleDictionary = new Dictionary<int, PeopleAddressEntity>();
            var ColumnSchema = _reamde.GetColumnSchema();

            for (int i = 0; i < _reamde.FieldCount; i++)
            {
                //peopleDictionary[i] = ColumnSchema[i].ColumnName.ToString();
                Console.WriteLine(ColumnSchema[i].ColumnName.ToString());
                Console.WriteLine(ColumnSchema[i].ColumnOrdinal.ToString());
                switch (ColumnSchema[i].ColumnName.ToString().ToLower())
                {
                    case "id":
                        //return peo.Id;
                        break;
                    case "firstname":
                        Console.WriteLine("Column " + i + "is " + ColumnSchema[i].ColumnName.ToString());
                        break;
                    case "lastname":
                        Console.WriteLine("Column " + i + "is " + ColumnSchema[i].ColumnName.ToString());
                        break;
                    case "email":
                        Console.WriteLine("Column " + i + "is " + ColumnSchema[i].ColumnName.ToString());
                        break;
                    case "createdon":
                        Console.WriteLine("Column " + i + "is " + ColumnSchema[i].ColumnName.ToString());
                        break;
                }
            }

            return null;
        }
    }
}
