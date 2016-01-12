using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataService;
using BusinessLayer.CommonOperation;
using System.Data;
using MySql.Data.MySqlClient;

namespace BusinessLayer.Supplier
{

    public class Supplier
    {
        public string  GetSupplier(int Sysid, out DataType.SupplierDataType _ExistData,bool IsLocal)
        {
            DataService Mycommon = new DataService(IsLocal);
            _ExistData = new DataType.SupplierDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "Sysid,"
          + "Customer,"
          + "Address1,"
          + "Address2,"
          + "Address3,"
          + "TPN,"
          + "Mobile,"
          + "Fax,"
          + "Email,"
          + "CreditLimit,"
          + "CurrentCredit,"
          + "VatRegNumber,"
          + "Status,"
          + "LastAccUser,"
          + "LastAccDate,"
          + "ContactPerson,"
          + "ContactTPnumber,"
          + "PaymentTerms,"
          + "CreditPeriod,"
          + "DeleveryMethod,"
          + "Shippingadd1,"
          + "ShippingAdd2,"
          + "ShippingAdd3,"
          + "Country,"
          + "CusID,"
          + "FFName,"
          + "FFAccountNo,"
          + "UnderG4,"
          + "Email_OrderAcn,"
          + "Email_Invoice,"
          + "Email_Complain,"
          + "NeedCustomerLogo,"
          + "CurrenyCode"
          + " from tblsupplier"
          + " Where 1=1 "
                + " and Sysid=@Sysid";
            oSqlCommand.Parameters.AddWithValue("@Sysid", Sysid);
            DataRow r = Mycommon.GetDataRow(sqlQuery, oSqlCommand, null, "Get Exist data Supplier");
            if (r != null)
            {
                try
                {
                    bool resp = false;
                    int inSysid = 0;
                    resp = int.TryParse(r["Sysid"].ToString(), out inSysid);
                    _ExistData.Sysid = inSysid;
                    _ExistData.Customer = r["Customer"].ToString();
                    _ExistData.Address1 = r["Address1"].ToString();
                    _ExistData.Address2 = r["Address2"].ToString();
                    _ExistData.Address3 = r["Address3"].ToString();
                    _ExistData.TPN = r["TPN"].ToString();
                    _ExistData.Mobile = r["Mobile"].ToString();
                    _ExistData.Fax = r["Fax"].ToString();
                    _ExistData.Email = r["Email"].ToString();
                    decimal deCreditLimit = 0;
                    resp = decimal.TryParse(r["CreditLimit"].ToString(), out deCreditLimit);
                    _ExistData.CreditLimit = deCreditLimit;
                    decimal deCurrentCredit = 0;
                    resp = decimal.TryParse(r["CurrentCredit"].ToString(), out deCurrentCredit);
                    _ExistData.CurrentCredit = deCurrentCredit;
                    _ExistData.VatRegNumber = r["VatRegNumber"].ToString();
                    int inStatus = 0;
                    resp = int.TryParse(r["Status"].ToString(), out inStatus);
                    _ExistData.Status = inStatus;
                    _ExistData.LastAccUser = r["LastAccUser"].ToString();
                    DateTime dtLastAccDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["LastAccDate"].ToString(), out dtLastAccDate);
                    if (resp)
                        _ExistData.LastAccDate = dtLastAccDate;
                    else
                        _ExistData.LastAccDate = new DateTime(1900, 1, 1);
                    _ExistData.ContactPerson = r["ContactPerson"].ToString();
                    _ExistData.ContactTPnumber = r["ContactTPnumber"].ToString();
                    _ExistData.PaymentTerms = r["PaymentTerms"].ToString();
                    int inCreditPeriod = 0;
                    resp = int.TryParse(r["CreditPeriod"].ToString(), out inCreditPeriod);
                    _ExistData.CreditPeriod = inCreditPeriod;
                    _ExistData.DeleveryMethod = r["DeleveryMethod"].ToString();
                    _ExistData.Shippingadd1 = r["Shippingadd1"].ToString();
                    _ExistData.ShippingAdd2 = r["ShippingAdd2"].ToString();
                    _ExistData.ShippingAdd3 = r["ShippingAdd3"].ToString();
                    _ExistData.Country = r["Country"].ToString();
                    _ExistData.CusID = r["CusID"].ToString();
                    _ExistData.FFName = r["FFName"].ToString();
                    _ExistData.FFAccountNo = r["FFAccountNo"].ToString();
                    int inUnderG4 = 0;
                    resp = int.TryParse(r["UnderG4"].ToString(), out inUnderG4);
                    _ExistData.UnderG4 = inUnderG4;
                    _ExistData.Email_OrderAcn = r["Email_OrderAcn"].ToString();
                    _ExistData.Email_Invoice = r["Email_Invoice"].ToString();
                    _ExistData.Email_Complain = r["Email_Complain"].ToString();
                    int inNeedCustomerLogo = 0;
                    resp = int.TryParse(r["NeedCustomerLogo"].ToString(), out inNeedCustomerLogo);
                    _ExistData.NeedCustomerLogo = inNeedCustomerLogo;
                    _ExistData.CurrenyCode = r["CurrenyCode"].ToString();
                    return "True";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
                return "data not found ";
        }

    }
    public class Customer
    {
        public string GetGetExistCustomer(int Sysid, out DataType.CustomerDataType _ExistData, bool IsLocal)
        {
            DataService Mycommon = new DataService(IsLocal);
            _ExistData = new DataType.CustomerDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "Sysid,"
          + "Customer,"
          + "Address1,"
          + "Address2,"
          + "Address3,"
          + "TPN,"
          + "Mobile,"
          + "Fax,"
          + "Email,"
          + "CreditLimit,"
          + "CurrentCredit,"
          + "VatRegNumber,"
          + "Status,"
          + "NIC,"
          + "Qualification,"
          + "LastAccUser,"
          + "LastAccDate,"
          + "ContactPerson,"
          + "ContactTPnumber,"
          + "Country,"
          + "CusID,"
          + "Email_OrderAcn,"
          + "Email_Invoice,"
          + "Email_Complain,"
          + "NeedCustomerLogo,"
          + "CurrenyCode"
          + " from tblsubcontractors"
          + " Where 1=1 "
                + " and Sysid=@Sysid";
            oSqlCommand.Parameters.AddWithValue("@Sysid", Sysid);
            DataRow r = Mycommon.GetDataRow(sqlQuery, oSqlCommand, null, "Get Exist data Customer");
            if (r != null)
            {
                try
                {
                    bool resp = false;
                    _ExistData.Customer = r["Customer"].ToString();
                    _ExistData.Address1 = r["Address1"].ToString();
                    _ExistData.Address2 = r["Address2"].ToString();
                    _ExistData.Address3 = r["Address3"].ToString();
                    _ExistData.TPN = r["TPN"].ToString();
                    _ExistData.Mobile = r["Mobile"].ToString();
                    _ExistData.Fax = r["Fax"].ToString();
                    _ExistData.Email = r["Email"].ToString();
                    decimal deCreditLimit = 0;
                    resp = decimal.TryParse(r["CreditLimit"].ToString(), out deCreditLimit);
                    _ExistData.CreditLimit = deCreditLimit;
                    decimal deCurrentCredit = 0;
                    resp = decimal.TryParse(r["CurrentCredit"].ToString(), out deCurrentCredit);
                    _ExistData.CurrentCredit = deCurrentCredit;
                    _ExistData.VatRegNumber = r["VatRegNumber"].ToString();
                    int inStatus = 0;
                    resp = int.TryParse(r["Status"].ToString(), out inStatus);
                    _ExistData.Status = inStatus;
                    _ExistData.NIC = r["NIC"].ToString();
                    _ExistData.Qualification = r["Qualification"].ToString();
                    _ExistData.LastAccUser = r["LastAccUser"].ToString();
                    DateTime dtLastAccDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["LastAccDate"].ToString(), out dtLastAccDate);
                    if (resp)
                        _ExistData.LastAccDate = dtLastAccDate;
                    else
                        _ExistData.LastAccDate = new DateTime(1900, 1, 1);
                    _ExistData.ContactPerson = r["ContactPerson"].ToString();
                    _ExistData.ContactTPnumber = r["ContactTPnumber"].ToString();
                    _ExistData.Country = r["Country"].ToString();
                    _ExistData.CusID = r["CusID"].ToString();
                    _ExistData.Email_OrderAcn = r["Email_OrderAcn"].ToString();
                    _ExistData.Email_Invoice = r["Email_Invoice"].ToString();
                    _ExistData.Email_Complain = r["Email_Complain"].ToString();
                    int inNeedCustomerLogo = 0;
                    resp = int.TryParse(r["NeedCustomerLogo"].ToString(), out inNeedCustomerLogo);
                    _ExistData.NeedCustomerLogo = inNeedCustomerLogo;
                    _ExistData.CurrenyCode = r["CurrenyCode"].ToString();
                    return "True";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
                return "data not found ";
        }
    }

    public class DataType
    {
        public struct SupplierDataType
        {
            public int Sysid;
            public string Customer;
            public string Address1;
            public string Address2;
            public string Address3;
            public string TPN;
            public string Mobile;
            public string Fax;
            public string Email;
            public decimal CreditLimit;
            public decimal CurrentCredit;
            public string VatRegNumber;
            public int Status;
            public string LastAccUser;
            public DateTime LastAccDate;
            public string ContactPerson;
            public string ContactTPnumber;
            public string PaymentTerms;
            public int CreditPeriod;
            public string DeleveryMethod;
            public string Shippingadd1;
            public string ShippingAdd2;
            public string ShippingAdd3;
            public string Country;
            public string CusID;
            public string FFName;
            public string FFAccountNo;
            public int UnderG4;
            public string Email_OrderAcn;
            public string Email_Invoice;
            public string Email_Complain;
            public int NeedCustomerLogo;
            public string CurrenyCode;
        }
        public struct CustomerDataType
        {
            public Int64 Sysid;
            public string Customer;
            public string Address1;
            public string Address2;
            public string Address3;
            public string TPN;
            public string Mobile;
            public string Fax;
            public string Email;
            public decimal CreditLimit;
            public decimal CurrentCredit;
            public string VatRegNumber;
            public int Status;
            public string NIC;
            public string Qualification;
            public string LastAccUser;
            public DateTime LastAccDate;
            public string ContactPerson;
            public string ContactTPnumber;
            public string Country;
            public string CusID;
            public string Email_OrderAcn;
            public string Email_Invoice;
            public string Email_Complain;
            public int NeedCustomerLogo;
            public string CurrenyCode;
        }

    }
}
