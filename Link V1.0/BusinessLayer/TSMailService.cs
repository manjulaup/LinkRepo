//Create by SATICIN On 23/Dec/2013
//
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using BusinessLayer.CommonOperation;
using BusinessLayer.Emails;
namespace BusinessLayer.TSMailServices
{
    public class TSMailService
    {
        private CommonOperations Mycommon = null;
        private Email MyMail = new Email();
        #region MailService
        public string SaveTSMailService(TSMsilServiceType.TSMailServiceDataType _SaveData)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblservicetimer ("
          + "TimerID,"
          + "Occurence,"
          + "TriggerDate,"
          + "TriggerTime,"
          + "NofTime,"
          + "CurrentCount,"
          + "TriggerStatus,"
         // + "SQLStatement,"
          + "WhereC0ndition,"
          + "SearchText,"
          + "SearchValue,"
          + "CustomerID,MailListID,MailSubject,BoadyHeader,MailCategory)"
           + " Values ("
           + "@TimerID,"
           + "@Occurence,"
           + "@TriggerDate,"
           + "@TriggerTime,"
           + "@NofTime,"
           + "@CurrentCount,"
           + "@TriggerStatus,"
         //  + "@SQLStatement,"
           + "@WhereC0ndition,"
           + "@SearchText,"
           + "@SearchValue,"
           + "@CustomerID,@MailListID,@MailSubject,@BoadyHeader,@MailCategory)";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@TimerID", _SaveData.TimerID);
                oSqlCommand.Parameters.AddWithValue("@Occurence", _SaveData.Occurence);
                oSqlCommand.Parameters.AddWithValue("@TriggerDate", _SaveData.TriggerDate);
                oSqlCommand.Parameters.AddWithValue("@TriggerTime", _SaveData.TriggerTime);
                oSqlCommand.Parameters.AddWithValue("@NofTime", _SaveData.NofTime);
                oSqlCommand.Parameters.AddWithValue("@CurrentCount", _SaveData.CurrentCount);
                oSqlCommand.Parameters.AddWithValue("@TriggerStatus", _SaveData.TriggerStatus);
             //   oSqlCommand.Parameters.AddWithValue("@SQLStatement", _SaveData.SQLStatement);
                oSqlCommand.Parameters.AddWithValue("@WhereC0ndition", _SaveData.WhereC0ndition);
                oSqlCommand.Parameters.AddWithValue("@SearchText", _SaveData.SearchText);
                oSqlCommand.Parameters.AddWithValue("@SearchValue", _SaveData.SearchValue);
                oSqlCommand.Parameters.AddWithValue("@CustomerID", _SaveData.CustomerID);
                oSqlCommand.Parameters.AddWithValue("@MailListID", _SaveData.MailListID);
                //MailSubject,BoadyHeader,MailCategory
                oSqlCommand.Parameters.AddWithValue("@MailSubject", _SaveData.MailSubject);
                oSqlCommand.Parameters.AddWithValue("@BoadyHeader", _SaveData.BoadyHeader);
                oSqlCommand.Parameters.AddWithValue("@MailCategory", _SaveData.MailCategory);
                
                string respond = Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand, null, "Save TSMailService");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateTSMailService(string KeyVal, TSMsilServiceType.TSMailServiceDataType _Update)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblservicetimer Set "
                + "Occurence=@Occurence,"
                + "TriggerDate=@TriggerDate,"
                + "TriggerTime=@TriggerTime,"
                + "NofTime=@NofTime,"
                + "CurrentCount=@CurrentCount,"
                + "TriggerStatus=@TriggerStatus,"
                + "SQLStatement=@SQLStatement,"
                + "WhereC0ndition=@WhereC0ndition,"
                + "SearchText=@SearchText,"
                + "CustomerID=@CustomerID,"
                + "MailListID=@MailListID,"
                + "MailSubject=@MailSubject,"
                + "BoadyHeader=@BoadyHeader,"
                + "MailCategory=@MailCategory"
                + " Where 1=1 "
                + " and SearchValue=@SearchValue";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@Occurence", _Update.Occurence);
                oSqlCommand.Parameters.AddWithValue("@TriggerDate", _Update.TriggerDate);
                oSqlCommand.Parameters.AddWithValue("@TriggerTime", _Update.TriggerTime);
                oSqlCommand.Parameters.AddWithValue("@NofTime", _Update.NofTime);
                oSqlCommand.Parameters.AddWithValue("@CurrentCount", _Update.CurrentCount);
                oSqlCommand.Parameters.AddWithValue("@TriggerStatus", _Update.TriggerStatus);
                oSqlCommand.Parameters.AddWithValue("@SQLStatement", _Update.SQLStatement);
                oSqlCommand.Parameters.AddWithValue("@WhereC0ndition", _Update.WhereC0ndition);
                oSqlCommand.Parameters.AddWithValue("@SearchText", _Update.SearchText);
                oSqlCommand.Parameters.AddWithValue("@SearchValue", KeyVal);
                oSqlCommand.Parameters.AddWithValue("@CustomerID", _Update.CustomerID);
                oSqlCommand.Parameters.AddWithValue("@MailListID", _Update.MailListID);
                oSqlCommand.Parameters.AddWithValue("@MailSubject", _Update.MailSubject);
                oSqlCommand.Parameters.AddWithValue("@BoadyHeader", _Update.BoadyHeader);
                oSqlCommand.Parameters.AddWithValue("@MailCategory", _Update.MailCategory);
                string respond = Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand, null, "Update TSMailService");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateTSMailService(TSMsilServiceType.TSMailServiceDataType _Update)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblservicetimer Set "
                + "TimerID=@TimerID,"
                + "Occurence=@Occurence,"
                + "TriggerDate=@TriggerDate,"
                + "TriggerTime=@TriggerTime,"
                + "NofTime=@NofTime,"
                + "CurrentCount=@CurrentCount,"
                + "TriggerStatus=@TriggerStatus,"
                + "SQLStatement=@SQLStatement,"
                + "WhereC0ndition=@WhereC0ndition,"
                + "SearchText=@SearchText,"
                + "SearchValue=@SearchValue,"
                + "CustomerID=@CustomerID,"
                + "MailListID=@MailListID,"
                + "MailSubject=@MailSubject,"
                + "BoadyHeader=@BoadyHeader,"
                + "MailCategory=@MailCategory"
                + " Where 1=1 "
                + " and TimerID=@TimerID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@TimerID", _Update.TimerID);
                oSqlCommand.Parameters.AddWithValue("@Occurence", _Update.Occurence);
                oSqlCommand.Parameters.AddWithValue("@TriggerDate", _Update.TriggerDate);
                oSqlCommand.Parameters.AddWithValue("@TriggerTime", _Update.TriggerTime);
                oSqlCommand.Parameters.AddWithValue("@NofTime", _Update.NofTime);
                oSqlCommand.Parameters.AddWithValue("@CurrentCount", _Update.CurrentCount);
                oSqlCommand.Parameters.AddWithValue("@TriggerStatus", _Update.TriggerStatus);
                oSqlCommand.Parameters.AddWithValue("@SQLStatement", _Update.SQLStatement);
                oSqlCommand.Parameters.AddWithValue("@WhereC0ndition", _Update.WhereC0ndition);
                oSqlCommand.Parameters.AddWithValue("@SearchText", _Update.SearchText);
                oSqlCommand.Parameters.AddWithValue("@SearchValue", _Update.SearchValue);
                oSqlCommand.Parameters.AddWithValue("@CustomerID", _Update.CustomerID);
                oSqlCommand.Parameters.AddWithValue("@MailListID", _Update.MailListID);
                oSqlCommand.Parameters.AddWithValue("@MailSubject", _Update.MailSubject);
                oSqlCommand.Parameters.AddWithValue("@BoadyHeader", _Update.BoadyHeader);
                oSqlCommand.Parameters.AddWithValue("@MailCategory", _Update.MailCategory);
                string respond = Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand, null, "Update TSMailService");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteTSMailService(string TimerID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblservicetimer"
                + " Where 1=1 "
                + " and TimerID=@TimerID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@TimerID", TimerID);
                string respond = Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand, null, "Delete TSMailService");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool ExistTSMailService(string TimerID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tblservicetimer"
                + " Where 1=1 "
                + " and TimerID=@TimerID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@TimerID", TimerID);
                bool respond = Mycommon.ExistIntable(sqlQuery, oSqlCommand);
                return respond;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ExistTSMailFromValue(string Value1)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tblservicetimer"
                + " Where 1=1 "
                + " and SearchValue=@SearchValue";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@SearchValue", Value1);
                bool respond = Mycommon.ExistIntable(sqlQuery, oSqlCommand);
                return respond;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string MakeTriggerDateAndtime(TSMsilServiceType.EmailServiceInstance InstanceType , DateTime InDate, out DateTime OutDate, out  string OutTime)
        {
            OutDate = new DateTime();
            OutTime = "00:00";
            try
            {
                switch (InstanceType)
                {
                    case TSMsilServiceType.EmailServiceInstance.Acknowladement:
                        DateTime Out1 = InDate.AddDays(2);
                        OutDate = Out1;
                        OutTime = "07:30";
                        break;
                    case TSMsilServiceType.EmailServiceInstance.EmployeePermentLetter:
                        break;
                    default:
                        break;
                }
                return "True";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
          
        }
        public string GetExistTSMailService(string TimerID, out TSMsilServiceType.TSMailServiceDataType _ExistData)
        {
            _ExistData = new TSMsilServiceType.TSMailServiceDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "SysID,"
          + "TimerID,"
          + "Occurence,"
          + "TriggerDate,"
          + "TriggerTime,"
          + "NofTime,"
          + "CurrentCount,"
          + "TriggerStatus,"
          + "SQLStatement,"
          + "WhereC0ndition,"
          + "SearchText,"
          + "SearchValue,"
          + "CustomerID,MailListID"
          + " from tblservicetimer"
          + " Where 1=1 "
                + " and TimerID=@TimerID";
            oSqlCommand.Parameters.AddWithValue("@TimerID", TimerID);
            DataRow r = Mycommon.GetDataRow(sqlQuery, oSqlCommand, null, "Get Exist data TSMailService");
            if (r != null)
            {
                try
                {
                    bool resp = false;
                    int inSysID = 0;
                    resp = int.TryParse(r["SysID"].ToString(), out inSysID);
                    _ExistData.SysID = inSysID;
                    _ExistData.TimerID = r["TimerID"].ToString();
                    _ExistData.Occurence = r["Occurence"].ToString();
                    DateTime dtTriggerDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["TriggerDate"].ToString(), out dtTriggerDate);
                    if (resp)
                        _ExistData.TriggerDate = dtTriggerDate;
                    else
                        _ExistData.TriggerDate = new DateTime(1900, 1, 1);
                    _ExistData.TriggerTime = r["TriggerTime"].ToString();
                    int inNofTime = 0;
                    resp = int.TryParse(r["NofTime"].ToString(), out inNofTime);
                    _ExistData.NofTime = inNofTime;
                    int inCurrentCount = 0;
                    resp = int.TryParse(r["CurrentCount"].ToString(), out inCurrentCount);
                    _ExistData.CurrentCount = inCurrentCount;
                    int inTriggerStatus = 0;
                    resp = int.TryParse(r["TriggerStatus"].ToString(), out inTriggerStatus);
                    _ExistData.TriggerStatus = inTriggerStatus;
                    _ExistData.SQLStatement = r["SQLStatement"].ToString();
                    _ExistData.WhereC0ndition = r["WhereC0ndition"].ToString();
                    _ExistData.SearchText = r["SearchText"].ToString();
                    _ExistData.SearchValue = r["SearchValue"].ToString();
                    int inCustomerID = 0;
                    resp = int.TryParse(r["CustomerID"].ToString(), out inCustomerID);
                    _ExistData.CustomerID = inCustomerID;
                    _ExistData.MailListID =r["MailListID"].ToString(); 
                    return "True";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
                return "Data not Found ";
           
        }
        public bool NowReadyToTrigger(DateTime Date1, string Time1)
        {
            string sql1 = "select sysid from tblservicetimer where TriggerDate='" + Date1.ToString("dd/MM/yyyy") + "' and TriggerTime='" + Time1 + "'";
            bool resp = Mycommon.ExistIntable(sql1);
            return resp;
        }
      
        public void UpdateTriggerNoneedtoFire(string SearchValue)
        {
            string sql1 = "update tblservicetimer set tblservicetimer.TriggerStatus=1 where  SearchValue='" + SearchValue + "'";
            string respond = Mycommon.ExicuteAnyCommand(sql1, "Update Trigger Status=1");
        }
        public void SetTriggerStatus(string TimerID,bool DoSomeCalculation)
        {
            bool resp;
            string sql1 = "";
            try
            {
                if (DoSomeCalculation)
                {
                    string sql2 = "SELECT tblforderacknowladgment.ACN FROM tblservicetimer tblservicetimer "
                    + " INNER JOIN tsfs.tblforderacknowladgment tblforderacknowladgment "
                    + " ON (tblservicetimer.SearchValue = tblforderacknowladgment.PONumber) "
                    + " AND (tblservicetimer.CustomerID = tblforderacknowladgment.CusTomer) "
                    + " where tblservicetimer.TimerID='" + TimerID + "'";
                     resp = Mycommon.ExistIntable(sql2);
                }
                   else
                {
                    resp = false;
                }
                    if (resp)
                        sql1 = "Update tblservicetimer set CurrentCount=CurrentCount +1 , TriggerStatus=1 where TimerID='" + TimerID + "'";
                    else
                        sql1 = "Update tblservicetimer set CurrentCount=CurrentCount +1 where TimerID='" + TimerID + "'";
                    string respond = Mycommon.ExicuteAnyCommand(sql1, "Update Trigger Status");
               
                }
            catch (Exception ex)
            {
                
         
            }
 
        }
        public string  TriggerMail(string TimerID, string Subject, string MailBobyHeader, string AckoneladmentMailBoady)
        {
            EmailInforType.MailDetails _CreateData = new EmailInforType.MailDetails();
            DataTable tb = GetMailList(TimerID);
            List<string> TomailList = new List<string>();
            //==============
            if (tb != null)
            {
                foreach (DataRow r in tb.Rows)
                {
                    string OneMail = r["emailadd"].ToString();
                    TomailList.Add(OneMail);
                }
                try
                {
                    _CreateData.ListTo = TomailList;
                    _CreateData.Subject = Subject;
                    _CreateData.MailBobyHeader = MailBobyHeader;
                    _CreateData.MailBody = AckoneladmentMailBoady;
                    string Respond = MyMail.CreateEmal(_CreateData);
                    return Respond;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
                return "Mail Not Send";
        }
        public string GetDateAndTime(string TimerID)
        {
            string FinalResult = "";
            string sql1 = "Select TriggerDate,TriggerTime from tblservicetimer where TimerID='" + TimerID + "'";
            DataRow r = Mycommon.GetDataRow(sql1, "Get Trigger date and time");
            if (r != null)
            {
                DateTime dt =DateTime.Parse( r["TriggerDate"].ToString());
                FinalResult = dt.ToString("dd/MMM/yyyy") + "@" + r["TriggerTime"].ToString().Trim();
            }
            return FinalResult;
        }
        public DataTable GetTobeTrigger()
        {
            string sql1 = "Select TimerID,WhereC0ndition,MailSubject,BoadyHeader,MailCategory,SearchValue from tblservicetimer where TriggerStatus=0";
            DataTable tb = Mycommon.GetDataTable(sql1, "Get To be trigger");
            return tb;
        }
        public string MakeSqlStatement(TSMsilServiceType.EmailServiceInstance Mailtype, out string ConformNeedToSend, out string AckoneladmentMailBoady)
        {

            AckoneladmentMailBoady = "";
            ConformNeedToSend = "";
            try
            {
                switch (Mailtype)
                {
                    case TSMsilServiceType.EmailServiceInstance.Acknowladement:
                        ConformNeedToSend = "SELECT count(tblforderacknowladgmentdetails.POLineNumber) as NofTime FROM tsfs.tblfcustomerpodetails "
                        + " INNER JOIN tsfs.tblforderacknowladgmentdetails ON tblfcustomerpodetails.DetailRef = tblforderacknowladgmentdetails.POLineNumber where tblfcustomerpo.COrderNumber";
                        AckoneladmentMailBoady = "Select CustomerPN, CellType, RequiredQty, ShippingDate from tblfcustomerpodetails where CPONumber";
                        break;
                    case TSMsilServiceType.EmailServiceInstance.EmployeePermentLetter:
                        break;
                    case TSMsilServiceType.EmailServiceInstance.InvoiceClear:

                        break;
                    default:
                        break;
                }
                return "True";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
        public string GetNewEmailServiceID()
        {
            string sql1 = "select max(substr(tblservicetimer.TimerID,5)) as MaxID from tsfs.tblservicetimer";
            DataRow r = Mycommon.GetDataRow(sql1, "get Timer New ID");
            if (r != null)
            {
                int MxID = 0;
                bool resp = int.TryParse(r["MaxID"].ToString(), out MxID);
                MxID += 1;
                string FinalID = "TIM-" + MxID.ToString("00000#");
                return FinalID;
            }
            else
              return   "TIM-000001";
        }

        public void  TestMailSend()
        {
            AutoSendEmail();
            
        }
        public string  MakeInvoiceMailBoady(string invoiceNo)
        {
            string FulText = "";
            string sql1 = "SELECT tblcustomer.Customer, tblshinvoiceheader.ExportingCarriear, tblshinvoiceheader.ShippingDate,"
                + "tblshinvoiceponumbers.CellType,tblshinvoiceponumbers.ShippingQty, tblshinvoiceheader.ShipVia"
            + " FROM tblcustomer INNER JOIN tblfcustomerpo ON tblcustomer.Sysid = tblfcustomerpo.CusID"
            + " INNER JOIN tblshinvoiceponumbers ON tblfcustomerpo.COrderNumber = tblshinvoiceponumbers.PONumber INNER JOIN tblshinvoiceheader "
            + " ON tblshinvoiceponumbers.InvoiceNo = tblshinvoiceheader.InvoiceID WHERE tblshinvoiceheader.InvoiceID='" + invoiceNo + "'";
            DataTable tb = Mycommon.GetDataTable(sql1, "Get Invoice Details");
            if (tb != null)
            {
                DataRow r = tb.Rows[0];
                DateTime dtp = Convert.ToDateTime(r["ShippingDate"].ToString());

                FulText = FulText + "<br>";
                FulText = "Customer Name  :- " + r["Customer"].ToString() + "<br>";
                FulText = FulText + "Ship Date      :- " + dtp.ToString("dd/MMM/yyyy") + "<br>";
                FulText = FulText + "Invoice Number :- " + invoiceNo + "<br>";
                FulText = FulText + "Shipping Mode  :- " + r["ShipVia"].ToString();
                return  FulText ;
            }
            else
                return "";
        }
        public void AutoSendEmail()
        {
            # region Emailservice
            string MailExtTime = DateTime.Now.ToString("HH:mm");
            DateTime MailExtDate = DateTime.Today;
            TSMailService MyTimerService = new TSMailService();
            DataTable tb = new DataTable();
            string MailBody = "";
            string DateAndTime = "";
            tb = MyTimerService.GetTobeTrigger();
            string respond = "";
            foreach (DataRow r in tb.Rows)
            {
                DateAndTime = MyTimerService.GetDateAndTime(r[0].ToString());
                int MailCat = -1;
                bool res1 = int.TryParse(r[4].ToString(), out MailCat);
                if (DateAndTime.Length > 0)
                {
                    DateTime d1 = new DateTime();
                    string t1 = "";
                    string[] spltstr = DateAndTime.Split('@');
                    d1 = DateTime.Parse(spltstr[0]);
                    t1 = spltstr[1];
                    if ((MailExtTime.Trim() == t1.Trim()) && (MailExtDate >= d1))
                    {
                        switch (MailCat)
                        {
                            case 0:
                             //   MailBody = MyTimerService.CreateAckoneladmentMailBoady(r[0].ToString());
                                break;
                            case 1:
                                MailBody = MakeInvoiceMailBoady(r[5].ToString());
                                break;
                            default:
                                break;
                        }
                       
                        respond =   TriggerMail(r[0].ToString(), r[2].ToString() + r[1].ToString(), r[3].ToString() + r[1].ToString(), MailBody);
                        if (respond == "True")
                            SetTriggerStatus(r[0].ToString(), false);
                        else
                        {
 
                        }
                    }
                }
            }
            # endregion
        }


        #endregion
        #region E-Mail AddressList
        public string SaveTSMailList(TSMsilServiceType.TSMailListDataType _SaveData)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblserviceemail ("
          + "SysID,"
          + "ServiceTimerID,"
          + "emailadd)"
           + " Values("
           + "@SysID,"
           + "@ServiceTimerID,"
           + "@emailadd)";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@SysID", _SaveData.SysID);
                oSqlCommand.Parameters.AddWithValue("@ServiceTimerID", _SaveData.ServiceTimerID);
                oSqlCommand.Parameters.AddWithValue("@emailadd", _SaveData.emailadd);
                string respond = Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand,null, "Save TSMailList");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string UpdateTSMailList(TSMsilServiceType.TSMailListDataType _Update)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblserviceemail Set "
                + "SysID=@SysID,"
                + "ServiceTimerID=@ServiceTimerID,"
                + "emailadd=@emailadd"
                + " Where 1=1 "
                + " and SysID=@SysID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@SysID", _Update.SysID);
                oSqlCommand.Parameters.AddWithValue("@ServiceTimerID", _Update.ServiceTimerID);
                oSqlCommand.Parameters.AddWithValue("@emailadd", _Update.emailadd);
                string respond = Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand,null ,  "Update TSMailList");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteTSMailList(int SysID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblserviceemail"
                + " Where 1=1 "
                + " and SysID=@SysID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@SysID", SysID);
                oSqlCommand.CommandText = sqlQuery;
                string respond = Mycommon.ExicuteAnyCommand(oSqlCommand,  "Delete TSMailList");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public bool ExistTSMailList(int SysID)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tblserviceemail"
                + " Where 1=1 "
                + " and SysID=@SysID";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@SysID", SysID);
                bool respond = Mycommon.ExistIntable(sqlQuery, oSqlCommand);
                return respond;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string GetExistTSMailList(int SysID, out TSMsilServiceType.TSMailListDataType _ExistData)
        {
            _ExistData = new TSMsilServiceType.TSMailListDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "SysID,"
          + "ServiceTimerID,"
          + "emailadd"
          + " from tblserviceemail"
          + " Where 1=1 "
                + " and SysID=@SysID";
            oSqlCommand.Parameters.AddWithValue("@SysID", SysID);
            DataRow r = Mycommon.GetDataRow(sqlQuery, oSqlCommand, null, "Get Exist data TSMailList");
            if (r != null)
            {
                try
                {
                    bool resp = false;
                    int inSysID = 0;
                    resp = int.TryParse(r["SysID"].ToString(), out inSysID);
                    _ExistData.SysID = inSysID;
                    _ExistData.ServiceTimerID = r["ServiceTimerID"].ToString();
                    _ExistData.emailadd = r["emailadd"].ToString();
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
        public string GetMailListID(string ServiceTimerID)
        {
            string sql1 = "select MailListID from tblservicetimer where TimerID='" + ServiceTimerID + "'";
            DataRow r = Mycommon.GetDataRow(sql1, "Get Mail List ID");
            if (r != null)
            {
                return r["MailListID"].ToString();
            }
            else
                return "LST-00001";
        }
        public DataTable GetMailList(string ServiceTimerID)
        {
            string listId = GetMailListID(ServiceTimerID);
            string sql1 = "Select emailadd from tblserviceemail where ServiceTimerID='" + listId + "'";
            DataTable tb = Mycommon.GetDataTable(sql1, "get Mail List");
            return tb;
        }
#endregion

    }
    public class TSMsilServiceType
    {
        public enum EmailServiceInstance
	    {
	             Acknowladement=1,
                 EmployeePermentLetter=2,
                 InvoiceClear=3,
	    }
        public struct TSMailServiceDataType
        {
            public int SysID;
            public string TimerID;
            public string Occurence;
            public DateTime TriggerDate;
            public string TriggerTime;
            public int NofTime;
            public int CurrentCount;
            public int TriggerStatus;
            public string SQLStatement;
            public string WhereC0ndition;
            public string SearchText;
            public string SearchValue;
            public int CustomerID;
            public string MailListID;
            public string MailSubject;
            public string BoadyHeader;
            public int MailCategory;
            public List<TSMailListDataType> AddressList;
        }
        public struct TSMailListDataType
        {
            public int SysID;
            public string ServiceTimerID;
            public string emailadd;
        }
    }
}
