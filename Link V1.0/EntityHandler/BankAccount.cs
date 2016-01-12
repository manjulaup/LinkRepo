using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityHandler
{
    public class BankAccount
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public string SWIFTCode { get; set; }
        public decimal OpenAmount { get; set; }
        public bool Active { get; set; }
        public int CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifiedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string AccountType { get; set; }
        public int Status { get; set; }
        public string BankCode { get; set; }
    }
}
