using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataService;
using System.Windows.Forms;
using System.Management;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
namespace BusinessLayer.CommonOperation
{
    public class CommonOperations:DataService 
    {
    
       
        private bool LoggingAsLocally = false;

        public CommonOperations(bool Islocal)
        {
            LoggingAsLocally = Islocal;
        
            
        }
        
        ~CommonOperations()
        {

        CloseDB();

        }
     //   public static MySqlConnection WEBConnection = new MySqlConnection(ConfigurationManager.AppSettings["saticinweb"].ToString());    
        public int GetObjectID(string Obgname)
        {
            int _ObgID = -1;
            String Str1;
            DataRow Mydr;
            Str1 = "Select SysID From tblobject Where ObgfileName='" + Obgname + "'";
            Mydr = GetDataRowAccount(Str1, "Get Object ID");
            if (Mydr != null)
            {
                _ObgID = Int32.Parse(Mydr.ItemArray.GetValue(0).ToString());
                return _ObgID;
            }
            else
                _ObgID = 0;
            return _ObgID;
        }

        public struct ThreeSConfiguration
        {
            public  string UserName;
            public  int CompanyID;
            public  bool LoggingAsLocal;
            public  bool IsAuthenticated;
            public  string DBName;
            public  string DBUsername;
            public  string DBPasswd;
            public  string DBServerName;
            public  string JobNumber;
            public  string ChequeNumber;
            public  int CurrentAccPeriod;
            public  decimal ExchangeRate;
            public  string HomeCurreny;
        }
        public enum EavlState
        {
            NotEvaluat = 0,
            Evaluated = 1,
        }
        public void CloseAllDataBase()
        {
          CloseDB();
        }
        //public bool ExistCalibrationCell(string Serial,out string Err)
        //{
        //    string sql1="Select sn from sn where sn=" + Serial ;
        //    string Er1 = "";
        //    DataRow r = GetOLEDBDataRow(sql1, "Check Serial from Calibation",out  Er1);
        //    Err = Er1;
        //    if (r != null)
        //        return true;
        //    else
        //        return false;
        //}

       //===============
        

        //==============

     
      


         
        public string GetDepartment(string DepCode)
        {
            string sql1 = "Select DepName From tbldepartment Where DepID='" + DepCode + "'";
            DataRow Myrow = GetDataRow(sql1, "GetDepartment");
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
            DataRow Myrow = GetDataRow(sql1, "GetDepartmentCode");
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

        public void UpdateSysConfig(ThreeSConfiguration  MyCnf)
        {
            String Str1="";
            String Respond;
            //Str1 = "Update tblcompanyconfig Set "
            //+ "CompanyName='" + MyCnf.CompanyName + "'"
            //+ ",Address1='" + MyCnf.Add1 + "'"
            //+ ",Address2='" + MyCnf.Add2 + "'"
            //+ ",TPnumber='" + MyCnf.Telephone + "'"
            //+ ",FaxNumber='" + MyCnf.Fax + "'"
            //+ ",Email='" + MyCnf.EmailAddress + "'"
            //+ ",BackGroundPictureParth='" + MyCnf.BackgoundPic + "'"
            //+ ",ReportPath=" + MyCnf.ReportPath + "'"
            //+ ",LastPurchaseBill='" + MyCnf.CuPurchaseBillnumber + "'"
            //+ ",LastSalesBill='" + MyCnf.CuSalesBillNo + "'"
            //+ " Where SysID=1000";
            Respond = ExicuteAnyCommand(Str1, "Save Config");
        }
        public bool ExistIntable(string Sqlstring)
        {
            string Mgx="";
            if (Sqlstring.Length > 175)
                Mgx = Sqlstring.Substring(0, 175);
            else
                Mgx = Sqlstring;

            DataRow Myrow = GetDataRow(Sqlstring, "ExistIntable-" + Mgx);
            if (Myrow != null)
                return true;
            else
                return false;
        }
        public bool ExistIntable(string Sqlstring,MySqlCommand cmdObg)
        {
            string Mgx = "";
            if (Sqlstring.Length > 175)
                Mgx = Sqlstring.Substring(0, 175);
            else
                Mgx = Sqlstring;

            DataRow Myrow = GetDataRow(Sqlstring,cmdObg,null , "ExistIntable-" + Mgx);
            if (Myrow != null)
                return true;
            else
                return false;
        }
        
        public string  ExistIntable(string Sqlstring, string filedRequired)
        {
            DataRow Myrow = GetDataRow(Sqlstring, "Exist Intable Result");
            string strResult;
            if (Myrow != null)
            {
                strResult = Myrow[filedRequired].ToString();
                return strResult;
            }
            else
                strResult = "Nill";
            return strResult;
        }
        /// <summary>
        /// Give Give Current connection with text command
        /// </summary>
        /// <param name="_CurrentCommad"></param>
        /// <returns></returns>
        public bool ExistIntable(MySqlCommand _CurrentCommad)
        {
            return ExistInTable(_CurrentCommad);
        }
        public void  ClearCurrentPanel(ref Panel CurrentPanel)
        {
            foreach (Control  item in CurrentPanel.Controls )
            {

                if (item is TextBox)
                    {
                        ((TextBox)item).Text = String.Empty;
                    }
                if (item is ComboBox )
                {
                    ((ComboBox)item).Text = String.Empty;
                }
                if (item is CheckBox)
                {
                    ((CheckBox)item).Checked =false ;
                }
                if (item is DataGridView )
                {
                    ((DataGridView)item).Rows .Clear ();
                }
            }
        }
        public void ClearCurrentPanelTestAndCombo(ref Panel CurrentPanel)
        {
            foreach (Control item in CurrentPanel.Controls)
            {

                if (item is TextBox)
                {
                    ((TextBox)item).Text = String.Empty;
                }
                if (item is ComboBox)
                {
                    ((ComboBox)item).Text = String.Empty;
                }
                if (item is CheckBox)
                {
                    ((CheckBox)item).Checked = false;
                }
              
            }
        }
        public void LoadCompany(ComboBox cmb)
        {
            string sql1 = "SELECT SysID, CompanyName FROM tblcompanyconfig";
            LoadStatusComboAccount(cmb, sql1);
        }
        public int GetMaxNumber(List<int> _NumberSeries)
            {
            int[] array1;
            array1 = _NumberSeries.ToArray();
            int m = array1[0];
            for (int i = 0; i < array1.Length; i++)
                {
                int mx = array1[i];
                if (m < mx)
                    m = mx;
                }
            return m = m + 1;

            }
        public void LoadCurrency(ComboBox cmb)
        {
            string sql1 = "Select Exrate,CurID from tblcurrency";
            LoadStatusComboAccount(cmb, sql1);
        }
        public bool ExistIntable(string Sqlstring, EavlState CurrentStatus)
        {
            DataRow Myrow = GetDataRow(Sqlstring, "ExistIntable");
            int Curst;
            if (Myrow != null)
            {
                if (string.IsNullOrEmpty(Myrow[0].ToString()) == false)
                {
                    switch (CurrentStatus)
                    {
                        case EavlState.Evaluated:
                            Curst = 1;
                            break;
                        case EavlState.NotEvaluat:
                            Curst = 0;
                            break;

                        default:
                            Curst = 1000;
                            break;
                    }
                    if (int.Parse(Myrow[0].ToString()) == Curst)
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
        public ThreeSConfiguration  CompanyCurrentConfig;
     
        #region Account
        
        
        
        

        public int ExistInDataGridview( DataGridView  _GridName, string ColumName, string CompareWith)
        {
               
            if (_GridName.RowCount > 0)
            {
                for (int i = 0; i < _GridName.RowCount; i++)
                {
                    if (_GridName.Rows[i].Cells[ColumName].Value.ToString() == CompareWith)
                        return i;
                }
                return -1;
            }
            else
                return -1;
        }
        public string GetDefinitionColunmName(string DefinitionHeader, string CategoryName)
        {
            string Str1 = "Select tblColunmname from tblexpendituredef Where Categoty='" + CategoryName + "' and tblHederName='" + DefinitionHeader + "'";
            DataRow Myrow;
            Myrow = GetDataRow(Str1, "Get Definition Header");
            if (Myrow != null)
                return Myrow["tblColunmname"].ToString();
            else
                return "Nill";
        }
        
       

        public void SetSystemDateFromDBServer()
        {
            DateTime d1 = GetDBDate();
            string t1 = GetDBTime();
            SYSTEMTIME CurDateTime = new SYSTEMTIME();
            CurDateTime.wYear = short.Parse(d1.Year.ToString()) ;
            CurDateTime.wMonth = short.Parse(d1.Month.ToString()) ;
            CurDateTime.wDay = short.Parse(d1.Day.ToString());
            string[] strsplit = t1.Split(':');
            CurDateTime.wHour = short.Parse(strsplit[0]);
            CurDateTime.wMinute = short.Parse(strsplit[1]);
           CurDateTime.wSecond =  short.Parse(strsplit[2]);
           SetLocalTime(ref CurDateTime);
        }
        public DateTime GetDBDate()
        {
           string  sql1 = "select curdate() as 'CurDate'";
           DataRow r = GetDataRow(sql1, "Get DB DateTime");
            DateTime d1;
            if (r != null)
                d1 = DateTime.Parse(r["CurDate"].ToString());
            else
                d1 = DateTime.MinValue;
            return d1;
        }
        public string  GetDBTime()
        {
            string sql1 = "select DATE_FORMAT(now(),'%H:%i:%s') as 'Curtime'";
            DataRow r = GetDataRow(sql1, "Get DB DateTime");
            string d1;
            if (r != null)
            {
                d1 = r["Curtime"].ToString();  
            }
            else
                d1 = "12:00:00";
            return d1;
        }

        public Int32 GetSysID(String tblName)
        {
            String str1;

            str1 = "Select Max(sysID) as 'MaxID' From " + tblName;
            DataRow myRow = GetDataRow(str1, "Generate System ID");
            if (myRow != null)
            {
                if (string.IsNullOrEmpty(myRow["MaxID"].ToString()) == false)
                    return Int32.Parse(myRow.ItemArray.GetValue(0).ToString()) + 1;
                else
                    return 1000;
            }
            else
                return 1000;

        }
        public void LoadDatatoComboWithOutBind(ComboBox ObgCombo, DataTable mytb, String DisplayMember, bool NeedDisplayContinue)
        {
          
            ObgCombo.Items.Clear();
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    if (NeedDisplayContinue == false)
                    {
                        ObgCombo.Enabled = true;
                    }

                    //ObgCombo.Items.Add(NillKey);
                    foreach (DataRow Myrow in mytb.Rows)
                    {
                        ObgCombo.Items.Add(Myrow[DisplayMember].ToString());
                    }
                }
                else
                {
                    if (NeedDisplayContinue == false)
                    {
                        ObgCombo.Enabled = false;
                    }
                    ObgCombo.Items.Add("Nill");
                }
            }
            else
            {
                if (NeedDisplayContinue == false)
                {
                    ObgCombo.Enabled = false;
                }
                ObgCombo.DataSource = null;
                ObgCombo.DataBindings.Clear();
            }
        }
        public string GetSelectedID(ComboBox cmb,bool GetValue)
        {
   
            ComboboxItem cmbItem = new ComboboxItem();
            try
            {
                cmbItem = (ComboboxItem)cmb.SelectedItem;
                if (GetValue)
                    return cmbItem.Value.ToString();
                else
                    return cmbItem.Text;
            }
            catch (Exception ex)
            {

                return "";
            }

        }
        public void LoadDatatoComboWithOutBind(ComboBox ObgCombo, String CmdStr, String DisplayMember, bool NeedDisplayContinue)
        {
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, "LoadDatatoComboNotBind");
            ObgCombo.Items.Clear();
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    if (NeedDisplayContinue == false)
                    {
                        ObgCombo.Enabled = true;
                    }

                    //ObgCombo.Items.Add(NillKey);
                    foreach (DataRow Myrow in mytb.Rows)
                    {
                        ObgCombo.Items.Add(Myrow[DisplayMember].ToString());
                    }
                }
                else
                {
                    if (NeedDisplayContinue == false)
                    {
                        ObgCombo.Enabled = false;
                    }
                    ObgCombo.Items.Add("Nill");
                }
            }
            else
            {
                if (NeedDisplayContinue == false)
                {
                    ObgCombo.Enabled = false;
                }
                ObgCombo.DataSource = null;
                ObgCombo.DataBindings.Clear();
            }
        }
        public void LoadToComboWithSelectedValue(ComboBox ObgCombo, DataTable mytb, string ID, string Textvalue)
        {


           
            ObgCombo.Items.Clear();

            if (mytb != null)
            {
                foreach (DataRow r in mytb.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = r[Textvalue].ToString();
                    item.Value = r[ID].ToString();
                    ObgCombo.Items.Add(item);
                }
            }


        }
        public void LoadToComboWithSelectedValue(ComboBox ObgCombo, string CmdStr,string ID,string Textvalue)
        {
            
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, "Load Status Combo");
            ObgCombo.Items.Clear();

            if (mytb != null)
            {
                foreach (DataRow r in mytb.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = r[Textvalue].ToString();
                    item.Value = r[ID].ToString();
                    ObgCombo.Items.Add(item);
                }
            }


        }
        
        public void LoadStatusComboAccount(ComboBox ObgCombo, int Type)
        {
            string CmdStr = "Select ID,StatusName from tblcombostatus where Category=" + Type + " Order by ID asc";
            DataTable mytb = new DataTable();
            mytb = GetDataTableAccount(CmdStr, "Load Status Combo");
            ObgCombo.Items.Clear();

            if (mytb != null)
            {
                foreach (DataRow r in mytb.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = r["StatusName"].ToString();
                    item.Value = r["ID"].ToString();
                    ObgCombo.Items.Add(item);

                }
            }


        }
        public void LoadStatusCombo(ComboBox ObgCombo, int Type)
        {
            string CmdStr = "Select ID,StatusName from tblcombostatus where Category=" + Type + " Order by ID asc";
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, "Load Status Combo"); 
            ObgCombo.Items.Clear();

            if (mytb != null)
            {
                foreach (DataRow r in mytb.Rows )
                {
                  ComboboxItem item = new ComboboxItem();
                  item.Text  = r["StatusName"].ToString();
                  item.Value = r["ID"].ToString();
                  ObgCombo.Items.Add(item);
                  
                }
            }
            
           
        }
        public void LoadStatusCombo(ComboBox ObgCombo, string CmdStr)
        {
           // string CmdStr = "Select ID,StatusName from tblcombostatus where Category=" + Type + " Order by ID asc";
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, "Load Status Combo");
            ObgCombo.Items.Clear();

            if (mytb != null)
            {
                foreach (DataRow r in mytb.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = r[1].ToString();
                    item.Value = r[0].ToString();
                    ObgCombo.Items.Add(item);

                }
            }


        }
        public void LoadStatusComboAccount(ComboBox ObgCombo, string CmdStr)
        {
            DataTable mytb = new DataTable();
            mytb = GetDataTableAccount(CmdStr, "Load Status Combo");
            ObgCombo.Items.Clear();

            if (mytb != null)
            {
                foreach (DataRow r in mytb.Rows)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = r[1].ToString();
                    item.Value = r[0].ToString();
                    ObgCombo.Items.Add(item);

                }
            }


        }
        public string Is64BitOperatingSystem()
        {
            string architectureStr;
            if (Directory.Exists(Environment.GetFolderPath(
                                   Environment.SpecialFolder.ProgramFiles)))
            {
                architectureStr = "32-bit";
            }
            else
            {
                architectureStr = "64-bit";
            }
            return architectureStr;
          
        }
       
        public void LoadStatusComboWithExtraword(ComboBox ObgCombo, string CmdStr,string IDvalue,string Displaytext)
        {

            // string CmdStr = "Select ID,StatusName from tblcombostatus where Category=" + Type + " Order by ID asc";
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, "Load Status Combo");
            ObgCombo.Items.Clear();

            ComboboxItem item = new ComboboxItem();
            item.Text = Displaytext;
            item.Value = IDvalue;
            ObgCombo.Items.Add(item);

            if (mytb != null)
            {
                foreach (DataRow r in mytb.Rows)
                {
                    item = new ComboboxItem();
                    item.Text = r[1].ToString();
                    item.Value = r[0].ToString();
                    ObgCombo.Items.Add(item);

                }
            }


        }
        public string GetStatusComboText(int ID, int Category)
        {
            string sql1 = "Select StatusName from tblcombostatus where ID=" + ID + " and Category=" + Category;
            DataRow r = GetDataRow(sql1, "Get Status Combo Text");
            if (r != null)
                return r["StatusName"].ToString();
            else
                return "-----";
        }
        public void LoadDatatoCombo(ComboBox ObgCombo, String CmdStr, String DisplayMember, bool NeedDisplayContinue)
        {
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, DisplayMember);
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    if (NeedDisplayContinue == false)
                    {
                        ObgCombo.Enabled  = true;
                    }

                    ObgCombo.DataSource = mytb;
                    ObgCombo.DisplayMember = DisplayMember;
                }
                else
                {
                    if (NeedDisplayContinue == false)
                    {
                        ObgCombo.Enabled = false;
                    }
                    ObgCombo.DataSource = null;
                    ObgCombo.DataBindings.Clear();
                }
            }
            else
            {
                if (NeedDisplayContinue == false)
                {
                    ObgCombo.Enabled = false;
                }
                ObgCombo.DataSource = null;
                ObgCombo.DataBindings.Clear();
            }
        }
        public void LoadDatatoCombo(ListBox ObgCombo, String CmdStr, String DisplayMember, bool NeedDisplayContinue, int MaxNumOfRecToDisplay)
        {
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, DisplayMember);
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    if (NeedDisplayContinue == false)
                    {
                        ObgCombo.Visible  = true;
                    }
                    if (MaxNumOfRecToDisplay < mytb.Rows.Count)
                        ObgCombo.Height = 19 * MaxNumOfRecToDisplay;
                    else
                    {
                        ObgCombo.Height = 19 * mytb.Rows.Count;
                    }
                    ObgCombo.DataSource = mytb;
                    ObgCombo.DisplayMember = DisplayMember;
                }
                else
                {
                    if (NeedDisplayContinue == false)
                    {
                        ObgCombo.Visible = false;
                    }
                    ObgCombo.DataBindings.Clear();
                }
            }
            else
            {
                if (NeedDisplayContinue == false)
                {
                    ObgCombo.Visible = false;
                }
                ObgCombo.DataSource = null;
                ObgCombo.DataBindings.Clear();
            }
        }
        public void LoadDatatoCombo(ListBox ObgCombo, String CmdStr, String DisplayMember)
        {
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, DisplayMember);
            ObgCombo.Items.Clear();  
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    foreach (DataRow Myr in mytb.Rows)
                    {
                        ObgCombo.Items.Add(Myr[DisplayMember].ToString());    
                    }
                  
                }
                else
                {
                   ObgCombo.Items.Clear();
                }
            }
            else
            {
                ObgCombo.Items.Clear();
            }
        }
        public void LoadDatatoCombo(ComboBox ObgCombo, String CmdStr, String DisplayMember, bool NeedDisplayContinue, string ExtraWord)
        {
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, DisplayMember);
            ObgCombo.Items.Clear();
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    if (NeedDisplayContinue == false)
                    {
                        ObgCombo.Enabled = true;
                    }

                    ObgCombo.Items.Add(ExtraWord);
                    foreach (DataRow Myrow in mytb.Rows)
                    {
                        ObgCombo.Items.Add(Myrow[DisplayMember].ToString());
                     }
                }
                else
                {
                    if (NeedDisplayContinue == false)
                    {
                        ObgCombo.Enabled = false;
                    }
                    ObgCombo.Items.Add("All");
                }
            }
            else
            {
                if (NeedDisplayContinue == false)
                {
                    ObgCombo.Enabled = false;
                }
                ObgCombo.DataSource = null;
                ObgCombo.DataBindings.Clear();
            }
        }
        public void LoadDatatoTable(DataGridView ObgTbl, String CmdStr, String FnName)
        {
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, FnName);
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    ObgTbl.Enabled = true;
                   // ObgTbl.BackgroundColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.AppWorkspace);
                    ObgTbl.DataSource = mytb;
                    for (int i = 0; i < ObgTbl.RowCount; i++)
                    {
                        //ObgTbl.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                        //if (i % 2 == 1)
                        //{
                        //    ObgTbl.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(251, 254, 181);
                        //}
                        //else
                        //{
                        //    ObgTbl.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(187,247,245);
                        //}
                        //ObgTbl.Refresh();
                    }
                    DocolourThisGrid(ObgTbl);
                }
            }
            else
            {
                ObgTbl.DataSource = DBNull.Value;
            }
        }
        public void LoadDatatoTableWithoutBind(DataGridView ObgTbl, String CmdStr, String FnName, string ConField, System.Drawing.Color  TrC,  System.Drawing.Color FlC)
        {
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, FnName);
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    Int32 NofColoums = mytb.Columns.Count;
                    String[] ParamCollection = new String[NofColoums];
                    ObgTbl.Rows.Clear();
                    int i = 0;
                    foreach (DataRow  Myr in  mytb.Rows)
                    {
                        
                        for (int x = 0; x < NofColoums; x++)
                        {
                            string MCX = mytb.Columns[x].DataType.ToString();
                            if (MCX != "System.DateTime")
                                ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                            else
                            {
                                DateTime Xdate;
                                if (string.IsNullOrEmpty(mytb.Rows[i].ItemArray.GetValue(x).ToString()) == false)
                                    Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                else
                                    Xdate = DateTime.MinValue;
                                ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                            }
                        }
                        ObgTbl.Rows.Add(ParamCollection);
                        ObgTbl.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                        if (Myr[ConField].ToString()   == "1")
                        {
                            ObgTbl.Rows[i].DefaultCellStyle.ForeColor  = TrC ; //  System.Drawing.Color.FromArgb(251, 254, 181);
                        }
                        else
                        {
                            ObgTbl.Rows[i].DefaultCellStyle.ForeColor = FlC;// System.Drawing.Color.FromArgb(176, 176, 255);
                        }
                        i += 1;
                    }
                }
            }
            else
                ObgTbl.Rows.Clear();
        }
        public void LoadDatatoTableWithoutBindAddContinue(DataGridView ObgTbl, DataTable mytb, String FnName,DateTime From,DateTime To)
        {
          
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    Int32 NofColoums = mytb.Columns.Count;
                    String[] ParamCollection = new String[NofColoums];
                    int i = 0;
                     ObgTbl.Rows.Add(1);
                     ObgTbl.Rows[ObgTbl.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(251, 224, 164);
                     ObgTbl.Rows[ObgTbl.Rows.Count - 1].Cells[1].Value = "From  " + From.ToString("dd/MMM/yyyy");
                     ObgTbl.Rows[ObgTbl.Rows.Count - 1].Cells[2].Value = "  To  " + To.ToString("dd/MMM/yyyy");
                    
                    foreach (DataRow Myr in mytb.Rows)
                    {
                        for (int x = 0; x < NofColoums; x++)
                        {
                            string MCX = mytb.Columns[x].DataType.ToString();
                            if (MCX != "System.DateTime")
                                ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                            else
                            {
                                DateTime Xdate;
                                if (string.IsNullOrEmpty(mytb.Rows[i].ItemArray.GetValue(x).ToString()) == false)
                                    Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                else
                                    Xdate = DateTime.MinValue;
                                ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                            }
                        }
                        ObgTbl.Rows.Add(ParamCollection);
                        ObgTbl.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                        ObgTbl.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(211, 254, 242); 
                       
                        i += 1;
                    }
                }
            }
          
        }
        public void LoadDatatoTableWithoutBindAddContinue(DataGridView ObgTbl, DataTable mytb, String FnName, DateTime From, DateTime To,string POType )
        {

            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    Int32 NofColoums = mytb.Columns.Count;
                    String[] ParamCollection = new String[NofColoums];
                    int i = 0;
                    ObgTbl.Rows.Add(1);
                    ObgTbl.Rows[ObgTbl.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(56, 169, 104);
                    ObgTbl.Rows[ObgTbl.Rows.Count - 1].Cells[1].Value = "From  " + From.ToString("dd/MMM/yyyy");
                    ObgTbl.Rows[ObgTbl.Rows.Count - 1].Cells[2].Value = "  To  " + To.ToString("dd/MMM/yyyy");
                    ObgTbl.Rows[ObgTbl.Rows.Count - 1].Cells[4].Value =  POType ;
                    foreach (DataRow Myr in mytb.Rows)
                    {
                        for (int x = 0; x < NofColoums; x++)
                        {
                            string MCX = mytb.Columns[x].DataType.ToString();
                            if (MCX != "System.DateTime")
                                ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                            else
                            {
                                DateTime Xdate;
                                if (string.IsNullOrEmpty(mytb.Rows[i].ItemArray.GetValue(x).ToString()) == false)
                                    Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                else
                                    Xdate = DateTime.MinValue;
                                ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                            }
                        }
                        ObgTbl.Rows.Add(ParamCollection);
                        ObgTbl.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                        ObgTbl.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 248, 138);

                        i += 1;
                    }
                }
            }

        }
        public void LoadDatatoTableWithoutBindwithCheckBox(DataGridView ObgTbl, String CmdStr, String FnName)
        {
            //"System.Int32"
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, FnName);
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    Int32 NofColoums = mytb.Columns.Count;
                    String[] ParamCollection = new String[NofColoums];
                    ObgTbl.Rows.Clear();
                    for (int i = 0; i < mytb.Rows.Count; i++)
                    {
                        for (int x = 0; x < NofColoums; x++)
                        {
                            string MCX = mytb.Columns[x].DataType.ToString();
                            if (MCX != "System.DateTime")
                            {
                                if (MCX != "System.Int32")

                                    ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                                else
                                {
                                    if (string.IsNullOrEmpty(mytb.Rows[i].ItemArray.GetValue(x).ToString()) == true)
                                        ParamCollection[x] = "0";
                                    else
                                        ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();

                                }
                            }
                            else
                            {
                                DateTime Xdate;
                                if (string.IsNullOrEmpty(mytb.Rows[i].ItemArray.GetValue(x).ToString()) == false)
                                    Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                else
                                    Xdate = DateTime.MinValue;
                                ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                            }
                        }
                        ObgTbl.Rows.Add(ParamCollection);
                        //ObgTbl.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                        //if (i % 2 == 1)
                        //{
                        //    ObgTbl.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(251, 254, 181);
                        //}
                        //else
                        //{
                        //    ObgTbl.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(192, 254, 240);
                        //}ObgTbl
                    }
                    DocolourThisGrid(ObgTbl);
                }
            }
            else
                ObgTbl.Rows.Clear();
        }
        public void DocolourThisGrid(DataGridView ObgTbl)
        {
            ObgTbl.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(192, 254, 240);
            ObgTbl.DefaultCellStyle.BackColor  = System.Drawing.Color.FromArgb(251, 254, 181);
        }
     
        public void LoadDatatoTableWithoutBindNotNullDate(DataGridView ObgTbl, String CmdStr, String FnName)
        {

            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, FnName);
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    Int32 NofColoums = mytb.Columns.Count;
                    String[] ParamCollection = new String[NofColoums];
                    ObgTbl.Rows.Clear();
                    for (int i = 0; i < mytb.Rows.Count; i++)
                    {
                        for (int x = 0; x < NofColoums; x++)
                        {
                            string MCX = mytb.Columns[x].DataType.ToString();
                            if (MCX != "System.DateTime")

                                ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                            else
                            {
                                DateTime Xdate;
                                if (string.IsNullOrEmpty(mytb.Rows[i].ItemArray.GetValue(x).ToString()) == false)
                                    Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                else
                                    Xdate = DateTime.MinValue;

                                if (Xdate.ToString("dd/MMM/yyyy") != "01/Jan/0001")
                                    ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                                else
                                    ParamCollection[x] = "";
                            }
                        }
                        try
                        {
                            ObgTbl.Rows.Add(ParamCollection);

                        }
                        catch (Exception ex)
                        {


                        }
                    }
                    DocolourThisGrid(ObgTbl);
                }
            }
            else
            {
                ObgTbl.Rows.Clear();
            }
        }
        public void LoadDatatoTableWithoutBindWithLable(DataGridView ObgTbl, String CmdStr, String FnName, Label _Progress)
        {
            ObgTbl.PerformLayout();
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, FnName);
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    Int32 NofColoums = mytb.Columns.Count;
                    String[] ParamCollection = new String[NofColoums];
                    ObgTbl.Rows.Clear();
                    for (int i = 0; i < mytb.Rows.Count; i++)
                    {
                        Application.DoEvents();
                        _Progress.Text = mytb.Rows[i][0].ToString();
                        for (int x = 0; x < NofColoums; x++)
                        {
                            string MCX = mytb.Columns[x].DataType.ToString();
                            if (MCX != "System.DateTime")

                                ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                            else
                            {
                                DateTime Xdate;
                                if (string.IsNullOrEmpty(mytb.Rows[i].ItemArray.GetValue(x).ToString()) == false)
                                    Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                else
                                    Xdate = DateTime.MinValue;
                                ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                            }
                        }
                        try
                        {
                            ObgTbl.Rows.Add(ParamCollection);

                        }
                        catch (Exception ex)
                        {


                        }
                    }
                    DocolourThisGrid(ObgTbl);
                }
            }
            else
            {
                ObgTbl.Rows.Clear();
            }
        }
        public void LoadDatatoTableWithoutBind(DataGridView ObgTbl, String CmdStr, String FnName)
        {
           // ObgTbl.PerformLayout(); 
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, FnName);
            if (mytb != null)
            {
                if (mytb.Rows.Count > 0)
                {
                    LoadDatatoTableWithoutBind(ObgTbl, mytb, FnName);
                    #region Previouse
                    //DocolourThisGrid(ObgTbl);
                    //Int32 NofColoums = mytb.Columns.Count;
                    //String[] ParamCollection = new String[NofColoums];
                    //ObgTbl.Rows.Clear();
                    //for (int i = 0; i < mytb.Rows.Count; i++)
                    //{
                    //    Application.DoEvents(); 
                    //    for (int x = 0; x < NofColoums; x++)
                    //    {
                    //        string MCX = mytb.Columns[x].DataType.ToString();
                    //        if (MCX != "System.DateTime")

                    //            ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                    //        else
                    //        {
                    //            DateTime dtx = new DateTime(1900, 1, 1);
                    //            bool resp = DateTime.TryParse(mytb.Rows[i].ItemArray.GetValue(x).ToString(), out dtx);
                    //            ParamCollection[x] = dtx.ToString("dd/MMM/yyyy");
                    //        }
                    //    }
                    //    try
                    //    {
                    //        ObgTbl.Rows.Add(ParamCollection);
                           
                    //    }
                    //    catch (Exception ex)
                    //    {


                    //    }
                    //}
                    #endregion
                }
            }
            else
            {
                ObgTbl.Rows.Clear();
            }
        }
       
        public void LoadDatatoTableWithoutBind(DataGridView ObgTbl, DataTable mytb, String FnName)
        {
            try
            {
                if (mytb != null)
                {
                    if (mytb.Rows.Count > 0)
                    {
                        Int32 NofColoums = mytb.Columns.Count;
                        String[] ParamCollection = new String[NofColoums];
                        ObgTbl.Rows.Clear();
                        for (int i = 0; i < mytb.Rows.Count; i++)
                        {
                            for (int x = 0; x < NofColoums; x++)
                            {
                                string MCX = mytb.Columns[x].DataType.ToString();
                                if (MCX != "System.DateTime")
                                    ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                                else
                                {
                                    DateTime dtx = new DateTime(1900, 1, 1);
                                    bool resp = DateTime.TryParse(mytb.Rows[i].ItemArray.GetValue(x).ToString(), out dtx);
                                    ParamCollection[x] = dtx.ToString("dd/MMM/yyyy");
                                    // DateTime Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                    // ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                                }
                            }
                            ObgTbl.Rows.Add(ParamCollection);

                        }
                        DocolourThisGrid(ObgTbl);
                    }
                }
                else
                    ObgTbl.Rows.Clear();
            }
            catch (Exception ex)
            {
          
            }
        }
        public void LoadDatatoTableWithoutBindForTrialBalance(DataGridView ObgTbl, DataTable mytb, String FnName)
        {
            try
            {
                if (mytb != null)
                {
                    if (mytb.Rows.Count > 0)
                    {
                        Int32 NofColoums = mytb.Columns.Count;
                        String[] ParamCollection = new String[NofColoums];
                        ObgTbl.Rows.Clear();
                        for (int i = 0; i < mytb.Rows.Count; i++)
                        {
                            for (int x = 0; x < NofColoums; x++)
                            {
                                string MCX = mytb.Columns[x].DataType.ToString();
                                if (MCX != "System.DateTime")
                                    ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                                else
                                {
                                    DateTime dtx = new DateTime(1900, 1, 1);
                                    bool resp = DateTime.TryParse(mytb.Rows[i].ItemArray.GetValue(x).ToString(), out dtx);
                                    ParamCollection[x] = dtx.ToString("dd/MMM/yyyy");
                                    // DateTime Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                    // ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                                }
                            }
                            ObgTbl.Rows.Add(ParamCollection);

                        }
                        DocolourThisGrid(ObgTbl);

                    }
                }
                else
                    ObgTbl.Rows.Clear();
            }
            catch (Exception ex)
            {

            }
        }
        private DataTable GetIsITMainAccountList()
        {
            DataTable dt=new DataTable ();
            return dt;
        }
        private bool ExitInDataTable(DataGridView tb, string KeyVal1, int Position)
        {
            foreach (DataGridViewRow  r in tb.Rows )
            {
                if (r.Cells[ Position].Value .ToString () == KeyVal1)
                {
                    return true;
                }
            }
            return false;
        }
        private bool NotinTable(string ECOno, string DepID)
        {
            string sql1 = "Select sysid from ecodecision where  Department='" + DepID + "' and ECONumber='" + ECOno + "'";
            bool resp = ExistIntable(sql1);
            return resp;
        }
        private bool AllECODecisionComplete(string ECOno)
        {
            string sql1 = "Select count(sysid) as 'NofC' from ecodecision where ECONumber='" + ECOno + "' and Decission=1";
            DataRow r = GetDataRow(sql1, "Load Data");
            if (r != null)
            {
                if (int.Parse (r["NofC"].ToString ())==6)
                    return false  ;
                else 
                    return  true ;
            }
            else 
                return false ;
        }
        private bool AllECODecisionComplete(string ECOno,string Dep,bool MyDecision)
        {
          
            string sql1 = "Select Decission from ecodecision where ECONumber='" + ECOno + "' and Department='" + Dep + "'";

            DataRow r = GetDataRow(sql1, "Load Data");
            if (r != null)
            {
                if (r["Decission"].ToString() == "0")
                    return false;
                else
                    return true;
            }
            else
                return true;
        }
        public void LoadDatatoTableWithoutBind(DataGridView ObgTbl, DataTable mytb, String FnName,int Position1,string Department,bool IsCompleted)
        {
            int mmf = 0;
            try
            {
                if (mytb != null)
                {
                    if (mytb.Rows.Count > 0)
                    {
                        Int32 NofColoums = mytb.Columns.Count;
                        String[] ParamCollection = new String[NofColoums];
                        ObgTbl.Rows.Clear();
                        for (int i = 0; i < mytb.Rows.Count; i++)
                        {
                            for (int x = 0; x < NofColoums; x++)
                            {
                                string MCX = mytb.Columns[x].DataType.ToString();
                                if (MCX != "System.DateTime")
                                    ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                                else
                                {
                                    DateTime Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                    ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                                }
                            }
                            bool resp;
                            if (IsCompleted == false)
                                resp = NotinTable(mytb.Rows[i][Position1].ToString(), Department);// ExitInDataTable(ObgTbl, mytb.Rows[i][Position1].ToString(), Position1);
                            else
                                resp = AllECODecisionComplete(mytb.Rows[i][Position1].ToString());
                            if (resp == false)
                            {
                                mmf = mmf + 1;
                                ParamCollection[0] = mmf.ToString();
                                ObgTbl.Rows.Add(ParamCollection);
                            }

                        }
                        DocolourThisGrid(ObgTbl);
                    }
                }
                else
                    ObgTbl.Rows.Clear();
            }
            catch (Exception ex)
            {
            }
        }
        public void LoadDatatoTableWithoutBind(DataGridView ObgTbl, DataTable mytb, String FnName, int Position1, string Department, bool IsCompleted,bool NotInMyDecision)
        {
            int mmf = 0;
            try
            {
                if (mytb != null)
                {
                    if (mytb.Rows.Count > 0)
                    {
                        Int32 NofColoums = mytb.Columns.Count;
                        String[] ParamCollection = new String[NofColoums];
                        ObgTbl.Rows.Clear();
                        for (int i = 0; i < mytb.Rows.Count; i++)
                        {
                            for (int x = 0; x < NofColoums; x++)
                            {
                                string MCX = mytb.Columns[x].DataType.ToString();
                                if (MCX != "System.DateTime")
                                    ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                                else
                                {
                                    DateTime Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                    ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                                }
                            }
                            bool resp;
                            if (IsCompleted == false)
                                resp = NotinTable(mytb.Rows[i][Position1].ToString(), Department);// ExitInDataTable(ObgTbl, mytb.Rows[i][Position1].ToString(), Position1);
                            else
                                resp = AllECODecisionComplete(mytb.Rows[i][Position1].ToString(), Department, false);
                            if (resp == false)
                            {
                                mmf = mmf + 1;
                                ParamCollection[0] = mmf.ToString();
                                ObgTbl.Rows.Add(ParamCollection);
                            }

                        }
                        DocolourThisGrid(ObgTbl);
                    }
                }
                else
                    ObgTbl.Rows.Clear();
            }
            catch (Exception ex)
            {
                
             
            }
        }
        public void LoadDatatoTableWithoutBind(DataGridView ObgTbl, String CmdStr, String FnName, bool Selection,bool DonotClear)
        {
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, FnName);
            try
            {
                if (mytb != null)
                {
                    if (mytb.Rows.Count > 0)
                    {
                        Int32 NofColoums = mytb.Columns.Count;
                        String[] ParamCollection;
                        if (Selection == true)
                            ParamCollection = new String[NofColoums + 1];
                        else
                            ParamCollection = new String[NofColoums];
                        if (DonotClear == false)
                            ObgTbl.Rows.Clear();
                        for (int i = 0; i < mytb.Rows.Count; i++)
                        {
                            if (Selection == false)
                            {


                                for (int x = 0; x < NofColoums; x++)
                                {
                                    string MCX = mytb.Columns[x].DataType.ToString();
                                    if (MCX != "System.DateTime")
                                        ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                                    else
                                    {
                                        DateTime Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                        ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                                    }
                                }
                            }
                            else
                            {
                                ParamCollection[0] = "0";
                                for (int x = 0; x < NofColoums; x++)
                                {
                                    string MCX = mytb.Columns[x].DataType.ToString();
                                    if (MCX != "System.DateTime")
                                        ParamCollection[x + 1] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                                    else
                                    {
                                        DateTime Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                        ParamCollection[x + 1] = Xdate.ToString("dd/MMM/yyyy");
                                    }
                                }
                            }
                            ObgTbl.Rows.Add(ParamCollection);
                           
                        }
                        DocolourThisGrid(ObgTbl);
                    }
                }
            }
            catch (Exception ex)
            {
                
              
            }
            
        }
        public void LoadDatatoTableWithoutBind(DataGridView ObgTbl, String CmdStr, String FnName, bool Selection)
        {
            DataTable mytb = new DataTable();
            mytb = GetDataTable(CmdStr, FnName);
            try
            {
                if (mytb != null)
                {
                    if (mytb.Rows.Count > 0)
                    {
                        Int32 NofColoums = mytb.Columns.Count;
                        String[] ParamCollection;
                        if (Selection == true)
                            ParamCollection = new String[NofColoums + 1];
                        else
                            ParamCollection = new String[NofColoums];
                        ObgTbl.Rows.Clear();
                        for (int i = 0; i < mytb.Rows.Count; i++)
                        {
                            if (Selection == false)
                            {


                                for (int x = 0; x < NofColoums; x++)
                                {
                                    string MCX = mytb.Columns[x].DataType.ToString();
                                    if (MCX != "System.DateTime")
                                        ParamCollection[x] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                                    else
                                    {
                                        DateTime Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                        ParamCollection[x] = Xdate.ToString("dd/MMM/yyyy");
                                    }
                                }
                            }
                            else
                            {
                                ParamCollection[0] = "0";
                                for (int x = 0; x < NofColoums; x++)
                                {
                                    string MCX = mytb.Columns[x].DataType.ToString();
                                    if (MCX != "System.DateTime")
                                        ParamCollection[x + 1] = mytb.Rows[i].ItemArray.GetValue(x).ToString();
                                    else
                                    {
                                        DateTime Xdate = DateTime.Parse(mytb.Rows[i].ItemArray.GetValue(x).ToString());
                                        ParamCollection[x + 1] = Xdate.ToString("dd/MMM/yyyy");
                                    }
                                }
                            }
                            ObgTbl.Rows.Add(ParamCollection);
                            //ObgTbl.Rows[i].DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                            //if (i % 2 == 1)
                            //{
                            //    ObgTbl.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(251, 254, 181);
                            //}
                            //else
                            //{
                            //    ObgTbl.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(192, 254, 240);
                            //}
                        }
                        DocolourThisGrid(ObgTbl);
                    }
                }
                else
                    ObgTbl.Rows.Clear();
            }
            catch (Exception ex)
            {
              
            }
        }

        public Boolean DBConnectionIsOk(bool Islocal)
        {
            try
            {
                DataService Myds = new DataService(Islocal);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public void WriteToLogTable(int ObgName1, String OPtype, String UserN, String LogStr)
        {
            Int32 SysID1 = GetSysID("tblEntryLog");
            String Respond;
            string ObgName = GetRunningOblectName(ObgName1);
            String Str1 = "INSERT INTO tblEntryLog "
           + "(SysID,ObgName,OperationType,LogStr,UserName,OcDate)"
           + " Values(" + SysID1 + ",'" + ObgName + "','" + OPtype + "','" + LogStr + "','" + UserN + "',Curdate())";
            Respond = ExicuteErrorCommand (Str1, "Write To Log");
        }
        private string GetRunningOblectName(int ObgID)
        {
            string str1 = "Select ObgName from tblobgtable where sysID=" + ObgID;
            DataRow myRow = GetDataRow(str1, "Generate System ID");
            if (myRow != null)
            {
                if (string.IsNullOrEmpty(myRow["ObgName"].ToString()) == false)
                    return myRow["ObgName"].ToString();
                else
                    return "Nill";
            }
            else
                return "Nill";
        }
        public string GetDBDate(out string Time)
        {
            string sql1 = "Select curDate() as d1,curTime() as t1";
            DataRow r = GetDataRow(sql1, "Get DAte Time");
            Time = "";
            string Date1;
            if (r != null)
            {
               
                Date1 = r["d1"].ToString();
                Time = r["t1"].ToString();
            }
            else
            {
                Date1 ="";
                Time = "";
            }
            return Date1;
        }

        public ThreeSConfiguration  GetCompanyConfig()
        {
            DataRow MyDr;
            ThreeSConfiguration ED = new ThreeSConfiguration();
            MyDr = GetDataRowAccount("Select * From tblcompanyconfig", "Get Company Config");
            ED.CurrentAccPeriod = int.Parse(MyDr["CurrentAccPeriod"].ToString());
            ED.CompanyID = int.Parse(MyDr["Sysid"].ToString());
            return ED;


        }
        public string GetMACAddress()
            {
                string ProcessorID = "Nill";
                ManagementObjectCollection objMOC;

                ManagementObjectSearcher objMOS = new ManagementObjectSearcher("Select * From Win32_Processor");
                objMOC = objMOS.Get();
                foreach (ManagementObject objMO in objMOC)
                {
                    ProcessorID = objMO["ProcessorID"].ToString();
                }
                return ProcessorID;
            }
        public string GetHomeCurrencyAndExrate(out string Curreny,out string Exrate)
        {
            Curreny = "";
            Exrate = "";

            string sql1 = "SELECT BaseCurrency, BaseExRate FROM tblcompanyconfig";
            DataRow dr = GetDataRowAccount(sql1, "Get Ex rate");
            if (dr != null)
                {
                Curreny = dr["BaseCurrency"].ToString();
                Exrate = dr["BaseExRate"].ToString();
                }
            else
            {
            Curreny = "";
            Exrate = "";
            }
            return "True";
        }
        public void KillUnuseConnections()
        {
            string sql1="SELECT * FROM INFORMATION_SCHEMA.PROCESSLIST WHERE DB = 'accounterp' and TIME>120";
            DataTable tb=GetDataTableforTransaction(sql1 ,"Get Process list");
            if (tb != null)
                {
                foreach (DataRow  r in tb.Rows)
                    {
                    string msc = "kill " + r["ID"].ToString();

                    }
                }
        }
        public string GetIPAddress()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
        public string GetCurrentMachineID()
        {
            string ProcessorID = "Nill";
            ManagementObjectCollection objMOC;

            ManagementObjectSearcher objMOS = new ManagementObjectSearcher("Select * From Win32_Processor");
            objMOC = objMOS.Get();
            foreach (ManagementObject objMO in objMOC)
            {
                ProcessorID = objMO["ProcessorID"].ToString();
            }
            return ProcessorID;
        }
        public string GetMainFieldList(string Mytbl1, string MyCategory, Int16 MainCatID)
        {

            string SqltblHeader;
            string Sql1;
            DataTable MyTb = new DataTable();
            SqltblHeader = "select ";
            Sql1 = "Select tblHederName,tblColunmname from tblexpendituredef Where Categoty='" + MyCategory + "'";
            MyTb = GetDataTable(Sql1, "GetMainFieldList");
            if (MyTb != null)
            {
                if (MyTb.Rows.Count > 0)
                {
                    foreach (DataRow MyRow in MyTb.Rows)
                    {
                        if (MyRow["tblColunmname"].ToString().Trim() == "SysID")
                        { SqltblHeader = SqltblHeader + Mytbl1 + "." + MyRow["tblColunmname"].ToString().Trim() + " As '" + MyRow["tblHederName"].ToString().Trim() + "',"; }
                        else
                        { SqltblHeader = SqltblHeader + MyRow["tblColunmname"].ToString().Trim() + " As '" + MyRow["tblHederName"].ToString().Trim() + "',"; }
                    }
                    SqltblHeader = SqltblHeader.Substring(0, SqltblHeader.Length - 1);
                    SqltblHeader = SqltblHeader.Replace("CommonDef", "tblassetsitemtable.AssetItem");
                    SqltblHeader = SqltblHeader + " from " + Mytbl1 + ",tblassetsitemtable ";
                    SqltblHeader = SqltblHeader + "Where " + Mytbl1 + ".MainCatID = " + MainCatID + " and tblassetsitemtable.sysid=" + Mytbl1 + ".AssetItemNumber";
                    return SqltblHeader;

                }
                else
                    return "Nill";
            }
            else
            {
                return "Nill";
            }


        }
        #endregion
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetLocalTime(ref SYSTEMTIME st);
        #region WEB Operation
     
    
      
    
     #endregion

        #region NumberDisplay
        private string LessThanTen(decimal Amount)
        {
            string LineWord = "";
            decimal Fractional = 0;
            decimal FullP = 0;
            string[] splt = Amount.ToString().Split('.');
            if (splt.Length == 1)
                LineWord = GetWord(Amount);
            else
            {

                Fractional = decimal.Parse(splt[1]);
                if (Fractional > 0)
                {
                    if (Fractional > 20 && Fractional < 99)
                    {
                        FullP = decimal.Parse(splt[0]);
                        LineWord = GetWord(FullP);
                        LineWord = LineWord + " and " + LessThanHundered(Fractional) + " Cents";
                    }
                    else
                    {
                        FullP = decimal.Parse(splt[0]);
                        LineWord = GetWord(FullP);
                        LineWord = LineWord + " and " + LessThanTwenty(Fractional) + " Cents";
                    }
                }
                else
                    LineWord = GetWord(Amount);

            }
            return LineWord;
        }
        private string LessThanHundered(decimal Amount)
        {
            decimal Remain = 0;
            string FullWord = "";
            string LineWord;
            decimal currentN;
            string HeadeWord = "";

            Remain = Amount % 10;
            currentN = (Amount - Remain);
            LineWord = GetWord(currentN);
            Amount = Remain;
            FullWord = LineWord + " " + GetWord(Remain);
            return FullWord;
        }
        private string LessThanTwenty(decimal Amount)
        {
            string LineWord = "";
            decimal Fractional = 0;
            decimal FullP = 0;
            string[] splt = Amount.ToString().Split('.');
            if (splt.Length == 1)
                LineWord = GetWord(Amount);
            else
            {
                Fractional = decimal.Parse(splt[1]);
                FullP = decimal.Parse(splt[0]);
                LineWord = GetWord(FullP);
                LineWord = LineWord + " and " + LessThanHundered(Fractional) + " Cents";
            }
            return LineWord;
        }
        private string GetBetweenWord(decimal Amount)
        {
            decimal Remain = 0;
            string FullWord = "";
            string LineWord;
            decimal currentN;
            string HeadeWord = "";
            if (Amount < 100)
            {
                Remain = Amount % 10;
                currentN = (Amount - Remain);
                LineWord = GetWord(currentN);
                Amount = Remain;
                FullWord = LineWord + " " + GetWord(Remain);
                //if (Remain > 1)
                //{
                // FullWord = FullWord;//+ " " + LessThanTen(Remain); 
                //}
                return FullWord;
            }
            else
            {
                Remain = Amount % 100;
                currentN = (Amount - Remain) / 100;
                LineWord = GetWord(currentN);
                HeadeWord = GetWord(100);
                Amount = Remain;
                FullWord = LineWord + " " + HeadeWord + " and ";
                if (Remain > 20)
                {
                    Remain = Amount % 10;
                    currentN = (Amount - Remain);
                    LineWord = GetWord(currentN);
                    Amount = Remain;
                    FullWord = FullWord + LineWord + " " + GetWord(Remain);
                    return FullWord;
                }

                return FullWord = FullWord + LineWord + " " + HeadeWord + " and " + FullWord;
            }




        }
        public string NumberConvertToText(decimal Amount)
        {
            decimal Remain = 0;
            string FullWord = "";
            string LineWord;
            decimal currentN;
            string HeadeWord = "";
            if (Amount > 1000000000000)
            {
                Remain = Amount % 1000000000000;
                currentN = (Amount - Remain) / 1000000000000;
                LineWord = GetWord(currentN);
                HeadeWord = GetWord(1000000000000);
                Amount = Remain;
                FullWord = LineWord + " " + HeadeWord + " ";
            }
            else if (Amount == 1000000000000)
            {
                currentN = Amount / 1000000000000;
                LineWord = GetWord(currentN);
                HeadeWord = GetWord(1000000000000);
                Amount = 0;
                FullWord = LineWord + " " + HeadeWord + " ";
            }

            if (Amount > 1000000000)
            {
                Remain = Amount % 1000000000;
                currentN = (Amount - Remain) / 1000000000;
                LineWord = GetWord(currentN);
                HeadeWord = GetWord(1000000000);
                Amount = Remain;
                FullWord = FullWord + LineWord + " " + HeadeWord + " ";
            }
            else if (Amount == 1000000000)
            {
                currentN = Amount / 1000000000;
                LineWord = GetWord(currentN);
                HeadeWord = GetWord(1000000000);
                Amount = 0;
                FullWord = FullWord + LineWord + " " + HeadeWord + " ";
            }
            if (Amount > 1000000)
            {
                Remain = Amount % 1000000;
                currentN = (Amount - Remain) / 1000000;
                if (currentN > 20)
                    LineWord = GetBetweenWord(currentN);
                else
                    LineWord = GetWord(currentN);

                HeadeWord = GetWord(1000000);
                Amount = Remain;
                FullWord = FullWord + LineWord + " " + HeadeWord + " ";
            }
            else if (Amount == 1000000)
            {
                currentN = Amount / 1000000;
                if (currentN > 20)
                    LineWord = GetBetweenWord(currentN);
                else
                    LineWord = GetWord(currentN);
                HeadeWord = GetWord(1000000);
                Amount = 0;
                FullWord = FullWord + LineWord + " " + HeadeWord + " ";
            }

            if (Amount > 1000)
            {
                Remain = Amount % 1000;
                currentN = (Amount - Remain) / 1000;
                if (currentN > 20)
                    LineWord = GetBetweenWord(currentN);
                else
                    LineWord = GetWord(currentN);
                HeadeWord = GetWord(1000);
                Amount = Remain;
                FullWord = FullWord + LineWord + " " + HeadeWord + " ";
            }
            else if (Amount == 1000)
            {
                currentN = Amount / 1000;
                LineWord = GetWord(currentN);
                HeadeWord = GetWord(1000);
                Amount = 0;
                FullWord = FullWord + LineWord + " " + HeadeWord + " ";
            }

            if (Amount > 100)
            {
                Remain = Amount % 100;
                currentN = (Amount - Remain) / 100;
                LineWord = GetWord(currentN);
                HeadeWord = GetWord(100);
                Amount = Remain;
                if (Amount == 0)
                    FullWord = FullWord + LineWord + " " + HeadeWord;
                else
                    FullWord = FullWord + LineWord + " " + HeadeWord + " and ";
            }
            else if (Amount == 100)
            {
                currentN = Amount / 100;
                LineWord = GetWord(currentN);
                HeadeWord = GetWord(100);
                Amount = 0;
                FullWord = FullWord + LineWord + " " + HeadeWord;
            }


            if (Amount > 20)
            {
                Remain = Amount % 10;
                currentN = (Amount - Remain) / 10;
                LineWord = GetWord(currentN * 10);

                Amount = Remain;

                FullWord = FullWord + " " + LineWord;

            }
            else if (Amount == 20)
            {
                currentN = 20;
                LineWord = GetWord(currentN);
                Amount = 0;
                FullWord = FullWord + LineWord;
            }
            else if (Amount < 20 && Amount > 10)
                FullWord = FullWord + LessThanTwenty(Amount);
            else if (Amount < 10)
                FullWord = FullWord + LessThanTen(Amount);

            if (Amount < 10)
            {
                FullWord = FullWord + LessThanTen(Amount);
            }
            return FullWord;

        }
        private string GetWord(decimal Number)
        {
            string sql1 = "Select NumberName from NumberTable where NumberID=" + Number;
            DataRow r = GetDataRow(sql1,"Get Number");
            if (r != null)
                return r["NumberName"].ToString();
            else
                return "";
        }
        #endregion
    }

    public class ComboboxItem
    {
        string _Text;

        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        object _Value;

        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }


        public string GetReleventTextFromID(ComboBox cmb, string ID,bool CheckWithID)
        {
            foreach (ComboboxItem Item in cmb.Items )
            {
                if (!CheckWithID)
                {
                    if (Item.Text.ToString().Trim().ToUpper() == ID.Trim().ToUpper())
                        return Item.Value.ToString();
                }
                else
                {
                    if (Item.Value.ToString().Trim().ToUpper() == ID.Trim().ToUpper())
                        return Item.Text;
                }
            }
            return "-1";
        }
       
        public override string ToString()
        {
            return Text;
        }
    }
   
}
