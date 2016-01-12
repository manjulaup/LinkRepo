using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.AccountCreations;
using BusinessLayer.CommonOperation;
using BusinessLayer.Billings;
using BusinessLayer.AccountTranactions;
using BusinessLayer.Supplier;
using EntityHandler;

namespace AccountERP
{
    public partial class frmBill : Form
    {

        Finance.MRPServiceReference.ServiceClient objService = new Finance.MRPServiceReference.ServiceClient();
        private AccountCreation MyAccount = null;
        private CommonOperations MyCommon = null;
        private AccountTranaction MyTransaction = null;
        private Billing MyBill = null;

        public frmBill()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmBill_Load(object sender, EventArgs e)
        {
            MyBill = new Billing(Program.AccountStatic.LoggingAsLocal);
            MyCommon = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
            MyAccount = new AccountCreation(Program.AccountStatic.LoggingAsLocal);
          
            panel1.Top = (this.Height - panel1.Height) / 2;
            panel1.Left = (this.Width - panel1.Width) / 2;
            //MyAccount.LoadSupplier(cmbSupplier);
            //Edit by manjula   *** Load suppliers from MRP
            LoadSuppliers();

            MyAccount.LoadSupplier(cmbSearchSupplier);
            MyCommon.LoadStatusComboAccount(cmbStatus, 4);
            EditScreen(false);
        }

        private void LoadSuppliers()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("SupplierID");
            dt.Columns.Add("SupplierName");

            LINKPayment[] objSupList = objService.GetCreditorFinalSupplierList();
            if (objSupList.Length > 0)
            {
                for (int j = 0; j < objSupList.Length; j++)
                {

                       dr = dt.NewRow();

                       dr[0] = objSupList[j].SupName.ToString();
                       dr[1] = objSupList[j].SupName.ToString();

                        dt.Rows.Add(dr.ItemArray);
                }

                cmbSupplier.DataSource = dt;
                cmbSupplier.DisplayMember = "SupplierID";
                cmbSupplier.ValueMember = "SupplierName";
            }

        }

        private void LoadGRN(LINKPayment objLink)
        {

        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
            {
                //string supid = MyCommon.GetSelectedID(cmbSupplier,true);
                //Edit by manjula
                string SupName = "R.D. LALITH   PRIYANTHA";
                string supid = "10519";    

                int SupID1 = int.Parse(supid);
                //lblAccnumber.Text = MyAccount.GetSupplierAccountNumber(SupID1);
                lblAccnumber.Text = "1295090";
                string curRate = "";
                decimal ExRate = MyAccount.GetExRate(lblAccnumber.Text, out curRate);
                lblExchangerate.Text = ExRate.ToString("#0.000");
                lblCurrency.Text = curRate;
                if (lblCurrency.Text  != "USD")
                    lblExchangerate.Enabled = true;
                else
                    lblExchangerate.Enabled = false;

               // MyBill.LoadGRNNumbers(cmbGRN, SupID1);
               // txtpayterm.Text = MyAccount.GetCreditPeriod(SupID1, true).ToString();
               // lblTotalBillOutstanding.Text = MyBill.GetTotalOutstanding(SupID1).ToString("##0.00");
               LINKPayment objLink=new LINKPayment();
                objLink.SupName=SupName;
                LoadGRN(objLink);

                txtpayterm.Text = "999";
                lblTotalBillOutstanding.Text = "9.99";

            }
        private void CalTotalGRNAmount()
        {
            decimal total = 0;
            foreach (DataGridViewRow item in dtpGRNDetals.Rows)
            {
                decimal d = decimal.Parse(item.Cells["dtpGRNDetals_Amount"].Value.ToString());
              total = total + d;
            }
            lblGrnToala.Text = total.ToString("##0.000");
        }

        private void cmbGRN_SelectedIndexChanged(object sender, EventArgs e)
            {
                 string supid = MyCommon.GetSelectedID(cmbSupplier,true);
                int SupID = int.Parse(supid);
                DataTable tb = MyBill.GetGRNData(cmbGRN.Text, SupID);
                MyCommon.LoadDatatoTableWithoutBind(dtpGRNDetals, tb, "Load GRN");
                
                chkSelect.Checked = true;
                calTotalValue();
                txtDescription.Text = "Purchase of " + cmbGRN.Text;
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
        private void txtToAccount_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                HanchyGrid.Visible = false;
            else 
            LoadExtAccountInHiaraky(txtToAccount.Text);
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

        private void HanchyGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape )
                HanchyGrid.Visible = false;
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
            int i = -1;
            if (MyAccount.ExistAccountCreation(lblToID.Text))
            {
                int lindex1 = -1;
                bool rsp4 = int.TryParse(lblDownIndex.Text, out lindex1);
                if (!rsp4)
                    lindex1 = -1;

                i = ExistInGrid(lindex1);
                decimal LKR = 0, USD = 0, d2 = 0, Exrate = 0;
                bool resp = decimal.TryParse(txtLineAmount.Text, out USD);
                resp = decimal.TryParse(lblExchangerate.Text, out Exrate);
                LKR = USD * Exrate;
                resp = decimal.TryParse(txtVat.Text, out d2);
                if (i == -1)
                {
                    int Ref = dgvAccount.Rows.Count + 1;
                    string[] row1 = { lblToID.Text, txtToAccount.Text, txtMomo.Text, LKR.ToString("#0.00"), USD.ToString("#0.00"), d2.ToString("#0.00"), GetNextLinNumber("dgvAccount_LineRef")  };
                    dgvAccount.Rows.Add(row1);
                }
                else
                {
                    dgvAccount.Rows[i].Cells["dgvAccount_ID"].Value = lblToID.Text;
                    dgvAccount.Rows[i].Cells["dgvAccount_Name"].Value = txtToAccount.Text;
                    dgvAccount.Rows[i].Cells["dgvAccount_Amount"].Value = LKR.ToString("##0.00");
                    dgvAccount.Rows[i].Cells["dgvAccount_Memo"].Value = txtMomo.Text;
                    dgvAccount.Rows[i].Cells["dgvAccount_Fcr"].Value = USD.ToString("##0.00");
                    dgvAccount.Rows[i].Cells["dgvAccount_VAT"].Value = d2.ToString("##0.00");
                }
                CalTotalAmount();
                CleareUpperLine();
            }
            else
                Program.VerningMessage("Invalied Account");
        }
        private void CleareUpperLine()
        {
            MyCommon.ClearCurrentPanelTestAndCombo(ref panel10);
            lblToID.Text = ""; 
        }
        private string SetDataToPayBill(out BillingDataTypes.BillingDataType _SaveData)
        {
            MakeDueDate();
            _SaveData = new BillingDataTypes.BillingDataType();
            try
            {
                _SaveData.AccountID = lblAccnumber.Text;
                _SaveData.AccPeriod = Program.AccountStatic.CurrentAccPeriod;
                _SaveData.BillNo = cmbGRN.Text;
                _SaveData.BillStatus = 0;
                _SaveData.CompanyID = Program.AccountStatic.CompanyID;
                _SaveData.Cr =decimal.Parse(lblTotalLKR.Text) ;
                _SaveData.CurRate = decimal.Parse(lblExchangerate.Text);
                _SaveData.Description = txtDescription.Text;
                _SaveData.FCr = decimal.Parse(lblTotalUSD.Text);
                _SaveData.PayToCatID = 0;
                _SaveData.PayToID = int.Parse(MyCommon.GetSelectedID(cmbSupplier, true));
                _SaveData.TobePayDate = DateTime.Parse(lblDueDate.Text);
                _SaveData.TrUser = Program.AccountStatic.UserName;
                _SaveData.TrDate = dtpInvoiceDate.Value;
                _SaveData.BillDate = dtpBilingDate.Value;  
             
                List<BillingDataTypes.BillingDetailsDataType> _SaveDetails = new List<BillingDataTypes.BillingDetailsDataType>();
                string respond = SetPayBillDetails(out _SaveDetails);
                _SaveData.BillingDetails = _SaveDetails;
                return "True";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        private void CalTotalAmount()
        {
            decimal LKR = 0,TtoalUSD=0, vattotal = 0;
            foreach (DataGridViewRow r in dgvAccount.Rows)
            {
                decimal amount = 0, vat = 0,usd=0;
                bool resp = decimal.TryParse(r.Cells["dgvAccount_Amount"].Value.ToString(), out amount);
                resp = decimal.TryParse(r.Cells["dgvAccount_VAT"].Value.ToString(), out vat);
                resp = decimal.TryParse(r.Cells["dgvAccount_Fcr"].Value.ToString(), out usd);
                LKR += amount;
                TtoalUSD += usd;
                vattotal += vat;
            }
            lblTotalVat.Text = vattotal.ToString("##0.00");
            lblTotalLKR.Text = LKR.ToString("##0.00");
            lblTotalUSD.Text = TtoalUSD.ToString("##0.00");
        }
        private string SetPayBillDetails(out List < BillingDataTypes.BillingDetailsDataType> _SaveDetails)
        {
            _SaveDetails = new List<BillingDataTypes.BillingDetailsDataType>();

            try
            {
                foreach (DataGridViewRow r in dgvAccount.Rows)
                {
                    decimal LKR = 0, vat = 0,USD=0;
                    bool resp = decimal.TryParse(r.Cells["dgvAccount_Amount"].Value.ToString(), out LKR);
                    resp = decimal.TryParse(r.Cells["dgvAccount_Fcr"].Value.ToString(), out USD);
                    resp = decimal.TryParse(r.Cells["dgvAccount_VAT"].Value.ToString(), out vat);
                    BillingDataTypes.BillingDetailsDataType _OneItem = new BillingDataTypes.BillingDetailsDataType();
                    _OneItem.AccID = r.Cells["dgvAccount_ID"].Value.ToString();
                    _OneItem.BillNo = cmbGRN.Text;
                    _OneItem.Description = r.Cells["dgvAccount_Memo"].Value.ToString();
                    _OneItem.Dr = LKR;
                    _OneItem.Fdr = USD;
                    _OneItem.ItemNo = int.Parse(r.Cells["dgvAccount_LineRef"].Value.ToString());
                    _OneItem.Vat = vat;
                    _SaveDetails.Add(_OneItem);

                }
                return "True";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            
        }

        private string Validation()
        {
            if (dgvAccount.Rows.Count==0)
                return "No DAta to Save";

            return "True";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Do you want to Save current record ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) ;
            {
                BillingDataTypes.BillingDataType _SaveData = new BillingDataTypes.BillingDataType();
                string respond = SetDataToPayBill(out _SaveData);
                if (!MyBill.ExistBilling(_SaveData.BillNo, _SaveData.PayToID))
                {
                    int BillStatus = -1;
                    int SupID = int.Parse(MyCommon.GetSelectedID(cmbSupplier, true));
                    BillStatus = MyBill.GetBillStatus(cmbGRN.Text, SupID);
                    if (BillStatus == 0)
                    {

                        if (respond == "True")
                        {
                            respond = MyBill.Save(_SaveData);
                            if (respond == "True")
                            {
                                Program.InformationMessage("Record saved successfully");
                                LoadBillList(MyCommon.GetSelectedID(cmbSupplier, true));

                            }
                            else
                                Program.VerningMessage(respond);
                        }
                        else
                            Program.VerningMessage(respond);
                    }
                    else
                    {
                        switch (BillStatus)
                        {
                            
                            case 2:
                                Program.VerningMessage("You cannot chage any details of this bill , Tt is already approved" );
                                break;
                            case 3:
                                Program.VerningMessage("You cannot chage any details of this bill, It is already accounted");
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    Program.VerningMessage("Use Update Button to update details");
                }
            }
            
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

        private void txtToAccount_TextChanged(object sender, EventArgs e)
        {

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
        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelect.Checked)
            {
                foreach (DataGridViewRow item in dtpGRNDetals.Rows)
                {
                    item.Cells["dtpGRNDetals_Select"].Value = "1";
                }
            }
            else
            {
                foreach (DataGridViewRow item in dtpGRNDetals.Rows)
                {
                    item.Cells["dtpGRNDetals_Select"].Value = "0";
                }
            }
            calTotalValue();
        }
        private int ExistInGrid(int IndexID)
        {
            foreach (DataGridViewRow r in dgvAccount.Rows)
            {
                string IndexGrid="";
                IndexGrid = r.Cells["dgvAccount_LineRef"].Value.ToString().Trim();
                int intIndexGrid = 0;
                bool reso = int.TryParse(IndexGrid, out intIndexGrid);
                if (intIndexGrid == IndexID)
                    return r.Index;
            }
            return -1;
        }
        private int ExistInGrid(string Account)
        {
            foreach (DataGridViewRow r in dgvAccount.Rows)
            {
                string accountid = "";
                accountid = r.Cells["dgvAccount_ID"].Value.ToString().Trim();
              
                string Me1 = "",str ;

                if (accountid == Account)
                    return r.Index;
            }
            return -1;
        }
        private void UpdateAutoAddLine(int IndexID,  decimal LKR,decimal  USD,decimal Vat)
        {
            decimal ExtLKR = 0, ExtUSD = 0,Vat1=0;
            bool resp = decimal.TryParse(dgvAccount.Rows[IndexID].Cells["dgvAccount_Amount"].Value.ToString(), out ExtLKR);
            resp = decimal.TryParse(dgvAccount.Rows[IndexID].Cells["dgvAccount_Fcr"].Value.ToString(), out ExtUSD);
            resp = decimal.TryParse(dgvAccount.Rows[IndexID].Cells["dgvAccount_VAT"].Value.ToString(), out Vat1);


            dgvAccount.Rows[IndexID].Cells["dgvAccount_Amount"].Value = (LKR + ExtLKR).ToString("##0.00");
            dgvAccount.Rows[IndexID].Cells["dgvAccount_Fcr"].Value = (USD + ExtUSD).ToString("##0.00");
            dgvAccount.Rows[IndexID].Cells["dgvAccount_VAT"].Value =( Vat + Vat1).ToString("##0.00");

        }
        private void btnAddSummary_Click(object sender, EventArgs e)
        {
            MakeSelectedAccountDetails();
        }

        private void MakeSelectedAccountDetails()
        {
            dgvAccount.Rows.Clear();
            foreach (DataGridViewRow item in dtpGRNDetals.Rows)
            {
                if (item.Cells["dtpGRNDetals_Select"].Value.ToString () == "1")
                {
                     int i = -1;
                     string AcName = MyAccount.GetAccountName(item.Cells["dtpGRNDetals_AcNumber"].Value.ToString());
                     string Memo1 = "Purchase of " + AcName + " - " + cmbGRN.Text;
                     decimal Exrate = 0;
                     bool rmd = decimal.TryParse(lblExchangerate.Text, out Exrate);
                    i = ExistInGrid( item.Cells["dtpGRNDetals_AcNumber"].Value.ToString());

                    decimal LKR = 0, d2 = 0,USD=0;
                    bool resp = decimal.TryParse(item.Cells["dtpGRNDetals_Amount"].Value.ToString(), out USD);
                    LKR = USD * Exrate;
                    resp = decimal.TryParse(txtVat.Text, out d2);

                     if (i == -1)
                    {
                        int Ref = dgvAccount.Rows.Count + 1;
                        string[] row1 = { item.Cells["dtpGRNDetals_AcNumber"].Value.ToString(), AcName, Memo1, LKR.ToString("#0.00"),USD.ToString ("##0.00"), d2.ToString("#0.00"),GetNextLinNumber("dgvAccount_LineRef")};
                        dgvAccount.Rows.Add(row1);
                    }
                    else
                    {
                        UpdateAutoAddLine(i,  LKR, USD,d2);
                    }
                    CalTotalAmount();
                }
            }
        }


        private void brnShow_Click(object sender, EventArgs e)
        {
            LoadBillList(MyCommon.GetSelectedID(cmbSearchSupplier, true));
        }
        
        private void LoadBillList(string SupID)
        {
            try
            {
                string Status1 = MyCommon.GetSelectedID(cmbStatus, true);
                int Status = 0;
                if (Status1 == "")
                    Status = 0;
                else
                    Status = int.Parse(Status1);
                int Supid = int.Parse(SupID);
                DataTable tb = MyBill.GetBillList(Status, Supid);
                MyCommon.LoadDatatoTableWithoutBind(dgvBillList, tb, "Load Bill List");
            }
            catch (Exception ex)
            {
                
             
            }
        }

        private void dgvBillList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DisplayExistingData(dgvBillList.Rows[e.RowIndex].Cells[0].Value.ToString(), int.Parse(dgvBillList.Rows[e.RowIndex].Cells["dgvBillList_SupID"].Value.ToString())); 
            }
        }
        private BillingDataTypes.BillingDataType _ExtData = new BillingDataTypes.BillingDataType();
        private void DisplayExistingData(string InvoiceNumber,int SupID)
        {
           
            string respond = MyBill.GetExistBilling(InvoiceNumber,SupID, out _ExtData);
            ComboboxItem cmb=new ComboboxItem ();

            if (respond == "True")
            {
                DataTable tb = MyBill.GetBillList(InvoiceNumber);
                MyCommon.LoadDatatoTableWithoutBind(dgvAccount, tb, "load Details");
                cmbSupplier.Text = cmb.GetReleventTextFromID(cmbSupplier, _ExtData.PayToID.ToString(), true);
                cmbSupplier_SelectedIndexChanged(null, null);
                cmbGRN.Text = _ExtData.BillNo;
                cmbGRN_SelectedIndexChanged(null, null);
                txtDescription.Text = _ExtData.Description;
                dtpBilingDate.Value = _ExtData.BillDate;
                lblExchangerate.Text = _ExtData.CurRate.ToString("#0.00");
                CalTotalAmount();
                tabControl1.SelectTab(0);
            }
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
                lblDownIndex.Text = dgvAccount.Rows[e.RowIndex].Cells["dgvAccount_LineRef"].Value.ToString();
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            int BillStatus = -1;
            if (MessageBox.Show("Do you want send to Approval ? ","Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== System.Windows.Forms.DialogResult.Yes)
            {
            int SupID= int.Parse(MyCommon.GetSelectedID(cmbSupplier ,true ));
            BillStatus = MyBill.GetBillStatus(cmbGRN.Text,SupID);
            if (BillStatus==0)
            {
                string respond=MyBill .SendToapproveval (cmbGRN.Text,Program .AccountStatic .UserName , SupID);
                if (respond =="True")
                    Program.InformationMessage ("Successfully Send to Approval");
                else 
                    Program.VerningMessage (respond);
            }
            else 
            {
                if (BillStatus==1)
                {
                    Program .VerningMessage ("This Bill Already send to Approval !!!");

                }
                else if (BillStatus==2)
                {
                    Program .VerningMessage ("This Bill Already Approved !!!");
                }
                else if (BillStatus==3)
                {
                    Program .VerningMessage ("This Bill Already Accounted !!!");
                }
            }
            }

        }
        private void btnApproved_Click(object sender, EventArgs e)
        {
             int BillStatus = -1;
            if (MessageBox.Show("Do you want Approved This ? ","Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== System.Windows.Forms.DialogResult.Yes)
            {
            int SupID= int.Parse(MyCommon.GetSelectedID(cmbSupplier ,true ));
            BillStatus = MyBill.GetBillStatus(cmbGRN.Text,SupID);
            if (BillStatus==1)
            {
                string respond=MyBill.BillApproved (cmbGRN.Text,Program .AccountStatic .UserName , SupID);
                if (respond =="True")
                    Program.InformationMessage ("Successfully  Approved");
                else 
                    Program.VerningMessage (respond);
            }
            else 
            {
                if (BillStatus==0)
                {
                    Program .VerningMessage ("This is not send to approval !!!");

                }
                else if (BillStatus==2)
                {
                    Program .VerningMessage ("This Bill Already Approved !!!");
                }
                else if (BillStatus==3)
                {
                    Program .VerningMessage ("This Bill Already Accounted !!!");
                }
            }
            }
        }

        private void btnPostTpAcc_Click(object sender, EventArgs e)
        {
            int BillStatus = -1;
            if (MessageBox.Show("Do you want post to account ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                MyTransaction = new AccountTranaction(Program.AccountStatic.LoggingAsLocal);
                int SupID = int.Parse(MyCommon.GetSelectedID(cmbSupplier, true));
                BillStatus = MyBill.GetBillStatus(cmbGRN.Text, SupID);
                if (BillStatus == 2)
                {
                    string respond = ""; // 
                         respond = MyTransaction.DoBillTransaction(_ExtData);
                        if (respond == "True")
                        {
                            respond = MyBill.BillAccount(cmbGRN.Text, Program.AccountStatic.UserName, SupID);

                            if (respond == "True")
                            {
                                Program.InformationMessage("Successfully  Approved");
                            }
                            else
                            {
                                Program.VerningMessage(respond);
                            }
                        }
                        else
                            Program.VerningMessage(respond);
                }
                else
                {
                    if (BillStatus == 0)
                    {
                        Program.VerningMessage("This is not send to approval !!!");

                    }
                    else if (BillStatus == 1)
                    {
                        Program.VerningMessage("This is not Approved Bill !!!");
                    }
                    else if (BillStatus == 3)
                    {
                        Program.VerningMessage("This Bill Already Accounted !!!");
                    }
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
             if (MessageBox.Show("Do you want to Update current record ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) ;
            {
                BillingDataTypes.BillingDataType _SaveData = new BillingDataTypes.BillingDataType();
                string respond = SetDataToPayBill(out _SaveData);
                if (MyBill.ExistBilling(_SaveData.BillNo, _SaveData.PayToID))
                {
                   
                    if (respond == "True")
                    {
                        respond = MyBill.Update(_SaveData);
                        if (respond == "True")
                        {
                            Program.InformationMessage("Record Updated Successfully");
                            LoadBillList(MyCommon.GetSelectedID(cmbSupplier, true));

                        }
                        else
                            Program.VerningMessage(respond);
                    }
                    else
                        Program.VerningMessage(respond);
                }
                else
                {
                    Program.VerningMessage("Use Save Button to save new records");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int BillStatus = -1;
            int SupID = int.Parse(MyCommon.GetSelectedID(cmbSupplier, true));
            BillStatus = MyBill.GetBillStatus(cmbGRN.Text, SupID);
            string respond="";
            if (MessageBox.Show("Do you want to delete current record ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (BillStatus == 3)
                    Program.VerningMessage("you cannot delete this bill it is already accounted");
                else
                {
                    respond = MyBill.DeleteBilling(cmbGRN.Text, SupID);
                    if (respond == "True")
                    {
                        Program.InformationMessage("Record Deleted Successfully");
                        ClearFullscreen();
                    }
                    else
                    {
                        Program.VerningMessage(respond);
                    }
                }
            }
        }

        private void btnCLear_Click(object sender, EventArgs e)
        {
            ClearFullscreen();
        }
        private void ClearFullscreen()
        {
            MyCommon.ClearCurrentPanel(ref panel10);
            MyCommon.ClearCurrentPanel(ref panel2);
            lblGrnToala.Text = "";
            lblCurrency.Text = "";
            lblAccnumber.Text = "";
            lblTotalBillOutstanding.Text = "";
            lblExchangerate.Text = "";
            lblDueDate.Text = "";
            lblTotalVat.Text = "";
            lblTotalLKR.Text = "";
            lblTotalBillOutstanding.Text = "";
            dtpGRNDetals.Rows.Clear();

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearFullscreen();
            EditScreen(true );
            dtpBilingDate.Value = DateTime.Today;
            dtpInvoiceDate.Value = DateTime.Today;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditScreen(true);
        }
        private void EditScreen(bool status)
        {
            panel3.Enabled = status;
            panel10.Enabled = status;
            panel2.Enabled = status;
            panel8.Enabled = status;
            panel9.Enabled = status;
        }

        private void dtpBilingDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvAccount_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string itemN = e.Row.Cells["dgvAccount_LineRef"].Value.ToString();
            if (MessageBox.Show("Do you want to delelte current row ? ", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                string supid = MyCommon.GetSelectedID(cmbSupplier, true);
                string respond = "";
                int Status = MyBill.GetBillStatus(cmbGRN.Text, int.Parse(supid));
                switch (Status)
                {
                    case 0:
                        respond = MyBill.DeleteBillingDetails(int.Parse(itemN), cmbGRN.Text);
                        if (respond == "True")
                        {
                            Program.InformationMessage("Record deleted successfully");
                            CalTotalAmount();
                            e.Cancel = false;
                        }
                        break;
                    case 1:
                         respond = MyBill.DeleteBillingDetails(int.Parse(itemN), cmbGRN.Text);
                        if (respond == "True")
                        {
                            Program.InformationMessage("Record deleted successfully");
                           
                            CalTotalAmount();
                            e.Cancel = false;
                        }
                        break;
                    default:
                        Program.VerningMessage("You cannot delete this record ?");
                        e.Cancel = true;
                        break;
                }
            }
            else
                e.Cancel = false;
 
        }
    }
}
