using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteSqlTool.Entities
{
    class PeopleEntity
    {      
            public int Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }

            public string? Email { get; set; }
            public DateTime CreatedDate { get; set; }     
    }
}
