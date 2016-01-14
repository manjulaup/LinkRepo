using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LINKPayment
/// </summary>
/// 
namespace EntityHandler
{
    public class LINKPayment
    {
        public LINKPayment()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string SupplierCode { get; set; }
        public string SupName { get; set; }
        public decimal Tot { get; set; }
        public int NoDays { get; set; }
        public string SupAddress { get; set; }
        public string SupContact { get; set; }
        public int SupplierID { get; set; }
        public int CreditPeriod { get; set; }

        public string GRNNo { get; set; }
        public DateTime GRNApprovedate { get; set; }

        public string InvoiceID { get; set; }
        public int ID { set; get; }
        public DateTime InvoiceDate { get; set; }
        public decimal Value { get; set; }
        public DateTime CreateDate  { get; set; }
	    public int CreateUser  { get; set; }
	    public DateTime Modifieddate { get; set; }
	    public int ModifiedUser  { get; set; }
	    public int Status  { get; set; }

        public string VoucherID { get; set; }
        public string AccNo { get; set; }
        public DateTime VoucherDate { get; set; }
        public DateTime ApproveDate { get; set; }

        public string Description {get; set;}

    }
}