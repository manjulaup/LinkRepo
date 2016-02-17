//Create by SATICIN On 01/Nov/2015
//
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using BusinessLayer.CommonOperation;
using DataLayer.DataService; 
namespace BusinessLayer.PaymentsAndReciept
{
    public class Payment
    {
        private DataService Mycommon = null;
        public Payment(bool IsLocal)
        {
            Mycommon = new DataService(IsLocal);
        }
         ~Payment()
        {
            Mycommon.CloseDB();
        }
        public DataTable GetSerchPayList(int  Category,int  ID, int para3)
        {
            string sql1 = "";
            switch (Category)
            {
                case 1:
                    sql1 = "";
                    break;
                default:
                    break;
            }
            if (para3 != -1)
            {
                sql1 = "SELECT tblpayment.PaymentID, tblpayment.Description, tblpayment.AccountID, accountname.AccountName, tblpayment.Cr, tblpayment.CurRate,   tblpayment.PayToName,tblpayment.ChequeNumber, tblpayment.PayActualDate "
                    + " FROM accountname INNER JOIN accounterp.tblpayment ON accountname.AccountID = tblpayment.AccountID where PayStatus=" + para3;
            }
            else
            {
                sql1 = "SELECT tblpayment.PaymentID, tblpayment.Description, tblpayment.AccountID, accountname.AccountName, tblpayment.Cr, tblpayment.CurRate,   tblpayment.PayToName,tblpayment.ChequeNumber, tblpayment.PayActualDate "
                    + " FROM accountname INNER JOIN accounterp.tblpayment ON accountname.AccountID = tblpayment.AccountID ";

            }
                DataTable tb = Mycommon.GetDataTableAccount(sql1, "get Payment List");
            return tb;
        }
        
        public string SavePayment_General(AccountTypes.Payment_GeneralDataType _SaveData,out string PVN)
        {
            MySql.Data.MySqlClient.MySqlTransaction Mytrans;
            MySqlConnection CurCon = new MySqlConnection();
            CurCon = Mycommon.AccountConnection;
            if (CurCon.State == ConnectionState.Closed)
                CurCon.Open();
            PVN = GetNewTMPPayID(_SaveData.CompanyID, _SaveData.AccPeriod);
            Mytrans = Mycommon.AccountConnection.BeginTransaction();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblpayment ("
          + "PaymentID,"
          + "PaymentMethod,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Cr,"
          + "FCr,"
          + "PayStatus,"
          + "TrRef,"
          + "TrUser,"
          + "TrDate,"
          + "TrTime,"
          + "ChequeNumber,AccPeriod,CompanyID,PayToCatID,PayToName,PayActualDate,IsAdvancePayment)"
           + " Values ("
           + "@PaymentID,"
           + "@PaymentMethod,"
           + "@Description,"
           + "@AccountID,"
           + "@CurRate,"
           + "@Cr,"
           + "@FCr,"
           + "@PayStatus,"
           + "@TrRef,"
           + "@TrUser,"
           + "curdate(),"
           + "curtime(),"
           + "@ChequeNumber,@AccPeriod,@CompanyID,@PayToCatID,@PayToName,@PayActualDate,IsAdvancePayment)";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@PaymentID", PVN);
                oSqlCommand.Parameters.AddWithValue("@PaymentMethod", _SaveData.PaymentMethod);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Cr", _SaveData.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _SaveData.FCr);
                oSqlCommand.Parameters.AddWithValue("@PayStatus", _SaveData.PayStatus);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _SaveData.TrRef);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _SaveData.TrUser);
                oSqlCommand.Parameters.AddWithValue("@ChequeNumber", _SaveData.ChequeNumber);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _SaveData.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@PayToCatID", _SaveData.PayToCatID);
                oSqlCommand.Parameters.AddWithValue("@PayToName", _SaveData.PayToName);
                oSqlCommand.Parameters.AddWithValue("@PayActualDate", _SaveData.PayActualDate);
                oSqlCommand.Parameters.AddWithValue("@IsAdvancePayment", _SaveData.IsAdvancePayment);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,CurCon,  "Save Payment_General");
                if (respond == "True")
                {
                    foreach (AccountTypes.Payment_GeneralDetailsDataType OneItem in _SaveData.Details)
                    {
                        bool Exist = ExistPayment_GeneralDetails(OneItem.ItemNo, PVN, CurCon, Mytrans);
                        if (!Exist)
                        {
                            AccountTypes.Payment_GeneralDetailsDataType OneItemSecond = new AccountTypes.Payment_GeneralDetailsDataType();
                            OneItemSecond = OneItem;
                            OneItemSecond.PvnNo = PVN;
                            respond = SavePayment_GeneralDetails(OneItemSecond, CurCon, Mytrans);
                        }
                        if (respond != "True")
                        {
                            Mytrans.Rollback();
                            return respond; 
                        }
                    }
                    if (respond == "True")
                    {
                        try
                        {
                            Mytrans.Commit();
                            return respond;
                        }
                        catch (Exception ex)
                        {
                            
                             Mytrans.Rollback ();
                            return ex.Message ;
                        }
                    }
                    else
                    {
                        Mytrans.Rollback ();
                        CurCon.Close();
                        CurCon.Dispose();
                        return respond;
                    }
                }
                else
                {
                    Mytrans.Rollback();
                     return respond;
                }
               
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdatePaymentstatus(string PVN, int Status)
        {
            string sql1 = "Update tblpayment set PayStatus=" + Status + " where PaymentID='" + PVN + "'";
            string respond = Mycommon.ExicuteAnyCommandAccount(sql1, "Update Status");
            return respond;
        }
        public string SendToApproval(string PVN,string User1)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
                {
                    string sql1 = "Update tblpayment set PayStatus=1,TrUser=@TrUser,TrDate=curDate(),TrTime=curtime() where PaymentID=@PaymentID";
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
        public string SetPaymentVoucherAsApproved(string PVN, string User1)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
                {
                string sql1 = "Update tblpayment set PayStatus=2,ApproveBy=@TrUser,ApproveDate=curDate(),ApproveTime=curtime() where PaymentID=@PaymentID";
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
       
        public int GetPVNStatus(string PVN)
        {
            string sql1 = "select PayStatus from tblpayment where PaymentID='" + PVN + "'";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get PVN Status");
            if (r != null)
            {
                int i = -1;
                bool resp = int.TryParse(r["PayStatus"].ToString(), out i);
                return i;
            }
            else
                return -1;
        }
        public string UpdatePayment_General(AccountTypes.Payment_GeneralDataType _Update)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();

            MySql.Data.MySqlClient.MySqlTransaction Mytrans;
            MySqlConnection CurCon = new MySqlConnection();
            CurCon = Mycommon.AccountConnection;
            if (CurCon.State == ConnectionState.Closed)
                CurCon.Open();
           
            Mytrans = Mycommon.AccountConnection.BeginTransaction();
             string sqlQuery = "Update tblpayment Set "
                + "PaymentMethod=@PaymentMethod,"
                + "Description=@Description,"
                + "AccountID=@AccountID,"
                + "CurRate=@CurRate,"
                + "Cr=@Cr,"
                + "FCr=@FCr,"
                + "PayStatus=@PayStatus,"
                + "TrUser=@TrUser,"
                + "TrDate=curdate(),"
                + "TrTime=curtime(),"
                + "ChequeNumber=@ChequeNumber,"
                + "AccPeriod=@AccPeriod,"
                + "CompanyID=@CompanyID,"
                + "PayToCatID=@PayToCatID,"
                + "PayToName=@PayToName,"
                + "PayActualDate=@PayActualDate,IsAdvancePayment=@IsAdvancePayment"
                + " Where 1=1 "
                + " and PaymentID=@PaymentID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@PaymentID", _Update.PaymentID);
                oSqlCommand.Parameters.AddWithValue("@PaymentMethod", _Update.PaymentMethod);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Cr", _Update.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _Update.FCr);
                oSqlCommand.Parameters.AddWithValue("@PayStatus", _Update.PayStatus);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _Update.TrUser);
                oSqlCommand.Parameters.AddWithValue("@ChequeNumber", _Update.ChequeNumber);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _Update.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@PayToCatID", _Update.PayToCatID);
                oSqlCommand.Parameters.AddWithValue("@PayToName", _Update.PayToName);
                oSqlCommand.Parameters.AddWithValue("@PayActualDate", _Update.PayActualDate);
                oSqlCommand.Parameters.AddWithValue("@IsAdvancePayment", _Update.IsAdvancePayment);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,CurCon,  "Update Payment_General");
                if (respond != "True")
                {
                    Mytrans.Rollback();
                    CurCon.Close();
                    CurCon.Dispose();
                    return respond;
                }
                else
                {
                    foreach (AccountTypes.Payment_GeneralDetailsDataType OneItem in _Update.Details)
                    {
                        bool Exist = ExistPayment_GeneralDetails(OneItem.ItemNo, OneItem.PvnNo, CurCon, Mytrans);
                        if (!Exist)
                        {
                            respond = SavePayment_GeneralDetails(OneItem, CurCon, Mytrans);
                        }
                        else
                        {
                            respond = UpdatePayment_GeneralDetails(OneItem, CurCon);
                        }
                        if (respond != "True")
                        {
                            Mytrans.Rollback();
                            return respond;
                        }
                    }
                    if (respond == "True")
                    {
                        try
                        {
                            Mytrans.Commit();
                            return respond;
                        }
                        catch (Exception ex)
                        {

                            Mytrans.Rollback();
                            return ex.Message;
                        }
                    }
                    else
                    {
                        Mytrans.Rollback();
                        CurCon.Close();
                        CurCon.Dispose();
                        return respond;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeletePayment_General(string PaymentID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblpayment"
                + " Where 1=1 "
                + " and PaymentID=@PaymentID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@PaymentID", PaymentID);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Delete Payment_General");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool ExistPayment_General(string PaymentID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select PaymentID from tblpayment"
                + " Where 1=1 "
                + " and PaymentID=@PaymentID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@PaymentID", PaymentID);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist Payment ID");
                return respond;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetExistPayment_General(string PaymentID, out  AccountTypes.Payment_GeneralDataType _ExistData)
        {
            _ExistData = new AccountTypes.Payment_GeneralDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "PaymentID,"
          + "PaymentMethod,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Cr,"
          + "FCr,"
          + "PayStatus,"
          + "TrRef,"
          + "TrUser,"
          + "TrDate,"
          + "TrTime,"
          + "ChequeNumber,PayToCatID,PayToName,PayActualDate,AccPeriod,CompanyID,IsAdvancePayment"
          + " from tblpayment"
          + " Where 1=1 "
                + " and PaymentID=@PaymentID";
            oSqlCommand.Parameters.AddWithValue("@PaymentID", PaymentID);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand,  "Get Exist data Payment_General");
            if (r != null)
            {
                try
                {
                    bool resp = false;
                    _ExistData.PaymentID = r["PaymentID"].ToString();
                    int inPaymentMethod = 0;
                    resp = int.TryParse(r["PaymentMethod"].ToString(), out inPaymentMethod);
                    _ExistData.PaymentMethod = inPaymentMethod;
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
                    int inPayStatus = 0;
                    resp = int.TryParse(r["PayStatus"].ToString(), out inPayStatus);
                    _ExistData.PayStatus = inPayStatus;
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
                    _ExistData.PayToName = r["PayToName"].ToString();
                    int inPayToCatID = 0;
                    resp = int.TryParse(r["PayToCatID"].ToString(), out inPayToCatID);
                    _ExistData.PayToCatID = inPayToCatID;

                    DateTime dtPayActualDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["PayActualDate"].ToString(), out dtPayActualDate);
                    if (resp)
                        _ExistData.PayActualDate = dtPayActualDate;
                    else
                        _ExistData.PayActualDate = new DateTime(1900, 1, 1);

                    //AccPeriod

                    int inAccPeriod = 0;
                    resp = int.TryParse(r["AccPeriod"].ToString(), out inAccPeriod);
                    _ExistData.AccPeriod = inAccPeriod;

                    int inCompanyID = 0;
                    resp = int.TryParse(r["CompanyID"].ToString(), out inCompanyID);
                    _ExistData.CompanyID  = inCompanyID;
                    //IsAdvancePayment
                    int inIsAdvancePayment = 0;
                    resp = int.TryParse(r["IsAdvancePayment"].ToString(), out inIsAdvancePayment);
                    _ExistData.IsAdvancePayment = inIsAdvancePayment;

                    List<AccountTypes.Payment_GeneralDetailsDataType> _Exlist = new List<AccountTypes.Payment_GeneralDetailsDataType>();
                    string respond1 = GetPaymentDetailList(PaymentID, out _Exlist);
                    _ExistData.Details = _Exlist;
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

        public string GetPaymentDetailList(string PVN, out List<AccountTypes.Payment_GeneralDetailsDataType> _PayList)
            {
                   MySqlCommand oSqlCommand = new MySqlCommand();
            _PayList=new List<AccountTypes.Payment_GeneralDetailsDataType> ();

                string sql1 = "select PvnNo, ItemNo from tblpaymetdetails where PvnNo=@PvnNo";
                oSqlCommand.Parameters.AddWithValue("@PvnNo", PVN);

                string respond = "";
                try
                    {
                    DataTable tb = Mycommon.GetDataTableAccount(sql1, oSqlCommand, "Get Payment list");
                    if (tb != null)
                        {
                        foreach (DataRow r in tb.Rows)
                            {
                            AccountTypes.Payment_GeneralDetailsDataType _Onedata = new AccountTypes.Payment_GeneralDetailsDataType();
                            int itemNo = 0;
                            bool resp = int.TryParse(r["ItemNo"].ToString(), out itemNo);
                            respond = GetExistPayment_GeneralDetails(itemNo, PVN, out _Onedata);
                            if (respond == "True")
                                _PayList.Add(_Onedata);
                            else
                                return respond;
                            }
                        return "True";
                        }
                    else
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
            string sql1 = "select max( substr(PaymentID,5)) as MaxN from accounterp.tblpayment where CompanyID=" + CompanyID + " and  PayStatus<>4 ";
            DataRow r = Mycommon.GetDataRow(sql1, "Get New PaymentID");
            if (r != null)
            {
                string Str = r["MaxN"].ToString();
                int FNumber = 0;
                bool resp = int.TryParse(Str, out FNumber);
                FNumber = FNumber + 1;
                PVNid = "TPV-" +  FNumber.ToString("0######");
               
            }
            else
            {
                PVNid = "TPV-"  +  "0000001";
            }
            return PVNid;

        }
        //public string GetNewPaymentID(int CompanyID,string PreFix,int Cur_AccYear)
        //{
        //    string PVNid = "";
        //    string sql1 = "select max( substr(ReceiptID,7)) as MaxN from tblreceipt where CompanyID=" + CompanyID + " and  PayStatus=4 and  substr(tblpayment.PaymentID,1,1)='" + PreFix + "'";
        //    DataRow r = Mycommon.GetDataRow(sql1, "Get New PaymentID");
        //    if (r != null)
        //    {
        //        string Str = r["MaxN"].ToString();
        //        string Ye = Str.Substring(2, 2);
        //        int Ye1 = int.Parse(Ye);
        //        int FNumber = 0;
        //        bool resp = int.TryParse(Str, out FNumber);

        //        if (Cur_AccYear == Ye1)
        //        {
        //            FNumber = FNumber + 1;
        //            PVNid = PreFix + Cur_AccYear.ToString("##") + FNumber.ToString("0######");
        //        }
        //    }
        //    else
        //    {
        //        PVNid = PreFix + "-" + Cur_AccYear.ToString("##") + "000000001";
        //    }
        //    return PVNid;

        //}
        public int GetStatus(string PVN)
        {
            string sql1 = "select PayStatus from tblpayment where PaymentID='" + PVN + "'";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get Status");
            if (r != null)
            {
                int i = 0;
                bool resp = int.TryParse(r["PayStatus"].ToString(), out i);
                return i;
            }
            return -1;
        }

        public string SavePayment_GeneralDetails(AccountTypes.Payment_GeneralDetailsDataType _SaveData,MySqlConnection ActCon,MySqlTransaction ActTrance)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblpaymetdetails ("
          + "ItemNo,"
          + "PvnNo,"
          + "AccID,"
          + "Description,"
          + "Dr,"
          + "Fdr,"
          + "PayTrRef,"
          + "Vat,JobNo,Exrate)"
           + " Values ("
           + "@ItemNo,"
           + "@PvnNo,"
           + "@AccID,"
           + "@Description,"
           + "@Dr,"
           + "@Fdr,"
           + "@PayTrRef,"
           + "@Vat,@JobNo,@Exrate)";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _SaveData.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@PvnNo", _SaveData.PvnNo);
                oSqlCommand.Parameters.AddWithValue("@AccID", _SaveData.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@Fdr", _SaveData.Fdr);
                oSqlCommand.Parameters.AddWithValue("@PayTrRef", _SaveData.PayTrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _SaveData.Vat);
                oSqlCommand.Parameters.AddWithValue("@JobNo", _SaveData.JobNo);
                oSqlCommand.Parameters.AddWithValue("@Exrate", _SaveData.Exrate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,ActCon,"Save Payment_GeneralDetails");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdatePayment_GeneralDetails(AccountTypes.Payment_GeneralDetailsDataType _Update,MySqlConnection _ActCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblpaymetdetails Set "
                + "ItemNo=@ItemNo,"
                + "PvnNo=@PvnNo,"
                + "AccID=@AccID,"
                + "Description=@Description,"
                + "Dr=@Dr,"
                + "Fdr=@Fdr,"
                + "PayTrRef=@PayTrRef,"
                + "Vat=@Vat,JobNo=@JobNo,Exrate=@Exrate"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and PvnNo=@PvnNo";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _Update.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@PvnNo", _Update.PvnNo);
                oSqlCommand.Parameters.AddWithValue("@AccID", _Update.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@Fdr", _Update.Fdr);
                oSqlCommand.Parameters.AddWithValue("@PayTrRef", _Update.PayTrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _Update.Vat);
                oSqlCommand.Parameters.AddWithValue("@JobNo", _Update.JobNo);
                oSqlCommand.Parameters.AddWithValue("@Exrate", _Update.Exrate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActCon,  "Update Payment_GeneralDetails");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeletePayment_GeneralDetails(int ItemNo, string PvnNo)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblpaymetdetails"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and PvnNo=@PvnNo";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
                oSqlCommand.Parameters.AddWithValue("@PvnNo", PvnNo);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Delete Payment_GeneralDetails");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool ExistPayment_GeneralDetails(int ItemNo, string PvnNo)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select ItemNo from tblpaymetdetails"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and PvnNo=@PvnNo";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
                oSqlCommand.Parameters.AddWithValue("@PvnNo", PvnNo);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist Details");
                return respond;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExistPayment_GeneralDetails(int ItemNo, string PvnNo,MySqlConnection Actcon,MySqlTransaction ActTrance)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select ItemNo from tblpaymetdetails"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and PvnNo=@PvnNo";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
                oSqlCommand.Parameters.AddWithValue("@PvnNo", PvnNo);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand,Actcon, "Exist Details");
                return respond;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public DataTable GetPAyDetailList(string PVN)
        {
            string sql1 = "SELECT AccID,(Select AccountName from accountname where AccountID=AccID) as acname, Description, JobNo, Vat, Dr,Fdr,Exrate, ItemNo FROM   tblpaymetdetails "
                + "  where PvnNo='" + PVN + "'";
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Payment Detail List");
            return tb;
        }
        public DataTable GetPendingBillList(int SupID)
        {
            string sql1="SELECT '0',BillNo, Description,FCr,FCr, AccountID, CurRate,  TobePayDate, BillDate, PayedAmount "
                + " FROM tblpendingpayablebill where  Supplier=" + SupID + " and BillStatus=0";
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Payment Detail List");
            return tb;
        }
        public string UpdatePendingBillAmount(int SupID,string BillNo,decimal Amount)
        {
            string sql1 = "Update tblpendingpayablebill set PayedAmount=PayedAmount + " + Amount + " where Supplier=" + SupID
                + " and BillNo='" + BillNo + "'";
            string respond = Mycommon.ExicuteAnyCommandAccount(sql1, "Update Bill amount");
            SetBillStatus(SupID, BillNo);
            return respond;
        }
        private void SetBillStatus(int SupID, string BillNo)
        {
            string sql1 = "Update tblpendingpayablebill set BillStatus=1 where Supplier=" + SupID
                          + " and BillNo='" + BillNo + "' and FCr=PayedAmount";
            string respond = Mycommon.ExicuteAnyCommandAccount(sql1, "Update Status");
        }
        public decimal GetOutstadingBillAmoount(int SupID, string BillNo)
        {
            string sql1 = "Select (FCr-PayedAmount) as Pendingamount from tblpendingpayablebill where Supplier=" + SupID
                + " and BillNo='" + BillNo + "'";
            DataRow r = Mycommon.GetDataRowAccount(sql1, "Get outstanding");
            if (r != null)
            {
                decimal d = 0;
                bool resp = decimal.TryParse(r["Pendingamount"].ToString(), out d);
                return d;
            }
            else
                return 0;
        }
        public string GetExistPayment_GeneralDetails(int ItemNo, string PvnNo, out AccountTypes.Payment_GeneralDetailsDataType _ExistData)
        {
            _ExistData = new AccountTypes.Payment_GeneralDetailsDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "ItemNo,"
          + "PvnNo,"
          + "AccID,"
          + "Description,"
          + "Dr,"
          + "Fdr,"
          + "PayTrRef,"
          + "Vat,JobNo,Exrate"
          + " from tblpaymetdetails"
          + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and PvnNo=@PvnNo";
            oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
            oSqlCommand.Parameters.AddWithValue("@PvnNo", PvnNo);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand, "Get Exist data Payment_GeneralDetails");
            if (r != null)
            {
                try
                {
                    bool resp = false;
                    int inItemNo = 0;
                    resp = int.TryParse(r["ItemNo"].ToString(), out inItemNo);
                    _ExistData.ItemNo = inItemNo;
                    _ExistData.PvnNo = r["PvnNo"].ToString();
                    _ExistData.AccID = r["AccID"].ToString();
                    _ExistData.Description = r["Description"].ToString();
                    decimal deDr = 0;
                    resp = decimal.TryParse(r["Dr"].ToString(), out deDr);
                    _ExistData.Dr = deDr;
                    decimal deFdr = 0;
                    resp = decimal.TryParse(r["Fdr"].ToString(), out deFdr);
                    _ExistData.Fdr = deFdr;
                    _ExistData.PayTrRef = r["PayTrRef"].ToString();
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
       
//==============================
        public DataTable GetProjectList()
        {
            string sql1 = "SELECT ProjectID, ProjectName, Description, UserName FROM projectmain ";
            DataTable tb = Mycommon.GetDataTable(sql1, "Get Project List");
            return tb;
        }
    #region AdvancePayment
        public string SaveAdvancePayment(AccountTypes.AdvancePaymentDataType _SaveData)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tbladvanced ("

          + "ADVReference,"
          + "AdvType,"
          + "ToID,"
          + "ToName,"
          + "Dr,"
          + "FDr,"
          + "Exrate,"
          + "AdvDate,"
          + "IssuedReference,"
          + "RvcAmount,"
          + "AdvStatus)"
           + " Values ("
           + "@ADVReference,"
           + "@AdvType,"
           + "@ToID,"
           + "@ToName,"
           + "@Dr,"
           + "@FDr,"
           + "@Exrate,"
           + "@AdvDate,"
           + "@IssuedReference,"
           + "@RvcAmount,"
           + "@AdvStatus)";
            try
                {

                oSqlCommand.Parameters.AddWithValue("@ADVReference", _SaveData.ADVReference);
                oSqlCommand.Parameters.AddWithValue("@AdvType", _SaveData.AdvType);
                oSqlCommand.Parameters.AddWithValue("@ToID", _SaveData.ToID);
                oSqlCommand.Parameters.AddWithValue("@ToName", _SaveData.ToName);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _SaveData.FDr);
                oSqlCommand.Parameters.AddWithValue("@Exrate", _SaveData.Exrate);
                oSqlCommand.Parameters.AddWithValue("@AdvDate", _SaveData.AdvDate);
                oSqlCommand.Parameters.AddWithValue("@IssuedReference", _SaveData.IssuedReference);
                oSqlCommand.Parameters.AddWithValue("@RvcAmount", _SaveData.RvcAmount);
                oSqlCommand.Parameters.AddWithValue("@AdvStatus", _SaveData.AdvStatus);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Save AdvancePayment");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }

        private string GetNewAdvReference(MySqlConnection _ActCon)
            {
            string sql1 = "SELECT max(substr(JounalID,4)) as MaxID FROM   tbladvanced";
            MySqlCommand oSqlCommand = new MySqlCommand();
            DataRow r = Mycommon.GetDataRowAccountTrans(sql1, oSqlCommand, _ActCon, "Get MaxID");
            if (r != null)
                {
                int i = 0;
                bool resp = int.TryParse(r["MaxID"].ToString(), out i);
                i = i + 1;
                return "ADV-" + i.ToString("0########");

                }
            else
                return "ADV-00000001";
            }
        public string SaveAdvancePayment(AccountTypes.AdvancePaymentDataType _SaveData, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string NewADVref = GetNewAdvReference(_ActCon);
            string sqlQuery = "Insert Into tbladvanced ("
          + "ADVReference,"
          + "AdvType,"
          + "ToID,"
          + "ToName,"
          + "Dr,"
          + "FDr,"
          + "Exrate,"
          + "AdvDate,"
          + "IssuedReference,"
          + "RvcAmount,"
          + "AdvStatus)"
           + " Values ("
           + "@ADVReference,"
           + "@AdvType,"
           + "@ToID,"
           + "@ToName,"
           + "@Dr,"
           + "@FDr,"
           + "@Exrate,"
           + "@AdvDate,"
           + "@IssuedReference,"
           + "@RvcAmount,"
           + "@AdvStatus)";
            try
                {

                oSqlCommand.Parameters.AddWithValue("@ADVReference", NewADVref);
                oSqlCommand.Parameters.AddWithValue("@AdvType", _SaveData.AdvType);
                oSqlCommand.Parameters.AddWithValue("@ToID", _SaveData.ToID);
                oSqlCommand.Parameters.AddWithValue("@ToName", _SaveData.ToName);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _SaveData.FDr);
                oSqlCommand.Parameters.AddWithValue("@Exrate", _SaveData.Exrate);
                oSqlCommand.Parameters.AddWithValue("@AdvDate", _SaveData.AdvDate);
                oSqlCommand.Parameters.AddWithValue("@IssuedReference", _SaveData.IssuedReference);
                oSqlCommand.Parameters.AddWithValue("@RvcAmount", _SaveData.RvcAmount);
                oSqlCommand.Parameters.AddWithValue("@AdvStatus", _SaveData.AdvStatus);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon, "Save AdvancePayment");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string GetAdvNumberFromReference(string ReferenceNo, MySqlConnection _ActCon)
            {
            string sql1 = "Select ADVReference from tbladvanced where IssuedReference=@IssuedReference";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@IssuedReference", ReferenceNo);
            DataRow r = Mycommon.GetDataRowAccountTrans(sql1, oSqlCommand, _ActCon, "Get AdvReference");
            if (r != null)
                return r["ADVReference"].ToString();
            else
                return "";

            }
        public string UpdateAdvancePayment(AccountTypes.AdvancePaymentDataType _Update, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tbladvanced Set "

                + "ADVReference=@ADVReference,"
                + "AdvType=@AdvType,"
                + "ToID=@ToID,"
                + "ToName=@ToName,"
                + "Dr=@Dr,"
                + "FDr=@FDr,"
                + "Exrate=@Exrate,"
                + "AdvDate=@AdvDate,"
                + "IssuedReference=@IssuedReference,"
                + "RvcAmount=@RvcAmount,"
                + "AdvStatus=@AdvStatus"
                + " Where 1=1 "
                + " and ADVReference=@ADVReference";
            try
                {

                oSqlCommand.Parameters.AddWithValue("@ADVReference", _Update.ADVReference);
                oSqlCommand.Parameters.AddWithValue("@AdvType", _Update.AdvType);
                oSqlCommand.Parameters.AddWithValue("@ToID", _Update.ToID);
                oSqlCommand.Parameters.AddWithValue("@ToName", _Update.ToName);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _Update.FDr);
                oSqlCommand.Parameters.AddWithValue("@Exrate", _Update.Exrate);
                oSqlCommand.Parameters.AddWithValue("@AdvDate", _Update.AdvDate);
                oSqlCommand.Parameters.AddWithValue("@IssuedReference", _Update.IssuedReference);
                oSqlCommand.Parameters.AddWithValue("@RvcAmount", _Update.RvcAmount);
                oSqlCommand.Parameters.AddWithValue("@AdvStatus", _Update.AdvStatus);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon, "Update AdvancePayment");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }

        public string SaveAdvance(AccountTypes.AdvancePaymentDataType _SAveData, MySqlConnection _ActCon)
            {

            string respond = "";
            if (!ExistAdvancePayment(_SAveData.ADVReference, _ActCon))
                respond = SaveAdvancePayment(_SAveData, _ActCon);
            else
                respond = UpdateAdvancePayment(_SAveData, _ActCon);
            return respond;

            }
        public string UpdateAdvancePayment(AccountTypes.AdvancePaymentDataType _Update)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tbladvanced Set "

                + "ADVReference=@ADVReference,"
                + "AdvType=@AdvType,"
                + "ToID=@ToID,"
                + "ToName=@ToName,"
                + "Dr=@Dr,"
                + "FDr=@FDr,"
                + "Exrate=@Exrate,"
                + "AdvDate=@AdvDate,"
                + "IssuedReference=@IssuedReference,"
                + "RvcAmount=@RvcAmount,"
                + "AdvStatus=@AdvStatus"
                + " Where 1=1 "
                + " and ADVReference=@ADVReference";
            try
                {

                oSqlCommand.Parameters.AddWithValue("@ADVReference", _Update.ADVReference);
                oSqlCommand.Parameters.AddWithValue("@AdvType", _Update.AdvType);
                oSqlCommand.Parameters.AddWithValue("@ToID", _Update.ToID);
                oSqlCommand.Parameters.AddWithValue("@ToName", _Update.ToName);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _Update.FDr);
                oSqlCommand.Parameters.AddWithValue("@Exrate", _Update.Exrate);
                oSqlCommand.Parameters.AddWithValue("@AdvDate", _Update.AdvDate);
                oSqlCommand.Parameters.AddWithValue("@IssuedReference", _Update.IssuedReference);
                oSqlCommand.Parameters.AddWithValue("@RvcAmount", _Update.RvcAmount);
                oSqlCommand.Parameters.AddWithValue("@AdvStatus", _Update.AdvStatus);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Update AdvancePayment");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeleteAdvancePayment(string ADVReference)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tbladvanced"
                + " Where 1=1 "
                + " and ADVReference=@ADVReference";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ADVReference", ADVReference);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Delete AdvancePayment");

                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistAdvancePayment(string ADVReference, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tbladvanced"
                + " Where 1=1 "
                + " and ADVReference=@ADVReference";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ADVReference", ADVReference);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand, _ActCon, "Exist Advance Payment");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public bool ExistAdvancePayment(string ADVReference)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tbladvanced"
                + " Where 1=1 "
                + " and ADVReference=@ADVReference";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ADVReference", ADVReference);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand, "Exist Advance Payment");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public string GetExistAdvancePayment(string ADVReference, out AccountTypes.AdvancePaymentDataType _ExistData)
            {
            _ExistData = new AccountTypes.AdvancePaymentDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "SysID,"
          + "ADVReference,"
          + "AdvType,"
          + "ToID,"
          + "ToName,"
          + "Dr,"
          + "FDr,"
          + "Exrate,"
          + "AdvDate,"
          + "IssuedReference,"
          + "RvcAmount,"
          + "AdvStatus"
          + " from tbladvanced"
          + " Where 1=1 "
                + " and ADVReference=@ADVReference";
            oSqlCommand.Parameters.AddWithValue("@ADVReference", ADVReference);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand, "Get Exist data AdvancePayment");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    int inSysID = 0;
                    resp = int.TryParse(r["SysID"].ToString(), out inSysID);
                    _ExistData.SysID = inSysID;
                    _ExistData.ADVReference = r["ADVReference"].ToString();
                    int inAdvType = 0;
                    resp = int.TryParse(r["AdvType"].ToString(), out inAdvType);
                    _ExistData.AdvType = inAdvType;
                    int inToID = 0;
                    resp = int.TryParse(r["ToID"].ToString(), out inToID);
                    _ExistData.ToID = inToID;
                    _ExistData.ToName = r["ToName"].ToString();
                    decimal deDr = 0;
                    resp = decimal.TryParse(r["Dr"].ToString(), out deDr);
                    _ExistData.Dr = deDr;
                    decimal deFDr = 0;
                    resp = decimal.TryParse(r["FDr"].ToString(), out deFDr);
                    _ExistData.FDr = deFDr;
                    decimal deExrate = 0;
                    resp = decimal.TryParse(r["Exrate"].ToString(), out deExrate);
                    _ExistData.Exrate = deExrate;
                    DateTime dtAdvDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["AdvDate"].ToString(), out dtAdvDate);
                    if (resp)
                        _ExistData.AdvDate = dtAdvDate;
                    else
                        _ExistData.AdvDate = new DateTime(1900, 1, 1);
                    _ExistData.IssuedReference = r["IssuedReference"].ToString();
                    decimal deRvcAmount = 0;
                    resp = decimal.TryParse(r["RvcAmount"].ToString(), out deRvcAmount);
                    _ExistData.RvcAmount = deRvcAmount;
                    int inAdvStatus = 0;
                    resp = int.TryParse(r["AdvStatus"].ToString(), out inAdvStatus);
                    _ExistData.AdvStatus = inAdvStatus;
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
    
    public class AccountTypes
    {
            public struct Payment_GeneralDataType
            {
                public string PaymentID;
                public int PaymentMethod;
                public string Description;
                public string AccountID;
                public decimal CurRate;
                public decimal Cr;
                public decimal FCr;
                public int PayStatus;
                public string TrRef;
                public string TrUser;
                public DateTime TrDate;
                public string TrTime;
                public string ChequeNumber;
                public int AccPeriod;
                public int CompanyID;
                public int PayToCatID;
                public string PayToName;
                public DateTime PayActualDate;
                public int IsAdvancePayment;
                public List<Payment_GeneralDetailsDataType> Details;
            }
            public struct Payment_GeneralDetailsDataType
            {
                public int ItemNo;
                public string PvnNo;
                public string  AccID;
                public string Description;
                public decimal Dr;
                public decimal Fdr;
                public string PayTrRef;
                public decimal Vat;
                public string JobNo;
                public decimal Exrate;
            }
            public struct AdvancePaymentDataType
                {
                    public int SysID;
                    public string ADVReference;
                    public int AdvType;
                    public int ToID;
                    public string ToName;
                    public decimal Dr;
                    public decimal FDr;
                    public decimal Exrate;
                    public DateTime AdvDate;
                    public string IssuedReference;
                    public decimal RvcAmount;
                    public int AdvStatus;
                }
       
    }
}
