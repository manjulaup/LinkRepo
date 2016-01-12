using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityHandler
{
    public class Objects
    {
        public string ObjectCode { get; set; }
        public string ObjectFileName { get; set; }
        public int ListOrdering { get; set; }
        public string Description { get; set; }
        public int LevelType { get; set; }
        public string HostID { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Status { get; set; }
        
    }
}
