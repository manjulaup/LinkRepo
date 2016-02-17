using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityHandler
{
    public class PaymentVoucher
    {
        public string BillNo { get; set; }
        public string GRNNo { get; set; }
        public decimal Amount { get; set; }
        public string PONo { get; set; }
    }
}
