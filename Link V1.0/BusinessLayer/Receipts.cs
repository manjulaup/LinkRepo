//Create by SATICIN On 04/Jan/2016
//
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using BusinessLayer.CommonOperation;
namespace BusinessLayer.Receipts
{
    public class Receipt
    {
        private CommonOperations Mycommon = null;
        public Receipt(bool IsLocal) //Constructor
        {
            Mycommon = new CommonOperations(IsLocal);
        }
        ~Receipt() //Deconstructor
        {
            Mycommon.CloseDB();
        }
        public string Save(REceiptTypes.ReceiptDataType _SaveData,out string RCVTmp)
        {
            RCVTmp = GetNewTMPPayID(_SaveData.CompanyID, _SaveData.AccPeriod);
            MySql.Data.MySqlClient.MySqlTransaction Mytrans;
            MySqlConnection CurCon = new MySqlConnection();
            CurCon = Mycommon.AccountConnection;
            if (CurCon.State == ConnectionState.Closed)
                CurCon.Open();
            string respond = "";
            Mytrans = Mycommon.AccountConnection.BeginTransaction();
            MySqlCommand oSqlCommand = new MySqlCommand();

            if (!ExistReceipt(RCVTmp, CurCon))
                {
                respond = SaveReceipt(_SaveData, RCVTmp, CurCon);
                if (respond != "True")
                    {
                    Mytrans.Rollback();
                    Mycommon.AccountConnection.Close();
                    CurCon.Dispose();
                    return respond;
                    }
                else
                    {
                    respond = SaveDetails(_SaveData.ReceiptList, RCVTmp, CurCon);
                    if (respond != "True")
                        {
                            Mytrans.Rollback();
                            Mycommon.AccountConnection.Close();
                            CurCon.Dispose();
                            return respond;
                        }
                        else
                        {
                            Mytrans.Commit();
                            Mycommon.AccountConnection.Close();
                            CurCon.Dispose();
                            return "True";
                        }
                    }
                }
            else
                return "Number Already Exist, Use Update Button";       
       
        }
        public string Update(REceiptTypes.ReceiptDataType _SaveData)
            {
            
            MySql.Data.MySqlClient.MySqlTransaction Mytrans;
            MySqlConnection CurCon = new MySqlConnection();
            CurCon = Mycommon.AccountConnection;
            if (CurCon.State == ConnectionState.Closed)
                CurCon.Open();
            string respond = "";
            Mytrans = Mycommon.AccountConnection.BeginTransaction();
            MySqlCommand oSqlCommand = new MySqlCommand();

            if (ExistReceipt(_SaveData.ReceiptID , CurCon))
                {
                respond = UpdateReceipt(_SaveData, CurCon);
                if (respond != "True")
                    {
                    Mytrans.Rollback();
                    Mycommon.AccountConnection.Close();
                    CurCon.Dispose();
                    return respond;
                    }
                else
                    {
                    respond = UpdateDetails(_SaveData.ReceiptList,  CurCon);
                    if (respond != "True")
                        {
                        Mytrans.Rollback();
                        Mycommon.AccountConnection.Close();
                        CurCon.Dispose();
                        return respond;
                        }
                    else
                        {
                        Mytrans.Commit();
                        Mycommon.AccountConnection.Close();
                        CurCon.Dispose();
                        return "True";
                        }
                    }
                }
            else
                return "Number Already Exist, Use Update Button";

            }
        public string SaveReceipt(REceiptTypes.ReceiptDataType _SaveData)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblreceipt ("
          + "ReceiptID,"
          + "ReceiptMethod,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Dr,"
          + "FDr,"
          + "ReceiptStatus,"
          + "TrRef,"
          + "TrUser,"
          + "TrDate,"
          + "TrTime,"
          + "ChequeNumber,"
          + "AccPeriod,"
          + "CompanyID,"
          + "RcptFromCatID,"
          + "RcptFromName,"
          + "RcptActualDate)"
           + " Values ("
           + "@ReceiptID,"
           + "@ReceiptMethod,"
           + "@Description,"
           + "@AccountID,"
           + "@CurRate,"
           + "@Dr,"
           + "@FDr,"
           + "@ReceiptStatus,"
           + "@TrRef,"
           + "@TrUser,"
           + "CurDate(),"
           + "CurTime(),"
           + "@ChequeNumber,"
           + "@AccPeriod,"
           + "@CompanyID,"
           + "@RcptFromCatID,"
           + "@RcptFromName,"
           + "@RcptActualDate)";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ReceiptID", _SaveData.ReceiptID);
                oSqlCommand.Parameters.AddWithValue("@ReceiptMethod", _SaveData.ReceiptMethod);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _SaveData.FDr);
                oSqlCommand.Parameters.AddWithValue("@ReceiptStatus", _SaveData.ReceiptStatus);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _SaveData.TrRef);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _SaveData.TrUser);
                oSqlCommand.Parameters.AddWithValue("@ChequeNumber", _SaveData.ChequeNumber);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _SaveData.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@RcptFromCatID", _SaveData.RcptFromCatID);
                oSqlCommand.Parameters.AddWithValue("@RcptFromName", _SaveData.RcptFromName);
                oSqlCommand.Parameters.AddWithValue("@RcptActualDate", _SaveData.RcptActualDate);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Save Receipt");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string SaveReceipt(REceiptTypes.ReceiptDataType _SaveData,string RcvNumber, MySqlConnection _ActCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblreceipt ("
          + "ReceiptID,"
          + "ReceiptMethod,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Dr,"
          + "FDr,"
          + "ReceiptStatus,"
          + "TrRef,"
          + "TrUser,"
          + "TrDate,"
          + "TrTime,"
          + "ChequeNumber,"
          + "AccPeriod,"
          + "CompanyID,"
          + "RcptFromCatID,"
          + "RcptFromName,"
          + "RcptActualDate)"
           + " Values ("
           + "@ReceiptID,"
           + "@ReceiptMethod,"
           + "@Description,"
           + "@AccountID,"
           + "@CurRate,"
           + "@Dr,"
           + "@FDr,"
           + "@ReceiptStatus,"
           + "@TrRef,"
           + "@TrUser,"
           + "CurDate(),"
           + "CurTime(),"
           + "@ChequeNumber,"
           + "@AccPeriod,"
           + "@CompanyID,"
           + "@RcptFromCatID,"
           + "@RcptFromName,"
           + "@RcptActualDate)";
            try
            {
            oSqlCommand.Parameters.AddWithValue("@ReceiptID", RcvNumber);
                oSqlCommand.Parameters.AddWithValue("@ReceiptMethod", _SaveData.ReceiptMethod);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _SaveData.FDr);
                oSqlCommand.Parameters.AddWithValue("@ReceiptStatus", _SaveData.ReceiptStatus);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _SaveData.TrRef);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _SaveData.TrUser);
                oSqlCommand.Parameters.AddWithValue("@ChequeNumber", _SaveData.ChequeNumber);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _SaveData.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@RcptFromCatID", _SaveData.RcptFromCatID);
                oSqlCommand.Parameters.AddWithValue("@RcptFromName", _SaveData.RcptFromName);
                oSqlCommand.Parameters.AddWithValue("@RcptActualDate", _SaveData.RcptActualDate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActCon, "Save Receipt");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateReceipt(REceiptTypes.ReceiptDataType _Update)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblreceipt Set "
                + "ReceiptID=@ReceiptID,"
                + "ReceiptMethod=@ReceiptMethod,"
                + "Description=@Description,"
                + "AccountID=@AccountID,"
                + "CurRate=@CurRate,"
                + "Dr=@Dr,"
                + "FDr=@FDr,"
                + "ReceiptStatus=@ReceiptStatus,"
                + "TrRef=@TrRef,"
                + "TrUser=@TrUser,"
                + "TrDate=CurDate(),"
                + "TrTime=CurTime(),"
                + "ChequeNumber=@ChequeNumber,"
                + "AccPeriod=@AccPeriod,"
                + "CompanyID=@CompanyID,"
                + "RcptFromCatID=@RcptFromCatID,"
                + "RcptFromName=@RcptFromName,"
                + "RcptActualDate=@RcptActualDate"
                + " Where 1=1 "
                + " and ReceiptID=@ReceiptID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ReceiptID", _Update.ReceiptID);
                oSqlCommand.Parameters.AddWithValue("@ReceiptMethod", _Update.ReceiptMethod);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _Update.FDr);
                oSqlCommand.Parameters.AddWithValue("@ReceiptStatus", _Update.ReceiptStatus);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _Update.TrRef);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _Update.TrUser);
                oSqlCommand.Parameters.AddWithValue("@ChequeNumber", _Update.ChequeNumber);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _Update.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@RcptFromCatID", _Update.RcptFromCatID);
                oSqlCommand.Parameters.AddWithValue("@RcptFromName", _Update.RcptFromName);
                oSqlCommand.Parameters.AddWithValue("@RcptActualDate", _Update.RcptActualDate);
               
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Update Receipt");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateReceipt(REceiptTypes.ReceiptDataType _Update, MySqlConnection _ActCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblreceipt Set "
                + "ReceiptID=@ReceiptID,"
                + "ReceiptMethod=@ReceiptMethod,"
                + "Description=@Description,"
                + "AccountID=@AccountID,"
                + "CurRate=@CurRate,"
                + "Dr=@Dr,"
                + "FDr=@FDr,"
                + "ReceiptStatus=@ReceiptStatus,"
                + "TrRef=@TrRef,"
                + "TrUser=@TrUser,"
                + "TrDate=CurDate(),"
                + "TrTime=CurTime(),"
                + "ChequeNumber=@ChequeNumber,"
                + "AccPeriod=@AccPeriod,"
                + "CompanyID=@CompanyID,"
                + "RcptFromCatID=@RcptFromCatID,"
                + "RcptFromName=@RcptFromName,"
                + "RcptActualDate=@RcptActualDate"
                + " Where 1=1 "
                + " and ReceiptID=@ReceiptID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ReceiptID", _Update.ReceiptID);
                oSqlCommand.Parameters.AddWithValue("@ReceiptMethod", _Update.ReceiptMethod);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _Update.FDr);
                oSqlCommand.Parameters.AddWithValue("@ReceiptStatus", _Update.ReceiptStatus);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _Update.TrRef);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _Update.TrUser);
                oSqlCommand.Parameters.AddWithValue("@ChequeNumber", _Update.ChequeNumber);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _Update.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@RcptFromCatID", _Update.RcptFromCatID);
                oSqlCommand.Parameters.AddWithValue("@RcptFromName", _Update.RcptFromName);
                oSqlCommand.Parameters.AddWithValue("@RcptActualDate", _Update.RcptActualDate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActCon, "Update Receipt");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteReceipt(string ReceiptID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblreceipt"
                + " Where 1=1 "
                + " and ReceiptID=@ReceiptID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ReceiptID", ReceiptID);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Delete Receipt");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool ExistReceipt(string ReceiptID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select ReceiptID from tblreceipt"
                + " Where 1=1 "
                + " and ReceiptID=@ReceiptID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ReceiptID", ReceiptID);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist Receipts");
                return respond;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ExistReceipt(string ReceiptID,MySqlConnection _ActCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select ReceiptID from tblreceipt"
                + " Where 1=1 "
                + " and ReceiptID=@ReceiptID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ReceiptID", ReceiptID);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand,_ActCon, "Exist Receipts");
                return respond;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable GetSeachTable(int status)
        {
        string sql1 = "";
        if (status != -1)
            {
            sql1 = "SELECT tblreceipt.ReceiptID, tblreceipt.Description, tblreceipt.AccountID,"
                + " accountname.AccountName, tblreceipt.FDr, tblreceipt.CurRate,"
                + " tblreceipt.RcptFromName,tblreceipt.ChequeNumber, tblreceipt.RcptActualDate "
                + " FROM accountname INNER JOIN accounterp.tblreceipt ON accountname.AccountID = tblreceipt.AccountID "
                + " where PayStatus=" + status;
            }
        else
            {
            sql1 = "SELECT tblreceipt.ReceiptID, tblreceipt.Description, tblreceipt.AccountID,"
                + " accountname.AccountName, tblreceipt.FDr, tblreceipt.CurRate,"
                + " tblreceipt.RcptFromName,tblreceipt.ChequeNumber, tblreceipt.RcptActualDate "
                + " FROM accountname INNER JOIN accounterp.tblreceipt ON accountname.AccountID = tblreceipt.AccountID ";

            }
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get List ");
            return tb;
            }
        public DataTable GetReceiptDetailsList(string Racpt)
            {
                string sql1 = "SELECT AccID,(Select AccountName from accountname where AccountID=AccID) as Acname, Description, JobNo, Vat, Cr,FCr,Exrate, ItemNo FROM   tblreceiptdetails "
                + "  where RcptNo='" + Racpt + "'";
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Payment Detail List");
            return tb;
            }
        private string SetPendingInvoiceStatus(int Customer, string InvoiceNumber)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string respond = "";
            try
                {
                    string sql1 = "Update tblpendingrecievebleinvoice set BillStatus=1 where (RevievedAmount=FDr) and  InvoiceNo=@InvoiceNo and Customer=@Customer ";
                oSqlCommand.Parameters.AddWithValue("@Customer", Customer);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNumber);
                respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand,  "Update RevQty");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public  string UpdateReceivedAmount(int Customer, string InvoiceNumber, decimal ReceivedAmount)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string respond = "";
            string sql1 = "Update tblpendingrecievebleinvoice "
            + " set RevievedAmount=RevievedAmount + @RevievedAmount where InvoiceNo=@InvoiceNo and Customer=@Customer";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@RevievedAmount", ReceivedAmount);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNumber);
                oSqlCommand.Parameters.AddWithValue("@Customer", Customer);
                respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand,  "Update RevQty");
                respond = SetPendingInvoiceStatus(Customer, InvoiceNumber);
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }

            }
        public int GetReceiptStatus(string REceiptID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sql1 = "select ReceiptStatus from tblreceipt where ReceiptID=@ReceiptID";
            oSqlCommand.Parameters.AddWithValue("@ReceiptID", REceiptID);
            DataRow r = Mycommon.GetDataRowAccount(sql1, oSqlCommand, "get Status");
            try
                {
                if (r != null)
                    {
                        int i = 0;
                        bool resp = int.TryParse(r["ReceiptStatus"].ToString(), out i);
                        return i;
                    }
                else
                    return 0;
                }
            catch (Exception ex)
                {
                return  0;
                }
        }
        public string GetExistReceipt(string ReceiptID, out REceiptTypes.ReceiptDataType _ExistData)
        {
            _ExistData = new REceiptTypes.ReceiptDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "ReceiptID,"
          + "ReceiptMethod,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Dr,"
          + "FDr,"
          + "ReceiptStatus,"
          + "TrRef,"
          + "TrUser,"
          + "TrDate,"
          + "TrTime,"
          + "ChequeNumber,"
          + "AccPeriod,"
          + "CompanyID,"
          + "RcptFromCatID,"
          + "RcptFromName,"
          + "RcptActualDate,"
          + "ApproveBy,"
          + "ApproveDate,"
          + "ApproveTime,"
          + "AcountedBy,"
          + "AccountDate,"
          + "AccountedTime"
          + " from tblreceipt"
          + " Where 1=1 "
                + " and ReceiptID=@ReceiptID";
            oSqlCommand.Parameters.AddWithValue("@ReceiptID", ReceiptID);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand,  "Get Exist data Receipt");
            if (r != null)
            {
                try
                {
                    bool resp = false;
                    _ExistData.ReceiptID = r["ReceiptID"].ToString();
                    int inReceiptMethod = 0;
                    resp = int.TryParse(r["ReceiptMethod"].ToString(), out inReceiptMethod);
                    _ExistData.ReceiptMethod = inReceiptMethod;
                    _ExistData.Description = r["Description"].ToString();
                    _ExistData.AccountID = r["AccountID"].ToString();
                    decimal deCurRate = 0;
                    resp = decimal.TryParse(r["CurRate"].ToString(), out deCurRate);
                    _ExistData.CurRate = deCurRate;
                    decimal deDr = 0;
                    resp = decimal.TryParse(r["Dr"].ToString(), out deDr);
                    _ExistData.Dr = deDr;
                    decimal deFDr = 0;
                    resp = decimal.TryParse(r["FDr"].ToString(), out deFDr);
                    _ExistData.FDr = deFDr;
                    int inReceiptStatus = 0;
                    resp = int.TryParse(r["ReceiptStatus"].ToString(), out inReceiptStatus);
                    _ExistData.ReceiptStatus = inReceiptStatus;
                    _ExistData.TrRef = r["TrRef"].ToString();
                    _ExistData.TrUser = r["TrUser"].ToString();
                    DateTime dtTrDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["TrDate"].ToString(), out dtTrDate);
                    if (resp)
                        _ExistData.TrDate = dtTrDate;
                    else
                        _ExistData.TrDate = new DateTime(1900, 1, 1);
                    _ExistData.TrTime = r["TrTime"].ToString();
                    _ExistData.ChequeNumber = r["ChequeNumber"].ToString();
                    int inAccPeriod = 0;
                    resp = int.TryParse(r["AccPeriod"].ToString(), out inAccPeriod);
                    _ExistData.AccPeriod = inAccPeriod;
                    int inCompanyID = 0;
                    resp = int.TryParse(r["CompanyID"].ToString(), out inCompanyID);
                    _ExistData.CompanyID = inCompanyID;
                    int inRcptFromCatID = 0;
                    resp = int.TryParse(r["RcptFromCatID"].ToString(), out inRcptFromCatID);
                    _ExistData.RcptFromCatID = inRcptFromCatID;
                    _ExistData.RcptFromName = r["RcptFromName"].ToString();
                    DateTime dtRcptActualDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["RcptActualDate"].ToString(), out dtRcptActualDate);
                    if (resp)
                        _ExistData.RcptActualDate = dtRcptActualDate;
                    else
                        _ExistData.RcptActualDate = new DateTime(1900, 1, 1);
                    _ExistData.ApproveBy = r["ApproveBy"].ToString();
                    DateTime dtApproveDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["ApproveDate"].ToString(), out dtApproveDate);
                    if (resp)
                        _ExistData.ApproveDate = dtApproveDate;
                    else
                        _ExistData.ApproveDate = new DateTime(1900, 1, 1);
                    _ExistData.ApproveTime = r["ApproveTime"].ToString();
                    _ExistData.AcountedBy = r["AcountedBy"].ToString();
                    DateTime dtAccountDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["AccountDate"].ToString(), out dtAccountDate);
                    if (resp)
                        _ExistData.AccountDate = dtAccountDate;
                    else
                        _ExistData.AccountDate = new DateTime(1900, 1, 1);
                    _ExistData.AccountedTime = r["AccountedTime"].ToString();
                 List <REceiptTypes.ReceiptDetailsDataType> _ExtDetails=new List<REceiptTypes.ReceiptDetailsDataType>();
                    string rsp = GetReceiptList(ReceiptID, out _ExtDetails);
                    _ExistData.ReceiptList = _ExtDetails;
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

        public string SaveReceiptDetails(REceiptTypes.ReceiptDetailsDataType _SaveData)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblreceiptdetails ("
          + "ItemNo,"
          + "RcptNo,"
          + "AccID,"
          + "Description,"
          + "Cr,"
          + "FCr,"
          + "RcptTrRef,"
          + "Vat,"
          + "JobNo,Exrate)"
           + " Values ("
           + "@ItemNo,"
           + "@RcptNo,"
           + "@AccID,"
           + "@Description,"
           + "@Cr,"
           + "@FCr,"
           + "@RcptTrRef,"
           + "@Vat,"
           + "@JobNo,@Exrate)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _SaveData.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@RcptNo", _SaveData.RcptNo);
                oSqlCommand.Parameters.AddWithValue("@AccID", _SaveData.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@Cr", _SaveData.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _SaveData.FCr);
                oSqlCommand.Parameters.AddWithValue("@RcptTrRef", _SaveData.RcptTrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _SaveData.Vat);
                oSqlCommand.Parameters.AddWithValue("@JobNo", _SaveData.JobNo);
                oSqlCommand.Parameters.AddWithValue("@Exrate", _SaveData.Exrate);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Save ReceiptDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string SaveReceiptDetails(REceiptTypes.ReceiptDetailsDataType _SaveData, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblreceiptdetails ("
          + "ItemNo,"
          + "RcptNo,"
          + "AccID,"
          + "Description,"
          + "Cr,"
          + "FCr,"
          + "RcptTrRef,"
          + "Vat,"
          + "JobNo,Exrate)"
           + " Values ("
           + "@ItemNo,"
           + "@RcptNo,"
           + "@AccID,"
           + "@Description,"
           + "@Cr,"
           + "@FCr,"
           + "@RcptTrRef,"
           + "@Vat,"
           + "@JobNo,@Exrate)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _SaveData.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@RcptNo", _SaveData.RcptNo);
                oSqlCommand.Parameters.AddWithValue("@AccID", _SaveData.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@Cr", _SaveData.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _SaveData.FCr);
                oSqlCommand.Parameters.AddWithValue("@RcptTrRef", _SaveData.RcptTrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _SaveData.Vat);
                oSqlCommand.Parameters.AddWithValue("@JobNo", _SaveData.JobNo);
                oSqlCommand.Parameters.AddWithValue("@Exrate", _SaveData.Exrate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActCon, "Save ReceiptDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdateReceiptDetails(REceiptTypes.ReceiptDetailsDataType _Update)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblreceiptdetails Set "
                + "ItemNo=@ItemNo,"
                + "RcptNo=@RcptNo,"
                + "AccID=@AccID,"
                + "Description=@Description,"
                + "Cr=@Cr,"
                + "FCr=@FCr,"
                + "RcptTrRef=@RcptTrRef,"
                + "Vat=@Vat,"
                + "JobNo=@JobNo,Exrate=@Exrate"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and RcptNo=@RcptNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _Update.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@RcptNo", _Update.RcptNo);
                oSqlCommand.Parameters.AddWithValue("@AccID", _Update.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@Cr", _Update.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _Update.FCr);
                oSqlCommand.Parameters.AddWithValue("@RcptTrRef", _Update.RcptTrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _Update.Vat);
                oSqlCommand.Parameters.AddWithValue("@JobNo", _Update.JobNo);
                oSqlCommand.Parameters.AddWithValue("@Exrate", _Update.Exrate);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Update ReceiptDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdateReceiptDetails(REceiptTypes.ReceiptDetailsDataType _Update, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblreceiptdetails Set "
                + "ItemNo=@ItemNo,"
                + "RcptNo=@RcptNo,"
                + "AccID=@AccID,"
                + "Description=@Description,"
                + "Cr=@Cr,"
                + "FCr=@FCr,"
                + "RcptTrRef=@RcptTrRef,"
                + "Vat=@Vat,"
                + "JobNo=@JobNo,Exrate=@Exrate"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and RcptNo=@RcptNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _Update.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@RcptNo", _Update.RcptNo);
                oSqlCommand.Parameters.AddWithValue("@AccID", _Update.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@Cr", _Update.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _Update.FCr);
                oSqlCommand.Parameters.AddWithValue("@RcptTrRef", _Update.RcptTrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _Update.Vat);
                oSqlCommand.Parameters.AddWithValue("@JobNo", _Update.JobNo);
                oSqlCommand.Parameters.AddWithValue("@Exrate", _Update.Exrate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon, "Update ReceiptDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeleteReceiptDetails(int ItemNo, string RcptNo)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblreceiptdetails"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and RcptNo=@RcptNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
                oSqlCommand.Parameters.AddWithValue("@RcptNo", RcptNo);
                string respond = Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand, null, "Delete ReceiptDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistReceiptDetails(int ItemNo, string RcptNo)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select RcptNo  from tblreceiptdetails"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and RcptNo=@RcptNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
                oSqlCommand.Parameters.AddWithValue("@RcptNo", RcptNo);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist Details");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public bool ExistReceiptDetails(int ItemNo, string RcptNo,MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select RcptNo  from tblreceiptdetails"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and RcptNo=@RcptNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
                oSqlCommand.Parameters.AddWithValue("@RcptNo", RcptNo);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand,_ActCon, "Exist Details");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public string GetReceiptList(string RcptNumber, out List<REceiptTypes.ReceiptDetailsDataType> _RcptDetailList)
        {
            _RcptDetailList = new List<REceiptTypes.ReceiptDetailsDataType>();
            string sql1 = "Select ItemNo,RcptNo from tblreceiptdetails Where RcptNo=@RcptNo";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@RcptNo", RcptNumber);
            DataTable tb = Mycommon.GetDataTableAccount(sql1, oSqlCommand, "Get Exist data ReceiptDetails");
            string Respond = "";
            if (tb != null)
            {
            foreach (DataRow r in tb.Rows)
                {
                    REceiptTypes.ReceiptDetailsDataType _OneItem = new REceiptTypes.ReceiptDetailsDataType();
                int i=int.Parse(r["ItemNo"].ToString());
                    Respond = GetExistReceiptDetails(i, RcptNumber, out _OneItem);
                   if (Respond =="True")
                       _RcptDetailList.Add(_OneItem);

                }
            }
            return Respond;
        }
        public string GetExistReceiptDetails(int ItemNo, string RcptNo, out REceiptTypes.ReceiptDetailsDataType _ExistData)
            {
            _ExistData = new REceiptTypes.ReceiptDetailsDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "ItemNo,"
          + "RcptNo,"
          + "AccID,"
          + "Description,"
          + "Cr,"
          + "FCr,"
          + "RcptTrRef,"
          + "Vat,"
          + "JobNo,Exrate"
          + " from tblreceiptdetails"
          + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and RcptNo=@RcptNo";
            oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
            oSqlCommand.Parameters.AddWithValue("@RcptNo", RcptNo);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand,  "Get Exist data ReceiptDetails");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    int inItemNo = 0;
                    resp = int.TryParse(r["ItemNo"].ToString(), out inItemNo);
                    _ExistData.ItemNo = inItemNo;
                    _ExistData.RcptNo = r["RcptNo"].ToString();
                    _ExistData.AccID = r["AccID"].ToString();
                    _ExistData.Description = r["Description"].ToString();
                    decimal deCr = 0;
                    resp = decimal.TryParse(r["Cr"].ToString(), out deCr);
                    _ExistData.Cr = deCr;
                    decimal deFCr = 0;
                    resp = decimal.TryParse(r["FCr"].ToString(), out deFCr);
                    _ExistData.FCr = deFCr;
                    _ExistData.RcptTrRef = r["RcptTrRef"].ToString();
                    decimal deVat = 0;
                    resp = decimal.TryParse(r["Vat"].ToString(), out deVat);
                    _ExistData.Vat = deVat;
                    _ExistData.JobNo = r["JobNo"].ToString();
                    decimal deExrate = 0;
                    resp = decimal.TryParse(r["Exrate"].ToString(), out deExrate);
                    _ExistData.Exrate = deExrate;
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
        public string SendToApproval(string PVN, string User1)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
                {
                string sql1 = "Update tblreceipt set ReceiptStatus=1,TrUser=@TrUser,TrDate=curDate(),TrTime=curtime() where ReceiptID=@PaymentID";
                oSqlCommand.Parameters.AddWithValue("@TrUser", User1);
                oSqlCommand.Parameters.AddWithValue("@PaymentID", PVN);
                string respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand, "Send To Approval");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }

            }
        public string SetReceiptVoucherAsApproved(string PVN, string User1)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
                {
                string sql1 = "Update tblreceipt set ReceiptStatus=2,ApproveBy=@TrUser,ApproveDate=curDate(),ApproveTime=curtime() where ReceiptID=@PaymentID";
                oSqlCommand.Parameters.AddWithValue("@TrUser", User1);
                oSqlCommand.Parameters.AddWithValue("@PaymentID", PVN);
                string respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand, "Exist bill details");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }

            }

        public string SaveDetails(List<REceiptTypes.ReceiptDetailsDataType> ReceiptList,string RcptNo, MySqlConnection _ActCon)
        {
            string respond="";
            try
                {
                foreach (REceiptTypes.ReceiptDetailsDataType OneItem in ReceiptList)
                    {
                    REceiptTypes.ReceiptDetailsDataType _OneItem = new REceiptTypes.ReceiptDetailsDataType();
                    _OneItem = OneItem;
                    _OneItem.RcptNo = RcptNo;
                    if (!ExistReceiptDetails(OneItem.ItemNo, RcptNo, _ActCon))
                    { 
                        respond = SaveReceiptDetails(_OneItem, _ActCon);
                    }
                    if (respond != "True")
                        return respond;
                    }
                return respond;
                }
            catch (Exception ex)
                {

                return ex.Message;
                }

        }
        public string UpdateDetails(List<REceiptTypes.ReceiptDetailsDataType> ReceiptList,  MySqlConnection _ActCon)
            {
            string respond = "";
            try
                {
                foreach (REceiptTypes.ReceiptDetailsDataType OneItem in ReceiptList)
                    {


                    if (ExistReceiptDetails(OneItem.ItemNo, OneItem.RcptNo, _ActCon))
                        {
                        respond = UpdateReceiptDetails(OneItem, _ActCon);
                        }
                    else
                        {
                        respond = SaveReceiptDetails(OneItem, _ActCon);
                        }
                    if (respond != "True")
                        return respond;
                    }
                return respond;
                }
            catch (Exception ex)
                {

                return ex.Message;
                }

            }

        public string GetNewTMPPayID(int CompanyID, int Cur_AccYear)
            {
            string PVNid = "";
            string sql1 = "select max( substr(ReceiptID,5)) as MaxN from tblreceipt where CompanyID=" + CompanyID + " and  ReceiptStatus<>3 ";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get New PaymentID");
            if (r != null)
                {
                string Str = r["MaxN"].ToString();
                int FNumber = 0;
                bool resp = int.TryParse(Str, out FNumber);
                FNumber = FNumber + 1;
                PVNid = "TRV-" + FNumber.ToString("0######");

                }
            else
                {
                PVNid = "TRV-" + "0000001";
                }
            return PVNid;

            }
        public DataTable  LoadPendingReceivebleList( int CustomerID)
        {
            string sql1 = "SELECT '0',InvoiceNo, Description, FDr, '', AccountID, CurRate, TobeRcvDate,InvoiceNoDate, RevievedAmount "
            + "FROM accounterp.tblpendingrecievebleinvoice "
            + " where BillStatus=0 and Customer=" + CustomerID;

        DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Pending Receiveble List");
        return tb;
        }
        }
    public class REceiptTypes
    {
        public struct ReceiptDataType
        {
            public string ReceiptID;
            public int ReceiptMethod;
            public string Description;
            public string AccountID;
            public decimal CurRate;
            public decimal Dr;
            public decimal FDr;
            public int ReceiptStatus;
            public string TrRef;
            public string TrUser;
            public DateTime TrDate;
            public string TrTime;
            public string ChequeNumber;
            public int AccPeriod;
            public int CompanyID;
            public int RcptFromCatID;
            public string RcptFromName;
            public DateTime RcptActualDate;
            public string ApproveBy;
            public DateTime ApproveDate;
            public string ApproveTime;
            public string AcountedBy;
            public DateTime AccountDate;
            public string AccountedTime;
            public List<ReceiptDetailsDataType> ReceiptList;
        }
        public struct ReceiptDetailsDataType
            {
            public int ItemNo;
            public string RcptNo;
            public string AccID;
            public string Description;
            public decimal Cr;
            public decimal FCr;
            public string RcptTrRef;
            public decimal Vat;
            public string JobNo;
            public decimal Exrate;
            }
    }
}
