//Create by SATICIN On 11/Dec/2015
//
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using BusinessLayer.CommonOperation;
using DataLayer.DataService;
using System.Windows.Forms;
using System.Drawing;
namespace BusinessLayer.Billings
    {
    public class Billing
        {
        private DataService Mycommon = null;
        private CommonOperations MyGeneral =null;
        public Billing(bool IsLocal)
        {
        Mycommon = new DataService(IsLocal);
        MyGeneral = new CommonOperations(IsLocal);
         }
         ~Billing()
        {
            Mycommon.CloseDB();
        }
        public BillingDataTypes.PendingPaymentDataType TransformBillToPending(BillingDataTypes.BillingDataType _DataIn)
        {
            BillingDataTypes.PendingPaymentDataType _TransForm = new BillingDataTypes.PendingPaymentDataType();

            _TransForm.AccountID = _DataIn.AccountID;
            _TransForm.AccPeriod = _DataIn.AccPeriod;
            _TransForm.BillNo = _DataIn.BillNo;
            _TransForm.BillStatus  = 0;
            _TransForm.CompanyID  = _DataIn.CompanyID ;
            _TransForm.Cr  = _DataIn.Cr ;
            _TransForm.CurRate = _DataIn.CurRate;
            _TransForm.Description  = _DataIn.Description;
            _TransForm.FCr  = _DataIn.FCr;
            _TransForm.PayedAmount =0;
            _TransForm.Supplier  = _DataIn.PayToID;
            _TransForm.TobePayDate  = _DataIn.TobePayDate;
            _TransForm.BillDate = _DataIn.TrDate;
            return _TransForm;
        }
        
        public string Save(BillingDataTypes.BillingDataType _SaveData)
        {
            MySql.Data.MySqlClient.MySqlTransaction Mytrans;
            MySqlConnection CurCon = new MySqlConnection();
            CurCon = Mycommon.AccountConnection;
            string respond = "";

            if (CurCon.State == ConnectionState.Closed)
                CurCon.Open();
           
            Mytrans = Mycommon.AccountConnection.BeginTransaction();
            MySqlCommand oSqlCommand = new MySqlCommand();

            respond = SaveBilling(_SaveData, CurCon);
            if (respond != "True")
            {
                Mytrans.Rollback();
                return respond;
            }
            else
            {

              
                    foreach (BillingDataTypes.BillingDetailsDataType _OneItem in _SaveData.BillingDetails)
                        {
                        if (!ExistBillingDetails(_OneItem.ItemNo, _OneItem.BillNo, CurCon))
                            {
                            respond = SaveBillingDetailsinTrance(_OneItem, CurCon);
                            if (respond != "True")
                                {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return respond;
                                }
                            }
                        else
                            {
                            respond = UpdateBillingDetails(_OneItem, CurCon);
                            if (respond != "True")
                                {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return respond;
                                }
                            }

                        }

                        if (respond != "True")
                            {
                            Mytrans.Rollback();
                            CurCon.Close();
                            return respond;
                            }
                        else
                            {
                            Mytrans.Commit();
                            CurCon.Close();
                            return "True";
                            }
                   
            }

        }
        public string Update(BillingDataTypes.BillingDataType _SaveData)
        {
                MySql.Data.MySqlClient.MySqlTransaction Mytrans;
                MySqlConnection CurCon = new MySqlConnection();
                CurCon = Mycommon.AccountConnection;
                string respond = "";

                if (CurCon.State == ConnectionState.Closed)
                    CurCon.Open();

                Mytrans = Mycommon.AccountConnection.BeginTransaction();
                MySqlCommand oSqlCommand = new MySqlCommand();

                respond = UpdateBilling(_SaveData, CurCon);
                if (respond != "True")
                    {
                    Mytrans.Rollback();
                    return respond;
                    }
                else
                    {

                    BillingDataTypes.PendingPaymentDataType _BillLog = new BillingDataTypes.PendingPaymentDataType();
                    _BillLog = TransformBillToPending(_SaveData);
                    if (!ExistPendingPaymentInTrans(_SaveData.CompanyID, _SaveData.BillNo, CurCon))
                        respond = SavePendingPaymentInTrans(_BillLog, CurCon);
                    else
                        respond = UpdatePendingPayment(_BillLog, CurCon);

                    if (respond != "True")
                        {
                        Mytrans.Rollback();
                        CurCon.Close();
                        return respond;
                        }
                    else
                        {
                        foreach (BillingDataTypes.BillingDetailsDataType _OneItem in _SaveData.BillingDetails)
                            {
                            if (!ExistBillingDetails(_OneItem.ItemNo, _OneItem.BillNo, CurCon))
                                {
                                respond = SaveBillingDetailsinTrance(_OneItem, CurCon);
                                if (respond != "True")
                                    {
                                    Mytrans.Rollback();
                                    CurCon.Close();
                                    return respond;
                                    }
                                }
                            else
                                {
                                respond = UpdateBillingDetails(_OneItem, CurCon);
                                if (respond != "True")
                                    {
                                    Mytrans.Rollback();
                                    CurCon.Close();
                                    return respond;
                                    }
                                }

                            }
                        if (respond != "True")
                            {
                            Mytrans.Rollback();
                            CurCon.Close();
                            return respond;
                            }
                        else
                            {
                            Mytrans.Commit();
                            CurCon.Close();
                            return "True";
                            }
                        }
                    }

        }
        public string SaveBilling(BillingDataTypes.BillingDataType _SaveData,MySqlConnection _CurCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblpayablebill ("
          + "BillNo,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Cr,"
          + "FCr,"
          + "BillStatus,"
          + "TrUser,"
          + "TrDate,"
          + "TrTime,"
          + "AccPeriod,"
          + "CompanyID,"
          + "PayToCatID,"
          + "PayToID,"
          + "TobePayDate,BillDate)"
           + " Values ("
           + "@BillNo,"
           + "@Description,"
           + "@AccountID,"
           + "@CurRate,"
           + "@Cr,"
           + "@FCr,"
           + "@BillStatus,"
           + "@TrUser,"
           + "curdate(),"
           + "curTime(),"
           + "@AccPeriod,"
           + "@CompanyID,"
           + "@PayToCatID,"
           + "@PayToID,"
           + "@TobePayDate,@BillDate)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@BillNo", _SaveData.BillNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Cr", _SaveData.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _SaveData.FCr);
                oSqlCommand.Parameters.AddWithValue("@BillStatus", _SaveData.BillStatus);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _SaveData.TrUser);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _SaveData.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@PayToCatID", _SaveData.PayToCatID);
                oSqlCommand.Parameters.AddWithValue("@PayToID", _SaveData.PayToID);
                oSqlCommand.Parameters.AddWithValue("@TobePayDate", _SaveData.TobePayDate);
                oSqlCommand.Parameters.AddWithValue("@BillDate", _SaveData.BillDate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_CurCon, "Save Billing");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }

                
            }
        public DataTable GetBillList(int Status_Bill,int SupplierID)
        {
            string sql1 = "";
            if (Status_Bill != -1)
            {
                sql1 = "SELECT BillNo, Description,BillDate, Cr, TobePayDate,CurRate,FCr,AccountID,PayToID  FROM tblpayablebill  where PayToID=" + SupplierID + " and  BillStatus=" + Status_Bill;
            }
            else
            {
                sql1 = "SELECT BillNo, Description,BillDate, Cr, TobePayDate,CurRate,FCr,AccountID,PayToID  FROM tblpayablebill where PayToID=" + SupplierID;

            }
                DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Pay Bill  List");
            return tb;
 
        }
        public DataTable GetBillList(string BillNumber)
        {
            string sql1 = "SELECT tblpayablebilldetails.AccID, accountname.AccountName, tblpayablebilldetails.Description, tblpayablebilldetails.Dr, tblpayablebilldetails.FDr, tblpayablebilldetails.Vat, tblpayablebilldetails.ItemNo "
                + " FROM accounterp.accountname "
                + " INNER JOIN accounterp.tblpayablebilldetails ON accountname.AccountID = tblpayablebilldetails.AccID "
                + " where  tblpayablebilldetails.BillNo='" + BillNumber + "'";
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get  Bill Details List");
            return tb;
        }
        public string UpdateBilling(BillingDataTypes.BillingDataType _Update, MySqlConnection _CurCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblpayablebill Set "
                + "BillNo=@BillNo,"
                + "Description=@Description,"
                + "AccountID=@AccountID,"
                + "CurRate=@CurRate,"
                + "Cr=@Cr,"
                + "FCr=@FCr,"
                + "BillStatus=@BillStatus,"
                + "TrUser=@TrUser,"
                + "TrDate=curdate(),"
                + "TrTime=curtime(),"
                + "AccPeriod=@AccPeriod,"
                + "CompanyID=@CompanyID,"
                + "PayToCatID=@PayToCatID,"
                + "PayToID=@PayToID,"
                + "TobePayDate=@TobePayDate,BillDate=@BillDate"
                + " Where 1=1 "
                + " and BillNo=@BillNo and PayToID=@PayToID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@BillNo", _Update.BillNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Cr", _Update.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _Update.FCr);
                oSqlCommand.Parameters.AddWithValue("@BillStatus", _Update.BillStatus);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _Update.TrUser);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _Update.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@PayToCatID", _Update.PayToCatID);
                oSqlCommand.Parameters.AddWithValue("@PayToID", _Update.PayToID);
                oSqlCommand.Parameters.AddWithValue("@TobePayDate", _Update.TobePayDate);
                oSqlCommand.Parameters.AddWithValue("@BillDate", _Update.BillDate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_CurCon, "Update Billing");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeleteBilling(string BillNo, int SupID)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblpayablebill"
                + " Where 1=1 "
                + " and BillNo=@BillNo and PayToID=@PayToID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
                oSqlCommand.Parameters.AddWithValue("@PayToID", SupID);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Delete Billing");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistBilling(string BillNo,int SupID)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select BillNo from tblpayablebill"
                + " Where 1=1 "
                + " and BillNo=@BillNo and PayToID=@PayToID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
                oSqlCommand.Parameters.AddWithValue("@PayToID", SupID);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist in the system");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public string GetPartCategoey(string GetPN)
        {
            string sql1 = "SELECT tblcategory.CatName FROM tblcategory "
                + " INNER JOIN tblcomponentmaster ON tblcategory.SysID = tblcomponentmaster.CategoryID "
                + " where tblcomponentmaster.SLpartNo='" + GetPN + "'";
            DataRow r = Mycommon.GetDataRow(sql1, "Get Product category");
            return r["CatName"].ToString();
        }
        public DataTable GetGRNData(string GRNNumber,int SupID)
        {
        string sql1 = "SELECT '1', (tblrowmaterilgrnreceivedetails.RCVQty *tblrowmaterilgrnreceivedetails.CostUnitPrice) as LineTotal,   tblrowmaterilgrnreceivedetails.S3PartNo,(Select concat(tblcomponentmaster.ComponentName, '  [',tblcomponentmaster.UMO,']') from tsfs.tblcomponentmaster where tblcomponentmaster.SLpartNo=tblrowmaterilgrnreceivedetails.S3PartNo ) as ComName,"
            + ""
            + " (Select AccountID from tblcomponentmaster where tblcomponentmaster.SLpartNo=tblrowmaterilgrnreceivedetails.S3PartNo  ) as AccountID, tblrowmaterilgrnreceivedetails.SysID "
            + " FROM tblrowmaterilgrnreceive "
            + " INNER JOIN tsfs.tblrowmaterilgrnreceivedetails ON tblrowmaterilgrnreceive.GRNnumber = tblrowmaterilgrnreceivedetails.GRNNumber "
            + " where tblrowmaterilgrnreceive.GRNnumber='" + GRNNumber + "' and tblrowmaterilgrnreceive.GRNSupplier=" + SupID;
        DataTable tb = Mycommon.GetDataTable(sql1, "Get GRN data");
        return tb;
            }
        public decimal GetTotalOutstanding(int SupID)
        {
            string sql1 = "SELECT sum(tblpendingpayablebill.FCr-tblpendingpayablebill.PayedAmount) as total "
                + " FROM accounterp.tblpendingpayablebill "
                + " where tblpendingpayablebill.Supplier=" + SupID;
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get total bill outtstanding");
            if (r != null)
            {
                decimal d = 0;
                bool resp = decimal.TryParse(r["total"].ToString(), out d);
                return d;
            }
            else
                return 0;
        }
        public void LoadGRNNumbers(ComboBox cmb, int  supplierID)
        {
        string sql1 = "Select GRNnumber from tblrowmaterilgrnreceive where Billed=0 and GRNSupplier=" + supplierID;
        MyGeneral.LoadDatatoComboWithOutBind(cmb, sql1, "GRNnumber", true);

            }

        public string GetExistBilling(string BillNo,int SupID, out BillingDataTypes.BillingDataType _ExistData)
            {
            _ExistData = new BillingDataTypes.BillingDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "BillNo,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Cr,"
          + "FCr,"
          + "BillStatus,"
          + "TrRef,"
          + "TrUser,"
          + "TrDate,"
          + "TrTime,"
          + "AccPeriod,"
          + "CompanyID,"
          + "PayToCatID,"
          + "PayToID,"
          + "PayAmount,"
          + "TobePayDate,BillDate"
          + " from tblpayablebill"
          + " Where 1=1 "
                + " and BillNo=@BillNo and PayToID=@PayToID";
            oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
            oSqlCommand.Parameters.AddWithValue("@PayToID", SupID);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand,  "Get Exist data Billing");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    _ExistData.BillNo = r["BillNo"].ToString();
                    _ExistData.Description = r["Description"].ToString();
                    _ExistData.AccountID = r["AccountID"].ToString();
                    decimal deCurRate = 0;
                    resp = decimal.TryParse(r["CurRate"].ToString(), out deCurRate);
                    _ExistData.CurRate = deCurRate;
                    decimal deCr = 0;
                    resp = decimal.TryParse(r["Cr"].ToString(), out deCr);
                    _ExistData.Cr = deCr;
                    decimal deFCr = 0;
                    resp = decimal.TryParse(r["FCr"].ToString(), out deFCr);
                    _ExistData.FCr = deFCr;
                    int inBillStatus = 0;
                    resp = int.TryParse(r["BillStatus"].ToString(), out inBillStatus);
                    _ExistData.BillStatus = inBillStatus;
                    _ExistData.TrRef = r["TrRef"].ToString();
                    _ExistData.TrUser = r["TrUser"].ToString();
                    DateTime dtTrDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["TrDate"].ToString(), out dtTrDate);
                    if (resp)
                        _ExistData.TrDate = dtTrDate;
                    else
                        _ExistData.TrDate = new DateTime(1900, 1, 1);
                    _ExistData.TrTime = r["TrTime"].ToString();
                    int inAccPeriod = 0;
                    resp = int.TryParse(r["AccPeriod"].ToString(), out inAccPeriod);
                    _ExistData.AccPeriod = inAccPeriod;
                    int inCompanyID = 0;
                    resp = int.TryParse(r["CompanyID"].ToString(), out inCompanyID);
                    _ExistData.CompanyID = inCompanyID;
                    int inPayToCatID = 0;
                    resp = int.TryParse(r["PayToCatID"].ToString(), out inPayToCatID);
                    _ExistData.PayToCatID = inPayToCatID;
                    int inPayToID = 0;
                    resp = int.TryParse(r["PayToID"].ToString(), out inPayToID);
                    _ExistData.PayToID = inPayToID;
                    decimal dePayAmount = 0;
                    resp = decimal.TryParse(r["PayAmount"].ToString(), out dePayAmount);
                    _ExistData.PayAmount = dePayAmount;
                    DateTime dtTobePayDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["TobePayDate"].ToString(), out dtTobePayDate);
                    if (resp)
                        _ExistData.TobePayDate = dtTobePayDate;
                    else
                        _ExistData.TobePayDate = new DateTime(1900, 1, 1);

                  
                    DateTime dtBillDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["BillDate"].ToString(), out dtBillDate);
                    if (resp)
                        _ExistData.BillDate = dtBillDate;
                    else
                        _ExistData.BillDate = new DateTime(1900, 1, 1);

                    List<BillingDataTypes.BillingDetailsDataType> _ExtList;
                    string respond = GetBillingDetaisList(BillNo, out _ExtList);
                    _ExistData.BillingDetails = _ExtList;

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

        public string SendToapproveval(string BillNo, string User, int SupID)
        {
            string sql1 = "update tblpayablebill set TrUser=@User,TrDate=curdate(),TrTime=curtime(),BillStatus=1 where BillNo=@BillNo and PayToID=@PayToID";
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
            {
                oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
                oSqlCommand.Parameters.AddWithValue("@User", User);
                oSqlCommand.Parameters.AddWithValue("@PayToID", SupID);
                string respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand, "Send to Approval");
                return respond;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string BillApproved(string BillNo,string User,int SupID)
        {
            string sql1 = "update tblpayablebill set ApproveBy=@User,ApproveDate=curdate(),ApproveTime=curtime(),BillStatus=2 where BillNo=@BillNo and PayToID=@PayToID";
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
            {
                oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
                oSqlCommand.Parameters.AddWithValue("@User", User);
                oSqlCommand.Parameters.AddWithValue("@PayToID", SupID);
                string respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand, "Update BillingDetails");
                return respond;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }


        }
        public string BillAccount(string BillNo, string User, int SupID)
        {
            string sql1 = "update tblpayablebill set AcountedBy=@User,AccountDate=curdate(),AccountedTime=curtime(),BillStatus=3 where BillNo=@BillNo and PayToID=@PayToID";
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
            {
                oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
                oSqlCommand.Parameters.AddWithValue("@User", User);
                oSqlCommand.Parameters.AddWithValue("@PayToID", SupID);
                string respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand, "Update Bill Account");
                return respond;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }


        }
        public int GetBillStatus(string BillNo,  int SupID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();

            string sql1 = "Select BillStatus from tblpayablebill where BillNo=@BillNo and PayToID=@PayToID";
            oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
            oSqlCommand.Parameters.AddWithValue("@PayToID", SupID);
            DataRow dr = Mycommon.GetDataRowAccount(sql1, oSqlCommand, "Get Bill Status");
            int status = 0;
            if (dr != null)
            {
                bool resp = int.TryParse(dr["BillStatus"].ToString(), out status);
                return status;
            }
            else
                return status;
        }
        public string SaveBillingDetails(BillingDataTypes.BillingDetailsDataType _SaveData)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblpayablebilldetails ("
          + "ItemNo,"
          + "BillNo,"
          + "AccID,"
          + "Description,"
          + "Dr,"
          + "Fdr,"
          + "TrRef,"
          + "Vat)"
           + " Values ("
           + "@ItemNo,"
           + "@BillNo,"
           + "@AccID,"
           + "@Description,"
           + "@Dr,"
           + "@Fdr,"
           + "@TrRef,"
           + "@Vat)";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _SaveData.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@BillNo", _SaveData.BillNo);
                oSqlCommand.Parameters.AddWithValue("@AccID", _SaveData.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@Fdr", _SaveData.Fdr);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _SaveData.TrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _SaveData.Vat);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,"Save BillingDetails");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string SaveBillingDetailsinTrance(BillingDataTypes.BillingDetailsDataType _SaveData,MySqlConnection _ActcCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblpayablebilldetails ("
          + "ItemNo,"
          + "BillNo,"
          + "AccID,"
          + "Description,"
          + "Dr,"
          + "Fdr,"
          + "TrRef,"
          + "Vat)"
           + " Values ("
           + "@ItemNo,"
           + "@BillNo,"
           + "@AccID,"
           + "@Description,"
           + "@Dr,"
           + "@Fdr,"
           + "@TrRef,"
           + "@Vat)";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _SaveData.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@BillNo", _SaveData.BillNo);
                oSqlCommand.Parameters.AddWithValue("@AccID", _SaveData.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@Fdr", _SaveData.Fdr);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _SaveData.TrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _SaveData.Vat);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActcCon, "Save BillingDetails");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateBillingDetails(BillingDataTypes.BillingDetailsDataType _Update, MySqlConnection _ActcCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblpayablebilldetails Set "
                + "ItemNo=@ItemNo,"
                + "BillNo=@BillNo,"
                + "AccID=@AccID,"
                + "Description=@Description,"
                + "Dr=@Dr,"
                + "Fdr=@Fdr,"
                + "TrRef=@TrRef,"
                + "Vat=@Vat"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and BillNo=@BillNo";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _Update.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@BillNo", _Update.BillNo);
                oSqlCommand.Parameters.AddWithValue("@AccID", _Update.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@Fdr", _Update.Fdr);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _Update.TrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _Update.Vat);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActcCon , "Update BillingDetails");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteBillingDetails(int ItemNo, string BillNo)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblpayablebilldetails"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and BillNo=@BillNo";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
                oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Delete BillingDetails");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool ExistBillingDetails(int ItemNo, string BillNo,MySqlConnection _ActcCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select ItemNo from tblpayablebilldetails"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and BillNo=@BillNo";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
                oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand,_ActcCon,"Exist bill details");
                return respond;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetBillingDetaisList(string Invoice,out List <BillingDataTypes.BillingDetailsDataType> _ExtList)
        {
            _ExtList = new List<BillingDataTypes.BillingDetailsDataType>();
            string sql1 = "Select ItemNo,BillNo from tblpayablebilldetails where BillNo='" + Invoice + "'";
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "get bill details list");
            foreach (DataRow  item in tb.Rows )
            {
                BillingDataTypes.BillingDetailsDataType _OneItem = new BillingDataTypes.BillingDetailsDataType();
                string respond = GetExistBillingDetails(int.Parse( item["ItemNo"].ToString()), Invoice, out _OneItem);
                if (respond == "True")
                    _ExtList.Add(_OneItem);
                else
                    return respond;
            }
            return "True";
        
        }

        public string GetExistBillingDetails(int ItemNo, string BillNo, out BillingDataTypes.BillingDetailsDataType _ExistData)
        {
            _ExistData = new BillingDataTypes.BillingDetailsDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "ItemNo,"
          + "BillNo,"
          + "AccID,"
          + "Description,"
          + "Dr,"
          + "Fdr,"
          + "TrRef,"
          + "Vat"
          + " from tblpayablebilldetails"
          + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and BillNo=@BillNo";
            oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
            oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand,  "Get Exist data BillingDetails");
            if (r != null)
            {
                try
                {
                    bool resp = false;
                    int inItemNo = 0;
                    resp = int.TryParse(r["ItemNo"].ToString(), out inItemNo);
                    _ExistData.ItemNo = inItemNo;
                    _ExistData.BillNo = r["BillNo"].ToString();
                    _ExistData.AccID = r["AccID"].ToString();
                    _ExistData.Description = r["Description"].ToString();
                    decimal deDr = 0;
                    resp = decimal.TryParse(r["Dr"].ToString(), out deDr);
                    _ExistData.Dr = deDr;
                    decimal deFdr = 0;
                    resp = decimal.TryParse(r["Fdr"].ToString(), out deFdr);
                    _ExistData.Fdr = deFdr;
                    _ExistData.TrRef = r["TrRef"].ToString();
                    decimal deVat = 0;
                    resp = decimal.TryParse(r["Vat"].ToString(), out deVat);
                    _ExistData.Vat = deVat;
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

#region Pending Payment
        public string SavePendingPayment(BillingDataTypes.PendingPaymentDataType _SaveData)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblpendingpayablebill ("
          + "CompanyID,"
          + "BillNo,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Cr,"
          + "FCr,"
          + "BillStatus,"
          + "AccPeriod,"
          + "Supplier,"
          + "TotalAmount,"
          + "PayedAmount,"
          + "TobePayDate)"
           + " Values ("
           + "@CompanyID,"
           + "@BillNo,"
           + "@Description,"
           + "@AccountID,"
           + "@CurRate,"
           + "@Cr,"
           + "@FCr,"
           + "@BillStatus,"
           + "@AccPeriod,"
           + "@Supplier,"
           + "@TotalAmount,"
           + "@PayedAmount,"
           + "@TobePayDate)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@BillNo", _SaveData.BillNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Cr", _SaveData.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _SaveData.FCr);
                oSqlCommand.Parameters.AddWithValue("@BillStatus", _SaveData.BillStatus);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _SaveData.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@Supplier", _SaveData.Supplier);
                oSqlCommand.Parameters.AddWithValue("@TotalAmount", _SaveData.TotalAmount);
                oSqlCommand.Parameters.AddWithValue("@PayedAmount", _SaveData.PayedAmount);
                oSqlCommand.Parameters.AddWithValue("@TobePayDate", _SaveData.TobePayDate);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Save PendingPayment");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }

        public string SavePP(BillingDataTypes.PendingPaymentDataType _SaveData, MySqlConnection _ActCon)
        {
            string respond = "";
            if (!ExistPendingPaymentInTrans(_SaveData.Supplier, _SaveData.BillNo, _ActCon))
                respond = SavePendingPaymentInTrans(_SaveData, _ActCon);
            else
                respond = UpdatePendingPayment(_SaveData, _ActCon);
            return respond;
        }
        public string SavePendingPaymentInTrans(BillingDataTypes.PendingPaymentDataType _SaveData,MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblpendingpayablebill ("
          + "CompanyID,"
          + "BillNo,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Cr,"
          + "FCr,"
          + "BillStatus,"
          + "AccPeriod,"
          + "Supplier,"
          + "TotalAmount,"
          + "PayedAmount,"
          + "TobePayDate,BillDate)"
           + " Values ("
           + "@CompanyID,"
           + "@BillNo,"
           + "@Description,"
           + "@AccountID,"
           + "@CurRate,"
           + "@Cr,"
           + "@FCr,"
           + "@BillStatus,"
           + "@AccPeriod,"
           + "@Supplier,"
           + "@TotalAmount,"
           + "@PayedAmount,"
           + "@TobePayDate,@BillDate)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@BillNo", _SaveData.BillNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Cr", _SaveData.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _SaveData.FCr);
                oSqlCommand.Parameters.AddWithValue("@BillStatus", _SaveData.BillStatus);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _SaveData.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@Supplier", _SaveData.Supplier);
                oSqlCommand.Parameters.AddWithValue("@TotalAmount", _SaveData.TotalAmount);
                oSqlCommand.Parameters.AddWithValue("@PayedAmount", _SaveData.PayedAmount);
                oSqlCommand.Parameters.AddWithValue("@TobePayDate", _SaveData.TobePayDate);
                oSqlCommand.Parameters.AddWithValue("@BillDate", _SaveData.BillDate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActCon,"Save PendingPayment");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }

        public string UpdatePendingPayment(BillingDataTypes.PendingPaymentDataType _Update, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblpendingpayablebill Set "
                + "Description=@Description,"
                + "AccountID=@AccountID,"
                + "CurRate=@CurRate,"
                + "Cr=@Cr,"
                + "FCr=@FCr,"
                + "BillStatus=@BillStatus,"
                + "AccPeriod=@AccPeriod,"
                + "Supplier=@Supplier,"
                + "TotalAmount=@TotalAmount,"
                + "PayedAmount=@PayedAmount,"
                + "TobePayDate=@TobePayDate,"
                + "BillDate=@BillDate"
                + " Where 1=1 "
                + " and Supplier=@Supplier"
                + " and BillNo=@BillNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@BillNo", _Update.BillNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Cr", _Update.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _Update.FCr);
                oSqlCommand.Parameters.AddWithValue("@BillStatus", _Update.BillStatus);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _Update.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@Supplier", _Update.Supplier);
                oSqlCommand.Parameters.AddWithValue("@TotalAmount", _Update.TotalAmount);
                oSqlCommand.Parameters.AddWithValue("@PayedAmount", _Update.PayedAmount);
                oSqlCommand.Parameters.AddWithValue("@TobePayDate", _Update.TobePayDate);
                oSqlCommand.Parameters.AddWithValue("@BillDate", _Update.BillDate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon,"Update PendingPayment");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeletePendingPayment(int Supplier, string BillNo)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblpendingpayablebill"
                + " Where 1=1 "
                + " and Supplier=@Supplier"
                + " and BillNo=@BillNo";
            try
                {
                    oSqlCommand.Parameters.AddWithValue("@Supplier", Supplier);
                oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Delete PendingPayment");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistPendingPayment(int Supplier, string BillNo)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tblpendingpayablebill"
                + " Where 1=1 "
                + " and Supplier=@Supplier"
                + " and BillNo=@BillNo";
            try
                {
                    oSqlCommand.Parameters.AddWithValue("@Supplier", Supplier);
                oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist In Table");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public bool ExistPendingPaymentInTrans(int Supplier, string BillNo, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select BillNo from tblpendingpayablebill"
                + " Where 1=1 "
                + " and Supplier=@Supplier"
                + " and BillNo=@BillNo";
            try
                {
                    oSqlCommand.Parameters.AddWithValue("@Supplier", Supplier);
                oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand,_ActCon, "Exist In Table");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public string GetExistPendingPayment(int CompanyID, string BillNo, out BillingDataTypes.PendingPaymentDataType _ExistData)
            {
            _ExistData = new BillingDataTypes.PendingPaymentDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "CompanyID,"
          + "BillNo,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Cr,"
          + "FCr,"
          + "BillStatus,"
          + "AccPeriod,"
          + "Supplier,"
          + "TotalAmount,"
          + "PayedAmount,"
          + "TobePayDate"
          + " from tblpendingpayablebill"
          + " Where 1=1 "
                + " and CompanyID=@CompanyID"
                + " and BillNo=@BillNo";
            oSqlCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
            oSqlCommand.Parameters.AddWithValue("@BillNo", BillNo);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand, "Get Exist data PendingPayment");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    int inCompanyID = 0;
                    resp = int.TryParse(r["CompanyID"].ToString(), out inCompanyID);
                    _ExistData.CompanyID = inCompanyID;
                    _ExistData.BillNo = r["BillNo"].ToString();
                    _ExistData.Description = r["Description"].ToString();
                    _ExistData.AccountID = r["AccountID"].ToString();
                    decimal deCurRate = 0;
                    resp = decimal.TryParse(r["CurRate"].ToString(), out deCurRate);
                    _ExistData.CurRate = deCurRate;
                    decimal deCr = 0;
                    resp = decimal.TryParse(r["Cr"].ToString(), out deCr);
                    _ExistData.Cr = deCr;
                    decimal deFCr = 0;
                    resp = decimal.TryParse(r["FCr"].ToString(), out deFCr);
                    _ExistData.FCr = deFCr;
                    int inBillStatus = 0;
                    resp = int.TryParse(r["BillStatus"].ToString(), out inBillStatus);
                    _ExistData.BillStatus = inBillStatus;
                    int inAccPeriod = 0;
                    resp = int.TryParse(r["AccPeriod"].ToString(), out inAccPeriod);
                    _ExistData.AccPeriod = inAccPeriod;
                    int inSupplier = 0;
                    resp = int.TryParse(r["Supplier"].ToString(), out inSupplier);
                    _ExistData.Supplier = inSupplier;
                    decimal deTotalAmount = 0;
                    resp = decimal.TryParse(r["TotalAmount"].ToString(), out deTotalAmount);
                    _ExistData.TotalAmount = deTotalAmount;
                    decimal dePayedAmount = 0;
                    resp = decimal.TryParse(r["PayedAmount"].ToString(), out dePayedAmount);
                    _ExistData.PayedAmount = dePayedAmount;
                    DateTime dtTobePayDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["TobePayDate"].ToString(), out dtTobePayDate);
                    if (resp)
                        _ExistData.TobePayDate = dtTobePayDate;
                    else
                        _ExistData.TobePayDate = new DateTime(1900, 1, 1);

                    DateTime dtBillDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["BillDate"].ToString(), out dtBillDate);
                    if (resp)
                        _ExistData.BillDate = dtBillDate;
                    else
                        _ExistData.BillDate = new DateTime(1900, 1, 1);

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



#endregion

        }
    public class BillingDataTypes
    {
        public struct BillingDataType
            {
            public string BillNo;
            public string Description;
            public string AccountID;
            public decimal CurRate;
            public decimal Cr;
            public decimal FCr;
            public int BillStatus;
            public string TrRef;
            public string TrUser;
            public DateTime TrDate;
            public string TrTime;
            public int AccPeriod;
            public int CompanyID;
            public int PayToCatID;
            public int PayToID;
            public decimal PayAmount;
            public DateTime TobePayDate;
            public DateTime BillDate;
            public List<BillingDetailsDataType> BillingDetails;
            }
        public struct BillingDetailsDataType
        {
            public int ItemNo;
            public string BillNo;
            public string AccID;
            public string Description;
            public decimal Dr;
            public decimal Fdr;
            public string TrRef;
            public decimal Vat;
        }
        public struct PendingPaymentDataType
            {
            public int CompanyID;
            public string BillNo;
            public string Description;
            public string AccountID;
            public decimal CurRate;
            public decimal Cr;
            public decimal FCr;
            public int BillStatus;
            public int AccPeriod;
            public int Supplier;
            public decimal TotalAmount;
            public decimal PayedAmount;
            public DateTime TobePayDate;
            public DateTime BillDate;
            }
        }
    }
