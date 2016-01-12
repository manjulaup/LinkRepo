//Create by SATICIN On 18/Oct/2015
//
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using BusinessLayer.CommonOperation;
using DataLayer.DataService;
using System.Windows.Forms;
namespace BusinessLayer.AccountCreations
{
    public class AccountCreation
    {
        private DataService Mycommon =null;
        private CommonOperations MyGeneral = null;
        public AccountCreation(bool IsLocal)
        {
            Mycommon = new DataService(IsLocal);
            MyGeneral = new CommonOperations(IsLocal);
        }
         ~AccountCreation()
        {
            Mycommon.CloseDB();
        }
        public AccountCreation()
        {
            
        }
        public bool IsITCashInly(int AcID)
        {
            string sql1 = "select IsItCash from accountname where AccountID=" + AcID;
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get is it Cash");
            if (r != null)
            {
                int i = 0;
                bool resp = int.TryParse(r["IsItCash"].ToString(), out i);
                if (i == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public string GetExchangeGainAccount()
        {
            string sql1 = "Select ExchangeGainAcID from tblcompanyconfig where SysID=1000";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Ex Gain account");
            if (r != null)
            {
                return r["ExchangeGainAcID"].ToString();
            }
            else
                return "";
        }
        
        public string GetExchangeLossAccount()
        {
            string sql1 = "Select ExchangeLossAcID from tblcompanyconfig where SysID=1000";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Ex Gain account");
            if (r != null)
            {
                return r["ExchangeLossAcID"].ToString();
            }
            else
                return "";
        }

        public  string GetSupplierAccountID(int SupplierID)
        {
            string sql1 = "Select SupplierAccount from tblsupplier where sysid=" + SupplierID;
            DataRow r = Mycommon.GetDataRow(sql1, "Get Supplier Account ID");
            if (r != null)
                {
                  return r["SupplierAccount"].ToString();
                }
            else
                return "";
        }
        public int GetCreditPeriod(int Supid,bool  issupplier)
        {
            string sql1 = "";
            if (issupplier)
            sql1="Select CreditPeriod from tblsupplier where sysid=" + Supid;
            else
                sql1 = "Select CreditPeriod from tblcustomer where sysid=" + Supid;
            DataRow r = Mycommon.GetDataRow(sql1, "Get Supplier Account ID");
            if (r != null)
            {
                int i = 0;
                bool resp = int.TryParse(r["CreditPeriod"].ToString(), out i);
                return i;
            }
            else
                return 0;
        }
        
        public decimal GetExRate(string  AccID,out string Currency)
        {
            Currency="Nill";
            string sql1 = "SELECT tblcurrency.ExRate,tblcurrency.CurID FROM accounterp.tblcurrency "
                + " INNER JOIN accounterp.accountname ON tblcurrency.CurID = accountname.Currency "
                + " where  accountname.AccountID ='" + AccID + "'";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get Exrate");
            if (r != null)
            {
                decimal d = 0;
                bool resp = decimal.TryParse(r["ExRate"].ToString (), out d);
                Currency = r["CurID"].ToString();
                return d;

            }
            else
                return 0;

        }

        public string SaveAccountCreation(AccountType.AccountCreationDataType _SaveData)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into accountname ("
           + "AccountID,"
          + "AccountName,"
          + "MainAccType,"
          + "IsSubAccount,"
          + "MainAccountID,"
          + "AccountStatus,"
          + "Currency,"
          + "BankAccountNo,"
           + "CreatedUser,"
          + "lastaccdate,CompanyID)"
           + " Values ("
          + "@AccountID,"
           + "@AccountName,"
           + "@MainAccType,"
           + "@IsSubAccount,"
           + "@MainAccountID,"
           + "@AccountStatus,"
           + "@Currency,"
           + "@BankAccountNo,"
           + "@CreatedUser,"
           + "curdate(),@CompanyID)";
            try
            {
               
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@AccountName", _SaveData.AccountName);
                oSqlCommand.Parameters.AddWithValue("@MainAccType", _SaveData.MainAccType);
                oSqlCommand.Parameters.AddWithValue("@IsSubAccount", _SaveData.IsSubAccount);
                oSqlCommand.Parameters.AddWithValue("@MainAccountID", _SaveData.MainAccountID);
                oSqlCommand.Parameters.AddWithValue("@AccountStatus", _SaveData.AccountStatus);
                oSqlCommand.Parameters.AddWithValue("@Currency", _SaveData.Currency);
                oSqlCommand.Parameters.AddWithValue("@BankAccountNo", _SaveData.BankAccountNo);
                oSqlCommand.Parameters.AddWithValue("@CreatedUser", _SaveData.CreatedUser);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
               
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Save AccountCreation");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateAccountCreation(AccountType.AccountCreationDataType _Update)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update accountname Set "
                + "AccountName=@AccountName,"
                + "MainAccType=@MainAccType,"
                + "IsSubAccount=@IsSubAccount,"
                + "MainAccountID=@MainAccountID,"
                + "AccountStatus=@AccountStatus,"
                + "Currency=@Currency,"
                + "CurrencySymble=@CurrencySymble,"
                + "BankAccountNo=@BankAccountNo,"
                + "CreatedUser=@CreatedUser,"
                + "lastaccdate=curdate()"
                + " Where 1=1 "
                + " and AccountID=@AccountID";
            try
            {
               
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@AccountName", _Update.AccountName);
                oSqlCommand.Parameters.AddWithValue("@MainAccType", _Update.MainAccType);
                oSqlCommand.Parameters.AddWithValue("@IsSubAccount", _Update.IsSubAccount);
                oSqlCommand.Parameters.AddWithValue("@MainAccountID", _Update.MainAccountID);
                oSqlCommand.Parameters.AddWithValue("@AccountStatus", _Update.AccountStatus);
                oSqlCommand.Parameters.AddWithValue("@Currency", _Update.Currency);
                oSqlCommand.Parameters.AddWithValue("@CurrencySymble", _Update.CurrencySymble);
                oSqlCommand.Parameters.AddWithValue("@BankAccountNo", _Update.BankAccountNo);
                oSqlCommand.Parameters.AddWithValue("@CreatedUser", _Update.CreatedUser);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Update AccountCreation");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteAccountCreation(string  AccountID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from accountname"
                + " Where 1=1 "
                + " and AccountID=@AccountID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@AccountID", AccountID);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Delete AccountCreation");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool ExistAccountCreation(string  AccountID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from accountname"
                + " Where 1=1 "
                + " and AccountID=@AccountID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@AccountID", AccountID);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist Account ID");
                return respond;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string GetExistAccountCreation(string  AccountID, out AccountType.AccountCreationDataType _ExistData)
        {
            _ExistData = new AccountType.AccountCreationDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "SysID,"
          + "AccountID,"
          + "AccountName,"
          + "MainAccType,"
          + "IsSubAccount,"
          + "MainAccountID,"
          + "AccountStatus,"
          + "Currency,"
          + "CurrencySymble,"
          + "BankAccountNo,"
          + "LastVoucherID,"
          + "LastTransRef,"
          + "CreatedUser,"
          + "lastaccdate,CompanyID"
          + " from accountname"
          + " Where 1=1 "
                + " and AccountID=@AccountID";
            oSqlCommand.Parameters.AddWithValue("@AccountID", AccountID);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand,  "Get Exist data AccountCreation");
            if (r != null)
            {
                try
                {
                    bool resp = false;
                    int inSysID = 0;
                    resp = int.TryParse(r["SysID"].ToString(), out inSysID);
                    _ExistData.SysID = inSysID;
                    _ExistData.AccountID = r["AccountID"].ToString();
                    _ExistData.AccountName = r["AccountName"].ToString();
                    int inMainAccType = 0;
                    resp = int.TryParse(r["MainAccType"].ToString(), out inMainAccType);
                    _ExistData.MainAccType = inMainAccType;
                    int inIsSubAccount = 0;
                    resp = int.TryParse(r["IsSubAccount"].ToString(), out inIsSubAccount);
                    _ExistData.IsSubAccount = inIsSubAccount;
                    
                    _ExistData.MainAccountID = r["MainAccountID"].ToString();
                    int inAccountStatus = 0;
                    resp = int.TryParse(r["AccountStatus"].ToString(), out inAccountStatus);
                    _ExistData.AccountStatus = inAccountStatus;
                    _ExistData.Currency = r["Currency"].ToString();
                    _ExistData.CurrencySymble = r["CurrencySymble"].ToString();
                    _ExistData.BankAccountNo = r["BankAccountNo"].ToString();
                    int inLastVoucherID = 0;
                    resp = int.TryParse(r["LastVoucherID"].ToString(), out inLastVoucherID);
                    _ExistData.LastVoucherID = inLastVoucherID;
                    _ExistData.LastTransRef = r["LastTransRef"].ToString();
                    _ExistData.CreatedUser = r["CreatedUser"].ToString();
                    DateTime dtlastaccdate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["lastaccdate"].ToString(), out dtlastaccdate);
                    if (resp)
                        _ExistData.lastaccdate = dtlastaccdate;
                    else
                        _ExistData.lastaccdate = new DateTime(1900, 1, 1);

                    int inCompanyID = 0;
                    resp = int.TryParse(r["CompanyID"].ToString(), out inCompanyID);
                    _ExistData.CompanyID = inCompanyID;

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
        public int GetAccountTypeFromAccountID(string AccountID)
        {
            string sql1 = "SELECT MainAccType FROM accountname where AccountID='" + AccountID + "'";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get Ac type");
            int i = 0;
            bool resp = int.TryParse(r["MainAccType"].ToString(), out i);
            return i;


        }
        public int GetAccountType(string AccounttypeName)
        {
            string sql1 = "Select SysID from accounttype where AccountType='" + AccounttypeName + "'";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get Account Type");
            try
            {
                if (r != null)
                    return int.Parse(r["SysID"].ToString());
                else
                    return -1;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }
        public string  GetAccountType(int  AccountType)
        {
            string sql1 = "Select AccountType from accounttype where SysID=" + AccountType ;
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get Account Type");
            try
            {
                if (r != null)
                    return r["AccountType"].ToString();
                else
                    return "Nill";
            }
            catch (Exception ex)
            {

                return "Nill";
            }
        }
        #region Aditional Functions
        public void LoadCurrency(ComboBox ObgCombo)
        {
            string sql1 = "select CurID,CurID from tblcurrency";
            DataTable mytb = new DataTable();
            mytb = Mycommon.GetDataTableAccount(sql1, "Load Status Combo");
            ObgCombo.Items.Clear();

            if (mytb != null)
            {
                foreach (DataRow r in mytb.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = r["CurID"].ToString();
                    item.Value = r["CurID"].ToString();
                    ObgCombo.Items.Add(item);

                }
            }
        }

        public void LoadAccountType(ComboBox ObgCombo)
        {
           
            string sql1 = "select SysID,AccountType from accounttype";
            DataTable mytb = new DataTable();
            mytb = Mycommon.GetDataTableAccount(sql1, "Load Status Combo");
            ObgCombo.Items.Clear();

            if (mytb != null)
            {
                foreach (DataRow r in mytb.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = r["AccountType"].ToString();
                    item.Value = r["SysID"].ToString();
                    ObgCombo.Items.Add(item);

                }
            }
        }
        public void LoadBankAccount(ComboBox ObgCombo)
            {
            string sql1 = "SELECT AccountNo,concat(AccountNo,' [', AccountType,'-', CurrenyType,']') as AcName "
                + " FROM tblbankaccount ";

            DataTable mytb = new DataTable();
            mytb = Mycommon.GetDataTableAccount(sql1, "Load Status Combo");
            ObgCombo.Items.Clear();

            if (mytb != null)
                {
                foreach (DataRow r in mytb.Rows)
                    {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = r["AccountNo"].ToString();
                    item.Value = r["AcName"].ToString();
                    ObgCombo.Items.Add(item);

                    }
                }
            }
        public void LoadMainAccount(ComboBox ObgCombo,int CompanyID)
        {
            string sql1 = "SELECT AccountID ,  AccountName "
                + " FROM accountname where CompanyID=" + CompanyID + " order by AccountID asc";
            DataTable mytb = new DataTable();
            mytb = Mycommon.GetDataTableAccount(sql1, "Load Status Combo");
            ObgCombo.Items.Clear();

            if (mytb != null)
            {
                foreach (DataRow r in mytb.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = r["AccountName"].ToString() +" [" +  r["AccountID"].ToString() + "]" ;
                    item.Value = r["AccountID"].ToString();
                    ObgCombo.Items.Add(item);

                }
            }
        }
        public DataTable GetAccountList(int CompanyName,int Status,int IsMainAcc)
        {
            string sql1 = "SELECT accountname.AccountID as 'Account ID', accounttype.AccountType as 'Acc Type', accountname.AccountName as 'Acc Name', accountname.Currency, accountname.BankAccountNo as 'Bank Acc No', accountname.LastVoucherID as 'Lst Voucher' "
                + " FROM accounterp.accounttype INNER JOIN accounterp.accountname ON accounttype.SysID = accountname.MainAccType "
                + " where accountname.CompanyID=" + CompanyName + " and AccountStatus=" + Status + " and IsSubAccount=" + IsMainAcc;
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Account List");
            return tb;
        }
        
       
        public DataTable GetAccountListForDDL(string WhereCon, int CompanyName, int Status,bool WithOutSub)
        {
            string sql1 = "";
            if (WithOutSub)
            {
                sql1 = "SELECT accountname.AccountID as 'Account ID', accountname.AccountName as 'Acc Name',accounttype.AccountType as 'Acc Type', accountname.Currency "
                      + " FROM accounterp.accounttype INNER JOIN accounterp.accountname ON accounttype.SysID = accountname.MainAccType "
                      + " where ( (accounttype.SysID<>2) and  (accounttype.SysID<>6)) and    accountname.CompanyID=" + CompanyName + " and AccountStatus=" + Status + " and accountname.AccountName like '" + WhereCon + "%'";
            }
            else
            {
                sql1 = "SELECT accountname.AccountID as 'Account ID', accountname.AccountName as 'Acc Name',accounttype.AccountType as 'Acc Type', accountname.Currency "
                      + " FROM accounterp.accounttype INNER JOIN accounterp.accountname ON accounttype.SysID = accountname.MainAccType "
                      + " where  accountname.CompanyID=" + CompanyName + " and AccountStatus=" + Status + " and accountname.AccountName like '" + WhereCon + "%'";

            }
                DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Account List");
            return tb;
        }
        public string GetAccountName(string AcID)
        {
           
            string sql1 = "Select accountname.AccountName  from accountname where AccountID='" + AcID + "'";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get AcName");
            if (r != null)
                return r["AccountName"].ToString();
            else
                return "";
        }
        public string GetSupplierAccountNumber(int SupplierID)
        {
            string sql1 = "select SupplierAccount from tblsupplier where sysid=" + SupplierID;
            DataRow r = Mycommon.GetDataRow(sql1, "Get Account ID");
            try
            {
                if (r != null)
                {
                    return r["SupplierAccount"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        public string GetCustomerAccountNumber(int SupplierID)
        {
            string sql1 = "select SupplierAccount from tblcustomer where sysid=" + SupplierID;
            DataRow r = Mycommon.GetDataRow(sql1, "Get Account ID");
            try
            {
                if (r != null)
                {
                    return r["SupplierAccount"].ToString();
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        public DataTable GetAccountList(int CompanyName, int Status)
        {
            string sql1 = "SELECT accountname.AccountID as 'Account ID', accounttype.AccountType as 'Acc Type', accountname.AccountName as 'Acc Name', accountname.Currency, accountname.BankAccountNo as 'Bank Acc No', accountname.LastVoucherID as 'Lst Voucher' "
                + " FROM accounterp.accounttype INNER JOIN accounterp.accountname ON accounttype.SysID = accountname.MainAccType "
                + " where accountname.CompanyID=" + CompanyName + " and AccountStatus=" + Status ;
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Account List");
            return tb;
        }
        //public void  LoadAccountList(ComboBox cmb,int CompanyName, int Status)
        //{
        //    string sql1 = "SELECT accountname.AccountID as 'Account ID', accounttype.AccountType as 'Acc Type', accountname.AccountName as 'Acc Name', accountname.Currency, accountname.BankAccountNo as 'Bank Acc No', accountname.LastVoucherID as 'Lst Voucher' "
        //        + " FROM accounterp.accounttype INNER JOIN accounterp.accountname ON accounttype.SysID = accountname.MainAccType "
        //        + " where accountname.CompanyID=" + CompanyName + " and AccountStatus=" + Status;
        //    DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Account List");
        //    if (tb != null)
        //    {
        //        foreach (DataRow r in tb.Rows)
        //        {
        //            ComboboxItem item = new ComboboxItem();
        //            item.Text = r["AccountName"].ToString() + " [" + r["AccountID"].ToString() + "]";
        //            item.Value = r["AccountID"].ToString();
        //            cmb.Items.Add(item);

        //        }
        //    }
        //}
        public DataTable GetAccountListByCat(int CompanyName, int Status,int CatID)
        {
            string sql1 = "SELECT accountname.AccountID as 'Account ID', accounttype.AccountType as 'Acc Type', accountname.AccountName as 'Acc Name', accountname.Currency, accountname.BankAccountNo as 'Bank Acc No', accountname.LastVoucherID as 'Lst Voucher' "
                + " FROM accounterp.accounttype INNER JOIN accounterp.accountname ON accounttype.SysID = accountname.MainAccType "
                + " where accountname.CompanyID=" + CompanyName + " and AccountStatus=" + Status + " and accountname.MainAccType=" + CatID + " Order By accountname.AccountID Asc";
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Account List");
            return tb;
        }
        public void  GetAccountListByCat(int CompanyName,ComboBox cmb, int CatID)
        {
            string sql1 = "";
            if (CatID != 0)
            {
                sql1 = "SELECT accountname.AccountID , accountname.AccountName "
                    + " FROM accountname "
                    + " where AccountStatus=1 and accountname.CompanyID=" + CompanyName + "  and accountname.MainAccType=" + CatID + " Order By accountname.AccountID Asc";
            }
            else
            {
                sql1 = "SELECT accountname.AccountID , accountname.AccountName "
                     + " FROM accountname "
                     + " where AccountStatus=1 and accountname.CompanyID=" + CompanyName + " Order By accountname.AccountID Asc";
            }

            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Account List");
            cmb.Items.Clear(); 
            if (tb != null)
            {
                foreach (DataRow r in tb.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text =  r["AccountName"].ToString() +" [" + r["AccountID"].ToString() + "]" ;
                    item.Value = r["AccountID"].ToString();
                    cmb.Items.Add(item);

                }
            }


                
               
        }
        public DataTable GetGeneralLedger(int AccountYear, DateTime Todate, int WithSubAccount)
        {
            string sql1 = "";
            MySqlCommand oSqlCommand = new MySqlCommand();
            if (WithSubAccount == 1)
            {
                sql1 = "SELECT tblaccounttransaction.AccountID, accountname.AccountName, (SUM(tblaccounttransaction.Dr)- SUM(tblaccounttransaction.Cr)) as Balance "
                 + " FROM  accounterp.accountname "
                 + " INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                 + " where tblaccounttransaction.AccountYear=@AccountYear"
                 + " and tblaccounttransaction.ActualDate<@Todate  GROUP BY "
                 + " tblaccounttransaction.AccountID ASC";
            }
            else
            {
                sql1 = "SELECT tblaccounttransaction.AccountID, accountname.AccountName, (SUM(tblaccounttransaction.Dr)- SUM(tblaccounttransaction.Cr)) as Balance "
                 + " FROM  accounterp.accountname "
                 + " INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                 + " where IsSubAccount=0 and tblaccounttransaction.AccountYear=@AccountYear"
                 + " and tblaccounttransaction.ActualDate<@Todate  GROUP BY "
                 + " tblaccounttransaction.AccountID ASC";
            }

            oSqlCommand.Parameters.AddWithValue("@AccountYear", AccountYear);
            oSqlCommand.Parameters.AddWithValue("@Todate", Todate);

            DataTable tb = Mycommon.GetDataTableAccount(sql1, oSqlCommand, "Get Trial Balance");
            DataTable trtable = MakeTrialBalance();
            DataTable SubAccList = GetFinalBalanceinSubAccount(false, AccountYear, Todate);

            if (tb != null)
            {
                foreach (DataRow r in tb.Rows)
                {
                    decimal amount = 0, dr = 0, Cr = 0;


                    bool resp = decimal.TryParse(r["Balance"].ToString(), out amount);
                    if (amount != 0)
                    {
                        if (amount > 0)
                            dr = amount;
                        else if (amount < 0)
                        {
                            Cr = amount * -1;
                        }

                        string[] row1 = { r["AccountID"].ToString(), r["AccountName"].ToString(), dr.ToString(), Cr.ToString() };
                        trtable.Rows.Add(row1);
                    }

                }
            }
            if (WithSubAccount == 0)
            {
                if (SubAccList != null)
                {
                    foreach (DataRow r1 in SubAccList.Rows)
                    {
                        decimal amount = 0, dr = 0, Cr = 0;
                        string Side = "";
                        decimal fbalance = 0;

                        bool rsp1 = decimal.TryParse(r1["balance"].ToString(), out fbalance);

                        if (fbalance > 0)
                        {
                            dr = fbalance;
                        }
                        else
                        {
                            Cr = fbalance * -1;
                        }

                        if ((dr + Cr) > 0)
                        {
                            string[] row2 = { r1["MainAccountID"].ToString(), GetAccountName(r1["MainAccountID"].ToString()), dr.ToString(), Cr.ToString() };
                            trtable.Rows.Add(row2);
                        }
                    }
                }
            }
            return trtable;

        }
        public DataTable GetSupplierOutstanding(string AccountID, DateTime Todate,int CompanyID)
        {
            string sql1 = "";
            string GetCur = GetCurrrencyType(AccountID);
            string GetGeneraAcSide = GetAccountGeneralSide(AccountID);

            if (GetCur == "LKR")
            {
                if (GetGeneraAcSide == "D")
                {
                    sql1 = "SELECT tblaccounttransaction.AccountID, accountname.AccountName, SUM(tblaccounttransaction.Dr - tblaccounttransaction.Cr)  Amount "
                            + " FROM accounterp.accountname INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                            + " WHERE tblaccounttransaction.CompanyID=@CompanyID and accountname.MainAccountID =@AccountID and tblaccounttransaction.ActualDate<@Todate"
                            + " GROUP BY tblaccounttransaction.AccountID Asc ";

                }
                else
                {
                    sql1 = "SELECT tblaccounttransaction.AccountID, accountname.AccountName, SUM(tblaccounttransaction.Dr - tblaccounttransaction.Cr) * -1 AS Amount "
                           + " FROM accounterp.accountname INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                           + " WHERE tblaccounttransaction.CompanyID=@CompanyID and accountname.MainAccountID =@AccountID and tblaccounttransaction.ActualDate<@Todate"
                           + " GROUP BY tblaccounttransaction.AccountID Asc ";
                }
            }
            else
            {
                if (GetGeneraAcSide == "D")
                {
                    sql1 = "SELECT tblaccounttransaction.AccountID, accountname.AccountName, SUM(tblaccounttransaction.FDr - tblaccounttransaction.FCr)  AS Amount "
                   + " FROM accounterp.accountname INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                   + " WHERE tblaccounttransaction.CompanyID=@CompanyID and accountname.MainAccountID =@AccountID and tblaccounttransaction.ActualDate<@Todate"
                   + " GROUP BY tblaccounttransaction.AccountID Asc ";

                }
                else
                {
                    sql1 = "SELECT tblaccounttransaction.AccountID, accountname.AccountName, SUM(tblaccounttransaction.FDr - tblaccounttransaction.FCr) * -1 AS Amount "
                          + " FROM accounterp.accountname INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                          + " WHERE tblaccounttransaction.CompanyID=@CompanyID and accountname.MainAccountID =@AccountID and tblaccounttransaction.ActualDate<@Todate"
                          + " GROUP BY tblaccounttransaction.AccountID Asc ";
                }
            }
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@Todate", Todate);
            oSqlCommand.Parameters.AddWithValue("@AccountID", AccountID);
            oSqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
            DataTable tb = Mycommon.GetDataTableAccount(sql1, oSqlCommand, "Get Trial Balance");
            return tb;

        }
        public string GetAccountGeneralSide(string AccountID)
        {
            string sql1 = "SELECT accounttype.GeneralAccSide"
                + " FROM accounttype INNER JOIN accounterp.accountname "
                + " ON accounttype.SysID = accountname.MainAccType "
                + " Where accountname.AccountID='" + AccountID + "'";
            DataRow tb = Mycommon.GetDataRowAccount(sql1, "Get account genera side");
            if (tb != null)
            {
                string Id = tb["GeneralAccSide"].ToString();
                if (Id.Trim() == "1")
                    return "D";
                else
                    return "C";
            }
            else
                return "C";
        }
        public DataTable GetTrialBalance(int AccountYear,DateTime Todate,int WithSubAccount)
        {
        string sql1 = "";
        MySqlCommand oSqlCommand = new MySqlCommand();
        if (WithSubAccount == 1)
            {
            sql1 = "SELECT tblaccounttransaction.AccountID, accountname.AccountName, (SUM(tblaccounttransaction.Dr)- SUM(tblaccounttransaction.Cr)) as Balance "
             + " FROM  accounterp.accountname "
             + " INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
             + " where tblaccounttransaction.AccountYear=@AccountYear"
             + " and tblaccounttransaction.ActualDate<@Todate  GROUP BY "
             + " tblaccounttransaction.AccountID ASC";
            }
        else
            {
                sql1 = "SELECT tblaccounttransaction.AccountID, accountname.AccountName, (SUM(tblaccounttransaction.Dr)- SUM(tblaccounttransaction.Cr)) as Balance "
                 + " FROM  accounterp.accountname "
                 + " INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                 + " where tblaccounttransaction.AccountYear=@AccountYear and ((accountname.MainAccType<>2) and (accountname.MainAccType<>6))"
                 + " and tblaccounttransaction.ActualDate<@Todate  GROUP BY "
                 + " tblaccounttransaction.AccountID ASC";
            }
           
            oSqlCommand.Parameters.AddWithValue("@AccountYear", AccountYear);
            oSqlCommand.Parameters.AddWithValue("@Todate", Todate);

            DataTable tb = Mycommon.GetDataTableAccount(sql1, oSqlCommand, "Get Trial Balance");
            DataTable trtable = MakeTrialBalance();
            DataTable SubAccList = GetFinalBalanceinSubAccount(false, AccountYear, Todate);
           
            if (tb != null)
            {
                foreach (DataRow r in tb.Rows)
                {
                    decimal amount = 0, dr = 0, Cr = 0;


                    bool resp = decimal.TryParse(r["Balance"].ToString(), out amount);
                    if (amount != 0)
                    {
                        if (amount > 0)
                            dr = amount;
                        else if (amount < 0)
                        {
                            Cr = amount * -1;
                        }

                        string[] row1 = { r["AccountID"].ToString(), r["AccountName"].ToString(), dr.ToString(), Cr.ToString() };
                        trtable.Rows.Add(row1);
                    }

                }
            }
         
            if (WithSubAccount == 0)
                {
                if (SubAccList != null)
                    {
                    foreach (DataRow r1 in SubAccList.Rows)
                        {
                        decimal amount = 0, dr = 0, Cr = 0;
                        string Side = "";
                        decimal fbalance = 0;

                        bool rsp1 = decimal.TryParse(r1["balance"].ToString(), out fbalance);

                        if (fbalance > 0)
                            {
                            dr = fbalance;
                            }
                        else
                            {
                            Cr = fbalance * -1;
                            }

                        if ((dr + Cr) > 0)
                            {
                            string[] row2 = { r1["MainAccountID"].ToString(), GetAccountName(r1["MainAccountID"].ToString()), dr.ToString(), Cr.ToString() };
                            trtable.Rows.Add(row2);
                            }
                        }
                    }
                }
            return trtable;

        }
        private DataTable GetMainAccountList()
        {
        string sql1 = "select distinct (MainAccountID) as MainAcID from accountname where IsSubAccount=1";
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "get Main Aclist");
            return tb;
        }
        
        private DataTable MakeTrialBalance()
        {
            DataTable table = new DataTable();
            table.Columns.Add("AccountID", typeof(string));
            table.Columns.Add("AccountName", typeof(string));
            table.Columns.Add("FinalBalanceDr", typeof(decimal));
            table.Columns.Add("FinalBalanceCr", typeof(decimal));
            return table;
        }
       
        public DataTable GetEachLedger(string AccountID, int AcocuntYear,bool IsForrien)
        {
            string sql1 = "";
            if (IsForrien)
                sql1 = "SELECT TrID, Description, MainRef, RelRef, VoucherID, ActualDate, FDr, FCr "
                + " FROM  tblaccounttransaction where AccountID=@AccountID and AccountYear=@AcocuntYear";

            else 
              sql1 ="SELECT TrID, Description, MainRef, RelRef, VoucherID, ActualDate, Dr, Cr "
                + " FROM  tblaccounttransaction where AccountID=@AccountID and AccountYear=@AcocuntYear";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@AccountID", AccountID);
            oSqlCommand.Parameters.AddWithValue("@AcocuntYear", AcocuntYear);
            DataTable tb = Mycommon.GetDataTableAccount(sql1, oSqlCommand, "Get Total Balance");
            return tb;

        }
        private DataTable MakeGeneralLedger()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TrID", typeof(string));
            table.Columns.Add("AccountID", typeof(string));
            table.Columns.Add("AccountName", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("ActualDate", typeof(string));
             table.Columns.Add("VoucherID", typeof(string));
            table.Columns.Add("FinalBalanceDr", typeof(decimal));
            table.Columns.Add("FinalBalanceCr", typeof(decimal));
            return table;
        }
        public DataTable GetGeneralLedger(int AcocuntYear, bool IsForrien, bool WithSub, DateTime Todate)
        {
            string sql1 = "";
            MySqlCommand oSqlCommand = new MySqlCommand();
            if (WithSub)
            {
                if (IsForrien)
                    sql1 = "SELECT tblaccounttransaction.TrID, tblaccounttransaction.AccountID, accountname.AccountName, tblaccounttransaction.Description, tblaccounttransaction.ActualDate, tblaccounttransaction.VoucherID, tblaccounttransaction.FDr, tblaccounttransaction.FCr "
                    + " FROM accountname INNER JOIN tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID where AccountYear=@AcocuntYear order by tblaccounttransaction.TrID Asc";

                else
                    sql1 = "SELECT tblaccounttransaction.TrID, tblaccounttransaction.AccountID, accountname.AccountName, tblaccounttransaction.Description, tblaccounttransaction.ActualDate, tblaccounttransaction.VoucherID, tblaccounttransaction.Dr, tblaccounttransaction.Cr "
                    + " FROM accountname INNER JOIN tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID where AccountYear=@AcocuntYear order by tblaccounttransaction.TrID Asc";


            }
            else
            {
                if (IsForrien)
                    sql1 = "SELECT tblaccounttransaction.TrID, tblaccounttransaction.AccountID, accountname.AccountName, tblaccounttransaction.Description, tblaccounttransaction.ActualDate, tblaccounttransaction.VoucherID, tblaccounttransaction.FDr, tblaccounttransaction.FCr "
                    + " FROM accountname INNER JOIN tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID where IsSubAccount=0 and AccountYear=@AcocuntYear order by tblaccounttransaction.TrID Asc";

                else
                    sql1 = "SELECT tblaccounttransaction.TrID, tblaccounttransaction.AccountID, accountname.AccountName, tblaccounttransaction.Description, tblaccounttransaction.ActualDate, tblaccounttransaction.VoucherID, tblaccounttransaction.Dr, tblaccounttransaction.Cr "
                    + " FROM accountname INNER JOIN tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID where IsSubAccount=0 and AccountYear=@AcocuntYear order by tblaccounttransaction.TrID Asc";
            }
           
           
            oSqlCommand.Parameters.AddWithValue("@AcocuntYear", AcocuntYear);
            DataTable tb = Mycommon.GetDataTableAccount(sql1, oSqlCommand, "Get Total Balance");
            DataTable SubAccList = GetFinalBalanceinSubAccount(IsForrien, AcocuntYear, Todate);

            if (WithSub)
                return tb;
            else
            {
                DataTable readytable = MakeGeneralLedger();
                foreach (DataRow  item in tb.Rows)
                {
                    string[] row1 = {item[0].ToString (),item[1].ToString (),item[2].ToString (),item[3].ToString (),DateTime.Parse (  item[4].ToString ()).ToString ("dd/MMM/yyyy"),item[5].ToString (),item[6].ToString (),item[7].ToString () };
                    readytable.Rows.Add(row1);
                }
                if (SubAccList != null)
                {
                    int i=0;
                    foreach (DataRow item1 in SubAccList.Rows)
                    {
                        i +=1;
                        string acname = GetAccountName(item1[0].ToString());
                        decimal dr = 0, cr = 0,amount=0;
                        bool resp = decimal.TryParse(item1[1].ToString(), out amount);
                        if (amount > 0)
                            dr = amount;
                        else if (amount < 0)
                            cr = amount * -1;

                        string[] row2 = { i.ToString("00000#"), item1[0].ToString(), acname, "", "", "", dr.ToString("##0.00"), cr.ToString("##0.00") };
                        readytable.Rows.Add(row2);
                    }

                }
                return readytable;
            }

        }
        public string GetAccountCurrenyType(string AccountNumber)
        {
            string sql1 = "SELECT Currency FROM accountname where  AccountID=@AccountID";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@AccountID", AccountNumber);
            DataRow r = Mycommon.GetDataRowAccount(sql1, oSqlCommand, "Get Currency Type");
            if (r != null)
                return r["Currency"].ToString();
            else
                return "";
            
        }

        public DataTable GetEachLedger(string AccountID, int AcocuntYear, bool IsForrien,DateTime Fromdate,DateTime ToDate)
        {
            string sql1 = "";
            if (IsForrien)
                sql1 = "SELECT tblaccounttransaction.TrID, tblaccounttransaction.Description, tblaccounttransaction.MainRef, tblaccounttransaction.RelRef, tblaccounttransaction.VoucherID, tblaccounttransaction.ActualDate, tblaccounttransaction.FDr, tblaccounttransaction.FCr "
                + " FROM  accounterp.tblaccounttransaction where tblaccounttransaction.AccountID=@AccountID and AccountYear=@AccountYear and ActualDate between @Fromdate and @ToDate";

            else
                sql1 = "SELECT tblaccounttransaction.TrID, tblaccounttransaction.Description, tblaccounttransaction.MainRef, tblaccounttransaction.RelRef, tblaccounttransaction.VoucherID, tblaccounttransaction.ActualDate, tblaccounttransaction.Dr, tblaccounttransaction.Cr "
                  + " FROM  accounterp.tblaccounttransaction where tblaccounttransaction.AccountID=@AccountID and AccountYear=@AccountYear";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@AccountID", AccountID);
            oSqlCommand.Parameters.AddWithValue("@AcocuntYear", AcocuntYear);
            oSqlCommand.Parameters.AddWithValue("@Fromdate", Fromdate);
            oSqlCommand.Parameters.AddWithValue("@ToDate", ToDate);
            DataTable tb = Mycommon.GetDataTableAccount(sql1, oSqlCommand, "Get Total Balance");
            return tb;

        }

        public string GetFinalBalance(string AcID,  bool  IsForienCur,int AcYear,out decimal Balance,  out string Side)
        {
            Balance = 0;
            Side = "";
            string sql1 = "";

            if (IsForienCur)
                sql1 = "select (sum(FDr)-sum(FCr)) as Balance from tblaccounttransaction where AccountID=@AcID and AccountYear=@AcYear";
            else 
                sql1 ="select (sum(Dr)-sum(Cr)) as Balance from tblaccounttransaction where AccountID=@AcID and AccountYear=@AcYear";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@AcID", AcID);
            oSqlCommand.Parameters.AddWithValue("@AcYear", AcYear);
            DataRow r = Mycommon.GetDataRowAccount(sql1, oSqlCommand, "Get Total Balance");
            if (r != null)
            {
                bool resp = decimal.TryParse(r["Balance"].ToString(), out Balance);
                if (Balance > 0)
                    Side = "D";
                else if (Balance < 0)
                    Side = "C";
                else
                    Side = "Z"; //Zero
                return "True";
            }
            else
                return "No Data";

          }
        public string GetCurrrencyType(string AcNumber)
        {
            string sql1 = "Select Currency From accountname where AccountID='" + AcNumber + "'";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get CurrencyType");
            if (r != null)
                return r["Currency"].ToString();
            else
                return "XXX";
        }
        public string GetFinalBalance(string AcID,  int AcYear, out decimal Balance)
        {
            bool IsForienCur=false ;
            string curtype = GetCurrrencyType(AcID);
            if (curtype == "LKR")
                IsForienCur = false;
            else
                IsForienCur = true;
            string Side;
            Balance = 0;
            Side = "";
            string sql1 = "";

            if (IsForienCur)
                sql1 = "select (sum(FDr)-sum(FCr)) as Balance from tblaccounttransaction where AccountID=@AcID and AccountYear=@AcYear";
            else
                sql1 = "select (sum(Dr)-sum(Cr)) as Balance from tblaccounttransaction where AccountID=@AcID and AccountYear=@AcYear";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@AcID", AcID);
            oSqlCommand.Parameters.AddWithValue("@AcYear", AcYear);
            DataRow r = Mycommon.GetDataRowAccount(sql1, oSqlCommand, "Get Total Balance");
            if (r != null)
            {
                bool resp = decimal.TryParse(r["Balance"].ToString(), out Balance);
                if (Balance > 0)
                    Side = "D";
                else if (Balance < 0)
                    Side = "C";
                else
                    Side = "Z"; //Zero
                return "True";
            }
            else
                return "No Data";

        }
        public string GetFinalBalanceinSubAccount(string MainAcID, bool IsForienCur, int AcYear, out decimal Balance, out string Side)
        {
            Balance = 0;
            Side = "";
            string sql1 = "";
             if (IsForienCur)
                 sql1 = " SELECT (sum(tblaccounttransaction.FDr)-sum(tblaccounttransaction.FCr)) as Balance "
                     + " FROM accountname INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                     + " where accountname.MainAccountID=@AcID and tblaccounttransaction.AccountYear=@AcYear";
             else
                 sql1 = " SELECT (sum(tblaccounttransaction.Dr)-sum(tblaccounttransaction.Cr)) as Balance "
                     + " FROM accountname INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                     + " where accountname.MainAccountID=@MainAcID and tblaccounttransaction.AccountYear=@AcYear";
             MySqlCommand oSqlCommand = new MySqlCommand();
             oSqlCommand.Parameters.AddWithValue("@MainAcID", MainAcID);
             oSqlCommand.Parameters.AddWithValue("@AcYear", AcYear);
            DataRow r = Mycommon.GetDataRowAccount(sql1, oSqlCommand, "Get Total Balance");
            if (r != null)
            {
                bool resp = decimal.TryParse(r["Balance"].ToString(), out Balance);
                if (Balance > 0)
                    Side = "D";
                else if (Balance < 0)
                    Side = "C";
                else
                    Side = "Z"; //Zero
                return "True";
            }
            else
                return "No Data";

        }
        public DataTable GetFinalBalanceinSubAccount(bool IsForienCur, int AcYear,DateTime Date1)
            {
            string sql1 = "";
            if (IsForienCur)
                {
                sql1 = "SELECT accountname.MainAccountID,(sum(tblaccounttransaction.FDr)-sum( tblaccounttransaction.FCr)) as balance "
                   + " FROM accounterp.accountname "
                   + "  INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                   + "  where tblaccounttransaction.ActualDate<'" + Date1.ToString("yyyy/mm/dd") + "' and accountname.IsSubAccount=1 and ((accountname.MainAccType=6) or (accountname.MainAccType=2)) and tblaccounttransaction.AccountYear=" + AcYear
                   + "  group by   accountname.MainAccountID ";

                }
            else
                {
                sql1 = "SELECT accountname.MainAccountID,(sum(tblaccounttransaction.Dr)-sum( tblaccounttransaction.Cr)) as balance "
                    + " FROM accounterp.accountname "
                    + "  INNER JOIN accounterp.tblaccounttransaction ON accountname.AccountID = tblaccounttransaction.AccountID "
                    + "  where tblaccounttransaction.ActualDate<'" + Date1.ToString("yyyy/mm/dd") + "' and accountname.IsSubAccount=1 and  ((accountname.MainAccType=6) or (accountname.MainAccType=2))  and tblaccounttransaction.AccountYear=" + AcYear
                    + "  group by   accountname.MainAccountID ";
                }
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Sub account Amount");
            return tb;

            }
        public DataTable GetSubAccountList(int CompanyName,int Status)
        {
            string sql1 = "SELECT accountname.MainAccountID as 'Main Acc ID', accountname.AccountID as 'Account ID', accounttype.AccountType as 'Acc Type', accountname.AccountName as 'Acc Name', accountname.Currency, accountname.BankAccountNo as 'Bank Acc No', accountname.LastVoucherID as 'Lst Voucher' "
                + " FROM accounterp.accounttype INNER JOIN accounterp.accountname ON accounttype.SysID = accountname.MainAccType "
                + " where accountname.CompanyID=" + CompanyName + " and accountname.AccountStatus=" + Status + " and accountname.IsSubAccount=1";
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Account List");
            return tb;
        }
        public DataTable GetSubAccountListDDL(int CompanyName, int Status)
        {
            string sql1 = "SELECT accountname.MainAccountID as 'Main Acc ID', accountname.AccountID as 'Account ID', accountname.AccountName as 'Acc Name', accounttype.AccountType as 'Acc Type', accountname.Currency "
                + " FROM accounterp.accounttype INNER JOIN accounterp.accountname ON accounttype.SysID = accountname.MainAccType "
                + " where accountname.CompanyID=" + CompanyName + " and accountname.AccountStatus=" + Status + " and accountname.IsSubAccount=1";
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Account List");
            return tb;
        }
        public void LoadSupplier(ComboBox cmb)
        {
            string sql1 = "SELECT tblsupplier.Sysid, tblsupplier.Customer "
                + " FROM accounterp.accountname "
                + " INNER JOIN tsfs.tblsupplier ON accountname.AccountID =tblsupplier.SupplierAccount";
            MyGeneral.LoadStatusCombo(cmb, sql1);  

        }
        public void LoadSupplier(DataGridView  dgv)
            {
                string sql1 = "Select SysID,Customer from tblsupplier where Status=1";
                MyGeneral.LoadDatatoTableWithoutBind(dgv, sql1,"Load Suppplier");
            }
        public void LoadCustomer(DataGridView dgv)
            {
                string sql1 = "Select SysID,Customer from tblcustomer where Status=1";
                MyGeneral.LoadDatatoTableWithoutBind(dgv, sql1, "Load Suppplier");
            }
        public void LoadCustomer(ComboBox cmb)
        {
            string sql1 = "SELECT tblcustomer.Sysid, tsfs.tblcustomer.Customer "
                + "FROM accounterp.accountname  INNER JOIN tsfs.tblcustomer ON accountname.AccountID =tblcustomer.SupplierAccount";
                MyGeneral.LoadStatusCombo(cmb, sql1);
        }
        public void LoadSubContractors(ComboBox cmb)
        {
            string sql1 = "Select SysID,Customer from tblsubcontractors where Status=1";
            MyGeneral.LoadStatusCombo(cmb, sql1);
        }
        public void LoadEmployee(ComboBox cmb)
        {
            string sql1 = "Select EmpNo,concat('(',SalaryName,') ', InitialWithName) as EmpName from tblemployee where IsDelete=0";
            MyGeneral.LoadStatusCombo(cmb, sql1);
        }
        public void LoadOther(ComboBox cmb)
        {
            string sql1 = "Select SysID, PayeeName from otherpayee";
            MyGeneral.LoadStatusComboAccount(cmb, sql1);
        }

        public void SaveotherPayee(string NameofPayee, string AddOfPayee)
        {
            string sql1 = "Insert into otherpayee (PayeeName,Address) values ('" + NameofPayee + "','" + AddOfPayee + "')";
            string respond = "";
            if (!ExistOtherPayyee(NameofPayee))
            respond=Mycommon.ExicuteAnyCommandAccount(sql1, "SAve Other Payyee");
        }
        public string GetOtherPayeeAdd(string PayeeName)
        {
            string sql1 = "Select Address from otherpayee where PayeeName='" + PayeeName + "'";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "get addres of Payee");
            if (r != null)
                return r["Address"].ToString();
            else
                return "";
        }
        private bool ExistOtherPayyee(string Payeename)
        {
            string sql1 = "Select SysID from otherpayee where PayeeName='" + Payeename + "'";
            return Mycommon.ExistInTableAccount(sql1, "Exit Oyher Payee");
        }
        #endregion
    }

    public class AccountType
    {
        public struct AccountCreationDataType
        {
            public int SysID;
            public string AccountID;
            public string AccountName;
            public int MainAccType;
            public int IsSubAccount;
            public string MainAccountID;
            public int AccountStatus;
            public string Currency;
            public string CurrencySymble;
            public string BankAccountNo;
            public int LastVoucherID;
            public string LastTransRef;
            public string CreatedUser;
            public DateTime lastaccdate;
            public int CompanyID;
        }
    }
}
