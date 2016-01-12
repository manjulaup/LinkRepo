//Create by SATICIN On 21/Dec/2015
//
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using BusinessLayer.CommonOperation;
using DataLayer.DataService;  
using System.Drawing;
namespace BusinessLayer.Journals
    {
    public class Journal
        {
        private DataService Mycommon = null;

        public Journal(bool IsLocal)
            {
                Mycommon = new DataService(IsLocal);
            }
        ~Journal()
        {
            Mycommon.CloseDB();
        }
        private string GetNewJEnumber(MySqlConnection _ActCon)
        {
            string sql1 = "SELECT max(substr(JounalID,4)) as MaxID FROM   tbljournalheader";
            MySqlCommand oSqlCommand = new MySqlCommand();
            DataRow r = Mycommon.GetDataRowAccountTrans(sql1,oSqlCommand, _ActCon,"Get MaxID");
            if (r != null)
            {
                int i = 0;
                bool resp = int.TryParse(r["MaxID"].ToString(), out i);
                i = i + 1;
                return "JE-" + i.ToString("0#########"); 

            }
            else 
                return "JE-000000001";
        }
        public string UpdateTrNumber(string JENumber,int IndexID,string TrRef)
        {
            string sql1 = "Update tbljournaldetails set AcTrRef=@TrRef where JeNumber=@JeNumber and LineRef=@LineRef";
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
                {
                    oSqlCommand.Parameters.AddWithValue("@JeNumber", JENumber);
                    oSqlCommand.Parameters.AddWithValue("@LineRef", IndexID);
                    oSqlCommand.Parameters.AddWithValue("@TrRef", TrRef);
                    string respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand, "Update TR ref on Journal details");
                    return respond;
                }
            catch (Exception ex)
                {
                    return ex.Message;
                }
        }
        public string SendToapproval(string JE, string UserN)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sql1 = "Update tbljournalheader set JeUser=@SendBy,JeStatus=1,ApproveDate=curdate(),ApproveTime=curTime() where JounalID=@JounalID";
            oSqlCommand.Parameters.AddWithValue("@JounalID", JE);
            oSqlCommand.Parameters.AddWithValue("@SendBy", UserN);

            string respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand, "Update TR ref on Journal details");
            return respond;

            }
        public string JEApproved(string JE,string UserN)
        {
                MySqlCommand oSqlCommand = new MySqlCommand();
                string sql1 = "Update tbljournalheader set ApproveBy=@ApproveBy,JeStatus=2,ApproveDate=curdate(),ApproveTime=curTime() where JounalID=@JounalID";
                oSqlCommand.Parameters.AddWithValue("@JounalID", JE);
                oSqlCommand.Parameters.AddWithValue("@ApproveBy", UserN);
                string respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand, "Update TR ref on Journal details");
                return respond;

        }

        public void SetJeStatus(string JE)
        {
            string sql1 = "Update tbljournalheader set JeStatus=0 where JounalID=@JounalID";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@JounalID", JE);
            string respond = Mycommon.ExicuteAnyCommandAccount(sql1, oSqlCommand, "Update status");
        }
        public int GetJEStatus(string JEN)
        {
            string sql1 = "select JeStatus from tbljournalheader where JounalID=@JounalID";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@JounalID", JEN);
            try
                {
                DataRow r = Mycommon.GetDataRowAccount(sql1, oSqlCommand, "Get JE status");
                int i = 0;
                if (r != null)
                    {
                    bool resp = int.TryParse(r["JeStatus"].ToString(), out i);
                    return i;
                    }
                else
                    return 0;
                }
            catch (Exception ex)
                {

                return 0;
                }
            
        
        }
        public  DataTable GetJEListByStatus(int Status)
        {
            string slq1 = "";
            if (Status != -1)
                slq1 = "SELECT JounalID, Reason, Jedate  FROM   tbljournalheader where JeStatus=" + Status;
            else
                slq1 = "SELECT JounalID, Reason, Jedate  FROM   tbljournalheader";
            DataTable tb = Mycommon.GetDataTableAccount(slq1, "Get Je List");
            return tb;
        }
        public DataTable GetJEDetailsList(string JENumber)
        {
            string sql1 = "SELECT tbljournaldetails.AccountID, accountname.AccountName, tbljournaldetails.Cr, tbljournaldetails.Dr, tbljournaldetails.CurRate, tbljournaldetails.Vat "
                + " FROM accounterp.accountname "
                + " INNER JOIN accounterp.tbljournaldetails ON accountname.AccountID = tbljournaldetails.AccountID "
                + " where tbljournaldetails.JeNumber='" + JENumber + "'";
            DataTable tb = Mycommon.GetDataTableAccount(sql1, "Get Je List");
            return tb;
        }
        public string Save(JournalType.JournalDataDataType _SaveData,out string JeNumber)
        {
                JeNumber = "";
                MySql.Data.MySqlClient.MySqlTransaction Mytrans;
                MySqlConnection CurCon = new MySqlConnection();
                CurCon = Mycommon.AccountConnection;
                string respond = "";
                decimal FullPayment = 0;
                if (CurCon.State == ConnectionState.Open)
                    {
                    CurCon.Close();
                    CurCon.Open();
                    }
                else
                    {
                    CurCon.Open();
                    }
                Mytrans = Mycommon.AccountConnection.BeginTransaction();
                respond = SaveJournalData(_SaveData, CurCon, out JeNumber);
                if (respond != "True")
                    {
                    Mytrans.Rollback();
                    CurCon.Close();
                    return respond;
                    }
                else
                    {
                    foreach (JournalType.JournalDetailsDataType item in _SaveData.DetailList)
                        {
                        JournalType.JournalDetailsDataType OneItem = new JournalType.JournalDetailsDataType();
                        OneItem = item;
                        OneItem.JeNumber = JeNumber;
                        if (!ExistJournalDetails(item.JeNumber, item.LineRef, CurCon))
                            {


                            respond = SaveJournalDetails(OneItem, CurCon);
                            if (respond != "True")
                                {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return respond;
                                }
                            }
                        else
                            {
                            respond = UpdateJournalDetails(OneItem, CurCon);
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
        public string Update(JournalType.JournalDataDataType _SaveData)
            {
            MySql.Data.MySqlClient.MySqlTransaction Mytrans;
            MySqlConnection CurCon = new MySqlConnection();
            CurCon = Mycommon.AccountConnection;
            string respond = "";
            decimal FullPayment = 0;
            if (CurCon.State == ConnectionState.Open)
                {
                CurCon.Close();
                CurCon.Open();
                }
            else
                {
                CurCon.Open();
                }
            Mytrans = Mycommon.AccountConnection.BeginTransaction();
            respond = UpdateJournalData(_SaveData, CurCon);
            if (respond != "True")
                {
                Mytrans.Rollback();
                CurCon.Close();
                return respond;
                }
            else
                {
                foreach (JournalType.JournalDetailsDataType item in _SaveData.DetailList)
                    {
                    if (!ExistJournalDetails(item.JeNumber, item.LineRef, CurCon))
                        {
                        respond = UpdateJournalDetails(item, CurCon);
                        if (respond != "True")
                            {
                            Mytrans.Rollback();
                            CurCon.Close();
                            return respond;
                            }
                        }
                    else
                        {
                        respond = UpdateJournalDetails(item, CurCon);
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

        public string SaveJournalData(JournalType.JournalDataDataType _SaveData, MySqlConnection _ActCon,out string JeNumber)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            JeNumber = GetNewJEnumber(_ActCon);
            string sqlQuery = "Insert Into tbljournalheader ("
          + "JounalID,"
          + "TimePeriod,"
          + "CompanyID,"
          + "Reason,"
          + "JeStatus,"
          + "Jedate,"
          + "JeUser,"
          + "LastAccDate,"
          + "JeTime)"
           + " Values ("
            + "@JounalID,"
           + "@TimePeriod,"
           + "@CompanyID,"
           + "@Reason,"
           + "@JeStatus,"
           + "@Jedate,"
           + "@JeUser,"
           + "curDate(),"
           + "curTime())";
            try
                {
              
                oSqlCommand.Parameters.AddWithValue("@JounalID", JeNumber);
                oSqlCommand.Parameters.AddWithValue("@TimePeriod", _SaveData.TimePeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@Reason", _SaveData.Reason);
                oSqlCommand.Parameters.AddWithValue("@JeStatus", _SaveData.JeStatus);
                oSqlCommand.Parameters.AddWithValue("@Jedate", _SaveData.Jedate);
                oSqlCommand.Parameters.AddWithValue("@JeUser", _SaveData.JeUser);
           
               
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand,_ActCon, "Save JournalData");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdateJournalData(JournalType.JournalDataDataType _Update,MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tbljournalheader Set "
              
                + "JounalID=@JounalID,"
                + "TimePeriod=@TimePeriod,"
                + "CompanyID=@CompanyID,"
                + "Reason=@Reason,"
                + "JeStatus=@JeStatus,"
                + "Jedate=@Jedate,"
                + "JeUser=@JeUser,"
                + "LastAccDate=curDate(),"
                + "JeTime=curTime()"
                + " Where 1=1 "
                + " and JounalID=@JounalID";
            try
                {
              
                oSqlCommand.Parameters.AddWithValue("@JounalID", _Update.JounalID);
                oSqlCommand.Parameters.AddWithValue("@TimePeriod", _Update.TimePeriod);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@Reason", _Update.Reason);
                oSqlCommand.Parameters.AddWithValue("@JeStatus", _Update.JeStatus);
                oSqlCommand.Parameters.AddWithValue("@Jedate", _Update.Jedate);
                oSqlCommand.Parameters.AddWithValue("@JeUser", _Update.JeUser);
                
                
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon, "Update JournalData");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeleteJournalData(string JounalID)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tbljournalheader"
                + " Where 1=1 "
                + " and JounalID=@JounalID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@JounalID", JounalID);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Delete JournalData");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistJournalData(string JounalID, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tbljournalheader"
                + " Where 1=1 "
                + " and JounalID=@JounalID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@JounalID", JounalID);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand,_ActCon,"Exist Journal");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }

        public bool ExistJournalData(string JounalID)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tbljournalheader"
                + " Where 1=1 "
                + " and JounalID=@JounalID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@JounalID", JounalID);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,  "Exist Journal");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public string GetExistJournalData(string JounalID, out JournalType.JournalDataDataType _ExistData)
            {
            _ExistData = new JournalType.JournalDataDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "SysID,"
          + "JounalID,"
          + "TimePeriod,"
          + "CompanyID,"
          + "Reason,"
          + "JeStatus,"
          + "Jedate,"
          + "JeUser,"
          + "LastAccDate,"
          + "JeTime,"
          + "ApproveBy,"
          + "ApproveDate,"
          + "ApproveTime,"
          + "AcountedBy,"
          + "AccountDate,"
          + "AccountedTime"
          + " from tbljournalheader"
          + " Where 1=1 "
                + " and JounalID=@JounalID";
            oSqlCommand.Parameters.AddWithValue("@JounalID", JounalID);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand, "Get Exist data JournalData");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    int inSysID = 0;
                    resp = int.TryParse(r["SysID"].ToString(), out inSysID);
                    _ExistData.SysID = inSysID;
                    _ExistData.JounalID = r["JounalID"].ToString();
                    int inTimePeriod = 0;
                    resp = int.TryParse(r["TimePeriod"].ToString(), out inTimePeriod);
                    _ExistData.TimePeriod = inTimePeriod;
                    int inCompanyID = 0;
                    resp = int.TryParse(r["CompanyID"].ToString(), out inCompanyID);
                    _ExistData.CompanyID = inCompanyID;
                    _ExistData.Reason = r["Reason"].ToString();
                    int inJeStatus = 0;
                    resp = int.TryParse(r["JeStatus"].ToString(), out inJeStatus);
                    _ExistData.JeStatus = inJeStatus;
                    DateTime dtJedate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["Jedate"].ToString(), out dtJedate);
                    if (resp)
                        _ExistData.Jedate = dtJedate;
                    else
                        _ExistData.Jedate = new DateTime(1900, 1, 1);
                    _ExistData.JeUser = r["JeUser"].ToString();
                    DateTime dtLastAccDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["LastAccDate"].ToString(), out dtLastAccDate);
                    if (resp)
                        _ExistData.LastAccDate = dtLastAccDate;
                    else
                        _ExistData.LastAccDate = new DateTime(1900, 1, 1);
                    _ExistData.JeTime = r["JeTime"].ToString();
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

                    List<JournalType.JournalDetailsDataType> _DataList = new List<JournalType.JournalDetailsDataType>();
                    string respond = GetJournalDetailsList(JounalID, out _DataList);
                    _ExistData.DetailList = _DataList;
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

        //==============

        public string SaveJournalDetails(JournalType.JournalDetailsDataType _SaveData,MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tbljournaldetails ("
              + "JeNumber,"
              + "LineRef,"
              + "AccountID,"
              + "Description,"
              + "CurRate,"
              + "Cr,"
              + "Dr,"
              + "AcTrRef,Vat)"
               + " Values ("
               + "@JeNumber,"
               + "@LineRef,"
               + "@AccountID,"
               + "@Description,"
               + "@CurRate,"
               + "@Cr,"
               + "@Dr,"
               + "@AcTrRef,@Vat)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@JeNumber", _SaveData.JeNumber);
                oSqlCommand.Parameters.AddWithValue("@LineRef", _SaveData.LineRef);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Cr", _SaveData.Cr);
                oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                oSqlCommand.Parameters.AddWithValue("@AcTrRef", _SaveData.AcTrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _SaveData.Vat);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon, "Save JournalDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdateJournalDetails(JournalType.JournalDetailsDataType _Update, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tbljournaldetails Set "
                + "JeNumber=@JeNumber,"
                + "LineRef=@LineRef,"
                + "AccountID=@AccountID,"
                + "Description=@Description,"
                + "CurRate=@CurRate,"
                + "Cr=@Cr,"
                + "Dr=@Dr,"
                + "AcTrRef=@AcTrRef,Vat=@Vat"
                + " Where 1=1 "
                + " and JeNumber=@JeNumber"
                + " and LineRef=@LineRef";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@JeNumber", _Update.JeNumber);
                oSqlCommand.Parameters.AddWithValue("@LineRef", _Update.LineRef);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Cr", _Update.Cr);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@AcTrRef", _Update.AcTrRef);
                oSqlCommand.Parameters.AddWithValue("@Vat", _Update.Vat);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _ActCon, "Update JournalDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeleteJournalDetails(string JeNumber, int LineRef)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tbljournaldetails"
                + " Where 1=1 "
                + " and JeNumber=@JeNumber"
                + " and LineRef=@LineRef";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@JeNumber", JeNumber);
                oSqlCommand.Parameters.AddWithValue("@LineRef", LineRef);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Delete JournalDetails");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistJournalDetails(string JeNumber, int LineRef, MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select LineRef from tbljournaldetails"
                + " Where 1=1 "
                + " and JeNumber=@JeNumber"
                + " and LineRef=@LineRef";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@JeNumber", JeNumber);
                oSqlCommand.Parameters.AddWithValue("@LineRef", LineRef);
                bool respond = Mycommon.ExistInTableAccountIntrance(sqlQuery, oSqlCommand,_ActCon,"Exsist Journal Details");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public string GetExistJournalDetails(string JeNumber, int LineRef, out JournalType.JournalDetailsDataType _ExistData)
            {
            _ExistData = new JournalType.JournalDetailsDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "JeNumber,"
          + "LineRef,"
          + "AccountID,"
          + "Description,"
          + "CurRate,"
          + "Cr,"
          + "Dr,"
          + "AcTrRef,Vat"
          + " from tbljournaldetails"
          + " Where 1=1 "
                + " and JeNumber=@JeNumber"
                + " and LineRef=@LineRef";
            oSqlCommand.Parameters.AddWithValue("@JeNumber", JeNumber);
            oSqlCommand.Parameters.AddWithValue("@LineRef", LineRef);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand,"Get Exist data JournalDetails");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    _ExistData.JeNumber = r["JeNumber"].ToString();
                    int inLineRef = 0;
                    resp = int.TryParse(r["LineRef"].ToString(), out inLineRef);
                    _ExistData.LineRef = inLineRef;
                    _ExistData.AccountID = r["AccountID"].ToString();
                    _ExistData.Description = r["Description"].ToString();
                    decimal deCurRate = 0;
                    resp = decimal.TryParse(r["CurRate"].ToString(), out deCurRate);
                    _ExistData.CurRate = deCurRate;
                    decimal deCr = 0;
                    resp = decimal.TryParse(r["Cr"].ToString(), out deCr);
                    _ExistData.Cr = deCr;
                    decimal deDr = 0;
                    resp = decimal.TryParse(r["Dr"].ToString(), out deDr);
                    _ExistData.Dr = deDr;

                    decimal deVat = 0;
                    resp = decimal.TryParse(r["Vat"].ToString(), out deVat);
                    _ExistData.Vat = deVat;
                    _ExistData.AcTrRef = r["AcTrRef"].ToString();
                   
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

        public string GetJournalDetailsList(string JeNumber, out List<JournalType.JournalDetailsDataType> _DataList)
        {
            _DataList = new List<JournalType.JournalDetailsDataType>();
            MySqlCommand oSqlCommand = new MySqlCommand();

            string sql1 = "Select JeNumber,LineRef from tbljournaldetails where JeNumber=@JeNumber";
            oSqlCommand.Parameters.AddWithValue("@JeNumber", JeNumber);
            DataTable tb = Mycommon.GetDataTableAccount(sql1, oSqlCommand, "Get Journal data List");
            if (tb != null)
                {
                foreach (DataRow r in tb.Rows )
                    {
                        JournalType.JournalDetailsDataType _OneItem = new JournalType.JournalDetailsDataType();
                        string respond = "";
                        string Je = "";
                        int Lineref = 0;
                        bool resp = int.TryParse(r["LineRef"].ToString(), out Lineref);
                        respond = GetExistJournalDetails(JeNumber, Lineref, out _OneItem);
                        if (respond == "True")
                            _DataList.Add(_OneItem);
                        else
                            return respond;
                    }
                return "True";
                }
            else
                return "No Data Found";
        }
        //==============
        public DataTable GetJernulDataListForGrid(string JEN)
        {
            string sql1 = "SELECT  tbljournaldetails.AccountID, accountname.AccountName, tbljournaldetails.Description, tbljournaldetails.Dr, tbljournaldetails.Cr,  ( tbljournaldetails.Dr/tbljournaldetails.CurRate) as FDR, ( tbljournaldetails.Cr/tbljournaldetails.CurRate) as FCR, tbljournaldetails.Vat ,tbljournaldetails.LineRef"
                + " FROM accounterp.accountname "
                + " INNER JOIN accounterp.tbljournaldetails ON accountname.AccountID = tbljournaldetails.AccountID "
                + " where tbljournaldetails.JeNumber=@JeNumber";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@JeNumber", JEN);
            DataTable tb = Mycommon.GetDataTableAccount(sql1, oSqlCommand, "Get Journal data List");
            return tb;
        }
        }
   
    public class JournalType
    {
        
        public struct JournalDataDataType
            {
            public int SysID;
            public string JounalID;
            public int TimePeriod;
            public int CompanyID;
            public string Reason;
            public int JeStatus;
            public DateTime Jedate;
            public string JeUser;
            public DateTime LastAccDate;
            public string JeTime;
            public string ApproveBy;
            public DateTime ApproveDate;
            public string ApproveTime;
            public string AcountedBy;
            public DateTime AccountDate;
            public string AccountedTime;
            public List<JournalDetailsDataType> DetailList;
            }
        public struct JournalDetailsDataType
            {
            public string JeNumber;
            public int LineRef;
            public string AccountID;
            public string Description;
            public decimal CurRate;
            public decimal Cr;
            public decimal Dr;
            public string AcTrRef;
            public decimal Vat;
            }
        }
    }
