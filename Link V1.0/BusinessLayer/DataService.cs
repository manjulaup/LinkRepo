using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

using System.Net.Mail;
namespace DataLayer.DataService
{
    public class DataService  //:  IDataService 
    {
      
        public MySqlConnection AccountERP = null;
        public MySqlConnection AccountConnection = null;
        private string readconf()
        {
        Validation.ValidationLayer.Validations MyValidation = new Validation.ValidationLayer.Validations();   
        string AppPath = Application.StartupPath;
        string lines = System.IO.File.ReadAllText(AppPath + @"\accconf.ncf");
        string getrt = MyValidation.Getpassword(lines.Trim());
        return getrt;
        }
        public DataService()
            {

            bool IsLocal;
            string readcon = readconf();
            if (readcon == "local")
                IsLocal = true;
            else
                IsLocal = false;
            string servername = "";
            string Connection = "";
            //IsLocal = false;
            if (IsLocal)
                {
                    servername = "localhost";
                    Connection = "server=" + servername + ";uid=root;pwd=slaf2011;Connection Lifetime=120;pooling=true ;max pool size=50;database=";
                    AccountERP = new MySqlConnection(Connection + "tsfs;");
                    AccountConnection = new MySqlConnection(Connection + "Accounterp;");
                }
            else
                {
                    servername = "198.101.10.177";
                    Connection = "server=" + servername + ";uid=root;pwd=anubaba123;Connection Lifetime=120;pooling=true ;max pool size=50;database=";
                    AccountERP = new MySqlConnection(Connection + "tsfs;");
                    AccountConnection = new MySqlConnection(Connection + "Accounterp;");
                }
            try
                {
               

                if (AccountERP.State != ConnectionState.Open)
                    {
                       AccountERP.Open();
                    }
                if (AccountConnection.State != ConnectionState.Open)
                    AccountConnection.Open();

                }
            catch (Exception es)
                {
                string Ms = es.Message;

                }
            
            }
        ~DataService()
        {
            CloseDB();
        }
        public DataService(bool IsLocal)
        {
            string servername = "";
            string Connection = "";
            if (IsLocal)
            {
                servername = "localhost";
                Connection = "server=" + servername + ";uid=root;pwd=slaf2011;Connection Lifetime=120;pooling=true ;max pool size=100;database=";
               
                AccountERP = new MySqlConnection(Connection + "tsfs;");
                AccountConnection = new MySqlConnection(Connection + "Accounterp;");

            }
            else
            {
                servername = "localhost";
                Connection = "server=" + servername + ";uid=root;pwd=slaf2011;Connection Lifetime=120;pooling=true ;max pool size=100;database=";
             
                AccountERP = new MySqlConnection(Connection + "tsfs;");
                AccountConnection = new MySqlConnection(Connection + "Accounterp;");
            }
            try
            {
               
            
                if (AccountERP.State != ConnectionState.Open)
                {
                    AccountERP.Open();
                }
                if (AccountConnection.State != ConnectionState.Open)
                    AccountConnection.Open();

            }
            catch (Exception es)
            {
                string Ms = es.Message;

            }

        }
        public void CloseDB()
        {
            try
            {
               
                if (AccountConnection.State == ConnectionState.Open)
                {
                    AccountConnection.Close();
                    AccountConnection.Dispose();
                }
                if (AccountERP.State == ConnectionState.Open)
                {
                    AccountERP.Close();
                    
                    AccountERP.Dispose();
                    
                }
            }
            catch (Exception ex)
            {
                
                string Ms = ex.Message;
            }
        }

        #region Account
        public String ExicuteAnyCommandAccount(String CmdStr, string FnNaame)
            {
            string respond;


            MySqlCommand Mycom = new MySqlCommand();
            Mycom.CommandType = CommandType.Text;

            Mycom.CommandText = CmdStr;
            if (AccountConnection.State == ConnectionState.Closed)
                AccountConnection.Open();
            Mycom.Connection = AccountConnection;
            try
                {

                int Res = Mycom.ExecuteNonQuery();
                if (Res > 0)
                {
                Mycom.Connection.Close();
                    AccountConnection.Close();
                    return "True";
                    
                }
                else
                    {
                    Mycom.Connection.Close();
                       AccountConnection.Close();
                    // new MySqlConnection(ConfigurationManager.AppSettings["3SFabrication"].ToString());
                    return "Effected Row Count is " + Res.ToString() + " ,Warning ....  Operation Not Successfull !!!";
                    }
                    Mycom.Connection.Close();
                }
            catch (Exception ex)
                {
                Mycom.Connection.Close();
                AccountConnection.Close();
                WriteToError(ex.Message.ToString() + "-" + CmdStr, FnNaame);
                return ex.Message;
                }

            }
        
        public bool ExistInTableAccount(string SqlQuery,MySqlCommand Command,string FunctionName)
        {

            DataRow r = GetDataRowAccount(SqlQuery, Command, FunctionName);
            if (r != null)
                return true;
            else
                return false;
          }
        public bool ExistInTableAccount(string SqlQuery, string FunctionName)
        {
            MySqlCommand Command = new MySqlCommand();
            DataRow r = GetDataRowAccount(SqlQuery, Command, FunctionName);
            if (r != null)
                return true;
            else
                return false;
        }
        public bool ExistInTableAccountIntrance(string SqlQuery, MySqlCommand Command,MySqlConnection Actcon, string FunctionName)
        {

            DataRow r = GetDataRowAccountTrans(SqlQuery, Command,Actcon, FunctionName);
            if (r != null)
                return true;
            else
                return false;
        }
        public String ExicuteAnyCommandAccountWithTrans(String CmdStr, MySqlCommand Mycom, MySqlConnection AtvConnection,string FnNaame)
        {
            Mycom.CommandType = CommandType.Text;
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AtvConnection;
            try
            {
                int Res = Mycom.ExecuteNonQuery();
                if (Res > 0)
                {
                    return "True";
                }
                else
                {
                    return "Effected Row Count is " + Res.ToString() + " ,Warning ....  Operation Not Successfull !!!";
                }
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message.ToString() + "-" + CmdStr, FnNaame);
                return ex.Message;
            }

        }

        public String ExicuteAnyCommandAccount(String CmdStr, MySqlCommand Mycom, string FnNaame )
        {
            string respond;
            Mycom.CommandType = CommandType.Text;
            Mycom.CommandText = CmdStr;
            if (AccountConnection.State == ConnectionState.Closed)
                AccountConnection.Open();
            Mycom.Connection = AccountConnection;
            try
            {
                int Res = Mycom.ExecuteNonQuery();
                if (Res > 0)
                    {
                    Mycom.Connection.Close();
                    Mycom.Dispose();
                    AccountConnection.Close();
                     return "True";
                    }
                else
                    {
                    Mycom.Connection.Close();
                    AccountConnection.Close();
                    return "Effected Row Count is " + Res.ToString() + " ,Warning ....  Operation Not Successfull !!!";
                    }
            }
            catch (Exception ex)
            {
            Mycom.Connection.Close();
            AccountConnection.Close();
                WriteToError(ex.Message.ToString() + "-" + CmdStr, FnNaame);
                return ex.Message;
            }

        }
        public DataRow GetDataRowAccount(String CmdStr, string FnNaame)
            {
            MySqlCommand Mycom = new MySqlCommand();
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AccountConnection;
            try
                {
            if (Mycom.Connection.State == ConnectionState.Closed)
                Mycom.Connection.Open();

                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                myda.Fill(Mytb);
                if (Mytb.Rows != null)
                    {
                    if (Mytb.Rows.Count > 0)
                        {
                        Mycom.Connection.Close();
                        Mycom.Dispose();
                        AccountConnection.Close();
                        return Mytb.Rows[0];
                        }
                    else
                        {
                        Mycom.Connection.Close();
                        AccountConnection.Close();
                        return null;
                        }
                    }
                else
                    {
                    Mycom.Connection.Close();
                    AccountConnection.Close();
                    return null;
                    }
                }
            catch (Exception ex)
                {
                Mycom.Connection.Close();
                AccountConnection.Close();
                string Ntext = "";
                if (CmdStr.Length > 175)
                    Ntext = CmdStr.Substring(0, 170);
                else
                    Ntext = CmdStr;
                WriteToError(ex.Message, Ntext + "-" + FnNaame);
                return null;
                }
            }
        
        public DataRow GetDataRowAccount(String CmdStr,MySqlCommand Mycom, string FnNaame)
        {
            
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AccountConnection;
            try
            {
            if (Mycom.Connection.State == ConnectionState.Closed)
                Mycom.Connection.Open();

                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                myda.Fill(Mytb);
                if (Mytb.Rows != null)
                {
                if (Mytb.Rows.Count > 0)
                    {
                    Mycom.Connection.Close();
                    Mycom.Dispose();
                    AccountConnection.Close();
                    return Mytb.Rows[0];
                    }
                else
                    {
                    Mycom.Connection.Close();
                    AccountConnection.Close();
                    return null;
                    }
                }
                else
                {
                    Mycom.Connection.Close();
                    AccountConnection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
            Mycom.Connection.Close();
            AccountConnection.Close();
                string Ntext = "";
                if (CmdStr.Length > 175)
                    Ntext = CmdStr.Substring(0, 170);
                else
                    Ntext = CmdStr;
                WriteToError(ex.Message, Ntext + "-" + FnNaame);
                return null;
            }
        }
        public DataRow GetDataRowAccountTrans(String CmdStr, MySqlCommand Mycom,MySqlConnection ActCon,  string FnNaame)
        {

            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = ActCon;
            try
            {
            if (Mycom.Connection.State == ConnectionState.Closed)
                Mycom.Connection.Open();

                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                myda.Fill(Mytb);
                if (Mytb.Rows != null)
                {
                    if (Mytb.Rows.Count > 0)
                    {
                       
                        return Mytb.Rows[0];
                    }
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string Ntext = "";
               
                return null;
            }
        }
        public DataTable GetDataTableAccount(String CmdStr, string FnNaame)
        {
            MySqlCommand Mycom = new MySqlCommand();
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AccountConnection;
            try
            {
            if (Mycom.Connection.State == ConnectionState.Closed)
                Mycom.Connection.Open();
            
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                
                myda.Fill(Mytb);
                if (Mytb != null)
                    {
                    if (Mytb.Rows.Count > 0)
                        {
                        Mycom.Connection.Close();
                        Mycom.Dispose();
                        AccountConnection.Close();
                        return Mytb;
                        }
                    else
                        {
                        AccountConnection.Close();
                        Mycom.Connection.Close();
                        return null;
                        }
                    }
                else
                    {
                    AccountConnection.Close();
                    Mycom.Connection.Close();
                    return null;
                    }
            }
            catch (Exception ex)
            {
                Mycom.Connection.Close();
                AccountConnection.Close();
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public DataTable GetDataTableAccount(String CmdStr,MySqlCommand Mycom, string FnNaame)
            {
           
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AccountConnection;
            try
                {
            if (Mycom.Connection.State == ConnectionState.Closed)
                Mycom.Connection.Open();

                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                    {
                    if (Mytb.Rows.Count > 0)
                        {
                        AccountConnection.Close();
                        Mycom.Dispose();
                        Mycom.Connection.Close();
                        return Mytb;
                        }
                    else
                        {
                        AccountConnection.Close();
                        Mycom.Connection.Close();
                        return null;
                        }
                    }
                else
                {
                AccountConnection.Close();
                Mycom.Connection.Close();
                    return null; 
                    }
                }
            catch (Exception ex)
                {
                AccountConnection.Close();
                Mycom.Connection.Close();
                WriteToError(ex.Message, FnNaame);
                return Mytb;
                }
            }

 #endregion

        public String IndExicuteAnyCommand(String CmdStr,MySqlConnection _CurrentConnection, string FnNaame)
        {
            string respond;
            MySqlCommand Mycom = new MySqlCommand();
            Mycom.CommandType = CommandType.Text;
            Mycom.CommandText = CmdStr;
            try
            {
            if (_CurrentConnection.State == ConnectionState.Closed)
                _CurrentConnection.Open();
            Mycom.Connection = _CurrentConnection;

                int Res = Mycom.ExecuteNonQuery();
                if (Res > 0)
                {
                Mycom.Connection.Close();
                    return "True"; 
                }
                else
                    {
                    Mycom.Connection.Close();
                    return "Effected Row Count is " + Res.ToString() + " ,Warning ....  Operation Not Successfull !!!";
                    }
            }
            catch (Exception ex)
            {
            Mycom.Connection.Close();
                WriteToError(ex.Message.ToString() + "-" + CmdStr, FnNaame);
                return ex.Message;
            }

        }
        public string IndGetDataTable(String CmdStr,MySqlConnection _CurrentConnection, string FnNaame, out DataTable Mytb)
        {
            MySqlCommand Mycom = new MySqlCommand();
            Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = _CurrentConnection;
            try
            {
            if (Mycom.Connection.State == ConnectionState.Closed)
                Mycom.Connection.Open();

                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                    {
                    if (Mytb.Rows.Count > 0)
                        {
                        Mycom.Connection.Close();
                        return "True";
                        }
                    else
                        {
                        Mycom.Connection.Close();
                        return "False";
                        }
                    }
                else
                    {
                    Mycom.Connection.Close();
                    return "False";
                    }
            }
            catch (Exception ex)
            {
            Mycom.Connection.Close();
                WriteToError(ex.Message, FnNaame);
                return ex.Message;
            }
        }
        public DataTable IndGetDataTable(String CmdStr,MySqlConnection _CurrentConnection, string FnNaame)
        {
            MySqlCommand Mycom = new MySqlCommand();
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = _CurrentConnection;
            try
            {
            if (Mycom.Connection.State == ConnectionState.Closed)
                Mycom.Connection.Open();

                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                    {
                    if (Mytb.Rows.Count > 0)
                        {
                        Mycom.Connection.Close();
                        return Mytb;
                        }
                    else
                        {
                        Mycom.Connection.Close();
                        return null;
                        }
                    }
                else
                {
                Mycom.Connection.Close();
                    return null; 
                    }
            }
            catch (Exception ex)
            {
            Mycom.Connection.Close();
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public DataRow IndGetDataRow(String CmdStr, string FnNaame, MySqlConnection _ActiveConnection)
        {
            MySqlCommand Mycom = new MySqlCommand();
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = _ActiveConnection;
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                myda.Fill(Mytb);
                if (Mytb.Rows != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb.Rows[0];
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string Ntext = "";
                if (CmdStr.Length > 175)
                    Ntext = CmdStr.Substring(0, 170);
                else
                    Ntext = CmdStr;
                WriteToError(ex.Message, Ntext + "-" + FnNaame);
                return null;
            }
        }
        public bool IndExistInTable(String CmdStr, string FnNaame, MySqlConnection _CurrentConnection)
        {
            string respond;
            MySqlCommand Mycom = new MySqlCommand();
            Mycom.CommandType = CommandType.Text;
            Mycom.CommandText = CmdStr;
            if (_CurrentConnection.State == ConnectionState.Closed)
                _CurrentConnection.Open();
            Mycom.Connection = _CurrentConnection;
            try
            {
                Int32 Res =Convert.ToInt32(Mycom.ExecuteScalar());
                if (Res > 0)
                {
                Mycom.Connection.Close();
                    return true; 
                    }
                else
                    {
                    Mycom.Connection.Close();
                    return false;
                    }
            }
            catch (Exception ex)
            {
            Mycom.Connection.Close();
                WriteToError(ex.Message.ToString() + "-" + CmdStr, FnNaame);
                return false;
            }
        }
        public String ExicuteAnyCommand(MySqlCommand Mycom, string FnNaame)
        {
            string respond;
            try
            {

                int Res = Mycom.ExecuteNonQuery();
                if (Res > 0)
                    return "True";
                else
                {
                    return "Effected Row Count is " + Res.ToString() + " ,Warning ....  Operation Not Successfull !!!";
                }
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message.ToString() + "-" + Mycom.CommandText , FnNaame);
                return ex.Message;
            }

        }
        public String ExicuteAnyCommand(String CmdStr, string FnNaame)
        {
            string respond;
         
            
            MySqlCommand Mycom = new MySqlCommand();
            Mycom.CommandType = CommandType.Text;
            
            Mycom.CommandText = CmdStr;
            if (AccountERP.State == ConnectionState.Closed)
                AccountERP.Open();
            Mycom.Connection = AccountERP;
            try
            {
               
             int Res =Mycom.ExecuteNonQuery();
             if (Res > 0)
                 {
                 Mycom.Connection.Close();
                 return "True";
                 }
             else
                 {
                 // new MySqlConnection(ConfigurationManager.AppSettings["3SFabrication"].ToString());
                 Mycom.Connection.Close();
                 return "Effected Row Count is " + Res.ToString() + " ,Warning ....  Operation Not Successfull !!!";
                 }
            }
            catch (Exception ex)
            {
            Mycom.Connection.Close();
                WriteToError(ex.Message.ToString() + "-" + CmdStr, FnNaame);
                return ex.Message;
            }
            
        }
     
        public String ExicuteErrorCommand(String CmdStr, string FnNaame)
        {
            string respond;


            MySqlCommand Mycom = new MySqlCommand();
            if (AccountERP.State == ConnectionState.Closed)
            {
                AccountERP  = new  MySqlConnection(ConfigurationManager.AppSettings["3SFabrication"].ToString());  
                AccountERP.Open();
            }
            Mycom.CommandText = CmdStr;
            Mycom.Connection =AccountERP ;
            try
            {
               // CreateEmail(CmdStr, FnNaame);
                Mycom.ExecuteNonQuery();
                Mycom.Connection.Close();
                return "True";
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                Mycom.Connection.Close();
                return ex.Message;
            }

        }
      
        public String ErrorWriteCommand(String CmdStr, string FnNaame)
        {
            string respond;


            MySqlCommand Mycom = new MySqlCommand();
            if (AccountERP.State == ConnectionState.Closed)
                AccountERP.Open();  
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AccountERP;
            try
            {
                
               // CreateEmail(CmdStr, FnNaame);
                Mycom.ExecuteNonQuery();
                return "True";
            }
            catch (Exception ex)
            {
               
                return ex.Message;
            }

        }
        public string GetDataTable(String CmdStr, string FnNaame, out DataTable Mytb)
        {
            MySqlCommand Mycom = new MySqlCommand();
             Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AccountERP;
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return "True";
                    else
                        return "False";
                }
                else
                    return "False";
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return ex.Message  ;
            }
        }
        public DataTable GetDataTable(String CmdStr, string FnNaame)
        {
            MySqlCommand Mycom = new MySqlCommand();
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AccountERP;
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                
                myda.Fill(Mytb);
                if (Mytb != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public DataTable GetDataTableforTransaction(String CmdStr, string FnNaame)
        {
            MySqlCommand Mycom = new MySqlCommand();
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AccountERP;
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public DataTable GetDataTableforTransaction(String CmdStr,MySqlCommand Mycom, string FnNaame)
        {
          
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public DataTable GetDataTableInProceedure(String ProcedureName, string FnNaame)
        { 
            DataTable Mytb = new DataTable();
            MySqlCommand Mycom = new MySqlCommand(ProcedureName, AccountERP);
           
            Mycom.CommandType = CommandType.StoredProcedure;
               
           try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public DataTable GetDataTableInProceedureWithParaAndValue(String ProcedureName, string FnNaame,List <string> ParaM1,List<string> ParaValue)
        {
            DataTable Mytb = new DataTable();

            MySqlCommand Mycom = new MySqlCommand(ProcedureName, AccountERP);
            int i=0;
            foreach (string var in ParaM1)
            {
                Mycom.Parameters.Add(new MySqlParameter(var, MySqlDbType.VarChar, 20));
                Mycom.Parameters[var].Value = ParaValue[i] ;
                i= i+1;
            }
           
            Mycom.CommandType = CommandType.StoredProcedure;

            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public DataTable GetDataTableInProceedureWithPara(String ProcedureName, string FnNaame,string ParaM1)
        {
            DataTable Mytb = new DataTable();

            MySqlCommand Mycom = new MySqlCommand(ProcedureName, AccountERP);
            Mycom.Parameters.Add(new MySqlParameter("ECOType", MySqlDbType.VarChar, 20));
            Mycom.Parameters["ECOType"].Value = ParaM1;
            Mycom.CommandType = CommandType.StoredProcedure;

            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public DataTable GetDataTableInProceedure(String ProcedureName, string FnNaame,List<string >ParamList)
        {
            DataTable Mytb = new DataTable();
            MySqlCommand Mycom = new MySqlCommand(ProcedureName, AccountERP);


            Mycom.CommandType = CommandType.StoredProcedure;
            foreach (string str in ParamList)
            {
                string[] splt = str.Split('@');
                Mycom.Parameters.AddWithValue(splt[0], splt[1]); 
            }
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public DataRow GetDataRow(String CmdStr, string FnNaame)
        {
            MySqlCommand Mycom = new MySqlCommand();
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AccountERP;
   try
            {
            if (Mycom.Connection.State == ConnectionState.Closed)
                Mycom.Connection.Open();
         
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                myda.Fill(Mytb);
                if (Mytb.Rows != null)
                {
                    if (Mytb.Rows.Count > 0)
                    {
                        Mycom.Connection.Close();
                        return Mytb.Rows[0];

                    }
                    else
                    {
                        Mycom.Connection.Close();
                        return null; 
                    }
                }
                else
                {
                    Mycom.Connection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                string Ntext = "";
                if (CmdStr.Length > 175)
                    Ntext = CmdStr.Substring(0, 170);
                else
                    Ntext = CmdStr;
                WriteToError(ex.Message, Ntext + "-" + FnNaame);
                return null;
            }
        }
        public DataRow GetDataRow(String CmdStr, MySqlCommand Mycom, MySqlConnection ActiveConnection, string FnNaame)
        {

            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            if (ActiveConnection == null)
                ActiveConnection = AccountERP;

            if (ActiveConnection.State == ConnectionState.Closed)
                ActiveConnection.Open();

            Mycom.Connection = ActiveConnection;
            if (Mycom.Connection.State == ConnectionState.Closed)
            {
                Mycom.Connection.Open();
            }
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                myda.Fill(Mytb);
                if (Mytb.Rows != null)
                {
                    if (Mytb.Rows.Count > 0)
                        if (Mytb.Rows[0].ItemArray.GetValue(0).ToString().Trim() != "")
                        {
                            AccountERP.Close();
                            return Mytb.Rows[0];
                        }
                        else
                        {
                            AccountERP.Close();
                            return null;
                        }
                    else
                    {
                        AccountERP.Close();
                        return null;
                    }
                }
                else
                {
                    WriteToError("No Data found", FnNaame);
                    AccountERP.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                AccountERP.Close();
                return null;
            }
        }
        public Int32 GetSysID(String tblName)
        {
            String str1;

            str1 = "Select Max(sysID) From " + tblName;
            DataRow myRow = GetDataRow(str1, "Generate System ID");
            if (myRow != null)
            {
                if (string.IsNullOrEmpty(myRow.ItemArray.GetValue(0).ToString()) == false)
                    return Int32.Parse(myRow.ItemArray.GetValue(0).ToString()) + 1;
                else
                    return 1000;
            }
            
            else
                return 1000;

        }
        public Int32 GetSysIDOnWeb(String tblName,MySqlConnection _ActiveConnection)
        {
            String str1;

            str1 = "Select Max(sysID) From " + tblName;
            DataRow myRow = GetDataRowOnWeb(str1, "Generate System ID", _ActiveConnection);
            if (myRow != null)
            {
                if (string.IsNullOrEmpty(myRow.ItemArray.GetValue(0).ToString()) == false)
                    return Int32.Parse(myRow.ItemArray.GetValue(0).ToString()) + 1;
                else
                    return 1000;
            }

            else
                return 1000;

        }
        public DataColumn GetDataColumn(String CmdStr, string FnNaame)
        {
            MySqlCommand Mycom = new MySqlCommand();
            DataTable Mytb = new DataTable();
            DataColumn mycol = new DataColumn();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = AccountERP;
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                myda.Fill(Mytb);
                if (Mytb.Rows != null)
                {
                    mycol = Mytb.Columns[0];
                    return mycol;
                }
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return mycol;
            }
            return mycol;
        }
        public void WriteToError(String Errmsg, String FunctioName)
        {
            String str1;

            Errmsg = Errmsg.Replace("'", "-");
            FunctioName = FunctioName.Replace("'", "-"); 
            str1 = "Insert INTO tblErrorLog (Ermsg,FnNmae,ErDate) "
            + " Values ('" + Errmsg + "','" + FunctioName + "',CurDate())";
            try
            {
                ErrorWriteCommand(str1, "Exicute Any SQL Command");
               // CreateEmail(Errmsg, FunctioName);
            }
            catch (Exception ex)
            {
               // CreateEmail(Errmsg, ex.Message);
              
            }
           

        }
        public String ExicuteAnyCommand(String CmdStr, MySqlCommand ActiveCommand, MySqlConnection ActiveConnection, string FnNaame)
        {

            if (ActiveConnection == null)
                ActiveConnection = AccountERP;

            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            ActiveCommand.CommandText = CmdStr;
            ActiveCommand.Connection = ActiveConnection;
            try
            {
                int Respond = ActiveCommand.ExecuteNonQuery();
                if (Respond > 0)
                {
                    return "True";
                }
                else
                {
                    ActiveConnection.Close();
                    return "Operation Output is 0 Count";
                }
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
               // ActiveConnection.Close();
                return ex.Message;
            }
        }
        private void CreateEmail(string Massage,string Subject)
        {
            try
            {
               MailMessage mail = new MailMessage();
             //SmtpClient SmtpServer = new SmtpClient("vmail.sltnet.lk");
              SmtpClient SmtpServer = new SmtpClient("smtp.mail.yahoo.com");
               
               mail.From = new MailAddress("saticin@yahoo.com", "3S ERP System");
               mail.To.Add("saticin@gmail.com");
               mail.IsBodyHtml = true;
               mail.Priority = MailPriority.High;
               mail.Subject = Subject;
               string MailBody = "<h3>" + Subject + "</h3><br><font color='blue'>"
                  + Massage
                  + "</font>" + "<br>" + "<br>"
                  + "<br>" + "<br>"
                  + "<b> Date" +DateTime.Today.ToString("dd/MMM/yyyy") + " Time " + DateTime.Now.ToString ("HH:mm:ss")      +" <b>";
               MailBody = "<body>" + MailBody + "</body>";
               mail.Body = MailBody;
               SmtpServer.Port = 25;
               SmtpServer.Credentials = new System.Net.NetworkCredential("saticin@yahoo.com", "Indrajith123");
               SmtpServer.EnableSsl = false;
               SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {

                string test = ex.Message;
            }
        }
        public bool ExistInTable(string ComStr, MySqlCommand Comm)
        {

            DataRow r = GetDataRow(ComStr, Comm, null,"Exist in Table new");
            if (r != null)
            {
                if (string.IsNullOrEmpty(r[0].ToString()) == false)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public bool ExistInTable(MySqlCommand Comm)
        {
        Comm.Connection = AccountERP;
            if (Comm.Connection.State == ConnectionState.Closed)
                Comm.Connection.Open();
            try
            {
                int resut =Convert.ToInt32(Comm.ExecuteScalar()) ;
                if (resut > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                return false;
            }
          
         
        }
        public bool ExistDataOnWeb(string ComStr, MySqlCommand Comm)
        {

            DataRow r = GetDataRowOnWeb(Comm, "Exist in Table new");
            if (r != null)
            {
                if (string.IsNullOrEmpty(r[0].ToString()) == false)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public String ExicuteAnyCommand(String CmdStr, MySqlConnection ActiveConnection,string  FnNaame)
        {
            MySqlCommand Mycom = new MySqlCommand();
            if (ActiveConnection == null)
                ActiveConnection = AccountERP;
            if (ActiveConnection.State == ConnectionState.Closed)
            {
                ActiveConnection.Open();
            }
            Mycom.CommandText = CmdStr;
            Mycom.Connection = ActiveConnection;
            try
            {
                int Respond = Mycom.ExecuteNonQuery();
                if (Respond > 0)
                {
                    return "True";
                }
                else
                {
                    ActiveConnection.Close();
                    return "Operation Output is 0 Count";
                }
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                ActiveConnection.Close();
                return ex.Message;
            }
        }
        public String ExicuteAnyCommandOnWeb(String CmdStr, MySqlConnection ActiveConnection, string FnNaame)
        {
            MySqlCommand Mycom = new MySqlCommand();
           
            Mycom.CommandText = CmdStr;
            Mycom.Connection = ActiveConnection;
            try
            {
                int Respond = Mycom.ExecuteNonQuery();
                if (Respond > 0)
                {
                    return "True";
                }
                else
                {
                    ActiveConnection.Close();
                    return "Operation Output is 0 Count";
                }
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                ActiveConnection.Close();
                return ex.Message;
            }
        }
        public String ExicuteAnyCommandOnWeb(String CmdStr, MySqlCommand Mycom, string FnNaame)
        {
            Mycom.CommandText = CmdStr;
            try
            {
                int Respond = Mycom.ExecuteNonQuery();
                if (Respond > 0)
                {
                    return "True";
                }
                else
                {
                   
                    return "Operation Output is 0 Count";
                }
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                
                return ex.Message;
            }
        }
        public DataRow GetDataRowOnWeb( MySqlCommand Mycom,string FnNaame)
        {
            DataTable Mytb = new DataTable();
           
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                myda.Fill(Mytb);
                if (Mytb.Rows != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb.Rows[0];
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string Ntext = "";
                string CmdStr = Mycom.CommandText;
                if (CmdStr.Length > 175)
                    Ntext = CmdStr.Substring(0, 170);
                else
                    Ntext = CmdStr;
                WriteToError(ex.Message, Ntext + "-" + FnNaame);
                return null;
            }
        }
        public DataRow GetDataRowOnWeb(String CmdStr, string FnNaame,MySqlConnection _ActiveConnection)
        {
            MySqlCommand Mycom = new MySqlCommand();
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = _ActiveConnection;
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);
                myda.Fill(Mytb);
                if (Mytb.Rows != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb.Rows[0];
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string Ntext = "";
                if (CmdStr.Length > 175)
                    Ntext = CmdStr.Substring(0, 170);
                else
                    Ntext = CmdStr;
                WriteToError(ex.Message, Ntext + "-" + FnNaame);
                return null;
            }
        }
        public DataTable GetDataTableOnWeb(String CmdStr, string FnNaame, MySqlConnection _ActiveConnection)
        {
            MySqlCommand Mycom = new MySqlCommand();
            DataTable Mytb = new DataTable();
            Mycom.CommandText = CmdStr;
            Mycom.Connection = _ActiveConnection;
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public DataTable GetDataTableOnWeb(MySqlCommand Mycom,string FnNaame)
        {
          
            DataTable Mytb = new DataTable();
         
            try
            {
                MySqlDataAdapter myda = new MySqlDataAdapter(Mycom);

                myda.Fill(Mytb);
                if (Mytb != null)
                {
                    if (Mytb.Rows.Count > 0)
                        return Mytb;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                WriteToError(ex.Message, FnNaame);
                return Mytb;
            }
        }
        public bool ExistDataOnTable(string sql1Cmd,MySqlCommand _ActvieComand,MySqlConnection _ActiveConnection)
        {
            _ActvieComand.CommandText = sql1Cmd;
            _ActvieComand.Connection = _ActiveConnection;
            Int32 usercount = 0;
            try
            {
                object obg = _ActvieComand.ExecuteScalar();
                usercount = Int32.Parse(obg.ToString());
                if (usercount > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
 
        }

    }
   
}

