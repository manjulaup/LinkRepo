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
using BusinessLayer.Supplier;
using BusinessLayer.AccountTranactions;
using BusinessLayer.Receipts ;
namespace AccountERP
{
    public partial class frmReceiptVoucher : Form
    {
        private AccountCreation MyAccount = null;
        private CommonOperations MyCommon = null;
        private Receipt MyReceipt = null; 
        public frmReceiptVoucher()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (panel14.Visible == true)
                panel14.Visible = false;
            else
                panel14.Visible = true;


            try
                {
                string PayToID = MyCommon.GetSelectedID(cmbPayTo, true);
                int CusSupID = int.Parse(PayToID);
                DataTable tb = MyReceipt.LoadPendingReceivebleList(CusSupID);
                MyCommon.LoadDatatoTableWithoutBind(dgvBillList, tb, "Load Peng bill");
                }
            catch (Exception ex)
                {


                }

        }
        private void LoadToCombo()
        {
            MyAccount.GetAccountListByCat(Program.AccountStatic.CompanyID, cmbFromAcc, 1);
            MyCommon.LoadStatusComboAccount(cmbPayMethod, 2);
            MyCommon.LoadStatusComboAccount(cmbPayFor, 7);
            MyCommon.LoadStatusComboAccount(cmbStatus, 4);
           EnableCurrent(false);
         LoadExtPaymentList();
        }
        private void EnableCurrent(bool status)
            {
            panel2.Enabled = status;
            panel3.Enabled = status;
            panel10.Enabled = status;
            }
        private void frmReceiptVoucher_Load(object sender, EventArgs e)
        {
            panel1.Top = (this.Height - panel1.Height) / 2;
            panel1.Left = (this.Width - panel1.Width) / 2;
            MyAccount = new AccountCreation(Program.AccountStatic.LoggingAsLocal);
            MyCommon = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
            MyReceipt = new Receipt(Program.AccountStatic.LoggingAsLocal);
            LoadToCombo();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmbFromAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GetFromAccID = "";
            GetFromAccID = MyCommon.GetSelectedID(cmbFromAcc, true);
            string Currentcy = "";
            txtExRate.Text = MyAccount.GetExRate(GetFromAccID, out Currentcy).ToString();
            lblCurrentcy.Text = Currentcy;
            decimal FinalBalance = 0;
            string rspt = MyAccount.GetFinalBalance(GetFromAccID, Program.AccountStatic.CurrentAccPeriod, out FinalBalance);
            lblBalance.Text = FinalBalance.ToString("##0.00");
            if (GetFromAccID == "10000")
            {
                ComboboxItem cmb = new ComboboxItem();
                cmbPayMethod.Text = cmb.GetReleventTextFromID(cmbPayMethod, "1", true);
                cmbPayMethod.Enabled = false;

            }
            else
                cmbPayMethod.Enabled = true;
        }
        private void LoadExtAccountInHiaraky(string KeyWord)
        {

            DataTable tb = MyAccount.GetAccountListForDDL(KeyWord, Program.AccountStatic.CompanyID, 1, true);
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
        private void cmbPayFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelItem = MyCommon.GetSelectedID(cmbPayFor, true);
            cmbPayTo.Text = "";
            switch (SelItem)
            {
                case "1":
                    btnBrowse.Enabled = true;
                    MyAccount.LoadSupplier(cmbPayTo);
                    txtToAccount.Enabled = false;
                    break;
                case "2":
                    MyAccount.LoadCustomer(cmbPayTo);
                    btnBrowse.Enabled = true;
                    txtToAccount.Enabled = false ;
                    break;
                case "3":
                    MyAccount.LoadEmployee(cmbPayTo);
                    btnBrowse.Enabled = false;
                    txtToAccount.Enabled = true;
                    break;
                case "4":
                    MyAccount.LoadSubContractors(cmbPayTo);
                    btnBrowse.Enabled = false;
                    txtToAccount.Enabled = true;
                    break;
                case "5":
                    MyAccount.LoadOther(cmbPayTo);
                    btnBrowse.Enabled = false;
                    txtToAccount.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void cmbPayTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Supplier MySupplier = new Supplier();
            DataType.CustomerDataType _ExCustomer = new DataType.CustomerDataType();
            DataType.SupplierDataType _ExSupplier = new DataType.SupplierDataType();
            Customer MyCustomer = new Customer();
            string PayTo = MyCommon.GetSelectedID(cmbPayFor, true);
            string PayToID = MyCommon.GetSelectedID(cmbPayTo, true);

            int CusSupID = 0;
            bool resp5 = int.TryParse(PayToID, out CusSupID);
            string respond = "";
            switch (PayTo)
            {
                case "1":
                    respond = MySupplier.GetSupplier(CusSupID, out _ExSupplier, Program.AccountStatic.LoggingAsLocal);
                    lblAddress.Text = _ExSupplier.Address1 + " " + _ExSupplier.Address2 + " " + _ExSupplier.Address3;
                    lblToID.Text = MyAccount.GetSupplierAccountNumber(CusSupID);
                    txtToAccount.Text = MyAccount.GetAccountName(lblToID.Text);
                    txtToAccount.Enabled = false;
                    HanchyGrid.Visible = false;
                    break;
                case "2":
                    lblToID.Text = MyAccount.GetCustomerAccountNumber(CusSupID);
                    respond = MyCustomer.GetGetExistCustomer(CusSupID, out _ExCustomer, Program.AccountStatic.LoggingAsLocal);
                    lblAddress.Text = _ExCustomer.Address1 + " " + _ExCustomer.Address2 + " " + _ExCustomer.Address3;
                    txtToAccount.Text = MyAccount.GetAccountName(lblToID.Text);
                    txtToAccount.Enabled = false;
                    HanchyGrid.Visible = false;
                    break;
                case "5":
                    lblToID.Text = "";
                    txtToAccount.Text = "";
                    txtToAccount.Enabled = true;
                    lblAddress.Text = MyAccount.GetOtherPayeeAdd(cmbPayTo.Text);
                    break;
                default:
                    lblToID.Text = "";
                    txtToAccount.Text = "";
                    txtToAccount.Enabled = true;
                    break;
            }
        }
        private void EditScreen(bool Status)
        {
            panel10.Enabled = Status;
            panel2.Enabled = Status;
            panel3.Enabled = Status;
            panel7.Enabled = Status; 
        }
        private void ClearCurrent()
        {
            MyCommon.ClearCurrentPanel(ref panel2);
            MyCommon.ClearCurrentPanel(ref panel3);
            MyCommon.ClearCurrentPanel(ref panel10);
            lblChkAmount.Text = "";
            lblToID.Text = "";

        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

            EditScreen(true);

        }

        private void btnCLear_Click(object sender, EventArgs e)
        {
            ClearCurrent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            EditScreen(true);
            ClearCurrent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
            {
            if (MyAccount.ExistAccountCreation(lblToID.Text))
                {
                string AccNo = lblToID.Text;

                int Lref = -1;
                bool resp = int.TryParse(lblLRef.Text, out Lref);
                if (!resp)
                {
                    Lref = -1;
                }
                else
                {
                    Lref = ExistTable(lblLRef.Text);
                }
                decimal LKR = 0;
                decimal USD1 = 0;
                decimal ExRate = 0;
                decimal VAT = 0;
                bool rsp = decimal.TryParse(txtAmount.Text, out USD1);
                rsp = decimal.TryParse(txtExRate.Text, out ExRate);
                rsp = decimal.TryParse(txtVat.Text, out VAT);
                LKR = USD1 * ExRate;

                if (Lref !=-1)
                {
                    dgvAccList.Rows[Lref].Cells[2].Value = txtToMemo.Text;
                    dgvAccList.Rows[Lref].Cells[3].Value = txtJobNumber.Text;
                    dgvAccList.Rows[Lref].Cells[4].Value = VAT.ToString("##0.00");
                    dgvAccList.Rows[Lref].Cells[5].Value = LKR.ToString("##0.00");
                    dgvAccList.Rows[Lref].Cells[6].Value = USD1.ToString("##0.00");
                    dgvAccList.Rows[Lref].Cells[7].Value = ExRate.ToString("##0.00");
                }
                else
                {
                    string[] Oneline = { AccNo, txtToAccount.Text, txtToMemo.Text, txtJobNumber.Text, VAT.ToString("##0.00"), LKR.ToString("#00.00"), USD1.ToString("#00.00"),ExRate.ToString ("##0.00"), GetNextLinNumber("dgvAccList_Ref") };
                    dgvAccList.Rows.Add(Oneline);
                   
                }
                CalTotalLine();
                ClearUpperLine();
            }
            else
            {
                Program.VerningMessage("Invalied Account Number");
 
            }
            }
        private string GetNextLinNumber(string ColID)
            {
            int FinalMaxN = 1;
            List<int> Nlist = new List<int>();
            if (dgvAccList.Rows.Count > 0)
                {
                foreach (DataGridViewRow r in dgvAccList.Rows)
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
        private void CalTotalLine()
            {
            decimal Vat;
            decimal Amount, FCR = 0;
            Vat = 0;
            Amount = 0;

            foreach (DataGridViewRow item in dgvAccList.Rows)
                {
                decimal Vat1 = 0, Amount1 = 0, FCR1 = 0;
                bool resp = decimal.TryParse(item.Cells["dgvAccList_Amount"].Value.ToString(), out Amount1);
                resp = decimal.TryParse(item.Cells["dgvAccList_Vat"].Value.ToString(), out Vat1);
                resp = decimal.TryParse(item.Cells["dgvAccList_Fcur"].Value.ToString(), out FCR1);
                FCR = FCR + FCR1;
                Vat = Vat + Vat1;
                Amount = Amount + Amount1;
                }
            txtTotalvat.Text = Vat.ToString("0.00");
            txtToalAmountLKR.Text = Amount.ToString("0.00");
            txtToalAmountFCR.Text = FCR.ToString("0.00");
            }
        private void ClearUpperLine()
            {
                txtToAccount.Text = "";
                lblLRef.Text = "";
                lblToID.Text = "";
                txtToMemo.Text = "";
                txtJobNumber.Text = "";
                txtVat.Text = "";
                txtAmount.Text = "";
            }

        private void btnAddToList_Click(object sender, EventArgs e)
            {
            string PayToID = MyCommon.GetSelectedID(cmbPayTo, true);


            decimal fullamount = 0, ExchageBalanceAmountLKR=0;
            bool resp = decimal.TryParse(txttotalAmount.Text, out fullamount);

            foreach (DataGridViewRow r in dgvBillList.Rows)
                {
                string curreny1 = MyAccount.GetAccountCurrenyType(r.Cells["dgvBillList_AccID"].Value.ToString());
                if (curreny1 == lblCurrentcy.Text.Trim())
                    {

                    if (chkAutoFill.Checked)
                        {
                        decimal onelineamount = 0;
                        resp = decimal.TryParse(r.Cells["dgvBillList_Cr"].Value.ToString(), out onelineamount);
                        decimal remain = fullamount - onelineamount;

                        

                        if ((fullamount > onelineamount) || (fullamount == onelineamount))
                            {
                            r.Cells["dgvBillList_CurPayment"].Value = r.Cells["dgvBillList_Cr"].Value.ToString();
                            r.Cells["dgvBillList_Select"].Value = "1";
                            fullamount = remain;
                            }
                        else
                            {
                            r.Cells["dgvBillList_CurPayment"].Value = fullamount.ToString("##0.00");
                            r.Cells["dgvBillList_Select"].Value = "1";
                            chkAutoFill.Checked = false;
                            }
                        }
                    if (r.Cells["dgvBillList_Select"].Value.ToString() == "1")
                        {
                        int Lref = 0;
                        bool res = int.TryParse(lblLRef.Text, out Lref);
                        if (Lref == 0)
                            {
                            Lref = dgvAccList.RowCount + 1;
                            }
                        decimal LKR = 0;
                        decimal USD1 = 0;
                        decimal ExRate = 0;

                        bool rsp = decimal.TryParse(r.Cells["dgvBillList_CurPayment"].Value.ToString(), out USD1);
                        rsp = decimal.TryParse(txtExRate.Text, out ExRate);
                        decimal payiedExrate = 0, DiffrenceRate=0;
                        rsp = decimal.TryParse(r.Cells["dgvBillList_Exrate"].Value.ToString(), out payiedExrate);
                       
                        int Index1 = ExistAutoTable(r.Cells["dgvBillList_Bill"].Value.ToString());

                        LKR = USD1 * payiedExrate;

                        if (payiedExrate != ExRate)
                        {
                            DiffrenceRate = ExRate - payiedExrate;
                            ExchageBalanceAmountLKR = ExchageBalanceAmountLKR + (DiffrenceRate * USD1);
                            ExRate = payiedExrate;
                        }


                        if (Index1 !=-1)
                            {
                                dgvAccList.Rows[Index1].Cells[2].Value = "Payment of " + r.Cells["dgvBillList_Bill"].Value.ToString();
                                dgvAccList.Rows[Index1].Cells[3].Value = r.Cells["dgvBillList_Bill"].Value.ToString();
                                dgvAccList.Rows[Index1].Cells[4].Value = "";
                                dgvAccList.Rows[Index1].Cells[5].Value = LKR.ToString("##0.00");
                                dgvAccList.Rows[Index1].Cells[6].Value = USD1.ToString("##0.00");
                            }
                        else
                            {
                            string AcName = MyAccount.GetAccountName(r.Cells["dgvBillList_AccID"].Value.ToString());
                            string[] Oneline = { r.Cells["dgvBillList_AccID"].Value.ToString(), AcName, "Payment of " + r.Cells["dgvBillList_Bill"].Value.ToString(), r.Cells["dgvBillList_Bill"].Value.ToString(), "", LKR.ToString("##0.00"), USD1.ToString("##0.00"),payiedExrate.ToString ("##0.00"), GetNextLinNumber("dgvAccList_Ref") };
                            dgvAccList.Rows.Add(Oneline);
                            }

                        }
                    }
                else
                    {
                    Program.VerningMessage("Currency type miss match");
                    }
                }
            if (ExchageBalanceAmountLKR > 0)
            {
                string GetExGainID = MyAccount.GetExchangeGainAccount();
                string AcName = MyAccount.GetAccountName(GetExGainID);
                string[] Oneline = { GetExGainID, AcName, "Exchange Gain ", "", "", ExchageBalanceAmountLKR.ToString("##0.00"), "0.00","1", GetNextLinNumber("dgvAccList_Ref") };
                dgvAccList.Rows.Add(Oneline);
            }
            else
            {
                ExchageBalanceAmountLKR = ExchageBalanceAmountLKR * -1;
                string GetExLossID = MyAccount.GetExchangeLossAccount();
                string AcName = MyAccount.GetAccountName(GetExLossID);
                string[] Oneline = { GetExLossID, AcName, "Exchange Loss ", "", "", ExchageBalanceAmountLKR.ToString("##0.00"), "0.00","1", GetNextLinNumber("dgvAccList_Ref") };
                dgvAccList.Rows.Add(Oneline);
            }
            CalTotalLine();
            panel14.Visible = false;
            }
        private int ExistAutoTable(string BillNo)
        {
            foreach (DataGridViewRow r in dgvAccList.Rows)
            {
                if (r.Cells["dgvAccList_Job"].Value.ToString() == BillNo)
                    return r.Index;
            }
            return -1;
        }
        private int ExistTable(string index)
        {
            foreach (DataGridViewRow r in dgvAccList.Rows)
            {
                if (r.Cells["dgvAccList_Ref"].Value.ToString() == index)
                    return r.Index;
            }
            return -1;
        }
        private string SetHeaderDatatoClass(out REceiptTypes.ReceiptDataType   _SaveHeader)
            {
            _SaveHeader = new REceiptTypes.ReceiptDataType();
            try
                {
                _SaveHeader.AccountID = MyCommon.GetSelectedID(cmbFromAcc, true);
                _SaveHeader.ChequeNumber = cmbMethodObg.Text;
                _SaveHeader.Dr = decimal.Parse(txtToalAmountLKR.Text);
                _SaveHeader.CurRate = decimal.Parse(txtExRate.Text);
                _SaveHeader.Description = txtfromMemo.Text;
                _SaveHeader.FDr = decimal.Parse(txtToalAmountFCR.Text);
                _SaveHeader.ReceiptID  = txtRcptNumber.Text;
                _SaveHeader.ReceiptMethod  = int.Parse(MyCommon.GetSelectedID(cmbPayMethod, true));
                _SaveHeader.ReceiptStatus  = 0;
                _SaveHeader.TrUser = Program.AccountStatic.UserName;
                _SaveHeader.CompanyID = Program.AccountStatic.CompanyID;
                _SaveHeader.AccPeriod = Program.AccountStatic.CurrentAccPeriod;
                _SaveHeader.RcptFromCatID  = int.Parse(MyCommon.GetSelectedID(cmbPayFor, true));
                _SaveHeader.RcptFromName  = cmbPayTo.Text;
                _SaveHeader.RcptActualDate  = dtpPVNDate.Value;
                List<REceiptTypes.ReceiptDetailsDataType> _SaveDetailList;
                string respond = SetDetailDataToClass(out _SaveDetailList);
                _SaveHeader.ReceiptList = _SaveDetailList;
                return "True";

                }
            catch (Exception ex)
                {

                return ex.Message;
                }
            }
        private string SetDetailDataToClass(out List<REceiptTypes.ReceiptDetailsDataType> _SaveDetailList)
            {
           // MyPay = new Payment(Program.AccountStatic.LoggingAsLocal);
            _SaveDetailList = new List<REceiptTypes.ReceiptDetailsDataType>();   
            try
                {
                foreach (DataGridViewRow OneRow in dgvAccList.Rows)
                    {
                    REceiptTypes.ReceiptDetailsDataType _OneItem = new REceiptTypes.ReceiptDetailsDataType();
                    _OneItem.AccID = OneRow.Cells["dgvAccList_AccountID"].Value.ToString();
                    _OneItem.Description = OneRow.Cells["dgvAccList_Memo"].Value.ToString();
                    bool resp = false;
                    decimal Cr = 0, FCr = 0, Vat = 0, Lexrare=0;
                    resp = decimal.TryParse(OneRow.Cells["dgvAccList_Amount"].Value.ToString(), out Cr);
                    resp = decimal.TryParse(OneRow.Cells["dgvAccList_Fcur"].Value.ToString(), out FCr);
                    resp = decimal.TryParse(OneRow.Cells["dgvAccList_Vat"].Value.ToString(), out Vat);
                    resp = decimal.TryParse(OneRow.Cells["dgvAccList__ExcRate"].Value.ToString(), out Lexrare);
                    _OneItem.Cr = Cr;
                    _OneItem.FCr = FCr;
                    _OneItem.ItemNo = int.Parse(OneRow.Cells["dgvAccList_Ref"].Value.ToString());
                    _OneItem.RcptNo  = txtRcptNumber.Text;
                    _OneItem.Vat = Vat;
                    _OneItem.JobNo = OneRow.Cells["dgvAccList_Job"].Value.ToString();
                    _OneItem.Exrate = Lexrare;
                    _SaveDetailList.Add(_OneItem);
                    }
                return "True";
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        private void LoadExtPaymentList()
            {
            DataTable tb = MyReceipt.GetSeachTable(-1);
            MyCommon.LoadDatatoTableWithoutBind(dgvPaymentList, tb, "Load Data List");

            }
        private void btnSave_Click(object sender, EventArgs e)
            {
            if (MessageBox.Show("Do you want to save current record ? ", "Confirmation ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                int VoucherStatus = MyReceipt.GetReceiptStatus(txtRcptNumber.Text);
                switch (VoucherStatus)
                    {

                    case 2:
                        Program.InformationMessage("This is aproval payment voucher, cannot chage");
                        break;
                    case 3:
                        Program.InformationMessage("Already Accounted, cannot chage");
                        break;
                    default:
                        if (!MyReceipt.ExistReceipt(txtRcptNumber.Text))
                            {
                            REceiptTypes.ReceiptDataType _SaveHeader = new REceiptTypes.ReceiptDataType();
                            string respond = "";
                            string Rcpt = "";
                            string SelItem = MyCommon.GetSelectedID(cmbPayFor, true);
                            if (SelItem == "5")
                                {
                                MyAccount.SaveotherPayee(cmbPayTo.Text, lblAddress.Text);
                                }
                            respond = SetHeaderDatatoClass(out _SaveHeader);
                            if (respond == "True")
                                {

                                respond = MyReceipt.Save (_SaveHeader, out Rcpt);
                                if (respond == "True")
                                    {
                                    txtRcptNumber.Text = Rcpt;
                                    LoadExtPaymentList();
                                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                else
                                    {
                                    txtRcptNumber.Text = "";
                                    Program.VerningMessage(respond);

                                    }
                                }

                            }
                        else
                            {
                            Program.VerningMessage("Use Update Button");
                            }
                        break;
                    }
                }
            }

        private void btnUpdate_Click(object sender, EventArgs e)
            {
            if (MessageBox.Show("Do you want to Update current record ? ", "Confirmation ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                int VoucherStatus = MyReceipt.GetReceiptStatus(txtRcptNumber.Text);
                switch (VoucherStatus)
                    {

                     case 3:
                        Program.InformationMessage("Already Accounted, cannot chage");
                        break;
                    default:
                        if (MyReceipt.ExistReceipt(txtRcptNumber.Text))
                            {
                            REceiptTypes.ReceiptDataType _SaveHeader = new REceiptTypes.ReceiptDataType();
                            string respond = "";
                            string Rcpt = "";
                            string SelItem = MyCommon.GetSelectedID(cmbPayFor, true);
                            if (SelItem == "5")
                                {
                                MyAccount.SaveotherPayee(cmbPayTo.Text, lblAddress.Text);
                                }
                            respond = SetHeaderDatatoClass(out _SaveHeader);
                            if (respond == "True")
                                {

                                respond = MyReceipt.Update (_SaveHeader);
                                if (respond == "True")
                                    {
                                  
                                    LoadExtPaymentList();
                                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                else
                                    {
                                    txtRcptNumber.Text = "";
                                    Program.VerningMessage(respond);

                                    }
                                }

                            }
                        else
                            {
                            Program.VerningMessage("Use Update Button");
                            }
                        break;
                    }
                }
            }

        private void btnPost_Click(object sender, EventArgs e)
            {
            if (MessageBox.Show("Do you want send to Approval ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                string respond = "";
                int VoucherStatus = MyReceipt.GetReceiptStatus(txtRcptNumber.Text);
                switch (VoucherStatus)
                    {
                    case 0:
                        respond = MyReceipt.SendToApproval(txtRcptNumber.Text, Program.AccountStatic.UserName);
                        if (respond == "True")
                            Program.InformationMessage("Successfully Send to Approval");
                        else
                            Program.VerningMessage(respond);
                        break;
                    case 1:
                        Program.VerningMessage("Already Approved!!! you cannot change ......");
                        break;
                    case 2:
                        Program.VerningMessage("Already Approved!!! you cannot change ......");
                        break;
                    case 3:
                        Program.VerningMessage("Already Accounted!!! you cannot change ......");
                        break;
                    default:
                        break;
                    }
                } 
            }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
            {
            string stid = MyCommon.GetSelectedID(cmbStatus, true);
            DataTable tb = MyReceipt.GetSeachTable (int.Parse(stid));
            MyCommon.LoadDatatoTableWithoutBind(dgvPaymentList, tb, "Load DAta List");
            }

        private void dgvPaymentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
            {
            if (e.RowIndex != -1)
                {
                string RcptID = dgvPaymentList.Rows[e.RowIndex].Cells["dgvPaymentList_PVN"].Value.ToString();
                DisplayExtDetails(RcptID);
                }
            }
        private REceiptTypes.ReceiptDataType _ExtData = new REceiptTypes.ReceiptDataType();  
        private void DisplayExtDetails(string RcptNumber)
            {

            string respond = "";
            respond = MyReceipt.GetExistReceipt(RcptNumber, out _ExtData);
            ComboboxItem cmb = new ComboboxItem();
            if (respond == "True")
                {
                txtRcptNumber.Text = _ExtData.ReceiptID;
                cmbFromAcc.Text = cmb.GetReleventTextFromID(cmbFromAcc, _ExtData.AccountID, true);
                cmbPayFor.Text = cmb.GetReleventTextFromID(cmbPayFor, _ExtData.RcptFromCatID.ToString(), true);
                cmbPayMethod.Text = cmb.GetReleventTextFromID(cmbPayMethod, _ExtData.ReceiptMethod.ToString(), true);
                cmbPayTo.Text = _ExtData.RcptFromName;
                cmbPayTo_SelectedIndexChanged(null, null);
                txtfromMemo.Text = _ExtData.Description;
                dtpPVNDate.Value = _ExtData.RcptActualDate;
                DataTable tb = MyReceipt.GetReceiptDetailsList(RcptNumber);
                MyCommon.LoadDatatoTableWithoutBind(dgvAccList, tb, "Load Payment list");
                CalTotalLine();
                }
            tabControl1.SelectTab(0);
            }

        private void btnApproved_Click(object sender, EventArgs e)
            {
            string PayFor = "";
            if (MessageBox.Show("Do you want approved this payment ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                string respond = "";
                int VoucherStatus = MyReceipt.GetReceiptStatus(txtRcptNumber.Text);
                switch (VoucherStatus)
                    {
                    case 0:
                        Program.VerningMessage("This is not in approval stage !!!......");
                        break;
                    case 1:
                        respond = MyReceipt.SetReceiptVoucherAsApproved(txtRcptNumber.Text, Program.AccountStatic.UserName);
                        if (respond == "True")
                            {
                            PayFor = MyCommon.GetSelectedID(cmbPayFor, true);
                            string PayToID = MyCommon.GetSelectedID(cmbPayTo, true);

                            if (PayFor == "2")
                                {
                                foreach (DataGridViewRow r in dgvAccList.Rows)
                                    {
                                    decimal amount = 0;
                                    bool redtp = decimal.TryParse(r.Cells["dgvAccList_Fcur"].Value.ToString(), out amount);
                                    string reply = MyReceipt.UpdateReceivedAmount(int.Parse(PayToID), r.Cells["dgvAccList_Job"].Value.ToString(), amount);
                                    }

                                }
                            Program.InformationMessage("Successfully Approved !!!!");
                            }
                        else
                            Program.VerningMessage(respond);
                        break;
                    case 2:
                        Program.VerningMessage("Already Approved !!!......");
                        break;
                    case 3:
                        Program.VerningMessage("Already Accounted!!! you cannot change ......");
                        break;
                    default:
                        break;
                    }
                } 
            }

        private void btnPostTpAcc_Click(object sender, EventArgs e)
            {
            string respond = "";
            if (MessageBox.Show("Do you want post this to account ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                AccountTranaction MyTransaction = new AccountTranaction(Program.AccountStatic.LoggingAsLocal);
                int VoucherStatus = MyReceipt.GetReceiptStatus(txtRcptNumber.Text);
                switch (VoucherStatus)
                    {
                    case 0:
                        Program.VerningMessage("This is not Approved Voucher !!!......");
                        break;
                    case 1:
                        Program.VerningMessage("This is not Approved Voucher !!!......");
                        break;
                    case 2:
                        string PVN = "";
                        respond = MyTransaction.DoReceipts(_ExtData, Program.AccountStatic.UserName, out PVN);
                        if (respond == "True")
                            {
                            txtRcptNumber.Text = PVN;
                            Program.InformationMessage("Successfully Posted to Account !!!!");
                            }
                        else
                            {
                            Program.VerningMessage(respond);
                            }
                        break;
                    case 3:
                        Program.VerningMessage("Already Accounted!!! you cannot change ......");
                        break;
                    default:
                        break;
                    }
                } 
            }

        private void dgvBillList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                panel14.Visible = false;
        }

        private void txtToAccount_KeyDown(object sender, KeyEventArgs e)
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

        private void dgvAccList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ComboboxItem cmb = new ComboboxItem();

            if (e.RowIndex != -1)
            {
                lblLRef.Text = dgvAccList.Rows[e.RowIndex].Cells["dgvAccList_Ref"].Value.ToString();
                lblToID.Text = dgvAccList.Rows[e.RowIndex].Cells["dgvAccList_AccountID"].Value.ToString();
                txtToAccount.Text = MyAccount.GetAccountName(lblToID.Text);
                txtToMemo.Text = dgvAccList.Rows[e.RowIndex].Cells["dgvAccList_Memo"].Value.ToString();
                txtJobNumber.Text = dgvAccList.Rows[e.RowIndex].Cells["dgvAccList_Job"].Value.ToString();
                txtVat.Text = dgvAccList.Rows[e.RowIndex].Cells["dgvAccList_Vat"].Value.ToString();
                txtAmount.Text = dgvAccList.Rows[e.RowIndex].Cells["dgvAccList_Fcur"].Value.ToString();
            }
        }
    }
}
