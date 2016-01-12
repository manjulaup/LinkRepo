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
    public partial class frmLedgers : Form
    {
        private CommonOperations MyCommon = null;
        private AccountCreation MyAccount = null;
        public frmLedgers()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose ();
        }

        private void CreateGrid(int RptID)
        {
            DataTable tb = new DataTable();
           
            switch (RptID )
            {
                case 1:
                    string AccountID = MyCommon.GetSelectedID(cmbPara3, true); 
                    dgvAccounts.ColumnCount =8;
                    dgvAccounts.Columns[0].Name = "Trans ID";
                    dgvAccounts.Columns[1].Name = "Description";
                    dgvAccounts.Columns[1].Width = 350;
                    dgvAccounts.Columns[2].Name = "Main Ref";
                    dgvAccounts.Columns[2].Width = 100;
                    dgvAccounts.Columns[3].Name = "Rel Ref";
                    dgvAccounts.Columns[4].Name = "Voucher";
                    dgvAccounts.Columns[5].Name = "Date";
                    dgvAccounts.Columns[6].Name = "Debit";
                    dgvAccounts.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAccounts.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
                    dgvAccounts.Columns[7].Name = "Credit";

                    tb = MyAccount.GetEachLedger(AccountID, Program.AccountStatic.CurrentAccPeriod, chkinFCur.Checked);

                    MyCommon.LoadDatatoTableWithoutBind(dgvAccounts, tb, "Load Each ledgers");
                    break;
                case 2:

                    dgvAccounts.ColumnCount =8;
                    dgvAccounts.Columns[0].Name = "Trans ID";
                    dgvAccounts.Columns[1].Name = "Account";
                    dgvAccounts.Columns[1].Width = 100;
                    dgvAccounts.Columns[2].Name = "Account Name";
                    dgvAccounts.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvAccounts.Columns[2].Width = 250;
                    dgvAccounts.Columns[3].Name = "Description";
                    dgvAccounts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvAccounts.Columns[3].Width = 300;
                    dgvAccounts.Columns[4].Name = "Date";
                    dgvAccounts.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvAccounts.Columns[5].Name = "Voucher";
                    dgvAccounts.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvAccounts.Columns[6].Name = "Debit";
                    dgvAccounts.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAccounts.Columns[7].Name = "Credit";
                    dgvAccounts.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                   
                   
                    if (chkinFCur.Checked)
                        tb = MyAccount.GetGeneralLedger(Program.AccountStatic.CurrentAccPeriod, true, chKWithSub.Checked,dtrTo.Value  );
                    else
                        tb = MyAccount.GetGeneralLedger(Program.AccountStatic.CurrentAccPeriod, false, chKWithSub.Checked, dtrTo.Value);
                    MyCommon.LoadDatatoTableWithoutBind(dgvAccounts, tb, "Load Each ledgers");
                    break;
                case 3:

                    dgvAccounts.ColumnCount = 4;
                    dgvAccounts.Columns[0].Name = "Account ID";
                    dgvAccounts.Columns[1].Name = "Account Name";
                    dgvAccounts.Columns[1].Width = 300;
                    dgvAccounts.Columns[2].Name = "Debit";
                    dgvAccounts.Columns[2].Width = 100;
                    dgvAccounts.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAccounts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvAccounts.Columns[3].Name = "Credit";
                    dgvAccounts.Columns[3].Width = 100;
                    int cht = 0;
                    if ( chKWithSub.CheckState== CheckState.Checked)
                        cht=1;
                    else 
                        cht=0;
                    tb = MyAccount.GetTrialBalance(Program.AccountStatic.CurrentAccPeriod, dtrTo.Value, cht);
                    MyCommon.LoadDatatoTableWithoutBind(dgvAccounts, tb, "Load Trial balance");
                    CalTotalTrialbalanceTotal();
                    string delete1 = "Delete from tbltrialbalance where UseName='" + Program.AccountStatic.UserName + "'";
                    string respond2 = MyCommon.ExicuteAnyCommandAccount(delete1, "SAve Trial Balance");
                    foreach (DataRow rm in tb.Rows)
                    {
                        string sql1 = "Insert into tbltrialbalance values(" + Program.AccountStatic.CompanyID + ",'"
                            + rm["AccountID"].ToString() + "','"
                            + rm["AccountName"].ToString() + "',"
                            + rm["FinalBalanceDr"].ToString() + ","
                            + rm["FinalBalanceCr"].ToString() + ",'" + Program.AccountStatic.UserName  + "')";
                        string respond = MyCommon.ExicuteAnyCommandAccount(sql1, "SAve Trial Balance");

                    }
                    break;
                case 4 :
                      AccountID = MyCommon.GetSelectedID(cmbPara3, true); 
                       dgvAccounts.ColumnCount = 3;
                    dgvAccounts.Columns[0].Name = "Account ID";
                    dgvAccounts.Columns[1].Name = "Account Name";
                    dgvAccounts.Columns[1].Width = 300;
                    dgvAccounts.Columns[2].Name = "Amount";
                    dgvAccounts.Columns[2].Width = 100;
                    dgvAccounts.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    tb = MyAccount.GetSupplierOutstanding(AccountID, dtrTo.Value, Program.AccountStatic.CompanyID); 
                    MyCommon.LoadDatatoTableWithoutBind(dgvAccounts, tb, "Load Supplier Outstanding");
                   
                    break;
                default:
                    break;
            }
        }
        private void CalTotalTrialbalanceTotal()
        {
            try
            {
                if (dgvAccounts.Rows.Count > 0)
                {
                    decimal TotoaDr = 0, TotalCr = 0;
                    foreach (DataGridViewRow item in dgvAccounts.Rows)
                    {
                        decimal dr = 0, cr = 0;
                        bool resp = decimal.TryParse(item.Cells[2].Value.ToString(), out dr);
                        resp = decimal.TryParse(item.Cells[3].Value.ToString(), out cr);
                        TotoaDr = TotoaDr + dr;
                        TotalCr = TotalCr + cr;
                    }
                    string[] row0 = { "", "", "", "" };
                    dgvAccounts.Rows.Add(row0);
                    string[] row1 = { "", "Total", TotoaDr.ToString("##0.00"), TotalCr.ToString("##0.00") };
                    dgvAccounts.Rows.Add(row1);
                }
            }
            catch (Exception ex)
            {

                Program.VerningMessage(ex.Message); 
            }
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            string rptid1 = MyCommon.GetSelectedID(cmbRptType, true);
            int rptid = int.Parse(rptid1);
            CreateGrid(rptid);
        }

        private void frmLedgers_Load(object sender, EventArgs e)
        {
            MyCommon = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
            MyAccount = new AccountCreation(Program.AccountStatic.LoggingAsLocal);
            MyCommon.LoadStatusComboAccount(cmbRptType, 5);

        }

        private void cmbRptType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rptid1 = MyCommon.GetSelectedID(cmbRptType, true);
            int rptid = int.Parse(rptid1);
            switch (rptid)
            {
                case 1:
                    MyAccount.GetAccountListByCat( Program.AccountStatic.CompanyID,cmbPara3, 0);
                    break;
                case 4:
                    MyAccount.GetAccountListByCat(Program.AccountStatic.CompanyID, cmbPara3, 0);
                    break;
                default:
                    break;
            }
        }


       
    }
}
