using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityHandler
{
    public class Supplier
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public DateTime CreditPeriod { get; set; }
        public string Type { get; set; }
        public bool VATEnable { get; set; }
        public string CompanyName { get; set; }
        public bool Active { get; set; }
        public string Remarks { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Status { get; set; }
    }
}
