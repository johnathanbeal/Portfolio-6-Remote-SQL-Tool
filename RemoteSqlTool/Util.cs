using Npgsql;
using RemoteSqlTool.Indexer;
using RemoteSqlTool.Repository;
using System;
using System.Collections.Generic;
using System.Reflection;
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
                case "id":
                case "aid":
                case "pid":
                    return 3;
                case "firstname":
                case "lastname":
                    return 14;
                case "email":
                    return 28;
                case "address":
                    return 31;
                case "city":
                    return 13;
                case "state":
                    return 5;
                case "zip":
                    return 6;
                case "created_on":
                    return 12;
                case "createddate":
                    return 12;
                default:
                    return 10;
            }
        }

        public static int TruncateValue(string _propertyName)
        {
            switch (_propertyName.ToLower())
            {
                case "Id":
                case "id":
                case "aid":
                case "pid":
                    return 3;
                case "firstname":
                case "lastname":
                    return 14;
                case "email":
                    return 28;
                case "address":
                    return 31;
                case "city":
                    return 13;
                case "state":
                    return 5;
                case "zip":
                    return 6;
                case "created_on":
                    return 12;
                case "created_date":
                    return 12;
                default:
                    return 10;
            }
        }

        public static PeopleAddressIndexer assignReaderValueToPropertyByNpgsqlDbColumn(NpgsqlDataReader _reamde)
        {
            PeopleAddressIndexer PeopleAddressIndexer = new PeopleAddressIndexer();
            var ColumnSchema = _reamde.GetColumnSchema();

            //PeopleAddressIndexer.Id = -1;
            //PeopleAddressIndexer.Firstname = -1;
            //PeopleAddressIndexer.Lastname = -1;
            //PeopleAddressIndexer.Email = -1;
            //PeopleAddressIndexer.CreatedOn = -1;
            //PeopleAddressIndexer.AddressId = -1;
            //PeopleAddressIndexer.PersonId = -1;
            //PeopleAddressIndexer.City = -1;
            //PeopleAddressIndexer.State = -1;
            //PeopleAddressIndexer.Zip = -1;
            //PeopleAddressIndexer.CreatedDate = -1;

            for (int i = 0; i < _reamde.FieldCount; i++)
            {
                //peopleDictionary[i] = ColumnSchema[i].ColumnName.ToString();
                Console.WriteLine("Column Name is :" + ColumnSchema[i].ColumnName.ToString());
                Console.WriteLine("Column Indexer is :" + ColumnSchema[i].ColumnOrdinal.ToString());
                switch (ColumnSchema[i].ColumnName.ToString().ToLower())
                {
                    case "id":
                        PeopleAddressIndexer.Id = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                    case "firstname":
                        PeopleAddressIndexer.Firstname = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                    case "lastname":
                        PeopleAddressIndexer.Lastname = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                    case "email":
                        PeopleAddressIndexer.Email = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                    case "createdon":
                        PeopleAddressIndexer.CreatedOn = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                    case "addressid":
                        PeopleAddressIndexer.AddressId = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                    case "personid":
                        PeopleAddressIndexer.PersonId = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                    case "city":
                        PeopleAddressIndexer.City = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                    case "state":
                        PeopleAddressIndexer.State = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                    case "zip":
                        PeopleAddressIndexer.Zip = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                    case "createddate":
                        PeopleAddressIndexer.CreatedDate = Int32.Parse(ColumnSchema[i].ColumnOrdinal.ToString());
                        break;
                }
            }

            return PeopleAddressIndexer;
        }

       
    }
}
