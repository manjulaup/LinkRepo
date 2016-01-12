using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityHandler
{
    public class Roles
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string HostID { get; set; }
        public int Status { get; set; }
    }
}
