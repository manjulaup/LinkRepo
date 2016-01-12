using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.CommonOperation;
using BusinessLayer.UserHandling;
using Validation.ValidationLayer;
using System.Reflection;
using System.IO;
namespace AccountERP
    {
    public partial class frmLogin : Form
        {
        public frmLogin()
            {
            InitializeComponent();
            }
       private UserHandling MyUser = new UserHandling();
        private bool Authenticated = false;
      private CommonOperations MyGeneral = null; 
        private void frmLogin_Load(object sender, EventArgs e)
            {
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2 - 25;
            picture.Image = imageList1.Images[0];
            this.Text = "User Authentication -" + AssemblyProduct;
          //  Program.LoggingAsLocal = false;
            }

        private void btnOk_Click(object sender, EventArgs e)
            {

              
                //if (Authenticated)
                //{

                  //  fm.menuStrip1.Enabled = true;
                  
                    Program.AccountStatic.UserName = txtUserName.Text;
                    Program.AccountStatic.CompanyID = 1000;
                    Program.AccountStatic.IsAuthenticated = true;
                    SetCompanyProfile();
                    this.Close();
                //}
                //else
                //{
               
               
                //    MessageBox.Show("Invalied Username or Password","E r r o r", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //    Program.AccountStatic.UserName = txtUserName.Text;
                //    Program.AccountStatic.IsAuthenticated = false;
                   
                //}
                
            }
        private void SetCompanyProfile()
        {
            CommonOperations.ThreeSConfiguration _Exdata = new CommonOperations.ThreeSConfiguration();
            CommonOperations myco = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
            _Exdata = myco.GetCompanyConfig();
            Program.AccountStatic.CurrentAccPeriod = _Exdata.CurrentAccPeriod;
            Program.AccountStatic.CompanyID = _Exdata.CompanyID;
        }
        private void btnCancel_Click(object sender, EventArgs e)
            {
                Application.Exit(); 
            }

        private void txtPWD_TextChanged(object sender, EventArgs e)
        {
           string pwd = MyUser.GetPassword();
           if (pwd == txtPWD.Text)
            {
                Authenticated = true;
                picture.Image = imageList1.Images[1];
                Program.AccountStatic.UserName = txtUserName.Text;
                Program.AccountStatic.IsAuthenticated = true;
            }
            else
            { 
                Authenticated = false;
                picture.Image = imageList1.Images[0];
                Program.AccountStatic.IsAuthenticated = false;
            }
        }
        private void WrireConf(string Server)
        {
        Validations MyValidation = new Validations();   
        string enkTwest = MyValidation.Getpassword(Server);
        string AppPath = Application.StartupPath;
        if (File.Exists(AppPath + @"\accconf.ncf"))
            File.Delete(AppPath + @"\accconf.ncf");
        using (StreamWriter writer =
        new StreamWriter(AppPath + @"\accconf.ncf", true))
            {

            writer.WriteLine(enkTwest);

            }
        }
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            MyUser.UserName = txtUserName.Text;
        }
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        private void txtPWD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MDIMain fm = new MDIMain();
                if (Authenticated)
                {
                    Program.AccountStatic.UserName = txtUserName.Text;
                    Program.AccountStatic.CompanyID = 1000;
                    Program.AccountStatic.IsAuthenticated = true;
                    SetCompanyProfile();
                    this.Close();
                }
                else
                {
                   // fm.menuStrip1.Enabled = false;
                }
            }
        }

        private void chkLocalLogging_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void cmbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem cmb=new ComboboxItem ();
            Program.AccountStatic.CompanyID = int.Parse(cmb.GetReleventTextFromID(cmbCompanyName, cmbCompanyName.Text, false));
            txtUserName.Enabled = true;
            txtPWD.Enabled = true; 
        }

        private void rbt3Sfab_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
            WrireConf("network");
            Program.AccountStatic.LoggingAsLocal = false;
            MyUser = new UserHandling(Program.AccountStatic.LoggingAsLocal);
            MyGeneral = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
                MyGeneral.LoadCompany(cmbCompanyName);
            }
            catch (Exception ex)
            {
                
             
            }
        }

        private void rbtLocal_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
            WrireConf("local");

            Program.AccountStatic.LoggingAsLocal = true;
            MyUser = new UserHandling(Program.AccountStatic.LoggingAsLocal);
            MyGeneral = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
            MyGeneral.LoadCompany(cmbCompanyName);
            }
            catch (Exception ex)
            {
                
               
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
            {
            this.Dispose();
            }

        }
    }
