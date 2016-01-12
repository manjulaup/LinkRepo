using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityHandler
{
    public class Status
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string HostID { get; set; }
    }
}
