using Npgsql;
using NpgsqlTypes;
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
        public static int PadSpace(string propertyName)
        {
            switch (propertyName.ToLower())
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

        public static int TruncateValue(string propertyName)
        {
            switch (propertyName.ToLower())
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

        public static NpgsqlDbType GetTypeFromColumn(string columnName)
        {
            switch (columnName.ToLower())
            {
                case "Id":
                case "id":
                case "aid":
                case "pid":
                    return NpgsqlDbType.Integer;
                case "firstname":
                case "lastname":
                    return NpgsqlDbType.Text;
                case "email":
                    return NpgsqlDbType.Text;
                case "address":
                    return NpgsqlDbType.Text;
                case "city":
                    return NpgsqlDbType.Text;
                case "state":
                    return NpgsqlDbType.Text;
                case "zip":
                    return NpgsqlDbType.Text;
                case "created_on":
                    return NpgsqlDbType.Date;
                case "created_date":
                    return NpgsqlDbType.Date;
                default:
                    return NpgsqlDbType.Varchar;
            }
        }

        public static PeopleAddressIndexer assignReaderValueToPropertyByNpgsqlDbColumn(NpgsqlDataReader reamde)
        {
            PeopleAddressIndexer PeopleAddressIndexer = new PeopleAddressIndexer();
            var ColumnSchema = reamde.GetColumnSchema();

            for (int i = 0; i < reamde.FieldCount; i++)
            {
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
