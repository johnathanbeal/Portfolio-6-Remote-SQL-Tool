using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteSqlTool.Repository
{
    class PeopleAddressEntity
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string? Email { get; set; }
        public DateTime CreatedOn { get; set; }

        public int AddressId { get; set; }

        public int PersonId { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string CreatedDate { get; set; }
    }
}
