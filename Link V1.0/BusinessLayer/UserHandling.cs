using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using DataLayer.DataService;
using BusinessLayer.CommonOperation;
using System.Windows.Forms;
using Validation.ValidationLayer;
using System.Configuration;


namespace BusinessLayer.UserHandling
{
    public class UserHandling
    {
        private bool IsLoacal=false;

      
        private CommonOperations MYCommon = null;
        private Validations MyValidation = new Validations();
       // private DataService MyDataService = new DataService();
        public UserHandling(bool IsLocalConnection)
        {
            IsLoacal = IsLocalConnection;
           
            MYCommon = new CommonOperations(IsLoacal); 
        }
        public UserHandling()
        {
            
        }
        
        public struct UserHandlingType
        {
            public int SysID;
            public string UserName;
            public string UPassword;
            public int GroupID;
            public string CurrentStatus;
            public DateTime CreateDate;
            public string DepID;
            public int Status;
        }
        public struct MyCurrentUserRight
        {
            public String ObgName;
            public Int16 Save;
            public Int16 Update;
            public Int16 Delete;
            public Int16 View;
            public Int32 ObgID;
            public Int32 Print;
            public string Department;
            public string FormName;
        }
        public struct ObjectUserRollDataType
            {
            public int SysID;
            public int RollID;
            public int userID;
            public int ObgID;
            public int OView;
            public int OSave;
            public int OUpdate;
            public int Odelete;
            public int OPrint;
            public int OPostToApp;
            public int OPostToAcc;
            }
        public List<MyCurrentUserRight> MyActiveUserRight = new List<MyCurrentUserRight>(); 
        Int32 _GroupID;
        String _GroupName;
        Int32 _RollID;
        Int32 _ObgID;
        String _ObjectName;
        Int16 _Osave;
        Int16 _Oview;
        Int16 _Odelete;
        Int16 _Oupdate;
        Int16 _Oprint;
        Int32 _UsysID;
        String _UserName;
        String _Password;
        String _Status;
        String _DepID;
        string _DepName;
        string _FormName;
        #region UserMappingWith Employee
        public string UpdateUserMapping(int UID,string EmpId)
        {
            try
            {
            string sqlEmployee = "update  tblemployee set userID=" + UID + " where EmpNo='" + EmpId + "'";
            string respond = MYCommon.ExicuteAnyCommand(sqlEmployee, "UpdateEmpNumber");
            return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        #region Properties


        public string FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }
        public string DepName
        {
            get { return _DepName; }
            set { _DepName = value; }
        }
        public String DepID
        {
            get { return _DepID; }
            set { _DepID = value; }
        }
        public Int16 Oprint
        {
            get { return _Oprint; }
            set { _Oprint = value; }
        }
        public Int32 UsysID
        {
            get { return _UsysID; }
            set { _UsysID = value; }
        }

        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public String Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public Int16 Oupdate
        {
            get { return _Oupdate; }
            set { _Oupdate = value; }
        }
        public Int16 Odelete
        {
            get { return _Odelete; }
            set { _Odelete = value; }
        }
        public Int16 Oview
        {
            get { return _Oview; }
            set { _Oview = value; }
        }
        public Int16 Osave
        {
            get { return _Osave; }
            set { _Osave = value; }
        }
        public String ObjectName
        {
            get { return _ObjectName; }
            set { _ObjectName = value; }
        }
        public Int32 RollID
        {
            get { return _RollID; }
            set { _RollID = value; }
        }
        public Int32 ObgID
        {
            get { return _ObgID; }
            set { _ObgID = value; }
        }
        public String GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        public Int32 GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }
        #endregion
        #region User Group
        private String AddNewUserGroup()
        {
            String Str1;
            Int32 SysID1 = MYCommon.GetSysID("tblUserGroup");
            String Respond;

            Str1 = "INSERT INTO tblUserGroup"
                + " (SysID,UserGroup) Values(" + SysID1 + ",'" + _GroupName + "')";
            try
            {
            Respond = MYCommon.ExicuteAnyCommand(Str1, "Add New User Group");
                if (Respond != "True")
                    return Respond;
                else
                    return "True";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
        public String SaveUserGroup()
        {
            String Respond;
            if (ExistUserGroup() == false)
            {
                Respond = AddNewUserGroup();
            }
            else
            {
                Respond = UpdateUserGroup();
            }

            return Respond;
        }
        public String UpdateUserGroup()
        {
            String Str1;
            _GroupID = GetUserGroupID(); 
            String Respond;
            Str1 = "UPDATE tbluserGroup Set"
                 + " UserGroup='" + _GroupName + "' Where SysID=" + _GroupID;
            Respond = MYCommon.ExicuteAnyCommand(Str1, "Update User Group");
            return Respond;
        }
        public Boolean ExistUserGroup()
        {
            String Str1;
            DataRow Mydr;
            Str1 = "Select SysID From tblUserGroup"
            + " Where UserGroup ='" + _GroupName + "'";
            Mydr = MYCommon.GetDataRow(Str1, "Exist User Group");
            if (Mydr != null)
                if (Mydr.ItemArray.GetValue(0).ToString() != "")
                {
                    return true;
                }
                else
                    return false;
            else
                return false;
        }
        public Int32 GetUserGroupID(Int32  UserID)
        {
            String Str1;
            DataRow Mydr;
            Str1 = "Select GroupID From tblusernamepassword"
            + " Where SysID=" + UserID ;
            Mydr = MYCommon.GetDataRow(Str1, "Exist User Group");
            if (Mydr != null)
                if (Mydr.ItemArray.GetValue(0).ToString() != "")
                {
                    _GroupID = Int32.Parse(Mydr.ItemArray.GetValue(0).ToString());
                    return Int32.Parse(Mydr.ItemArray.GetValue(0).ToString());
                }
                else
                {
                    _GroupID = 0;
                    return 0;
                }
            else
            {
                _GroupID = 0;
                return 0;
            }
        }
        public Int32 GetUserGroupIDFromName(string GroupName)
        {
            String Str1;
            DataRow Mydr;
            Str1 = "Select SysID From tblUserGroup"
            + " Where UserGroup='" + GroupName + "'";
            Mydr = MYCommon.GetDataRow(Str1, "Exist User Group");
            if (Mydr != null)
                if (Mydr.ItemArray.GetValue(0).ToString() != "")
                {
                    _GroupID = Int32.Parse(Mydr.ItemArray.GetValue(0).ToString());
                    return Int32.Parse(Mydr.ItemArray.GetValue(0).ToString());
                }
                else
                {
                    _GroupID = 0;
                    return 0;
                }
            else
            {
                _GroupID = 0;
                return 0;
            }
        }
        public Int32 GetUserGroupID()
        {
            String Str1;
            DataRow Mydr;
            Str1 = "Select SysID From tblUserGroup"
            + " Where UserGroup='" + _GroupName + "'";
            Mydr = MYCommon.GetDataRow(Str1, "Exist User Group");
            if (Mydr != null)
                if (Mydr.ItemArray.GetValue(0).ToString() != "")
                {
                    _GroupID = Int32.Parse(Mydr.ItemArray.GetValue(0).ToString());
                    return Int32.Parse(Mydr.ItemArray.GetValue(0).ToString());
                }
                else
                {
                    _GroupID = 0;
                    return 0;
                }
            else
            {
                _GroupID = 0;
                return 0;
            }
        }
        public string GetDepartment(string DepCode)
        {
            string sql1 = "Select DepName From tbldepartment Where DepID='" + DepCode + "'";
            DataRow Myrow = MYCommon.GetDataRow(sql1, "GetDepartment");
            if (Myrow != null)
            {
                if (string.IsNullOrEmpty(Myrow["DepName"].ToString()) == false)
                    return Myrow["DepName"].ToString();
                else
                    return "Nill";
            }
            else
                return "Nill";
        }
        public string GetDepartmentCode(string DepName)
        {
            string sql1 = "Select DepID From tbldepartment Where DepName='" + DepName + "'";
            DataRow Myrow = MYCommon.GetDataRow(sql1, "GetDepartmentCode");
            if (Myrow != null)
            {
                if (string.IsNullOrEmpty(Myrow["DepID"].ToString()) == false)
                    return Myrow["DepID"].ToString();
                else
                    return "Nill";
            }
            else
                return "Nill";
        }
        public String GetUserGroup()
        {
            String Str1;
            DataRow Mydr;
            Str1 = "Select UserGroup  From tblUserGroup"
            + " Where SysID=" + _GroupID;
            Mydr = MYCommon.GetDataRow(Str1, "Exist User Group");
            if (Mydr != null)
                if (Mydr.ItemArray.GetValue(0).ToString() != "")
                {
                    _GroupName = Mydr.ItemArray.GetValue(0).ToString();
                    return Mydr.ItemArray.GetValue(0).ToString();
                }
                else
                {
                    _GroupName = "Nill";
                    return "Nill";
                }
            else
            {
                _GroupName = "Nill";
                return "Nill";
            }
        }
        #endregion
        #region User Object Roll
        public string SaveObjectUserRoll(ObjectUserRollDataType _SaveData)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblobguserprofile ("
          + "RollID,"
          + "userID,"
          + "ObgID,"
          + "OView,"
          + "OSave,"
          + "OUpdate,"
          + "Odelete,"
          + "OPrint,"
          + "OPostToApp,"
          + "OPostToAcc)"
           + " Values ("
           + "@RollID,"
           + "@userID,"
           + "@ObgID,"
           + "@OView,"
           + "@OSave,"
           + "@OUpdate,"
           + "@Odelete,"
           + "@OPrint,"
           + "@OPostToApp,"
           + "@OPostToAcc)";
            try
                {

                oSqlCommand.Parameters.AddWithValue("@RollID", _SaveData.RollID);
                oSqlCommand.Parameters.AddWithValue("@userID", _SaveData.userID);
                oSqlCommand.Parameters.AddWithValue("@ObgID", _SaveData.ObgID);
                oSqlCommand.Parameters.AddWithValue("@OView", _SaveData.OView);
                oSqlCommand.Parameters.AddWithValue("@OSave", _SaveData.OSave);
                oSqlCommand.Parameters.AddWithValue("@OUpdate", _SaveData.OUpdate);
                oSqlCommand.Parameters.AddWithValue("@Odelete", _SaveData.Odelete);
                oSqlCommand.Parameters.AddWithValue("@OPrint", _SaveData.OPrint);
                oSqlCommand.Parameters.AddWithValue("@OPostToApp", _SaveData.OPostToApp);
                oSqlCommand.Parameters.AddWithValue("@OPostToAcc", _SaveData.OPostToAcc);
                string respond = MYCommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Save ObjectUserRoll");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdateObjectUserRoll(ObjectUserRollDataType _Update)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblobguserprofile Set "
                + "SysID=@SysID,"
                + "RollID=@RollID,"
                + "userID=@userID,"
                + "ObgID=@ObgID,"
                + "OView=@OView,"
                + "OSave=@OSave,"
                + "OUpdate=@OUpdate,"
                + "Odelete=@Odelete,"
                + "OPrint=@OPrint,"
                + "OPostToApp=@OPostToApp,"
                + "OPostToAcc=@OPostToAcc"
                + " Where 1=1 "
                + " and RollID=@RollID"
                + " and userID=@userID"
                + " and ObgID=@ObgID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@SysID", _Update.SysID);
                oSqlCommand.Parameters.AddWithValue("@RollID", _Update.RollID);
                oSqlCommand.Parameters.AddWithValue("@userID", _Update.userID);
                oSqlCommand.Parameters.AddWithValue("@ObgID", _Update.ObgID);
                oSqlCommand.Parameters.AddWithValue("@OView", _Update.OView);
                oSqlCommand.Parameters.AddWithValue("@OSave", _Update.OSave);
                oSqlCommand.Parameters.AddWithValue("@OUpdate", _Update.OUpdate);
                oSqlCommand.Parameters.AddWithValue("@Odelete", _Update.Odelete);
                oSqlCommand.Parameters.AddWithValue("@OPrint", _Update.OPrint);
                oSqlCommand.Parameters.AddWithValue("@OPostToApp", _Update.OPostToApp);
                oSqlCommand.Parameters.AddWithValue("@OPostToAcc", _Update.OPostToAcc);
                string respond = MYCommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Update ObjectUserRoll");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeleteObjectUserRoll(int RollID, int userID, int ObgID)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblobguserprofile"
                + " Where 1=1 "
                + " and RollID=@RollID"
                + " and userID=@userID"
                + " and ObgID=@ObgID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@RollID", RollID);
                oSqlCommand.Parameters.AddWithValue("@userID", userID);
                oSqlCommand.Parameters.AddWithValue("@ObgID", ObgID);
                string respond = MYCommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Delete ObjectUserRoll");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistObjectUserRoll(int RollID, int userID, int ObgID)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tblobguserprofile"
                + " Where 1=1 "
                + " and RollID=@RollID"
                + " and userID=@userID"
                + " and ObgID=@ObgID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@RollID", RollID);
                oSqlCommand.Parameters.AddWithValue("@userID", userID);
                oSqlCommand.Parameters.AddWithValue("@ObgID", ObgID);
                bool respond = MYCommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist User Object");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public string GetExistObjectUserRoll(int RollID, int userID, int ObgID, out ObjectUserRollDataType _ExistData)
            {
            _ExistData = new ObjectUserRollDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "SysID,"
          + "RollID,"
          + "userID,"
          + "ObgID,"
          + "OView,"
          + "OSave,"
          + "OUpdate,"
          + "Odelete,"
          + "OPrint,"
          + "OPostToApp,"
          + "OPostToAcc"
          + " from tblobguserprofile"
          + " Where 1=1 "
                + " and RollID=@RollID"
                + " and userID=@userID"
                + " and ObgID=@ObgID";
            oSqlCommand.Parameters.AddWithValue("@RollID", RollID);
            oSqlCommand.Parameters.AddWithValue("@userID", userID);
            oSqlCommand.Parameters.AddWithValue("@ObgID", ObgID);
            DataRow r = MYCommon.GetDataRowAccount(sqlQuery, oSqlCommand, "Get Exist data ObjectUserRoll");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    int inSysID = 0;
                    resp = int.TryParse(r["SysID"].ToString(), out inSysID);
                    _ExistData.SysID = inSysID;
                    int inRollID = 0;
                    resp = int.TryParse(r["RollID"].ToString(), out inRollID);
                    _ExistData.RollID = inRollID;
                    int inuserID = 0;
                    resp = int.TryParse(r["userID"].ToString(), out inuserID);
                    _ExistData.userID = inuserID;
                    int inObgID = 0;
                    resp = int.TryParse(r["ObgID"].ToString(), out inObgID);
                    _ExistData.ObgID = inObgID;
                    int inOView = 0;
                    resp = int.TryParse(r["OView"].ToString(), out inOView);
                    _ExistData.OView = inOView;
                    int inOSave = 0;
                    resp = int.TryParse(r["OSave"].ToString(), out inOSave);
                    _ExistData.OSave = inOSave;
                    int inOUpdate = 0;
                    resp = int.TryParse(r["OUpdate"].ToString(), out inOUpdate);
                    _ExistData.OUpdate = inOUpdate;
                    int inOdelete = 0;
                    resp = int.TryParse(r["Odelete"].ToString(), out inOdelete);
                    _ExistData.Odelete = inOdelete;
                    int inOPrint = 0;
                    resp = int.TryParse(r["OPrint"].ToString(), out inOPrint);
                    _ExistData.OPrint = inOPrint;
                    int inOPostToApp = 0;
                    resp = int.TryParse(r["OPostToApp"].ToString(), out inOPostToApp);
                    _ExistData.OPostToApp = inOPostToApp;
                    int inOPostToAcc = 0;
                    resp = int.TryParse(r["OPostToAcc"].ToString(), out inOPostToAcc);
                    _ExistData.OPostToAcc = inOPostToAcc;
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
        #region User Rolls
        public String SaveUserRoll()
        {
            String Str1;
            String Respond;
            _RollID = MYCommon.GetSysID("tblUserRoll");
            Str1 = "INSERT INTO tbluserRoll "
            + "(SysID,GrpID,ObgID,Osave,OUpdate,Odelete,Oview,Oprint,DepID)"
            + "Values (" + _RollID + "," + _GroupID + "," + _ObgID + "," + _Osave
            + "," + _Oupdate + "," + _Odelete + "," + _Oview + "," + _Oprint + ",'" + _DepID + "')";
            try
            {
            Respond = MYCommon.ExicuteAnyCommand(Str1, "Save User Rolls");
                if (Respond != "True")
                    return Respond;
                else
                    return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public int  GetObjectID(string Obgname)
        {
            String Str1;
            DataRow Mydr;
            Str1 = "Select SysID From tblobject Where ObgfileName='" + Obgname + "'";
            Mydr = MYCommon.GetDataRowAccount(Str1, "Get Object ID");
            if (Mydr != null)
            {
                _ObgID = Int32.Parse(Mydr.ItemArray.GetValue(0).ToString());
                return _ObgID;
            }
            else
                _ObgID = 0;
            return _ObgID;
        }
        public String GetObjectName(int ID)
        {
            String Str1;
            DataRow Mydr;
            Str1 = "Select ObgfileName,ObgDisplayName From tblobject Where SysID=" + ID;
            Mydr = MYCommon.GetDataRow(Str1, "Get Object Name");
            if (Mydr != null)
            {
                _ObjectName = Mydr.ItemArray.GetValue(0).ToString().Trim();
                _FormName = Mydr.ItemArray.GetValue(1).ToString().Trim();
                return _ObjectName;
            }
            else
                _ObjectName = "Nill";
            return _ObjectName;
        }
        public String GetObjectName()
            {
            String Str1;
            DataRow Mydr;
            Str1 = "Select ObgfileName,ObgDisplayName From tblobject Where SysID=" + _ObgID ;
            Mydr = MYCommon.GetDataRow(Str1, "Get Object Name");
            if (Mydr != null)
                {
                _ObjectName = Mydr.ItemArray.GetValue(0).ToString().Trim();
                _FormName = Mydr.ItemArray.GetValue(1).ToString().Trim();
                return _ObjectName;
                }
            else
                _ObjectName = "Nill";
            return _ObjectName;
            }
        public  void LoadRolls(ComboBox _Cmb)
        {
            string sql1 = "select SysID,RollName from tbluserroll";
            MYCommon.LoadStatusComboAccount(_Cmb, sql1);
        }

        public Boolean ExistUserRoll()
        {
            String Str1;
            DataRow Mydr;
            GetUserGroupID();
            //GetObjectID();
            Str1 = "SELECT Osave,OUpdate,Odelete,Oview,Oprint"
            + " FROM tblUserRoll Where GrpID=" + _GroupID + " AND ObgID=" + _ObgID + " and DepID='" + _DepID + "'";
            Mydr = MYCommon.GetDataRow(Str1, "Exist User Roll");
            if (Mydr != null)
                return true;
            else
                return false;
        }
        public String UpdateUserRolls()
        {
            String Str1;
            String Respond;
            Str1 = "UPDATE tblUserRoll"
            + " SET Osave = " + _Osave
            + ",OUpdate = " + _Oupdate
            + ",Odelete = " + _Odelete
            + ",Oview = " + _Oview
            + ",Oprint = " + _Oprint
            + " WHERE GrpID=" + _GroupID + " AND ObgID=" + _ObgID + " AND DepId='" + _DepID + "'";
            try
            {
            Respond = MYCommon.ExicuteAnyCommand(Str1, "Update User Rolls");
                if (Respond != "True")
                    return Respond;
                else
                    return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string DeleteUser(int  UserID)
        {
            string sql1 = "Delete from tblusernamepassword where sysid=" + UserID;
            string respond = MYCommon.ExicuteAnyCommand(sql1, "Delete User");
            return respond;
        }
        public void  DeleteUserRolls()
        {
            String Str1;
            String Respond;
            Str1 = "Delete From tblUserRoll WHERE GrpID=" + _GroupID + " AND ObgID=" + _ObgID + " AND DepId='" + _DepID + "'";
            try
            {
            Respond = MYCommon.ExicuteAnyCommand(Str1, "Update User Rolls");
               
            }
            catch (Exception ex)
            {
               
            }
        }
        #endregion
        #region Username and Password
        public String SavePassword(out int UID)
        {
            UID = 0;
            _UsysID = MYCommon.GetSysID("tblUserNamePassword");
            MySqlCommand MyCom = new MySqlCommand("addusername", null);
            MyCom.CommandType = CommandType.StoredProcedure;
            MyCom.Parameters.Add(new MySqlParameter("SysID", MySqlDbType.Int16));
            MyCom.Parameters.Add(new MySqlParameter("Username", MySqlDbType.VarChar, 20));
            MyCom.Parameters.Add(new MySqlParameter("PSW", MySqlDbType.VarChar, 20));
            MyCom.Parameters.Add(new MySqlParameter("GPD", MySqlDbType.Int16));
            MyCom.Parameters.Add(new MySqlParameter("Status1", MySqlDbType.VarChar, 12));
            MyCom.Parameters.Add(new MySqlParameter("DepID", MySqlDbType.VarChar, 5));
            MyCom.Parameters["SysID"].Value = _UsysID;
            MyCom.Parameters["Username"].Value = _UserName;
            MyCom.Parameters["PSW"].Value = _Password;
            MyCom.Parameters["GPD"].Value = _GroupID;
            MyCom.Parameters["Status1"].Value = _Status;
            MyCom.Parameters["DepID"].Value = _DepID;

            try
            {
               string resp= MYCommon.ExicuteAnyCommand( MyCom,"Save User Name");
               
                UID = _UsysID;
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public DataTable GetMenuObjectList(int PerentID, int NodLevel)
            {

                string Sql1 = "SELECT SysID, ObgDisplayName, ObgfileName, PerentID, IconID, NodLevel FROM tblobject where PerentID=" + PerentID + " and NodLevel=" + NodLevel;
                DataTable tb = MYCommon.GetDataTableAccount(Sql1, "get Object List");
            return tb;
            }
        public String UpdateUserDetails()
        {

            MySqlCommand MyCom = new MySqlCommand("UpdateUserdetails",null);
            MyCom.CommandType = CommandType.StoredProcedure;
            MyCom.Parameters.Add(new MySqlParameter("UserID", MySqlDbType.Int16));
            MyCom.Parameters.Add(new MySqlParameter("Username", MySqlDbType.VarChar, 20));
            MyCom.Parameters.Add(new MySqlParameter("PSW", MySqlDbType.VarChar, 20));
            MyCom.Parameters.Add(new MySqlParameter("GPD", MySqlDbType.Int16));
            MyCom.Parameters.Add(new MySqlParameter("Status1", MySqlDbType.VarChar, 12));
            MyCom.Parameters.Add(new MySqlParameter("DepID", MySqlDbType.VarChar, 5));
            MyCom.Parameters["UserID"].Value = _UsysID;
            MyCom.Parameters["Username"].Value = _UserName;
            MyCom.Parameters["PSW"].Value = _Password;
            MyCom.Parameters["GPD"].Value = _GroupID;
            MyCom.Parameters["Status1"].Value = "Active";
            MyCom.Parameters["DepID"].Value = _DepID;
            try
            {
                string resp = MYCommon.ExicuteAnyCommand(MyCom, "Save User Name");

                return resp;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public String GetPassword()
        {
            String Str1;
            DataRow Myr;
            String Pwd1;
            Str1 = "Select Upassword  From tblUserNamePassword Where "
            + "UserName='" + _UserName + "'";
            Myr = MYCommon.GetDataRow(Str1, "Get Password");
            try
            {
                if (Myr != null)
                {
                    Pwd1 = Myr.ItemArray.GetValue(0).ToString().Trim();
                    _Password = MyValidation.Getpassword(Pwd1);
                    return _Password;
                }
                else
                    return "Nill";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
        public Boolean ExistUser()
        {
            String Str1;
            DataRow MyDr;
            Str1 = "Select UserName  From tblUserNamePassword Where "
            + "UserName='" + _UserName + "'";
            MyDr = MYCommon.GetDataRow(Str1, "Exist User");
            if (MyDr != null)
                return true;
            else
                return false;

        }

        public string GetUserDepartment(string UserName1)
        {
            String Str1;
            DataRow MyDr;
            Str1 = "Select DepID  From tblUserNamePassword Where "
            + "UserName='" + UserName1 + "'";
            MyDr = MYCommon.GetDataRow(Str1, "Get User Department");
            if (MyDr != null)
                return MyDr["DepID"].ToString();
            else
                return "Nill";
        }
        public Int32 GetUserID(String UserN)
        {
            String Str1;
            DataRow MyDr;
            Str1 = "Select SysID  From tblUserNamePassword Where "
            + "UserName='" + UserN + "'";
            MyDr = MYCommon.GetDataRow(Str1, "Exist User");
            if (MyDr != null)
                return Int32.Parse(MyDr.ItemArray.GetValue(0).ToString());
            else
                return 0;

        }
        public string GetUserName(int UID)
        {
            String Str1;
            DataRow MyDr;
            Str1 = "Select UserName From tblUserNamePassword Where SysID=" + UID;

            MyDr = MYCommon.GetDataRow(Str1, "Get User Name");
            if (MyDr != null)
                return MyDr["UserName"].ToString();
            else
                return "Nill";

        }
        public bool GetUserLogingStatus(int IUD)
        {
            string sql1 = "Select Status from tblusernamepassword Where sysId=" + IUD;
            DataRow r = MYCommon.GetDataRow (sql1, "Get Status");
            if (r != null)
            {
                if (string.IsNullOrEmpty(r["Status"].ToString()) == false)
                {
                    if (int.Parse(r["Status"].ToString()) == 1)
                        return true;
                    else
                        return false;
                }
                else
                    return false;


            }
            else
                return false;
        }
        public String UpdateUser()
        {

            MySqlCommand MyCom = new MySqlCommand("UpdateUserPassword",null);
            MyCom.CommandType = CommandType.StoredProcedure;
            MyCom.Parameters.Add(new MySqlParameter("@Uname", MySqlDbType.VarChar, 20));
            MyCom.Parameters.Add(new MySqlParameter("@Upwd", MySqlDbType.VarChar, 20));
            MyCom.Parameters["@Uname"].Value = _UserName;
            MyCom.Parameters["@Upwd"].Value = _Password;
            try
            {
                string resp = MYCommon.ExicuteAnyCommand(MyCom, "Save User Name");
                return resp;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public void GetUserRight()
        {
            MyCurrentUserRight MyRight = new MyCurrentUserRight(); 
            String Pdw = MyValidation.Getpassword(_Password);
            DataTable MyDr = new DataTable();
            MySqlCommand MyCom = new MySqlCommand("GetPWD",null  );
            MyCom.CommandType = CommandType.StoredProcedure;
            MyCom.Parameters.Add(new MySqlParameter("@Username1", MySqlDbType.VarChar, 20));
            MyCom.Parameters.Add(new MySqlParameter("@PW", MySqlDbType.VarChar, 20));
            MyCom.Parameters["@Username1"].Value = _UserName;
            MyCom.Parameters["@PW"].Value = Pdw;
            MySqlDataAdapter MyDA = new MySqlDataAdapter(MyCom);
            MyDA.Fill(MyDr);
            if (MyDr != null)
            {
                for (int i = 0; i < MyDr.Rows.Count; i++)
                {
                    MyRight.ObgID = Int32.Parse(MyDr.Rows[i].ItemArray.GetValue(0).ToString());
                    _ObgID = Int32.Parse(MyDr.Rows[i].ItemArray.GetValue(0).ToString());
                    GetObjectName();
                    MyRight.ObgName = _ObjectName;
                    MyRight.FormName = _FormName;
                    MyRight.Save = Int16.Parse(MyDr.Rows[i].ItemArray.GetValue(1).ToString());
                    MyRight.Update = Int16.Parse(MyDr.Rows[i].ItemArray.GetValue(2).ToString());
                    MyRight.Delete = Int16.Parse(MyDr.Rows[i].ItemArray.GetValue(3).ToString());
                    MyRight.View = Int16.Parse(MyDr.Rows[i].ItemArray.GetValue(4).ToString());
                    MyRight.Print = Int16.Parse(MyDr.Rows[i].ItemArray.GetValue(5).ToString());
                    MyRight.Department = MyDr.Rows[i].ItemArray.GetValue(6).ToString();
                    MyActiveUserRight.Add(MyRight);
                }
               
               
            }
        }
        public Int32 SaveUserLogin(Int32 Uid1)
        {
            String Str1;
            String Respond;
            Int32 LogSysID = MYCommon.GetSysID("tblUserlog");
            Str1 = "INSERT INTO tblUserlog"
                + "(sysID,Uid,UlogDate,"
                + "UlogTime)"
                + " VALUES(" + LogSysID + "," + Uid1
                + ",curDate(),'" + DateTime.Now.ToLongTimeString() + "')";
            Respond = MYCommon.ExicuteAnyCommand(Str1, "Save User Loging");
            if (Respond != "True")
                return 0;
            else
                return LogSysID;
        }
        public int GetDepartMentPriority(string DepName)
        {
            string sql1 = "Select Priority From tbldepartment Where DepName='" + DepName + "'";
            DataRow Myrow = MYCommon.GetDataRow (sql1, "GetDepartMentPriority");
            if (Myrow != null)
            {
                if (string.IsNullOrEmpty(Myrow["Priority"].ToString()) == false)
                    return int.Parse(Myrow["Priority"].ToString());
                else
                    return 0;
            }
            else
                return 0;
  
        }
        public void ChangeUserLogingStatus(int UserID,int Status)
        {
            string sql1 = "Update tblusernamepassword set Status=" + Status + " Where SysID=" + UserID;
            string respond = MYCommon.ExicuteAnyCommand(sql1, "Update Losing Status");  

        }
        public void UserLogOut(Int32 RecNo)
        {
            String Str1;
            Str1 = "Update tblUserlog Set UlogOutDate=CurDate(),UlogOutTime='"
                + DateTime.Now.ToLongTimeString() + "' Where Sysid=" + RecNo;
            String Respond = MYCommon.ExicuteAnyCommand(Str1, "Update User Logout");
        }

        #endregion
        #region On WeB
        //public string SaveUserHandling(UserHandlingType _SaveData)
        //{
        //    MySqlCommand oSqlCommand = new MySqlCommand();
        //    int SysID = MYCommon.GetSysID("tblusernamepassword", null);
        //    string sqlQuery = "Insert Into tblusernamepassword ("
        //  + "SysID,"
        //  + "UserName,"
        //  + "UPassword,"
        //  + "GroupID,"
        //  + "CurrentStatus,"
        //  + "CreateDate,"
        //  + "DepID,"
        //  + "Status)"
        //   + " Values ("
        //   + "@SysID,"
        //   + "@UserName,"
        //   + "@UPassword,"
        //   + "@GroupID,"
        //   + "@CurrentStatus,"
        //   + "CurDate(),"
        //   + "@DepID,"
        //   + "@Status)";
        //    try
        //    {
        //        oSqlCommand.Parameters.AddWithValue("@SysID", SysID);
        //        oSqlCommand.Parameters.AddWithValue("@UserName", _SaveData.UserName);
        //        oSqlCommand.Parameters.AddWithValue("@UPassword", _SaveData.UPassword);
        //        oSqlCommand.Parameters.AddWithValue("@GroupID", _SaveData.GroupID);
        //        oSqlCommand.Parameters.AddWithValue("@CurrentStatus", _SaveData.CurrentStatus);
        //        oSqlCommand.Parameters.AddWithValue("@DepID", _SaveData.DepID);
        //        oSqlCommand.Parameters.AddWithValue("@Status", _SaveData.Status);
        //        string respond = MYCommon.ExicuteAnyCommandOnWeb(sqlQuery, oSqlCommand, "Save UserName");
        //        return respond;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //public string UpdateUserHandling(UserHandlingType _Update)
        //{
        //    MySqlCommand oSqlCommand = new MySqlCommand();
        //    string sqlQuery = "Update tblusernamepassword Set "
        //        + "SysID=@SysID,"
        //        + "UserName=@UserName,"
        //        + "UPassword=@UPassword,"
        //        + "GroupID=@GroupID,"
        //        + "CurrentStatus=@CurrentStatus,"
        //        + "CreateDate=CurDate(),"
        //        + "DepID=@DepID,"
        //        + "Status=@Status"
        //        + "Where 1=1 "
        //        + " and UserName=@UserName"
        //        + " and UPassword=@UPassword";
        //    try
        //    {
        //        oSqlCommand.Parameters.AddWithValue("@SysID", _Update.SysID);
        //        oSqlCommand.Parameters.AddWithValue("@UserName", _Update.UserName);
        //        oSqlCommand.Parameters.AddWithValue("@UPassword", _Update.UPassword);
        //        oSqlCommand.Parameters.AddWithValue("@GroupID", _Update.GroupID);
        //        oSqlCommand.Parameters.AddWithValue("@CurrentStatus", _Update.CurrentStatus);
        //        oSqlCommand.Parameters.AddWithValue("@DepID", _Update.DepID);
        //        oSqlCommand.Parameters.AddWithValue("@Status", _Update.Status);
        //        string respond = MYCommon.ExicuteAnyCommandOnWeb(sqlQuery, oSqlCommand, "Update on web UserName");
        //        return respond;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //public string DeleteUserHandling(string UserName, string UPassword)
        //{
        //    MySqlCommand oSqlCommand = new MySqlCommand();
        //    string sqlQuery = "Delete from tblusernamepassword"
        //        + " Where 1=1 "
        //        + " and UserName=@UserName"
        //        + " and UPassword=@UPassword";
        //    try
        //    {
        //        oSqlCommand.Parameters.AddWithValue("@UserName", UserName);
        //        oSqlCommand.Parameters.AddWithValue("@UPassword", UPassword);
        //        string respond = MYCommon.ExicuteAnyCommandOnWeb(sqlQuery, oSqlCommand, "Delete UserName");
        //        return respond;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        //public bool ExistUserHandling(string UserName, string UPassword)
        //{
        //    MySqlCommand oSqlCommand = new MySqlCommand();
        //    string sqlQuery = "Select SysID from tblusernamepassword"
        //        + " Where 1=1 "
        //        + " and UserName=@UserName"
        //        + " and UPassword=@UPassword";
        //    try
        //    {
        //        oSqlCommand.Parameters.AddWithValue("@UserName", UserName);
        //        oSqlCommand.Parameters.AddWithValue("@UPassword", UPassword);
        //        bool respond = MYCommon.ExistIntableOnWeb(sqlQuery, oSqlCommand);
        //        return respond;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}
        public string GetExistUserHandling(string UserName, string UPassword, out UserHandlingType _ExistData)
        {
            _ExistData = new UserHandlingType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "SysID,"
          + "UserName,"
          + "UPassword,"
          + "GroupID,"
          + "CurrentStatus,"
          + "CreateDate,"
          + "DepID,"
          + "Status"
          + " from tblusernamepassword"
          + " Where 1=1 "
                + " and UserName=@UserName"
                + " and UPassword=@UPassword";
            oSqlCommand.Parameters.AddWithValue("@UserName", UserName);
            oSqlCommand.Parameters.AddWithValue("@UPassword", UPassword);
            DataRow r = MYCommon.GetDataRow(sqlQuery, oSqlCommand, null, "Update User ");
            if (r != null)
            {
                try
                {
                    bool resp = false;
                    int inSysID = 0;
                    resp = int.TryParse(r["SysID"].ToString(), out inSysID);
                    _ExistData.SysID = inSysID;
                    _ExistData.UserName = r["UserName"].ToString();
                    _ExistData.UPassword = r["UPassword"].ToString();
                    int inGroupID = 0;
                    resp = int.TryParse(r["GroupID"].ToString(), out inGroupID);
                    _ExistData.GroupID = inGroupID;
                    _ExistData.CurrentStatus = r["CurrentStatus"].ToString();
                    DateTime dtCreateDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["CreateDate"].ToString(), out dtCreateDate);
                    _ExistData.CreateDate = dtCreateDate;
                    _ExistData.DepID = r["DepID"].ToString();
                    int inStatus = 0;
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
                return "data not found ";
        }
#endregion
    }
}
