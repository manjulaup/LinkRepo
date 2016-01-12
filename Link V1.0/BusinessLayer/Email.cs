using System;
using System.Data;
using MySql.Data.MySqlClient  ;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail; 
using BusinessLayer.CommonOperation ;

namespace BusinessLayer.Emails
{
   public class Email
    {
       private CommonOperations Mycommon = null;
       public Email()
       { 
       }
       public Email(bool Islocal)
           {
           Mycommon = new CommonOperations(Islocal);
           }
       public string CreateEmal(BusinessLayer.Emails.EmailInforType.MailDetails _Details)
       {
           try
           {
               MailMessage mail = new MailMessage();
              // SmtpClient SmtpServer = new SmtpClient("vmail.sltnet.lk");
               SmtpClient SmtpServer = new SmtpClient("mail.3sfab.com");
             
              // SmtpClient SmtpServer = new SmtpClient("smtp.mail.yahoo.com");
               mail.From = new MailAddress("erp@3sfab.com", "3S ERP System");
             //  mail.From = new MailAddress("saticin@yahoo.com", "3S ERP System");
           
               if (_Details.FileAttachment!=null )
                   mail.Attachments.Add(_Details.FileAttachment);  
               //=========================================================
               if (_Details.ListTo != null)
               {
                   if (_Details.ListTo.Count > 0)
                   {
                       foreach (string oneAdd in _Details.ListTo)
                       {
                           mail.To.Add(oneAdd);
                       }
                   }
                   else
                   {
                       mail.To.Add(_Details.ToAdd);
                   }
               }
               else
                   mail.To.Add(_Details.ToAdd);
               //=======================================
               if (_Details.ListBcc != null)
               {
                   if (_Details.ListBcc.Count > 0)
                   {
                       foreach (string BccAdd in _Details.ListBcc)
                       {
                           mail.Bcc.Add(BccAdd);
                       }
                   }
                   else
                   {
                       if (_Details.Bcc != null)
                           mail.Bcc.Add(_Details.Bcc);
                   }
               }
               else
               {
                   if (_Details.Bcc != null)
                       mail.Bcc.Add(_Details.Bcc);
               }
               //==========================================
               if (_Details.ListCC != null)
               {
                   if (_Details.ListCC.Count > 0)
                   {
                       foreach (string CcAdd in _Details.ListCC)
                       {
                           mail.CC.Add(CcAdd);
                       }
                   }
                   else
                   {
                       if (_Details.CC != null)
                           mail.CC.Add(_Details.CC);
                   }
               }
               else
               {
                   if (_Details.CC != null)
                       mail.CC.Add(_Details.CC);
               }
               //==========================================
               mail.IsBodyHtml = true;
               mail.Priority = MailPriority.High;
               mail.Subject = _Details.Subject;
               string MailBody ="<h3>" + _Details.MailBobyHeader + "</h3><br><font color='blue'>"
                   +  _Details.MailBody
                   + "</font>"+  "<br>" + "<br>"
                   + "<br>" + "<br>"
                   + "<b> This is a 3S - ERP system generated note. Do not reply. <b>";
               MailBody = "<body>" + MailBody + "</body>";
               mail.Body = MailBody;
               //=========================================================
               SmtpServer.Port = 25;
               SmtpServer.Credentials = new System.Net.NetworkCredential("erp@3sfab.com", "melani123");
               SmtpServer.EnableSsl = false;
               SmtpServer.Send(mail);
               return "True";
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       }
       public string  SaveEssencialDecission(EmailInforType.EssencialDecissionType _SaveData)
    {
        MySqlCommand oSqlCommand=new MySqlCommand();
          string sqlQuery ="Insert Into ecoessencialdecission ("
            + "SysID,"
            + "Department,"
            + "FirstPerson,"
            + "FirstEmail,"
            + "SecondPeson,"
            + "SeconfEmail,"
            + "Status)"
             + " Values ("
             + "@SysID,"
             + "@Department,"
             + "@FirstPerson,"
             + "@FirstEmail,"
             + "@SecondPeson,"
             + "@SeconfEmail,"
             + "@Status)";
          try
          {
                oSqlCommand.Parameters.AddWithValue("@SysID",_SaveData.SysID);
                oSqlCommand.Parameters.AddWithValue("@Department",_SaveData.Department);
                oSqlCommand.Parameters.AddWithValue("@FirstPerson",_SaveData.FirstPerson);
                oSqlCommand.Parameters.AddWithValue("@FirstEmail",_SaveData.FirstEmail);
                oSqlCommand.Parameters.AddWithValue("@SeconfEmail", _SaveData.SecondEmail);
                oSqlCommand.Parameters.AddWithValue("@SecondPeson",_SaveData.SecondPeson);
                oSqlCommand.Parameters.AddWithValue("@Status",_SaveData.Status);
                string respond=Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand, null,"SAve ECO Email");
                return respond;
          }
            catch (Exception ex)
          {
               return ex.Message;
          }
}
       public string  UpdateEssencialDecission(EmailInforType.EssencialDecissionType _Update)
        {
              MySqlCommand oSqlCommand=new MySqlCommand();
              string sqlQuery ="Update ecoessencialdecission Set "
                  + "SysID=@SysID,"
                  + "Department=@Department,"
                  + "FirstPerson=@FirstPerson,"
                  + "FirstEmail=@FirstEmail,"
                  + "SecondPeson=@SecondPeson,"
                  + "SeconfEmail=@SeconfEmail,"
                  + "Status=@Status"
                  + " Where 1=1 "
                  + " and SysID=@SysID";
                  try
                      {
                     oSqlCommand.Parameters.AddWithValue("@SysID",_Update.SysID);
                     oSqlCommand.Parameters.AddWithValue("@Department",_Update.Department);
                     oSqlCommand.Parameters.AddWithValue("@FirstPerson",_Update.FirstPerson);
                     oSqlCommand.Parameters.AddWithValue("@SeconfEmail", _Update.SecondEmail);
                     oSqlCommand.Parameters.AddWithValue("@FirstEmail",_Update.FirstEmail);
                     oSqlCommand.Parameters.AddWithValue("@SecondPeson",_Update.SecondPeson);
                     oSqlCommand.Parameters.AddWithValue("@Status",_Update.Status);
                    string respond=Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand, null,"Update eco email");
                    return respond;
                      }
                  catch (Exception ex)
                  {
                      return ex.Message;
                  }
        }
       public string  DeleteEssencialDecission( int SysID)
        {
          MySqlCommand oSqlCommand=new MySqlCommand();
          string sqlQuery ="Delete from ecoessencialdecission"
              + " Where 1=1 "
              + " and SysID=@SysID";
          try
          {
            oSqlCommand.Parameters.AddWithValue("@SysID",SysID);
            string respond=Mycommon.ExicuteAnyCommand(sqlQuery, oSqlCommand, null,"Delete emal");
            return respond;
          }
          catch (Exception ex)
          {
              return ex.Message;
          }
        }
       public bool  ExistEssencialDecission( int SysID)
        {
              MySqlCommand oSqlCommand=new MySqlCommand();
              string sqlQuery ="Select SysID from ecoessencialdecission"
                  + " Where 1=1 "
                  + " and SysID=@SysID";
              try
              {
                  oSqlCommand.Parameters.AddWithValue("@SysID",SysID);
                  bool respond=Mycommon.ExistIntable (sqlQuery, oSqlCommand);
                  return respond;
              }
              catch (Exception ex)
              {
                return false;
              }
        }
        public List<EmailInforType.EssencialDecissionType> GetEmailDecissionList()
             {
               List<EmailInforType.EssencialDecissionType> _EmailList = new List<EmailInforType.EssencialDecissionType>();
               string sql1 = "Select SysID from ecoessencialdecission where status=1";
               DataTable tb = new DataTable();
               tb = Mycommon.GetDataTable(sql1, "Get Email List");
               if (tb != null)
               {
                   foreach (DataRow r in tb.Rows)
                   {
                       EmailInforType.EssencialDecissionType onemail = new EmailInforType.EssencialDecissionType();
                       string respond = GetExistEssencialDecission(int.Parse(r["Sysid"].ToString()), out onemail);
                       if (respond == "True")
                           _EmailList.Add(onemail);
                   }
               }
               return _EmailList;
                 
               }
        public string  GetExistEssencialDecission(int SysID,out EmailInforType.EssencialDecissionType _ExistData)
            {
                 _ExistData=new EmailInforType.EssencialDecissionType();
                  MySqlCommand oSqlCommand=new MySqlCommand();
                  string sqlQuery ="Select "
                + "SysID,"
                + "Department,"
                + "FirstPerson,"
                + "FirstEmail,"
                + "SecondPeson,"
                + "SeconfEmail,"
                + "Status"
                + " from ecoessencialdecission"
                + " Where 1=1 and status=1 "
                      + " and SysID=" + SysID ;
                 
                 DataRow r=Mycommon.GetDataRow(sqlQuery,"get Email Data");
                  if (r !=null)
                      {
                  try
                  {
                   bool resp=false;
                   int inSysID=0;
                   resp = int.TryParse(r["SysID"].ToString(), out inSysID);
                  _ExistData.SysID = inSysID;
                  _ExistData.Department = r["Department"].ToString();
                  _ExistData.FirstPerson = r["FirstPerson"].ToString();
                  _ExistData.FirstEmail = r["FirstEmail"].ToString();
                  _ExistData.SecondPeson = r["SecondPeson"].ToString();
                  _ExistData.SecondEmail = r["SeconfEmail"].ToString();
                   int inStatus=0;
                   resp = int.TryParse(r["Status"].ToString(), out inStatus);
                  _ExistData.Status = inStatus;
                       return "True";
                      }
                      catch (Exception ex)
                      {
                           return ex.Message;
                      }
                      }
                      else
                   return  "data not found ";
               }
    }
    public class EmailInforType
    {
        public struct MailDetails
        {
            public string ToAdd;
            public string CC;
            public string Bcc;
            public string Subject;
            public string MailBobyHeader;
            public string MailBody;
            public List<string> ListTo;
            public List<string> ListCC;
            public List<string> ListBcc;
            public Attachment FileAttachment;
            public AttachmentCollection FileAttachachmentList;

        }
        public struct EssencialDecissionType
        {
            public int SysID;
            public string Department;
            public string FirstPerson;
            public string FirstEmail;
            public string SecondPeson;
            public string SecondEmail;
            public int Status;
        }
    }
}
