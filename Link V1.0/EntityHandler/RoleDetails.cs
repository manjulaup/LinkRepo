using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityHandler
{
    public class RoleDetails
    {
        public string RoleID { get; set; }
        public string ObjectCode { get; set; }
        public string PermID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedUser { get; set; }
        public string HostID { get; set; }
    }
}
