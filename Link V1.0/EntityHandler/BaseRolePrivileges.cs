using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityHandler
{
    public class BaseRolePrivileges
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string ObjectName { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool View { get; set; }
        public bool Insert { get; set; }
        public bool Delete { get; set; }
        public bool Print { get; set; }
        public bool Approve { get; set; }
        public int Status { get; set; }
    }
}
