using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityHandler
{
    public class CustomerMaster
    {
        public int Customer_Code { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Other_Names { get; set; }
        public string Address { get; set; }
        public string Customer_Type { get; set; }
        public string Category { get; set; }
        public int Area { get; set; }
        public decimal Credits_Limits { get; set; }
        public int Status { get; set; }
        public int UserID { get; set; }
        public int Active { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string SalesMethod { get; set; }
        public string ProductCategory { get; set; }
        public string CusFINCode { get; set; }

    }
}
