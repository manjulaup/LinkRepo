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
    public partial class frmAccountCreation : Form
    {
        public frmAccountCreation()
        {
            InitializeComponent();
        }
        private AccountCreation MyAccount = null;
        private CommonOperations MyCommon = null;
        
        private void frmAccountCreation_Paint(object sender, PaintEventArgs e)
            {
            panel1.Left = (this.Width - panel1.Width) / 2;
            panel1.Top = (this.Height - panel1.Height) / 2; 
            }

        private void frmAccountCreation_Load(object sender, EventArgs e)
            {
            MyCommon = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
            MyAccount = new AccountCreation(Program.AccountStatic.LoggingAsLocal);
            panel1.Top = (this.Height - panel1.Height) / 2;
            panel1.Left = (this.Width - panel1.Width) / 2;
            InitializeScreen();
            panel7.Enabled = false;
            panel8.Enabled = false;
            LoadExistingAccount();
            LoadExtAccountInHiaraky();
           
            }
        private void LoadExistingAccount()
        {
        DataTable tb = MyAccount.GetAccountList(Program.AccountStatic.CompanyID, 1);
           
            MyCommon.LoadDatatoTableWithoutBind(dgvAccountList, tb, "Load Account List");
        }
        private void LoadExistingAccount(int CatID)
        {
        DataTable tb = MyAccount.GetAccountList(Program.AccountStatic.CompanyID, 1);

            MyCommon.LoadDatatoTableWithoutBind(dgvAccountList, tb, "Load Account List");
        }
        private void LoadExtAccountInHiaraky()
        {
            DataTable tb = MyAccount.GetAccountList(Program.AccountStatic.CompanyID, 1, 0);
            DataTable subtb = MyAccount.GetSubAccountList(Program.AccountStatic.CompanyID, 1);
            DataSet dsDataset = new DataSet();
            try
            {
                dsDataset.Tables.Add(tb);
                dsDataset.Tables.Add(subtb);
                DataRelation Datatablerelation = new DataRelation("Sub Account", dsDataset.Tables[0].Columns[0], dsDataset.Tables[1].Columns[1], false);
                dsDataset.Relations.Add(Datatablerelation);
                HanchyGrid.DataSource = dsDataset.Tables[0];
            }
            catch (Exception ex)
            {
                
               
            }
          
        }
        private void LoadExtAccountInHierarchical()
        {
            DataTable tb = MyAccount.GetAccountList(Program.AccountStatic.CompanyID, 1, 0);
            DataTable subtb = MyAccount.GetSubAccountList(Program.AccountStatic.CompanyID, 1);
            DataSet dsDataset = new DataSet();
            try
            {
                dsDataset.Tables.Add(tb);
                dsDataset.Tables.Add(subtb);
                DataRelation Datatablerelation = new DataRelation("Sub Account", dsDataset.Tables[0].Columns[0], dsDataset.Tables[1].Columns[0], true);
                dsDataset.Relations.Add(Datatablerelation);
              

               // dgvHirachiyGrid.DataSource = dsDataset.Tables[0];
            }
            catch (Exception ex)
            {


            }

        }
        private void InitializeScreen()
        {
            ComboboxItem cmb = new ComboboxItem();
            MyCommon.ClearCurrentPanel(ref panel7);
            MyAccount.LoadAccountType (cmbAccountType);
            MyAccount.LoadAccountType(cmbSearchAccType);
            MyCommon.LoadStatusComboAccount(cmbAccStatus, 1);
            MyAccount.LoadCurrency(cmbCurrenyType);
            cmbCurrenyType.Text = cmb.GetReleventTextFromID(cmbCurrenyType,"LKR", true);
            cmbAccStatus.Text = cmb.GetReleventTextFromID(cmbAccStatus, "1", true);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string SetDataToCLass(out AccountType.AccountCreationDataType _Savedata)
        {
            ComboboxItem cmb=new ComboboxItem();
            _Savedata = new AccountType.AccountCreationDataType();
            _Savedata.AccountID = txtAccID.Text;
            _Savedata.AccountName = txtAccountDescription.Text;
            _Savedata.AccountStatus = int.Parse(cmb .GetReleventTextFromID(cmbAccStatus,cmbAccStatus.Text ,false));
            _Savedata.BankAccountNo = cmbBankAccountNo.Text;
            _Savedata.CreatedUser = Program.AccountStatic.UserName;
            _Savedata.Currency = cmbCurrenyType.Text;
            if (chkIsSubaccount.CheckState == CheckState.Checked)
            {
                _Savedata.IsSubAccount = 1;
                _Savedata.MainAccountID = cmb.GetReleventTextFromID(cmbMainAccount, cmbMainAccount.Text, false  ); 
            }
            else
            {
                _Savedata.IsSubAccount = 0;
                _Savedata.MainAccountID = "0";
            }
            _Savedata.lastaccdate = DateTime.Today;
            _Savedata.MainAccType = int.Parse(cmb.GetReleventTextFromID(cmbAccountType, cmbAccountType.Text, false));
            _Savedata.CompanyID = Program.AccountStatic.CompanyID;
            return "True";
        }

        private void btnCLear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear Cleacreen ?","Confirmation",  MessageBoxButtons.YesNo, MessageBoxIcon.Question)== System.Windows.Forms.DialogResult.Yes)
                MyCommon.ClearCurrentPanel(ref panel7);
        }

        private void chkIsSubaccount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsSubaccount.CheckState == CheckState.Checked)
            { 
                cmbMainAccount.Enabled = true;
                MyAccount.LoadMainAccount(cmbMainAccount, Program.AccountStatic.CompanyID);  
            }
            else
                cmbMainAccount.Enabled = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            panel7.Enabled = true;
            InitializeScreen();
            panel8.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             
            if (MessageBox.Show("Do you want to Save Creent Record ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                AccountType.AccountCreationDataType _Savedata = new AccountType.AccountCreationDataType();
                string respond = SetDataToCLass(out _Savedata);
                if (!MyAccount.ExistAccountCreation(_Savedata.AccountID))
                {
                    if (respond == "True")
                    {
                        respond = MyAccount.SaveAccountCreation(_Savedata);
                        if (respond == "True")
                        {
                            MessageBox.Show("Record Saved Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadExistingAccount();
                            LoadExtAccountInHiaraky();
                            InitializeScreen();
                        }
                        else
                            MessageBox.Show(respond, "E r r o r", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                    else
                        MessageBox.Show(respond, "E r r o r", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show("Account Number Already in the System, Use Update Button to change delatails", "E r r o r", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void cmbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem cmb = new ComboboxItem();
            if (cmb.GetReleventTextFromID(cmbAccountType, cmbAccountType.Text, false) == "1")

                cmbBankAccountNo.Enabled = true;
            else
                cmbBankAccountNo.Enabled = false;
        }
        private void DisplayExtData(string  AccNo)
        {
            AccountType.AccountCreationDataType _Savedata = new AccountType.AccountCreationDataType();
            string respond = MyAccount.GetExistAccountCreation(AccNo, out _Savedata);
            InitializeScreen();
              ComboboxItem cmb = new ComboboxItem();
            if (respond == "True")
            {
                txtAccID.Text = _Savedata.AccountID.ToString ();
                txtAccountDescription.Text = _Savedata.AccountName;
                cmbAccountType.Text = cmb.GetReleventTextFromID(cmbAccountType, _Savedata.MainAccType.ToString(), true);
                if (_Savedata.IsSubAccount == 1)
                { chkIsSubaccount.CheckState = CheckState.Checked;
                    cmbMainAccount.Text = cmb.GetReleventTextFromID(cmbMainAccount, _Savedata.MainAccountID.ToString (), true);
                }
                else
                    chkIsSubaccount.CheckState = CheckState.Unchecked;
                cmbCurrenyType.Text = cmb.GetReleventTextFromID(cmbCurrenyType, _Savedata.Currency , true);
                cmbBankAccountNo.Text = _Savedata.BankAccountNo;
                cmbAccStatus.Text = cmb.GetReleventTextFromID(cmbAccStatus, _Savedata.AccountStatus.ToString (), true);
                
            }
        }

        private void dgvAccountList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                
                string AccIDstr = dgvAccountList.Rows[e.RowIndex].Cells["dgvAccountList_AccountID"].Value.ToString();
                DisplayExtData(AccIDstr);
                tabControl1.SelectTab(0); 
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel7.Enabled = true;
            panel8.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Update Creent Record ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                AccountType.AccountCreationDataType _Savedata = new AccountType.AccountCreationDataType();
                string respond = SetDataToCLass(out _Savedata);
                if (MyAccount.ExistAccountCreation(_Savedata.AccountID))
                {
                    if (respond == "True")
                    {
                        respond = MyAccount.UpdateAccountCreation(_Savedata);
                        if (respond == "True")
                        {
                            MessageBox.Show("Record Saved Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadExistingAccount();
                            LoadExtAccountInHiaraky();
                        }
                        else
                            MessageBox.Show(respond, "E r r o r", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                    else
                        MessageBox.Show(respond, "E r r o r", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show("No account Number in the system, Use Save Button to Add new account delatails", "E r r o r", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Delete Creent Record ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    string respond = MyAccount.DeleteAccountCreation(txtAccID.Text);
                    if (respond == "True")
                    {
                        MessageBox.Show("Account Deleted Successfully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InitializeScreen();
                        LoadExistingAccount();
                        LoadExtAccountInHiaraky();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message , "E r r o r", MessageBoxButtons.OK, MessageBoxIcon.Stop);
 

                }
            }
        }

        private void cmbSearchAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem cmb = new ComboboxItem();
            string CatID = cmb.GetReleventTextFromID(cmbSearchAccType, cmbSearchAccType.Text, false);
            DataTable tb = MyAccount.GetAccountListByCat(Program.AccountStatic.CompanyID, 1, int.Parse(CatID));
            MyCommon.LoadDatatoTableWithoutBind(dgvAccountList, tb, "Load Account List");
        }

        private void HanchyGrid_Navigate(object sender, NavigateEventArgs ne)
            {
                
            }

        private void cmbMainAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            int MainAcType = MyAccount.GetAccountTypeFromAccountID(MyCommon.GetSelectedID(cmbMainAccount,true));
            switch (MainAcType)
            {
                case 2:
                    chkPaybleREceiveble.Checked = true;
                    break;
                case 6:
                    chkPaybleREceiveble.Checked = true;
                    break;
                default:
                    chkPaybleREceiveble.Checked = false ;
                     break;
            }
        }

    }
}
