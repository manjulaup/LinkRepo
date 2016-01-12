using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.CommonOperation;
using BusinessLayer.AccountCreations;
namespace AccountERP
{
    public partial class frmCompanyConfig : Form
    {
      private   CommonOperations MyCom = null;
        private AccountCreation Myaccount=null ;
            public frmCompanyConfig()
            {
                InitializeComponent();
            }

        private void btnCreate_Click(object sender, EventArgs e)
            {
            if (MessageBox.Show("Do you want to Create ?", "Create New Ledgers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
            progressBar1.Value = 0; progressBar1.Maximum = dgvList.Rows.Count;
            foreach (DataGridViewRow r in dgvList.Rows )
                {
                    if (r.Cells["dgv_Select"].Value == "1")
                    {
                        AccountType.AccountCreationDataType _Savedata = new AccountType.AccountCreationDataType();
                        string respond = "";
                        respond = SetDataToClass(r, out _Savedata);
                        if (respond == "True")
                        {
                            if (!Myaccount.ExistAccountCreation(_Savedata.AccountID))
                                respond = Myaccount.SaveAccountCreation(_Savedata);
                            if (respond != "True")
                                Program.VerningMessage(respond);

                        }
                    }
                    progressBar1.Value = r.Index +1;
                }
            Program.InformationMessage("Account Saved Successfully");
                progressBar1.Value = 0;
            }
            }

        private void frmCompanyConfig_Load(object sender, EventArgs e)
            {
                MyCom = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
                Myaccount=new AccountCreation (Program.AccountStatic.LoggingAsLocal);
                panel1.Top = (this.Height - panel1.Height) / 2;
                panel1.Left = (this.Width - panel1.Width) / 2;
                Myaccount.LoadSupplier (dgvList);
            DataTable tb=MyCom .GetDataTableAccount ("Select CurID from tblcurrency","xx");
            MyCom.LoadDatatoComboWithOutBind(txtCurrency, tb, "CurID", true);  
            }
        private string SetDataToClass( DataGridViewRow r, out AccountType.AccountCreationDataType _Savedata)
        {
            _Savedata = new AccountType.AccountCreationDataType();
            try
                {
                if (btnSupplier.Checked)
                    {
                        if (txtCurrency.Text == "USD")
                        {
                            _Savedata.AccountID = "20005-" + r.Cells["dgvList_ID"].Value.ToString();
                            _Savedata.MainAccountID = "20005";
                        }
                        else
                        { _Savedata.AccountID = "20000-" + r.Cells["dgvList_ID"].Value.ToString();
                        _Savedata.MainAccountID = "20000";
                        }

                    _Savedata.AccountName = r.Cells["dgvList_Name"].Value.ToString();
                    _Savedata.AccountStatus = 1;
                    _Savedata.BankAccountNo = "";
                    _Savedata.CompanyID = Program.AccountStatic.CompanyID;
                    _Savedata.CreatedUser = Program.AccountStatic.UserName;
                    _Savedata.Currency = txtCurrency.Text ;
                    _Savedata.CurrencySymble = txtCurrency.Text ;
                    _Savedata.IsSubAccount = 1;
                   
                    _Savedata.MainAccType = 6;

                    }
                else if (btnCustomer.Checked)
                    {
                    _Savedata.AccountID = "10105-" + r.Cells["dgvList_ID"].Value.ToString();
                    _Savedata.AccountName = r.Cells["dgvList_Name"].Value.ToString();
                    _Savedata.AccountStatus = 1;
                    _Savedata.BankAccountNo = "";
                    _Savedata.CompanyID = Program.AccountStatic.CompanyID;
                    _Savedata.CreatedUser = Program.AccountStatic.UserName;
                    _Savedata.Currency = txtCurrency.Text ;
                    _Savedata.CurrencySymble = txtCurrency.Text ;
                    _Savedata.IsSubAccount = 1;
                    _Savedata.MainAccountID = "10105";
                    _Savedata.MainAccType = 2;
                    }
                return "True";
                }
            catch (Exception ex)
                {

                return ex.Message;
                }
        }
        private void btnExit_Click(object sender, EventArgs e)
            {
            this.Dispose();
            }

        private void btnCustomer_CheckedChanged(object sender, EventArgs e)
            {
            Myaccount.LoadCustomer(dgvList); 
            }

        private void btnSupplier_CheckedChanged(object sender, EventArgs e)
            {
            Myaccount.LoadSupplier(dgvList); 
            }
    }
}
