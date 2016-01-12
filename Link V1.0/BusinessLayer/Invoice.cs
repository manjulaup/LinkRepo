//Create by SATICIN On 31/Dec/2015
//
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using BusinessLayer.CommonOperation;
using System.Windows.Forms;
using BusinessLayer;

namespace BusinessLayer.Invoices
    {
    public class Invoice
        {
        private CommonOperations Mycommon = null;
        public Invoice(bool IsLocal)
        {
            Mycommon = new CommonOperations(IsLocal); 
        }
        ~Invoice()
        {
            Mycommon.CloseDB(); 
        }
        

        public string SaveInvoice(InvoiceDataTypes.InvoiceDataType _SaveData)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblrecievebleinvoice ("
          + "InvoiceNo,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Dr,"
          + "FDr,"
          + "InvoiceStatus,"
          + "TrRef,"
          + "TrUser,"
          + "TrDate,"
          + "TrTime,"
          + "AccPeriod,"
          + "CompanyID,"
          + "RcvFromCatID,"
          + "RcvFromID,"
          + "RcvAmount,"
          + "TobeRcvDate,"
          + "InvoiceDate)"
           + " Values ("
           + "@InvoiceNo,"
           + "@Description,"
           + "@AccountID,"
           + "@CurRate,"
           + "@Dr,"
           + "@FDr,"
           + "@InvoiceStatus,"
           + "@TrRef,"
           + "@TrUser,"
           + "@TrDate,"
           + "@TrTime,"
           + "@AccPeriod,"
           + "@CompanyID,"
           + "@RcvFromCatID,"
           + "@RcvFromID,"
           + "@RcvAmount,"
           + "@TobeRcvDate,"
           + "@InvoiceDate)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", _SaveData.InvoiceNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _SaveData.FDr);
                oSqlCommand.Parameters.AddWithValue("@InvoiceStatus", _SaveData.InvoiceStatus);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _SaveData.TrRef);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _SaveData.TrUser);
                oSqlCommand.Parameters.AddWithValue("@TrDate", _SaveData.TrDate);
                oSqlCommand.Parameters.AddWithValue("@TrTime", _SaveData.TrTime);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _SaveData.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@RcvFromCatID", _SaveData.RcvFromCatID);
                oSqlCommand.Parameters.AddWithValue("@RcvFromID", _SaveData.RcvFromID);
                oSqlCommand.Parameters.AddWithValue("@RcvAmount", _SaveData.RcvAmount);
                oSqlCommand.Parameters.AddWithValue("@TobeRcvDate", _SaveData.TobeRcvDate);
                oSqlCommand.Parameters.AddWithValue("@InvoiceDate", _SaveData.InvoiceDate);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Save Invoice");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string SaveInvoiceInTrans(InvoiceDataTypes.InvoiceDataType _SaveData,MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblrecievebleinvoice ("
          + "InvoiceNo,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Dr,"
          + "FDr,"
          + "InvoiceStatus,"
          + "TrRef,"
          + "TrUser,"
          + "TrDate,"
          + "TrTime,"
          + "AccPeriod,"
          + "CompanyID,"
          + "RcvFromCatID,"
          + "RcvFromID,"
          + "RcvAmount,"
          + "TobeRcvDate,"
          + "InvoiceDate)"
           + " Values ("
           + "@InvoiceNo,"
           + "@Description,"
           + "@AccountID,"
           + "@CurRate,"
           + "@Dr,"
           + "@FDr,"
           + "@InvoiceStatus,"
           + "@TrRef,"
           + "@TrUser,"
           + "curdate(),"
           + "curtime(),"
           + "@AccPeriod,"
           + "@CompanyID,"
           + "@RcvFromCatID,"
           + "@RcvFromID,"
           + "@RcvAmount,"
           + "@TobeRcvDate,"
           + "@InvoiceDate)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", _SaveData.InvoiceNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _SaveData.FDr);
                oSqlCommand.Parameters.AddWithValue("@InvoiceStatus", _SaveData.InvoiceStatus);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _SaveData.TrRef);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _SaveData.TrUser);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _SaveData.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@RcvFromCatID", _SaveData.RcvFromCatID);
                oSqlCommand.Parameters.AddWithValue("@RcvFromID", _SaveData.RcvFromID);
                oSqlCommand.Parameters.AddWithValue("@RcvAmount", _SaveData.RcvAmount);
                oSqlCommand.Parameters.AddWithValue("@TobeRcvDate", _SaveData.TobeRcvDate);
                oSqlCommand.Parameters.AddWithValue("@InvoiceDate", _SaveData.InvoiceDate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon,"Save Invoice");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string Save(InvoiceDataTypes.InvoiceDataType _SaveData)
            {
            #region Initial Open
           string respond = "";
            MySql.Data.MySqlClient.MySqlTransaction Mytrans;
            MySqlConnection CurCon = new MySqlConnection();
            CurCon = Mycommon.AccountConnection;
            if (CurCon.State == ConnectionState.Closed)
                CurCon.Open();
            Mytrans = Mycommon.AccountConnection.BeginTransaction();
            MySqlCommand oSqlCommand = new MySqlCommand();
            #endregion
            if (!ExistInvoice(_SaveData.InvoiceNo, CurCon))
                {
                    respond = SaveInvoiceInTrans(_SaveData, CurCon);
                    if (respond != "True")
                        {
                            Mytrans.Rollback();
                            CurCon.Close();
                            return respond;
                        }
                    else
                        {
                        if (_SaveData.InvoiceDtails.Count > 0)
                            {
                            foreach (InvoiceDataTypes.InvoiceDetailsDataType _Oneitem in _SaveData.InvoiceDtails)
                                {
                                if (!ExistInvoiceDetails(_Oneitem.ItemNo, _Oneitem.InvoiceNO, CurCon))
                                    {
                                        respond = SaveInvoiceDetails(_Oneitem, CurCon);
                                        if (respond != "True")
                                        {
                                            Mytrans.Rollback();
                                            CurCon.Close();
                                            return respond;
                                        }
                                    }
                                else
                                    {
                                        respond = UpdateInvoiceDetails(_Oneitem, CurCon);
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
                        else
                            {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return "No Details to save";
                            }
                        }
                }
            else
                {
                    Mytrans.Rollback();
                    CurCon.Close();
                    return "Use Update Option";
                }

            }
        public string Update(InvoiceDataTypes.InvoiceDataType _SaveData)
        {
        #region Initial Open
        string respond = "";
        MySql.Data.MySqlClient.MySqlTransaction Mytrans;
        MySqlConnection CurCon = new MySqlConnection();
        CurCon = Mycommon.AccountConnection;
        if (CurCon.State == ConnectionState.Closed)
            CurCon.Open();
        Mytrans = Mycommon.AccountConnection.BeginTransaction();
        MySqlCommand oSqlCommand = new MySqlCommand();
        #endregion
        if (ExistInvoice(_SaveData.InvoiceNo, CurCon))
            {
            respond = UpdateInvoiceInTrance(_SaveData, CurCon);
            if (respond != "True")
                {
                Mytrans.Rollback();
                CurCon.Close();
                return respond;
                }
            else
                {
                if (_SaveData.InvoiceDtails.Count > 0)
                    {
                    foreach (InvoiceDataTypes.InvoiceDetailsDataType _Oneitem in _SaveData.InvoiceDtails)
                        {
                        if (!ExistInvoiceDetails(_Oneitem.ItemNo, _Oneitem.InvoiceNO, CurCon))
                            {
                            respond = SaveInvoiceDetails(_Oneitem, CurCon);
                            if (respond != "True")
                                {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return respond;
                                }
                            }
                        else
                            {
                            respond = UpdateInvoiceDetails(_Oneitem, CurCon);
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
                else
                    {
                    Mytrans.Rollback();
                    CurCon.Close();
                    return "No Details to save";
                    }
                }
            }
        else
            {
            Mytrans.Rollback();
            CurCon.Close();
            return "Use Update Option";
            }
        }
        public int GetInvoiceStatus(string InvoiceNo)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "select InvoiceStatus from tblrecievebleinvoice where InvoiceNo=@InvoiceNo";
            oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            DataRow r = Mycommon.GetDataRowAccount (sqlQuery,oSqlCommand, "Get InvoiceStatus");
            if (r != null)
                {
                    int i = 0;
                    bool resp = int.TryParse(r["InvoiceStatus"].ToString(), out i);
                    return i;
                }
            else
                return 0;
        }
        public string InvoiceApproved(string InvoiceNo, string User1)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblrecievebleinvoice Set "
                 + "InvoiceStatus=2,"
                 + "ApproveBy=@ApproveBy,"
                 + "ApproveDate=curdate(),"
                 + "ApproveTime=curtime()"
                 + " Where 1=1 "
                 + " and InvoiceNo=@InvoiceNo";
            oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            oSqlCommand.Parameters.AddWithValue("@ApproveBy", User1);
            string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Update as Approved");
            return respond;
        }
        public string InvoiceAccountUpdated(string InvoiceNo, string User1,MySqlConnection _ActCon)
            {
                MySqlCommand oSqlCommand = new MySqlCommand();
                string sqlQuery = "Update tblrecievebleinvoice Set "
                     + "InvoiceStatus=3,"
                     + "AcountedBy=@AcountedBy,"
                     + "AccountDate=curdate(),"
                     + "AccountedTime=curtime()"
                     + " Where 1=1 "
                     + " and InvoiceNo=@InvoiceNo";
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                oSqlCommand.Parameters.AddWithValue("@AcountedBy", User1);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActCon, "Update as Account Updated");
                return respond;
            }
        public string SendToApprovel(string InvoiceNo,string User1)
        {
              MySqlCommand oSqlCommand = new MySqlCommand();
              string sqlQuery = "Update tblrecievebleinvoice Set "
                   + "InvoiceStatus=1,"
                   + "TrUser=@TrUser,"
                   + "TrDate=curdate(),"
                   + "TrTime=curtime()"
                   + " Where 1=1 "
                   + " and InvoiceNo=@InvoiceNo";
              oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
              oSqlCommand.Parameters.AddWithValue("@TrUser", User1);
              string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Update Send To Approvel");
              return respond;
        }

        public string UpdateInvoice(InvoiceDataTypes.InvoiceDataType _Update)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblrecievebleinvoice Set "
                + "InvoiceNo=@InvoiceNo,"
                + "Description=@Description,"
                + "AccountID=@AccountID,"
                + "CurRate=@CurRate,"
                + "Dr=@Dr,"
                + "FDr=@FDr,"
                + "InvoiceStatus=@InvoiceStatus,"
                + "TrRef=@TrRef,"
                + "TrUser=@TrUser,"
                + "TrDate=@TrDate,"
                + "TrTime=@TrTime,"
                + "AccPeriod=@AccPeriod,"
                + "CompanyID=@CompanyID,"
                + "RcvFromCatID=@RcvFromCatID,"
                + "RcvFromID=@RcvFromID,"
                + "RcvAmount=@RcvAmount,"
                + "TobeRcvDate=@TobeRcvDate,"
                + "InvoiceDate=@InvoiceDate"
                + " Where 1=1 "
                + " and InvoiceNo=@InvoiceNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", _Update.InvoiceNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _Update.FDr);
                oSqlCommand.Parameters.AddWithValue("@InvoiceStatus", _Update.InvoiceStatus);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _Update.TrRef);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _Update.TrUser);
                oSqlCommand.Parameters.AddWithValue("@TrDate", _Update.TrDate);
                oSqlCommand.Parameters.AddWithValue("@TrTime", _Update.TrTime);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _Update.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@RcvFromCatID", _Update.RcvFromCatID);
                oSqlCommand.Parameters.AddWithValue("@RcvFromID", _Update.RcvFromID);
                oSqlCommand.Parameters.AddWithValue("@RcvAmount", _Update.RcvAmount);
                oSqlCommand.Parameters.AddWithValue("@TobeRcvDate", _Update.TobeRcvDate);
                oSqlCommand.Parameters.AddWithValue("@InvoiceDate", _Update.InvoiceDate);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Update Invoice");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdateInvoiceInTrance(InvoiceDataTypes.InvoiceDataType _Update,MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblrecievebleinvoice Set "
                + "InvoiceNo=@InvoiceNo,"
                + "Description=@Description,"
                + "AccountID=@AccountID,"
                + "CurRate=@CurRate,"
                + "Dr=@Dr,"
                + "FDr=@FDr,"
                + "InvoiceStatus=@InvoiceStatus,"
                + "TrRef=@TrRef,"
                + "TrUser=@TrUser,"
                + "TrDate=curdate(),"
                + "TrTime=curtime(),"
                + "AccPeriod=@AccPeriod,"
                + "CompanyID=@CompanyID,"
                + "RcvFromCatID=@RcvFromCatID,"
                + "RcvFromID=@RcvFromID,"
                + "RcvAmount=@RcvAmount,"
                + "TobeRcvDate=@TobeRcvDate,"
                + "InvoiceDate=@InvoiceDate"
                + " Where 1=1 "
                + " and InvoiceNo=@InvoiceNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", _Update.InvoiceNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _Update.FDr);
                oSqlCommand.Parameters.AddWithValue("@InvoiceStatus", _Update.InvoiceStatus);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _Update.TrRef);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _Update.TrUser);
                oSqlCommand.Parameters.AddWithValue("@TrDate", _Update.TrDate);
                oSqlCommand.Parameters.AddWithValue("@TrTime", _Update.TrTime);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _Update.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@RcvFromCatID", _Update.RcvFromCatID);
                oSqlCommand.Parameters.AddWithValue("@RcvFromID", _Update.RcvFromID);
                oSqlCommand.Parameters.AddWithValue("@RcvAmount", _Update.RcvAmount);
                oSqlCommand.Parameters.AddWithValue("@TobeRcvDate", _Update.TobeRcvDate);
                oSqlCommand.Parameters.AddWithValue("@InvoiceDate", _Update.InvoiceDate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActCon,"Update Invoice");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeleteInvoice(string InvoiceNo)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblrecievebleinvoice"
                + " Where 1=1 "
                + " and InvoiceNo=@InvoiceNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Delete Invoice");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistInvoice(string InvoiceNo)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select InvoiceNo from tblrecievebleinvoice"
                + " Where 1=1 "
                + " and InvoiceNo=@InvoiceNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exsist Invoice");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public bool ExistInvoice(string InvoiceNo, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select InvoiceNo from tblrecievebleinvoice"
                + " Where 1=1 "
                + " and InvoiceNo=@InvoiceNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand,_ActCon, "Exsist Invoice");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public string GetExistInvoice(string InvoiceNo, out InvoiceDataTypes.InvoiceDataType _ExistData)
            {
            _ExistData = new InvoiceDataTypes.InvoiceDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
                + "InvoiceNo,"
                + "Description,"
                + "AccountID,"
                + "CurRate,"
                + "Dr,"
                + "FDr,"
                + "InvoiceStatus,"
                + "TrRef,"
                + "TrUser,"
                + "TrDate,"
                + "TrTime,"
                + "AccPeriod,"
                + "CompanyID,"
                + "RcvFromCatID,"
                + "RcvFromID,"
                + "RcvAmount,"
                + "TobeRcvDate,"
                + "InvoiceDate,"
                + "ApproveBy,"
                + "ApproveDate,"
                + "ApproveTime,"
                + "AcountedBy,"
                + "AccountDate,"
                + "AccountedTime"
                + " from tblrecievebleinvoice"
                + " Where 1=1 "
                + " and InvoiceNo=@InvoiceNo";
            oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand,  "Get Exist data Invoice");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    _ExistData.InvoiceNo = r["InvoiceNo"].ToString();
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
                    int inInvoiceStatus = 0;
                    resp = int.TryParse(r["InvoiceStatus"].ToString(), out inInvoiceStatus);
                    _ExistData.InvoiceStatus = inInvoiceStatus;
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
                    int inRcvFromCatID = 0;
                    resp = int.TryParse(r["RcvFromCatID"].ToString(), out inRcvFromCatID);
                    _ExistData.RcvFromCatID = inRcvFromCatID;
                    int inRcvFromID = 0;
                    resp = int.TryParse(r["RcvFromID"].ToString(), out inRcvFromID);
                    _ExistData.RcvFromID = inRcvFromID;
                    decimal deRcvAmount = 0;
                    resp = decimal.TryParse(r["RcvAmount"].ToString(), out deRcvAmount);
                    _ExistData.RcvAmount = deRcvAmount;
                    DateTime dtTobeRcvDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["TobeRcvDate"].ToString(), out dtTobeRcvDate);
                    if (resp)
                        _ExistData.TobeRcvDate = dtTobeRcvDate;
                    else
                        _ExistData.TobeRcvDate = new DateTime(1900, 1, 1);
                    DateTime dtInvoiceDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["InvoiceDate"].ToString(), out dtInvoiceDate);
                    if (resp)
                        _ExistData.InvoiceDate = dtInvoiceDate;
                    else
                        _ExistData.InvoiceDate = new DateTime(1900, 1, 1);
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

                    List<InvoiceDataTypes.InvoiceDetailsDataType> _ExistDataList = new List<InvoiceDataTypes.InvoiceDetailsDataType>();
                    string respond = GetInvoiceDetailList(InvoiceNo, out _ExistDataList);
                    _ExistData.InvoiceDtails = _ExistDataList;
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

        public string SaveInvoiceDetails(InvoiceDataTypes.InvoiceDetailsDataType _SaveData,MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblrecievebleinvoicedetails ("
          + "ItemNo,"
          + "InvoiceNO,"
          + "AccID,"
          + "Description,"
          + "Cr,"
          + "FCr,"
          + "TrRef,"
          + "Vat)"
           + " Values ("
           + "@ItemNo,"
           + "@InvoiceNO,"
           + "@AccID,"
           + "@Description,"
           + "@Cr,"
           + "@FCr,"
           + "@TrRef,"
           + "@Vat)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _SaveData.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNO", _SaveData.InvoiceNO);
                oSqlCommand.Parameters.AddWithValue("@AccID", _SaveData.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@Cr", _SaveData.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _SaveData.FCr);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _SaveData.TrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _SaveData.Vat);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon, "Save InvoiceDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdateInvoiceDetails(InvoiceDataTypes.InvoiceDetailsDataType _Update,MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblrecievebleinvoicedetails Set "
                + "ItemNo=@ItemNo,"
                + "InvoiceNO=@InvoiceNO,"
                + "AccID=@AccID,"
                + "Description=@Description,"
                + "Cr=@Cr,"
                + "FCr=@FCr,"
                + "TrRef=@TrRef,"
                + "Vat=@Vat"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and InvoiceNO=@InvoiceNO";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", _Update.ItemNo);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNO", _Update.InvoiceNO);
                oSqlCommand.Parameters.AddWithValue("@AccID", _Update.AccID);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@Cr", _Update.Cr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _Update.FCr);
                oSqlCommand.Parameters.AddWithValue("@TrRef", _Update.TrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _Update.Vat);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon, "Update InvoiceDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeleteInvoiceDetails(int ItemNo, string InvoiceNO)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblrecievebleinvoicedetails"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and InvoiceNO=@InvoiceNO";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNO", InvoiceNO);
                string respond = Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand, null, "Delete InvoiceDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistInvoiceDetails(int ItemNo, string InvoiceNO,MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select ItemNo from tblrecievebleinvoicedetails"
                + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and InvoiceNO=@InvoiceNO";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNO", InvoiceNO);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand, _ActCon,"Exist invoiceDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public DataTable GetExistInvoiceDetails(string InvoiceNO)
        {
        MySqlCommand oSqlCommand = new MySqlCommand();
        string sqlQuery = "Select "
              + "AccID,(select AccountName from accountname where accountname.AccountID=tblrecievebleinvoicedetails.AccID) as acname,"
              + "Description,"
              + "Cr,"
              + "FCr,"
               + "Vat,ItemNo"
              + " from tblrecievebleinvoicedetails"
              + " Where 1=1 "
              + " and InvoiceNO=@InvoiceNO";
              oSqlCommand.Parameters.AddWithValue("@InvoiceNO", InvoiceNO);
              DataTable tb = Mycommon.GetDataTableAccount(sqlQuery, oSqlCommand, "get Invoice List");
              return tb;
            }
        public string GetInvoiceDetailList(string InvoiceNO, out List<InvoiceDataTypes.InvoiceDetailsDataType> _ExistDataList)
            {
                _ExistDataList = new List<InvoiceDataTypes.InvoiceDetailsDataType>();
                try
                    {
                    MySqlCommand oSqlCommand = new MySqlCommand();
                    string sqlQuery = "Select ItemNo, InvoiceNO   from tblrecievebleinvoicedetails"
                   + " Where 1=1 "
                   + " and InvoiceNO=@InvoiceNO";
                    oSqlCommand.Parameters.AddWithValue("@InvoiceNO", InvoiceNO);
                    DataTable tb = Mycommon.GetDataTableAccount(sqlQuery, oSqlCommand, "get Invoice List");
                    if (tb != null)
                        {
                        foreach (DataRow r in tb.Rows)
                            {
                                InvoiceDataTypes.InvoiceDetailsDataType _ExOneData = new InvoiceDataTypes.InvoiceDetailsDataType();
                                int i = 0;
                                bool resp = int.TryParse(r["ItemNo"].ToString(), out i);
                                string respond = "";
                                respond = GetExistInvoiceDetails(i, InvoiceNO, out _ExOneData);
                                if (respond == "True")
                                    _ExistDataList.Add(_ExOneData);
                            }
                        return "True";
                        }
                    return "No data Found";
                    }
                catch (Exception ex)
                    {
                    return ex.Message;
                    }
            }
        public string GetExistInvoiceDetails(int ItemNo, string InvoiceNO, out InvoiceDataTypes.InvoiceDetailsDataType _ExistData)
            {
            _ExistData = new InvoiceDataTypes.InvoiceDetailsDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "ItemNo,"
          + "InvoiceNO,"
          + "AccID,"
          + "Description,"
          + "Cr,"
          + "FCr,"
          + "TrRef,"
          + "Vat"
          + " from tblrecievebleinvoicedetails"
          + " Where 1=1 "
                + " and ItemNo=@ItemNo"
                + " and InvoiceNO=@InvoiceNO";
            oSqlCommand.Parameters.AddWithValue("@ItemNo", ItemNo);
            oSqlCommand.Parameters.AddWithValue("@InvoiceNO", InvoiceNO);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand,  "Get Exist data InvoiceDetails");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    int inItemNo = 0;
                    resp = int.TryParse(r["ItemNo"].ToString(), out inItemNo);
                    _ExistData.ItemNo = inItemNo;
                    _ExistData.InvoiceNO = r["InvoiceNO"].ToString();
                    _ExistData.AccID = r["AccID"].ToString();
                    _ExistData.Description = r["Description"].ToString();
                    decimal deCr = 0;
                    resp = decimal.TryParse(r["Cr"].ToString(), out deCr);
                    _ExistData.Cr = deCr;
                    decimal deFCr = 0;
                    resp = decimal.TryParse(r["FCr"].ToString(), out deFCr);
                    _ExistData.FCr = deFCr;
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

        

        public void LoadInvoice(ComboBox cmb, int Customer)
        {
            string sql1 ="SELECT tblshinvoiceheader.InvoiceID "
                + " FROM tblcustomer INNER JOIN tsfs.tblshinvoiceheader ON tblcustomer.Customer = tblshinvoiceheader.Consignee "
                + " where tblshinvoiceheader.IsAccounted=0 and   tblcustomer.Sysid=" + Customer;
            Mycommon.LoadDatatoComboWithOutBind(cmb, sql1, "InvoiceID", true);
        }
        public void LoadInvoiceDetails(DataGridView _DataGrid, string InvoiceNo)
        {
            string sql1 = "SELECT UnitPrice,(ShippingQty * UnitPrice) as LineTotal,CellType, PONumber, ShippingQty,CustomerPart "
                + " FROM tblshinvoiceponumbers where InvoiceNo='" + InvoiceNo + "'";
            Mycommon.LoadDatatoTableWithoutBind(_DataGrid, sql1, "Load Invoice");
        }
        public void LoadInvoiceList(DataGridView _DataGrid,int Status)
        {
            string sql1 = "SELEct InvoiceNo, Description, AccountID, Dr, FDr, InvoiceDate "
                + " FROM tblrecievebleinvoice where  InvoiceStatus=" + Status;
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "get invoice list");
            Mycommon.LoadDatatoTableWithoutBind(_DataGrid, tb, "Load Invoice List");
        }
        public string GetSalesAccount(int CusID)
        {
            string sql1 = "select SPAccount from tblcustomer where  sysid=" + CusID;
            DataRow r = Mycommon.GetDataRow(sql1, "Get Customer Sales Acccount");
            if (r != null)
            {
                return r["SPAccount"].ToString();
            }
            else
                return "";
        }

        #region Pending Invoice
       
        public string SavePending(InvoiceDataTypes.PendingInvoiceDataType _SaveData, MySqlConnection _ActCon)
        {
                string respond = "";
            try
                {
                if (!ExistPendingReceivebleInvoice(_SaveData.Customer, _SaveData.InvoiceNo))
                    respond = SavePendingReceivebleInvoice(_SaveData, _ActCon);
                else
                    respond = UpdatePendingReceivebleInvoice(_SaveData, _ActCon);
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
        }
        private string UpdateReceivedAmount(int Customer,string InvoiceNumber, decimal ReceivedAmount, MySqlConnection _ActCon)
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
                    respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand,_ActCon, "Update RevQty");
                    respond = SetPendingInvoiceStatus(Customer, InvoiceNumber, _ActCon);
                    return respond;
                }
            catch (Exception ex)
                {
                    return ex.Message;
                }

        }
        private string SetPendingInvoiceStatus(int Customer, string InvoiceNumber, MySqlConnection _ActCon)
        {
                    MySqlCommand oSqlCommand = new MySqlCommand();
                    string respond = "";
                 try 
	                {
                        string sql1 = "Update tblpendingrecievebleinvoice set BillStatus=1 where InvoiceNo=@InvoiceNo and Customer=@Customer ";
                        oSqlCommand.Parameters.AddWithValue("@Customer", Customer);
                        respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, _ActCon, "Update RevQty");
                        return respond;
	                }
	                catch (Exception ex)
	                {
		                 return ex.Message;
	                }
        }
        public string SavePendingReceivebleInvoice(InvoiceDataTypes.PendingInvoiceDataType _SaveData)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblpendingrecievebleinvoice ("
          + "CompanyID,"
          + "Customer,"
          + "InvoiceNo,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Dr,"
          + "FDr,"
          + "BillStatus,"
          + "AccPeriod,"
          + "TotalAmount,"
          + "TobeRcvDate,"
          + "InvoiceNoDate)"
           + " Values ("
           + "@CompanyID,"
           + "@Customer,"
           + "@InvoiceNo,"
           + "@Description,"
           + "@AccountID,"
           + "@CurRate,"
           + "@Dr,"
           + "@FDr,"
           + "@BillStatus,"
           + "@AccPeriod,"
           + "@TotalAmount,"
           + "@TobeRcvDate,"
           + "@InvoiceNoDate)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@Customer", _SaveData.Customer);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", _SaveData.InvoiceNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _SaveData.FDr);
                oSqlCommand.Parameters.AddWithValue("@BillStatus", _SaveData.BillStatus);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _SaveData.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@TotalAmount", _SaveData.TotalAmount);
                oSqlCommand.Parameters.AddWithValue("@TobeRcvDate", _SaveData.TobeRcvDate);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNoDate", _SaveData.InvoiceNoDate);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Save PendingReceivebleInvoice");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string SavePendingReceivebleInvoice(InvoiceDataTypes.PendingInvoiceDataType _SaveData, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblpendingrecievebleinvoice ("
          + "CompanyID,"
          + "Customer,"
          + "InvoiceNo,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Dr,"
          + "FDr,"
          + "BillStatus,"
          + "AccPeriod,"
          + "TotalAmount,"
          + "TobeRcvDate,"
          + "InvoiceNoDate)"
           + " Values ("
           + "@CompanyID,"
           + "@Customer,"
           + "@InvoiceNo,"
           + "@Description,"
           + "@AccountID,"
           + "@CurRate,"
           + "@Dr,"
           + "@FDr,"
           + "@BillStatus,"
           + "@AccPeriod,"
           + "@TotalAmount,"
           + "@TobeRcvDate,"
           + "@InvoiceNoDate)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@Customer", _SaveData.Customer);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", _SaveData.InvoiceNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _SaveData.FDr);
                oSqlCommand.Parameters.AddWithValue("@BillStatus", _SaveData.BillStatus);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _SaveData.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@TotalAmount", _SaveData.TotalAmount);
                oSqlCommand.Parameters.AddWithValue("@TobeRcvDate", _SaveData.TobeRcvDate);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNoDate", _SaveData.InvoiceNoDate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActCon , "Save PendingReceivebleInvoice");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdatePendingReceivebleInvoice(InvoiceDataTypes.PendingInvoiceDataType _Update)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblpendingrecievebleinvoice Set "
                + "CompanyID=@CompanyID,"
                + "Customer=@Customer,"
                + "InvoiceNo=@InvoiceNo,"
                + "Description=@Description,"
                + "AccountID=@AccountID,"
                + "CurRate=@CurRate,"
                + "Dr=@Dr,"
                + "FDr=@FDr,"
                + "BillStatus=@BillStatus,"
                + "AccPeriod=@AccPeriod,"
                + "TotalAmount=@TotalAmount,"
                + "TobeRcvDate=@TobeRcvDate,"
                + "InvoiceNoDate=@InvoiceNoDate"
                + " Where 1=1 "
                + " and Customer=@Customer"
                + " and InvoiceNo=@InvoiceNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@Customer", _Update.Customer);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", _Update.InvoiceNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _Update.FDr);
                oSqlCommand.Parameters.AddWithValue("@BillStatus", _Update.BillStatus);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _Update.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@TotalAmount", _Update.TotalAmount);
                oSqlCommand.Parameters.AddWithValue("@TobeRcvDate", _Update.TobeRcvDate);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNoDate", _Update.InvoiceNoDate);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Update PendingReceivebleInvoice");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdatePendingReceivebleInvoice(InvoiceDataTypes.PendingInvoiceDataType _Update, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblpendingrecievebleinvoice Set "
                + "CompanyID=@CompanyID,"
                + "Customer=@Customer,"
                + "InvoiceNo=@InvoiceNo,"
                + "Description=@Description,"
                + "AccountID=@AccountID,"
                + "CurRate=@CurRate,"
                + "Dr=@Dr,"
                + "FDr=@FDr,"
                + "BillStatus=@BillStatus,"
                + "AccPeriod=@AccPeriod,"
                + "TotalAmount=@TotalAmount,"
                + "TobeRcvDate=@TobeRcvDate,"
                + "InvoiceNoDate=@InvoiceNoDate"
                + " Where 1=1 "
                + " and Customer=@Customer"
                + " and InvoiceNo=@InvoiceNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@Customer", _Update.Customer);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", _Update.InvoiceNo);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _Update.FDr);
                oSqlCommand.Parameters.AddWithValue("@BillStatus", _Update.BillStatus);
                oSqlCommand.Parameters.AddWithValue("@AccPeriod", _Update.AccPeriod);
                oSqlCommand.Parameters.AddWithValue("@TotalAmount", _Update.TotalAmount);
                oSqlCommand.Parameters.AddWithValue("@TobeRcvDate", _Update.TobeRcvDate);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNoDate", _Update.InvoiceNoDate);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon ,"Update PendingReceivebleInvoice");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeletePendingReceivebleInvoice(int Customer, string InvoiceNo)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblpendingrecievebleinvoice"
                + " Where 1=1 "
                + " and Customer=@Customer"
                + " and InvoiceNo=@InvoiceNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@Customer", Customer);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                string respond = Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand, null, "Delete PendingReceivebleInvoice");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistPendingReceivebleInvoice(int Customer, string InvoiceNo)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select InvoiceNo from tblpendingrecievebleinvoice"
                + " Where 1=1 "
                + " and Customer=@Customer"
                + " and InvoiceNo=@InvoiceNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@Customer", Customer);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist in Invoice");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public bool ExistPendingReceivebleInvoice(int Customer, string InvoiceNo,MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select InvoiceNo from tblpendingrecievebleinvoice"
                + " Where 1=1 "
                + " and Customer=@Customer"
                + " and InvoiceNo=@InvoiceNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@Customer", Customer);
                oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand, _ActCon,"Exist Rending Invoice");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }

        public decimal GetPendingBalance(int Customer,string InvoiceNo)
        { 
            string sql1="select (FDr-RevievedAmount) as Balance from tblpendingrecievebleinvoice "
                + " where InvoiceNo=@InvoiceNo and Customer=@Customer";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@Customer", Customer);
            oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);

            DataRow r = Mycommon.GetDataRowAccount(sql1,oSqlCommand, "Get Receiveble balance");
            if (r != null)
                {
                    decimal d = 0;
                    bool resp = decimal.TryParse(r[""].ToString(), out d);
                    return d;
                }
            else
                return 0;

        }
        public string GetExistPendingReceivebleInvoice(int Customer, string InvoiceNo, out InvoiceDataTypes.PendingInvoiceDataType  _ExistData)
            {
            _ExistData = new InvoiceDataTypes.PendingInvoiceDataType();
            
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "CompanyID,"
          + "Customer,"
          + "InvoiceNo,"
          + "Description,"
          + "AccountID,"
          + "CurRate,"
          + "Dr,"
          + "FDr,"
          + "BillStatus,"
          + "AccPeriod,"
          + "TotalAmount,"
          + "RevievedAmount,"
          + "TobeRcvDate,"
          + "InvoiceNoDate"
          + " from tblpendingrecievebleinvoice"
          + " Where 1=1 "
                + " and Customer=@Customer"
                + " and InvoiceNo=@InvoiceNo";
            oSqlCommand.Parameters.AddWithValue("@Customer", Customer);
            oSqlCommand.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
            DataRow r = Mycommon.GetDataRow(sqlQuery, oSqlCommand, null, "Get Exist data PendingReceivebleInvoice");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    int inCompanyID = 0;
                    resp = int.TryParse(r["CompanyID"].ToString(), out inCompanyID);
                    _ExistData.CompanyID = inCompanyID;
                    int inCustomer = 0;
                    resp = int.TryParse(r["Customer"].ToString(), out inCustomer);
                    _ExistData.Customer = inCustomer;
                    _ExistData.InvoiceNo = r["InvoiceNo"].ToString();
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
                    int inBillStatus = 0;
                    resp = int.TryParse(r["BillStatus"].ToString(), out inBillStatus);
                    _ExistData.BillStatus = inBillStatus;
                    int inAccPeriod = 0;
                    resp = int.TryParse(r["AccPeriod"].ToString(), out inAccPeriod);
                    _ExistData.AccPeriod = inAccPeriod;
                    decimal deTotalAmount = 0;
                    resp = decimal.TryParse(r["TotalAmount"].ToString(), out deTotalAmount);
                    _ExistData.TotalAmount = deTotalAmount;
                    decimal deRevievedAmount = 0;
                    resp = decimal.TryParse(r["RevievedAmount"].ToString(), out deRevievedAmount);
                    _ExistData.RevievedAmount = deRevievedAmount;
                    DateTime dtTobeRcvDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["TobeRcvDate"].ToString(), out dtTobeRcvDate);
                    if (resp)
                        _ExistData.TobeRcvDate = dtTobeRcvDate;
                    else
                        _ExistData.TobeRcvDate = new DateTime(1900, 1, 1);
                    DateTime dtInvoiceNoDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["InvoiceNoDate"].ToString(), out dtInvoiceNoDate);
                    if (resp)
                        _ExistData.InvoiceNoDate = dtInvoiceNoDate;
                    else
                        _ExistData.InvoiceNoDate = new DateTime(1900, 1, 1);
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
    public class InvoiceDataTypes
    {
        public struct InvoiceDataType
            {
            public string InvoiceNo;
            public string Description;
            public string AccountID;
            public decimal CurRate;
            public decimal Dr;
            public decimal FDr;
            public int InvoiceStatus;
            public string TrRef;
            public string TrUser;
            public DateTime TrDate;
            public string TrTime;
            public int AccPeriod;
            public int CompanyID;
            public int RcvFromCatID;
            public int RcvFromID;
            public decimal RcvAmount;
            public DateTime TobeRcvDate;
            public DateTime InvoiceDate;
            public string ApproveBy;
            public DateTime ApproveDate;
            public string ApproveTime;
            public string AcountedBy;
            public DateTime AccountDate;
            public string AccountedTime;
            public List<InvoiceDetailsDataType> InvoiceDtails;
            }
        public struct InvoiceDetailsDataType
            {
            public int ItemNo;
            public string InvoiceNO;
            public string AccID;
            public string Description;
            public decimal Cr;
            public decimal FCr;
            public string TrRef;
            public decimal Vat;
            }
        public struct PendingInvoiceDataType
            {
            public int CompanyID;
            public int Customer;
            public string InvoiceNo;
            public string Description;
            public string AccountID;
            public decimal CurRate;
            public decimal Dr;
            public decimal FDr;
            public int BillStatus;
            public int AccPeriod;
            public decimal TotalAmount;
            public decimal RevievedAmount;
            public DateTime TobeRcvDate;
            public DateTime InvoiceNoDate;
            }
    }
    }
