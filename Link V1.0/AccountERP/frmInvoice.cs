using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.CommonOperation;
using BusinessLayer.Invoices;
using BusinessLayer.AccountTranactions;
using BusinessLayer.Supplier ;
using BusinessLayer.AccountCreations;
namespace AccountERP
{
    public partial class frmInvoice : Form
    {
        private CommonOperations MyCommon = null;
        private Invoice MyInvoice = null;
        private AccountCreation MyAccount = null;
        private AccountTranaction MtTransaction = null;
        public frmInvoice()
        {
            InitializeComponent();
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {
            MyCommon = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
            MyAccount = new AccountCreation(Program.AccountStatic.LoggingAsLocal);
            MyInvoice = new Invoice(Program.AccountStatic.LoggingAsLocal);

            panel1.Top = (this.Height - panel1.Height) / 2;
            panel1.Left = (this.Width - panel1.Width) / 2;
            MyAccount.LoadCustomer(cmbSupplier);
            MyCommon.LoadStatusComboAccount(cmbStatus, 4);
            LoadList();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            string supid = MyCommon.GetSelectedID(cmbSupplier, true);
            int SupID1 = int.Parse(supid);
            lblAccnumber.Text = MyAccount.GetCustomerAccountNumber(SupID1);
            string curr = "";
            decimal ExRate = MyAccount.GetExRate(lblAccnumber.Text, out curr);
            lblExchangerate.Text = ExRate.ToString("#0.000");
            lblCurrency.Text = curr;
            MyInvoice.LoadInvoice(cmbGRN, SupID1);
            txtpayterm.Text = MyAccount.GetCreditPeriod(SupID1,false).ToString();

        }

        private void dtpInvoiceDate_ValueChanged(object sender, EventArgs e)
        {
            MakeDueDate();
        }
        private void MakeDueDate()
        {
            double d = 0;
            bool resp = double.TryParse(txtpayterm.Text, out d);
            DateTime dt1 = dtpInvoiceDate.Value.AddDays(d);
            lblDueDate.Text = dt1.ToString("dd/MMM/yyyy");
        }

        private void cmbGRN_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyInvoice.LoadInvoiceDetails(dtpGRNDetals,cmbGRN.Text);
           
            calTotalValue();
            txtDescription.Text = "Sales of " + cmbGRN.Text;
            txtLineAmount.Text = lblGrnToala.Text;
        }
        private void calTotalValue()
        {
            decimal total1 = 0;
            foreach (DataGridViewRow item in dtpGRNDetals.Rows)
            {

                decimal Lt = decimal.Parse(item.Cells["dtpGRNDetals_Amount"].Value.ToString());
                total1 = total1 + Lt;

            }
            lblGrnToala.Text = total1.ToString("#0.00");
        }

        private void txtToAccount_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==  Keys .Escape )
                HanchyGrid.Visible = false;
            else
               LoadExtAccountInHiaraky(txtToAccount.Text);
        }
        private void LoadExtAccountInHiaraky(string KeyWord)
        {

            DataTable tb = MyAccount.GetAccountListForDDL(KeyWord, Program.AccountStatic.CompanyID, 1,true);
            try
            {
                if (tb != null)
                    HanchyGrid.Visible = true;
                else
                    HanchyGrid.Visible = false;
                if (tb.Rows.Count < 17)
                    HanchyGrid.Height = 28 + (tb.Rows.Count * 22);

                else
                    HanchyGrid.Height = 240;
                MyCommon.LoadDatatoTableWithoutBind(HanchyGrid, tb, "Load Accounts");

            }
            catch (Exception ex)
            {

                HanchyGrid.Visible = false;
            }

        }

        private void HanchyGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                lblToID.Text = HanchyGrid.Rows[e.RowIndex].Cells["HanchyGrid_AcID"].Value.ToString();
                txtToAccount.Text = HanchyGrid.Rows[e.RowIndex].Cells["HanchyGrid_Name"].Value.ToString();
                HanchyGrid.Visible = false;
            }
        }
        private int ExistInGrid(string Memo, string AcID)
        {
            foreach (DataGridViewRow r in dgvAccount.Rows)
            {
                string me1 = "", ac1 = "";
                me1 = r.Cells["dgvAccount_Memo"].Value.ToString().Trim();
                ac1 = r.Cells["dgvAccount_ID"].Value.ToString().Trim();
                if ((me1 == Memo) && (ac1 == AcID))
                    return r.Index;
            }
            return -1;
        }
        private string GetNextLinNumber(string ColID)
            {
            int FinalMaxN = 1;
            List<int> Nlist = new List<int>();
            if (dgvAccount.Rows.Count > 0)
                {
                foreach (DataGridViewRow r in dgvAccount.Rows)
                    {
                    string ln = r.Cells[ColID].Value.ToString();
                    int N = 0;
                    bool resp = int.TryParse(ln, out N);
                    Nlist.Add(N);
                    }
                FinalMaxN = MyCommon.GetMaxNumber(Nlist);
                return FinalMaxN.ToString();
                }
            else
                return "1";
            }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (MyAccount.ExistAccountCreation ( lblToID.Text))
            {
            int i = -1;
            i = ExistInGrid(txtMomo.Text, lblToID.Text);
            decimal d1 = 0, d2 = 0,lkr=0,exrt=0;
            bool resp = decimal.TryParse(txtLineAmount.Text, out d1);
            resp = decimal.TryParse(txtVat.Text, out d2);
            resp = decimal.TryParse(lblExchangerate.Text, out exrt);
            lkr = d1 * exrt;
            if (i == -1)
            {
                int Ref = dgvAccount.Rows.Count + 1;
                string[] row1 = { lblToID.Text, txtToAccount.Text, txtMomo.Text, lkr.ToString("#0.00"), d1.ToString("#0.00"), d2.ToString("#0.00"), GetNextLinNumber("dgvAccount_LineRef") };
                dgvAccount.Rows.Add(row1);
            }
            else
            {
                dgvAccount.Rows[i].Cells["dgvAccount_Amount"].Value = lkr.ToString("##0.000");
                dgvAccount.Rows[i].Cells["dgvAccount_Fcr"].Value = d1.ToString("##0.000");
                dgvAccount.Rows[i].Cells["dgvAccount_VAT"].Value = d2.ToString("##0.000");
            }
            CalTotalAmount();
            CleareUpperLine();
            MakeDueDate();
            }
            else 
                Program .VerningMessage ("Invalied  Account Number");
        }
        private void CleareUpperLine()
        {
            MyCommon.ClearCurrentPanelTestAndCombo(ref panel10);
            lblToID.Text = "";
        }
        private void CalTotalAmount()
        {
            decimal amounttotal = 0, vattotal = 0,TotalFcr=0;
            foreach (DataGridViewRow r in dgvAccount.Rows)
            {
                decimal amount = 0, vat = 0,Fcr=0;
                bool resp = decimal.TryParse(r.Cells["dgvAccount_Amount"].Value.ToString(), out amount);
                resp = decimal.TryParse(r.Cells["dgvAccount_VAT"].Value.ToString(), out vat);
                resp = decimal.TryParse(r.Cells["dgvAccount_Fcr"].Value.ToString(), out Fcr);
                amounttotal += amount;
                vattotal += vat;
                TotalFcr = TotalFcr + Fcr;
            }
            lblTotalVat.Text = vattotal.ToString("##0.00");
            lblTotalAmount.Text = amounttotal.ToString("##0.00");
            lblFcr.Text = TotalFcr.ToString("##0.00");
        }

        private void btnAddSummary_Click(object sender, EventArgs e)
        {
            AddNewRow();
            MakeDueDate();
        }
        private void AddNewRow()
        {
            string supid = MyCommon.GetSelectedID(cmbSupplier, true);
            int SupID1 = int.Parse(supid);
            string AccountID = MyInvoice.GetSalesAccount(SupID1);
            string acname=MyAccount .GetAccountName( AccountID);
            decimal TotalLKR = 0;
            bool resp = decimal.TryParse(lblGrnToala.Text, out TotalLKR);
            TotalLKR = TotalLKR * decimal.Parse(lblExchangerate.Text );
            string[] row1 = { AccountID, acname, "Salse of " + cmbGRN.Text, TotalLKR.ToString("##0.00"), lblGrnToala.Text, "0", GetNextLinNumber("dgvAccount_LineRef") };
            dgvAccount.Rows.Add(row1);
            CalTotalAmount();
        }

        private void dgvAccount_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                lblToID.Text = dgvAccount.Rows[e.RowIndex].Cells["dgvAccount_ID"].Value.ToString();
                txtToAccount.Text = dgvAccount.Rows[e.RowIndex].Cells["dgvAccount_Name"].Value.ToString();
                txtMomo.Text = dgvAccount.Rows[e.RowIndex].Cells["dgvAccount_Memo"].Value.ToString();
                txtLineAmount.Text = dgvAccount.Rows[e.RowIndex].Cells["dgvAccount_Fcr"].Value.ToString();
                txtVat.Text = dgvAccount.Rows[e.RowIndex].Cells["dgvAccount_VAT"].Value.ToString();
            }
        }

        private void dgvAccount_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete current row? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            if (MessageBox.Show("Do you want to Save current record ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) ;
            {

            int InvoiceStatus = MyInvoice.GetInvoiceStatus(cmbGRN .Text);
            switch (InvoiceStatus)
            {
                case 0:
                    
                InvoiceDataTypes.InvoiceDataType _saveData = new InvoiceDataTypes.InvoiceDataType();
                if (!MyInvoice.ExistInvoice(_saveData.InvoiceNo))
                {
                    string respond = SetDatatoClass(out _saveData);
                    if (respond == "True")
                    { 
                        respond = MyInvoice.Save(_saveData);
                    if (respond == "True")
                        Program.InformationMessage("Saved Successfully");
                    else
                        Program.VerningMessage(respond);
                    }
                    else
                        Program.VerningMessage(respond);
                }
                else
                    Program.VerningMessage("Use Update Button");
                break;
                case 2:
                    Program.VerningMessage("Already Approved Invoice cannot change");
                break;
                case 3:
                    Program.VerningMessage("Already Accounted Invoice cannot change");
                break;
                default:
                break;
            }
           
            }
        }
        private string SetDatatoClass(out InvoiceDataTypes.InvoiceDataType _saveData)
        {
            _saveData = new InvoiceDataTypes.InvoiceDataType();
            try
            {
                decimal TotalLKR = 0, TotalUSD = 0;
                bool resp = decimal.TryParse(lblTotalAmount.Text, out TotalLKR);
                resp = decimal.TryParse(lblFcr.Text, out TotalUSD);
                string supid = MyCommon.GetSelectedID(cmbSupplier, true);
                int SupID1 = int.Parse(supid);
              
                _saveData.AccPeriod = Program.AccountStatic.CurrentAccPeriod;
                _saveData.AccountID = lblAccnumber.Text;
                _saveData.CompanyID = Program.AccountStatic.CompanyID;
                _saveData.CurRate = decimal.Parse(lblExchangerate.Text);
                _saveData.Description = txtDescription.Text;
                _saveData.Dr = TotalLKR;
                _saveData.FDr = TotalUSD;
                _saveData.InvoiceDate = dtpInvoiceDate.Value;
                _saveData.InvoiceNo = cmbGRN.Text;
                _saveData.InvoiceStatus = 0;
                _saveData.RcvAmount = TotalLKR;
                _saveData.RcvFromCatID = 0;
                _saveData.RcvFromID = SupID1;
                _saveData.TobeRcvDate = DateTime.Parse(lblDueDate.Text);
                _saveData.TrUser = Program.AccountStatic.UserName;
                List<InvoiceDataTypes.InvoiceDetailsDataType> _DetailList = new List<InvoiceDataTypes.InvoiceDetailsDataType>();
                string respond = SetDetailsToClass(out _DetailList);
                _saveData.InvoiceDtails = _DetailList;
                return "True";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
        private string SetDetailsToClass(out List<InvoiceDataTypes.InvoiceDetailsDataType> _DetailList)
        {
            _DetailList = new List<InvoiceDataTypes.InvoiceDetailsDataType>();

            try
            {
                if (dgvAccount.Rows.Count > 0)
                {
                    foreach (DataGridViewRow r in dgvAccount.Rows)
                    {
                        InvoiceDataTypes.InvoiceDetailsDataType _OneDAta = new InvoiceDataTypes.InvoiceDetailsDataType();
                        decimal TotalLKR = 0, TotalUSD = 0,Vat=0;
                        bool resp = decimal.TryParse(r.Cells["dgvAccount_Amount"].Value.ToString(), out  TotalLKR);
                        resp = decimal.TryParse(r.Cells["dgvAccount_Fcr"].Value.ToString(), out  TotalUSD);
                        resp = decimal.TryParse(r.Cells["dgvAccount_VAT"].Value.ToString(), out  Vat);
                        int i = 0;
                        resp = int.TryParse(r.Cells["dgvAccount_LineRef"].Value.ToString(), out i);

                        _OneDAta.AccID = r.Cells["dgvAccount_ID"].Value.ToString();
                        _OneDAta.Cr = TotalLKR;
                        _OneDAta.Description = r.Cells["dgvAccount_Memo"].Value.ToString();
                        _OneDAta.FCr = TotalUSD;
                        _OneDAta.InvoiceNO = cmbGRN.Text;
                        _OneDAta.ItemNo = i;
                        _OneDAta.Vat = Vat;
                        _DetailList.Add(_OneDAta);
                    }
                    return "True";
                }
                else
                {
                    return "No Data To Save";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
             if (MessageBox.Show("Do you want to Update current record ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) ;
            {

            int InvoiceStatus = MyInvoice.GetInvoiceStatus(cmbGRN .Text);
            switch (InvoiceStatus)
            {
                case 0:
                    
                InvoiceDataTypes.InvoiceDataType _saveData = new InvoiceDataTypes.InvoiceDataType();
                if (MyInvoice.ExistInvoice(_saveData.InvoiceNo))
                {
                    string respond = SetDatatoClass(out _saveData);
                    if (respond == "True")
                    { 
                        respond = MyInvoice.Update(_saveData);
                    if (respond == "True")
                        Program.InformationMessage("Saved Successfully");
                    else
                        Program.VerningMessage(respond);
                    }
                    else
                        Program.VerningMessage(respond);
                }
                else
                    Program.VerningMessage("Use Save Button");
                break;
                case 2:
                    Program.VerningMessage("Already Approved Invoice cannot change");
                break;
                case 3:
                    Program.VerningMessage("Already Accounted Invoice cannot change");
                break;
                default:
                break;
            }
           
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Update current record ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) ;
            {
                int InvoiceStatus = MyInvoice.GetInvoiceStatus(cmbGRN.Text);
                switch (InvoiceStatus)
                {
                    case 0:
                        string respond = "";
                        respond = MyInvoice.SendToApprovel(cmbGRN.Text, Program.AccountStatic.UserName);
                        if (respond == "True")
                            Program.InformationMessage("Successfully Send to Approval ");
                        else
                            Program.VerningMessage(respond);
                        break;
                    case 1:
                        Program.VerningMessage("Already Send to Approval");
                        break;
                    case 2:
                        Program.VerningMessage("Already Approved");
                        break;
                    case 3:
                        Program.VerningMessage("Already Accounted");
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Update current record ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) ;
            {
                int InvoiceStatus = MyInvoice.GetInvoiceStatus(cmbGRN.Text);
                switch (InvoiceStatus)
                {
                    case 1:
                        string respond = "";
                        respond = MyInvoice.InvoiceApproved(cmbGRN.Text, Program.AccountStatic.UserName);
                        if (respond == "True")
                            Program.InformationMessage("Successfully Send to Approval ");
                        else
                            Program.VerningMessage(respond);
                        break;
                    case 0:
                        Program.VerningMessage("This invoice not in Aproval Stage");
                        break;
                    case 2:
                        Program.VerningMessage("Already Approved");
                        break;
                    case 3:
                        Program.VerningMessage("Already Accounted");
                        break;
                    default:
                        break;
                }
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
         
           // ComboboxItem cmb = new ComboboxItem();
            string Status1 = MyCommon.GetSelectedID(cmbStatus, true);
            int Status = 0;
            if (Status1 == "")
                Status = 0;
            else
                Status = int.Parse(Status1);
            MyInvoice.LoadInvoiceList(DgvInvoiceList, Status);
            
        }
        private void LoadList()
        {
            MyInvoice.LoadInvoiceList(DgvInvoiceList, 0);
        }

        private void DgvInvoiceList_DoubleClick(object sender, EventArgs e)
        {
          
        }
      private InvoiceDataTypes.InvoiceDataType _saveData = new InvoiceDataTypes.InvoiceDataType();
        private void DgvInvoiceList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != null)
            {
                string invoice = DgvInvoiceList.Rows[e.RowIndex].Cells["DgvInvoiceList_Invoice"].Value.ToString();
                string respond = MyInvoice.GetExistInvoice(invoice, out _saveData);
                DisplayData();
            }
        }
        private void DisplayData()
        {
            ComboboxItem cmb = new ComboboxItem();

            cmbGRN.Text = _saveData.InvoiceNo;
            cmbSupplier.Text = cmb.GetReleventTextFromID(cmbSupplier, _saveData.RcvFromID.ToString (), true);
            dtpInvoiceDate.Value = _saveData.InvoiceDate;
       //     dtpBilingDate.Value =  _saveData.
            txtDescription.Text = _saveData.Description;
            DataTable tb = MyInvoice.GetExistInvoiceDetails(cmbGRN.Text);
            lblDueDate.Text = _saveData.TobeRcvDate.ToString("dd/MMM/yyyy");
            MyCommon.LoadDatatoTableWithoutBind(dgvAccount, tb, "Load deails");
            CalTotalAmount();
            tabControl1.SelectTab(0);
        }

        private void btnPostTpAcc_Click(object sender, EventArgs e)
        {
             if (MessageBox.Show("Do you want to Update Account ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) ;
            {
                int InvoiceStatus = MyInvoice.GetInvoiceStatus(cmbGRN.Text);
                switch (InvoiceStatus)
                {
                    case 1:
                           Program.VerningMessage("Not approved invoice");
                        break;
                    case 0:
                        Program.VerningMessage("This invoice not in Aproval Stage");
                        break;
                    case 2:
                        MtTransaction=new AccountTranaction (Program .AccountStatic.LoggingAsLocal);
                        string respond = MtTransaction.DoInvoice(_saveData);
                        if (respond == "True")
                        {
                            Program.InformationMessage("Successfully Posted to account");
                        }
                        else
                            Program.VerningMessage(respond);

                        break;
                    case 3:
                        Program.VerningMessage("Already Accounted");
                        break;
                    default:
                        break;
                }
            }
        }
        private void EditCurrentScreen(bool status)
        {
            panel10.Enabled = status;
            panel3.Enabled = status;
            panel7.Enabled = status;
            panel4.Enabled = status;
        }
        private void ClearCurrentScreen()
        {
            MyCommon.ClearCurrentPanel(ref panel10);
           
            MyCommon.ClearCurrentPanel(ref panel4);
            MyCommon.ClearCurrentPanel(ref panel3);
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            GetCustomerMaster();
            ClearCurrentScreen();
            EditCurrentScreen(true);
            dtpBilingDate.Value = DateTime.Today;
            dtpInvoiceDate.Value = DateTime.Today;
        }

        public void GetCustomerMaster()
        {
            try
            {
                Finance.MRPServiceReference.ServiceClient objService = new Finance.MRPServiceReference.ServiceClient();
                List<EntityHandler.CustomerMaster> objList = new List<EntityHandler.CustomerMaster>();
                EntityHandler.CustomerMaster [] objArry = objService.GetCustomerMaster();
                objList = new BusinessHandler.CustomerMaster().BALGetCustomerMaster();
            
                cmbSupplier.Items.Clear();
                ComboboxItem itemDefault = new ComboboxItem();
                itemDefault.Text = "--Select--";
                itemDefault.Value = "0";
                cmbSupplier.Items.Add(itemDefault);

                for (int i = 0; i < objArry.Length; i++)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = objArry[i].Name;
                    item.Value = objArry[i].Customer_Code;
                    cmbSupplier.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditCurrentScreen(true);
        }

        private void btnCLear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear current screen ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Retry)
            {
                ClearCurrentScreen();
            }
        }
    }
}
