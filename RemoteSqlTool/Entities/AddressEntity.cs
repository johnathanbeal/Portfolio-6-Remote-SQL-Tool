using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteSqlTool.Entities
{
    class AddressEntity
    {
        public int Id { get; set; }

        public int PeopleId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
