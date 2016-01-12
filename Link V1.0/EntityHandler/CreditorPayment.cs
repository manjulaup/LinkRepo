using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityHandler
{
    public class CreditorPayment
    {
        public string ReferenceNumber { get; set; }
        public string SupplierCode { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedUser { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public string HostID { get; set; }
        public bool Active { get; set; }
    }
}
