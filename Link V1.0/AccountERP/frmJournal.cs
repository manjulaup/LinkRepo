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
using BusinessLayer.Journals;
using BusinessLayer.AccountTranactions;
namespace AccountERP
{
    public partial class frmJournal : Form
    {
        private AccountCreation MyAccount = null;
        private CommonOperations MyCommon = null;
       
        private Journal MyJournal = null;
        private AccountTranaction MyTransaction = null;
        private bool IsLocalLoging = false;
        public frmJournal()
        {
            InitializeComponent();
        }

        private void frmJournal_Load(object sender, EventArgs e)
        {
            MyAccount = new AccountCreation(Program.AccountStatic.LoggingAsLocal);
            MyCommon = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
           
            MyJournal = new Journal(Program.AccountStatic.LoggingAsLocal);
           
            IsLocalLoging = Program.AccountStatic.LoggingAsLocal;
            panel1.Top = (this.Height - panel1.Height) / 2;
            panel1.Left = (this.Width - panel1.Width) / 2;
            MyCommon.LoadCurrency(cmbCurrency);
            MyCommon.LoadStatusComboAccount(cmbPayFor, 6);
            string HmCur = "", HmRate = "";
            string respond = MyCommon.GetHomeCurrencyAndExrate(out HmCur, out HmRate);
            cmbCurrency.Text = HmCur;
            MyCommon.LoadStatusComboAccount(cmbStatus, 4);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
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

        private void frmJournal_KeyUp(object sender, KeyEventArgs e)
        {

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
                lblAcCurrency.Text = MyAccount.GetCurrrencyType(lblToID.Text);
                string HmCur = "";
               // lblExrate.Text
                string Exrate= MyAccount.GetExRate(lblToID.Text, out HmCur).ToString ();
                lblAcCurrency.Text = HmCur;
                HanchyGrid.Visible = false;
            }
        }
        private void ClearUpprLine()
        {
            lblToID.Text = "";
            txtToAccount.Text = "";
            txtMemo.Text = "";
            txtDr.Text = "";
            txtCr.Text = "";
            txtVat.Text = "";
            lblLineRef.Text = "";
            lblAcCurrency.Text = "";
        }
        private string  GetNextLinNumber(string ColID)
        {
            int FinalMaxN = 1;
            List<int> Nlist = new List<int>(); 
            if (dgvJE.Rows.Count > 0)
                {
                foreach (DataGridViewRow r in dgvJE.Rows)
                    {
                        string ln = r.Cells[ColID].Value.ToString();
                        int N = 0;
                        bool resp = int.TryParse(ln, out N);
                        Nlist.Add(N); 
                    }
                FinalMaxN = MyCommon.GetMaxNumber(Nlist);
                return FinalMaxN.ToString ();
                }
            else
                return "1";
        }
        private void btnAddToList_Click(object sender, EventArgs e)
        {
            if (MyAccount.ExistAccountCreation(lblToID.Text))
            {
                int Lref = 0;
                bool res = int.TryParse(lblLineRef.Text, out Lref);

                int index1 = ExistGridLine(lblLineRef.Text);
                decimal exrate = 0;

                decimal LKRDr = 0, LKRCr = 0, USDDr = 0, USDCr = 0, Vat = 0;
                bool resp = decimal.TryParse(txtDr.Text, out USDDr);
                resp = decimal.TryParse(txtCr.Text, out USDCr);

                resp = decimal.TryParse(txtVat.Text, out Vat);

                resp = decimal.TryParse(lblExrate.Text, out exrate);
                LKRDr = USDDr * exrate;
                LKRCr = USDCr * exrate;


                if (index1 == -1)
                {
                string[] Row1 = { lblToID.Text, txtToAccount.Text, txtMemo.Text, LKRDr.ToString("##0.00"), LKRCr.ToString("##0.00"), USDDr.ToString("##0.00"), USDCr.ToString("##0.00"), Vat.ToString("##0.00"), GetNextLinNumber("dgvJE_Ref") };
                    dgvJE.Rows.Add(Row1);
                }
                else
                {
                    UpdateGridLine(index1, LKRDr, LKRCr, USDDr, USDCr, Vat);
                }

                CalTotalValue();
                ClearUpprLine();
            }
            else
            {
                Program.VerningMessage("Invalied Account");
            }
        }

        private void UpdateGridLine(int index1, decimal Debit, decimal Credit, decimal FDebit, decimal FCredit, decimal Vat)
        {
            dgvJE.Rows[index1].Cells["dgvJE_AcNo"].Value = lblToID.Text;
            dgvJE.Rows[index1].Cells["dgvJE_Name"].Value = txtToAccount.Text;
            dgvJE.Rows[index1].Cells["dgvJE_Memo"].Value = txtMemo.Text;
            dgvJE.Rows[index1].Cells["dgvJE_Dr"].Value = Debit.ToString("##0.00");
            dgvJE.Rows[index1].Cells["dgvJE_Cr"].Value = Credit.ToString("##0.00");
            dgvJE.Rows[index1].Cells["dgvJE_FDr"].Value = FDebit.ToString("##0.00");
            dgvJE.Rows[index1].Cells["dgvJE_FCr"].Value = FCredit.ToString("##0.00");

            dgvJE.Rows[index1].Cells["dgvJE_Vat"].Value = Vat.ToString("##0.00");
        }
        private void CalTotalValue()
        {
            decimal TotalLKRDr = 0, TotalLKRCr = 0, TotalUSDDr = 0, TotalUSDCr = 0, TotalVat = 0;
            foreach (DataGridViewRow r in dgvJE.Rows)
            {
                decimal LKRDr = 0, LKRCr = 0,USDDr = 0, USDCr = 0, Vat = 0;
                bool resp = decimal.TryParse(r.Cells["dgvJE_Dr"].Value.ToString(), out LKRDr);
                resp = decimal.TryParse(r.Cells["dgvJE_Cr"].Value.ToString(), out LKRCr);
                resp = decimal.TryParse(r.Cells["dgvJE_Vat"].Value.ToString(), out Vat);
                resp = decimal.TryParse(r.Cells["dgvJE_FDr"].Value.ToString(), out USDDr);
                resp = decimal.TryParse(r.Cells["dgvJE_FCr"].Value.ToString(), out USDCr);
                TotalLKRDr = TotalLKRDr + LKRDr;
                TotalLKRCr = TotalLKRCr + LKRCr;
                TotalUSDDr = TotalUSDDr + USDDr;
                TotalUSDCr = TotalUSDCr + USDCr;
                TotalVat = TotalVat + Vat;


            }
            txtToalDr.Text = TotalLKRDr.ToString("##0.00");
            txtTotalCr.Text = TotalLKRCr.ToString("##0.00");
            txtToalFdr.Text = TotalUSDDr.ToString("##0.00");
            txtToalFcr.Text = TotalUSDCr.ToString("##0.00");
            txtTotalVat.Text = TotalVat.ToString("##0.00");
        }
        private int ExistGridLine(string Lref)
        {
            if (dgvJE.Rows.Count > 0)
            {
                foreach (DataGridViewRow r in dgvJE.Rows)
                {
                    if (r.Cells["dgvJE_Ref"].Value.ToString().Trim() == Lref)
                    {
                        return r.Index;
                    }
                }
                return -1;
            }
            else
            {
                return -1;
            }
        }
        private bool ChechBothSideIsCorrectlyBalance()
        {
            decimal TotalDebt = 0, TotalCredit = 0;
                bool resp = decimal.TryParse(txtToalDr.Text, out TotalDebt);
                resp = decimal.TryParse(txtTotalCr.Text, out TotalCredit);
                if (TotalDebt == TotalCredit)
                    return true;
                else
                    return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
              
                int Jstatus = MyJournal.GetJEStatus(textJENo.Text);
                string respond = "";
                JournalType.JournalDataDataType _SaveData = new JournalType.JournalDataDataType();
                if (!ChechBothSideIsCorrectlyBalance())
                {
                    Program.VerningMessage("Total Credit And Debit Amount Miss match, pls re-check total amount");
                }
                else
                {
                    if (Jstatus != 3)
                    {
                        string JENo;
                        respond = SetDataToJournal(out _SaveData);
                        if (respond != "True")
                            Program.VerningMessage(respond);
                        else
                        {
                            respond = MyJournal.Save(_SaveData, out JENo);
                            if (respond == "True")
                            {
                                textJENo.Text = JENo;
                                Program.InformationMessage("Journal Entry saved successfully");
                            }
                            else
                                Program.VerningMessage(respond);
                        }
                    }
                    else
                    {
                        Program.VerningMessage("This journal already Accounted you cannot change");
                    }
                }
            }
        }
        private string SetDataToJournal(out JournalType.JournalDataDataType _SaveData)
        {
            _SaveData = new JournalType.JournalDataDataType();
            try
            {
                _SaveData.Jedate = dtpJEdate.Value;
                _SaveData.JeStatus = 0;
                _SaveData.JounalID = textJENo.Text;
                _SaveData.Reason = txtDescription.Text;
                _SaveData.JeUser = Program.AccountStatic.UserName;
                _SaveData.TimePeriod = Program.AccountStatic.CurrentAccPeriod;
                _SaveData.CompanyID = Program.AccountStatic.CompanyID ;
                List<JournalType.JournalDetailsDataType> _JournalList = new List<JournalType.JournalDetailsDataType>();
                string respond = SetJournalDetailList(out _JournalList);
                _SaveData.DetailList = _JournalList;
                return "True";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
        private string SetJournalDetailList(out List<JournalType.JournalDetailsDataType> _JournalList)
        {
            _JournalList = new List<JournalType.JournalDetailsDataType>();
            if (dgvJE.Rows.Count > 0)
            {
                foreach (DataGridViewRow r in dgvJE.Rows)
                {
                    JournalType.JournalDetailsDataType _OneData = new JournalType.JournalDetailsDataType();
                    decimal dr = 0, cr = 0, vat = 0;
                    int Ref1 = 0;
                    try
                    {
                        bool resp = decimal.TryParse(r.Cells["dgvJE_Dr"].Value.ToString(), out dr);
                        resp = decimal.TryParse(r.Cells["dgvJE_Cr"].Value.ToString(), out cr);
                        resp = decimal.TryParse(r.Cells["dgvJE_Vat"].Value.ToString(), out vat);
                        resp = int.TryParse(r.Cells["dgvJE_Ref"].Value.ToString(), out Ref1);
                        _OneData.AccountID = r.Cells["dgvJE_AcNo"].Value.ToString();
                        _OneData.Cr = cr;
                        _OneData.CurRate = decimal.Parse(lblExrate.Text);
                        _OneData.Description = r.Cells["dgvJE_Memo"].Value.ToString();
                        _OneData.Dr = dr;
                        _OneData.JeNumber = textJENo.Text;
                        _OneData.LineRef = Ref1;
                        _OneData.Vat = vat;
                        _JournalList.Add(_OneData);
                    }
                    catch (Exception EX)
                    {

                        return EX.Message;
                    }
                }
                return "True";

            }
            else
                return "No Data Found";
        }
        private void dgvJE_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                lblToID.Text = dgvJE.Rows[e.RowIndex].Cells["dgvJE_AcNo"].Value.ToString();
                txtToAccount.Text = dgvJE.Rows[e.RowIndex].Cells["dgvJE_Name"].Value.ToString();
                txtMemo.Text = dgvJE.Rows[e.RowIndex].Cells["dgvJE_Memo"].Value.ToString();
                txtDr.Text = dgvJE.Rows[e.RowIndex].Cells["dgvJE_FDr"].Value.ToString();
                txtCr.Text = dgvJE.Rows[e.RowIndex].Cells["dgvJE_FCr"].Value.ToString();
                txtVat.Text = dgvJE.Rows[e.RowIndex].Cells["dgvJE_Vat"].Value.ToString();
                lblLineRef.Text = dgvJE.Rows[e.RowIndex].Cells["dgvJE_Ref"].Value.ToString();
                lblAcCurrency.Text = MyAccount.GetCurrrencyType(lblToID.Text);

            }
        }

        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem cmb = new ComboboxItem();

            lblExrate.Text = cmb.GetReleventTextFromID(cmbCurrency, cmbCurrency.Text, false);
        }

        private void chkHomeCurAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHomeCurAdd.Checked)
            {
                cmbCurrency.Enabled = false;

                string HmCur = "", HmRate = "";
                string respond = MyCommon.GetHomeCurrencyAndExrate(out HmCur, out HmRate);
                if (lblAcCurrency.Text != HmCur)
                {
                    cmbCurrency.Text = HmCur;
                    lblExrate.Text = HmRate;
                }
            }
            else
            {
                cmbCurrency.Enabled = true;
                lblExrate.Text = "";
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            MyCommon.ClearCurrentPanel(ref panel3);
            MyCommon.ClearCurrentPanel(ref panel4); 
            string HmCur = "", HmRate = "";
            string respond = MyCommon.GetHomeCurrencyAndExrate(out HmCur, out HmRate);
            cmbCurrency.Text = HmCur;
            lblExrate.Text = HmRate;
            EditCurrent(true);
            lblToID.Text = "";
            lblAcCurrency.Text = "";
           
        }

        private void EditCurrent(bool status)
        {
            panel4.Enabled = status;
            panel3.Enabled = status;
        }
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Status = int.Parse(MyCommon.GetSelectedID(cmbStatus, true));
            DataTable tb = new DataTable();
            tb = MyJournal.GetJEListByStatus(Status);
            MyCommon.LoadDatatoTableWithoutBind(dgvJESearch, tb, "Load Journal Data");

        }

        private void dgvJESearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string JEn = dgvJESearch.Rows[e.RowIndex].Cells["dgvJESearch_JE"].Value.ToString();
                DataTable tb = new DataTable();
                tb = MyJournal.GetJEDetailsList(JEn);
                MyCommon.LoadDatatoTableWithoutBind(dgvJESearchDetails, tb, "Load Journal Data");
            }
        }
        private JournalType.JournalDataDataType _ExtData = new JournalType.JournalDataDataType();
        private void dgvJESearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string JEn = dgvJESearch.Rows[e.RowIndex].Cells["dgvJESearch_JE"].Value.ToString();

                string respond = MyJournal.GetExistJournalData(JEn, out _ExtData);
                if (respond == "True")
                {
                    textJENo.Text = _ExtData.JounalID;
                    dtpJEdate.Value = _ExtData.Jedate;
                    txtDescription.Text = _ExtData.Reason;
                    DataTable tb = MyJournal.GetJernulDataListForGrid(JEn);
                    MyCommon.LoadDatatoTableWithoutBind(dgvJE, tb, "");
                    CalTotalValue();
                    tabControl1.SelectTab(0);
                }
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (ChechBothSideIsCorrectlyBalance())
            {
                int Jstatus = MyJournal.GetJEStatus(textJENo.Text);
                string respond = "";
                if (MessageBox.Show("Do you want send to approval ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    switch (Jstatus)
                    {
                        case 0:
                            respond = MyJournal.SendToapproval(textJENo.Text, Program.AccountStatic.UserName);
                            if (respond == "True")
                            {
                                Program.InformationMessage("Successfully send to approval");
                            }
                            else
                            {
                                Program.VerningMessage(respond);
                            }

                            break;
                        case 1:
                            Program.VerningMessage("You have already send to approval .....");
                            break;
                        case 2:
                            Program.VerningMessage("This Journal already approved .....");
                            break;
                        case 3:
                            Program.VerningMessage("This Journal already Accounted .....");
                            break;
                        default:
                            break;
                    }
                }

            }
            else
                Program.VerningMessage("Debit balance and Credit Blance Not Match !!!!.....");
        }

        private void btnPostTpAcc_Click(object sender, EventArgs e)
        {
            MyTransaction = new AccountTranaction(Program.AccountStatic.LoggingAsLocal);
            int Jstatus = MyJournal.GetJEStatus(textJENo.Text);
            string respond = "";
            if (MessageBox.Show("Do you want send to approval ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                switch (Jstatus)
                {
                    case 0:
                        Program.VerningMessage("Not approved Journal");
                        break;
                    case 1:
                         Program.VerningMessage("This Journal still pending approval stage .....");
                        break;
                    case 2:
                        respond = MyTransaction.DoJournal(_ExtData,Program .AccountStatic.UserName  );
                        if (respond == "True")
                        {
                            Program.InformationMessage("Successfully posted to account ....");
                        }
                        else
                        {
                            Program.VerningMessage(respond);
                        }
                        break;
                    case 3:
                        Program.VerningMessage("This Journal already Accounted .....");
                        break;
                    default:
                        break;
                }
            }
           
        }

        private void btnApproved_Click(object sender, EventArgs e)
        {
            if (ChechBothSideIsCorrectlyBalance())
            {
                int Jstatus = MyJournal.GetJEStatus(textJENo.Text);
                string respond = "";
                if (MessageBox.Show("Do you want send to approval ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    switch (Jstatus)
                    {
                        case 0:
                            Program.VerningMessage("This journal not in a coorect stage");
                            break;
                        case 1:
                            respond = MyJournal.JEApproved(textJENo.Text, Program.AccountStatic.UserName);
                            if (respond == "True")
                            {
                                Program.InformationMessage("Successfully Approved");
                            }
                            else
                            {
                                Program.VerningMessage(respond);
                            }

                            break;
                        case 2:
                            Program.VerningMessage("This Journal already approved .....");
                            break;
                        case 3:
                            Program.VerningMessage("This Journal already Accounted .....");
                            break;
                        default:
                            break;
                    }
                }
            }
            else
                Program.VerningMessage("Debit balance and Credit Blance Not Match !!!!.....");
        }

      

        private void dgvJE_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCLear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                MyCommon.ClearCurrentPanel(ref panel3);
                MyCommon.ClearCurrentPanel(ref panel4); 
                
            }
        }

        private void cmbPayFor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelItem = MyCommon.GetSelectedID(cmbPayFor, true);
            cmbPayTo.Text = "";
            switch (SelItem)
            {
                case "1":
                    MyAccount.LoadSupplier(cmbPayTo);
                    txtToAccount.Enabled = false;
                    break;
                case "2":
                    MyAccount.LoadCustomer(cmbPayTo);
                    txtToAccount.Enabled = false ;
                    break;
                default:
                    cmbPayTo.Enabled = false;
                    txtToAccount.Enabled = true;
                    break;
            }
        }

        private void cmbPayTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PayToID = MyCommon.GetSelectedID(cmbPayTo, true);

            int CusSupID = 0;
            bool resp5 = int.TryParse(PayToID, out CusSupID);
            string respond = "";
            string PayTo = MyCommon.GetSelectedID(cmbPayFor, true);
            switch (PayTo)
            {
                case "1":
                    lblToID.Text = MyAccount.GetSupplierAccountNumber(CusSupID);
                    txtToAccount.Text = MyAccount.GetAccountName(lblToID.Text);
                    txtToAccount.Enabled = false;
                    HanchyGrid.Visible = false;
                    lblAcCurrency.Text = MyAccount.GetCurrrencyType(lblToID.Text);
                    cmbCurrency.Text = lblAcCurrency.Text;
                    break;
                case "2":
                    lblToID.Text = MyAccount.GetCustomerAccountNumber(CusSupID);
                    txtToAccount.Text = MyAccount.GetAccountName(lblToID.Text);
                    lblAcCurrency.Text = MyAccount.GetCurrrencyType(lblToID.Text);
                    txtToAccount.Enabled = false;
                    HanchyGrid.Visible = false;

                    cmbCurrency.Text = lblAcCurrency.Text;
                    break;
               
                default:
                    lblToID.Text = "";
                    txtToAccount.Text = "";
                    txtToAccount.Enabled = true;
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditCurrent(true);
        }

        private void dgvJE_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string itemN = e.Row.Cells["dgvJE_Ref"].Value.ToString();
            if (MessageBox.Show("Do you want to delelte current row ? ", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                int JeStatus = MyJournal.GetJEStatus(textJENo.Text);
                if (JeStatus != 3)
                {
                    string respond=MyJournal .DeleteJournalDetails(textJENo.Text ,int .Parse (itemN ));
                    if (respond == "True")
                    {
                        MyJournal.SetJeStatus(textJENo.Text);
                        CalTotalValue();

                    }
                }
                else
                {
                    Program.VerningMessage("You cannot delete this record, I t is already Accounted");
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MyJournal.ExistJournalData(textJENo.Text))
            {
                if (MessageBox.Show("Do you want to save ? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    int Jstatus = MyJournal.GetJEStatus(textJENo.Text);
                    string respond = "";
                    JournalType.JournalDataDataType _SaveData = new JournalType.JournalDataDataType();
                    if (!ChechBothSideIsCorrectlyBalance())
                    {
                        Program.VerningMessage("Total Credit And Debit Amount Miss match, pls re-check total amount");
                    }
                    else
                    {
                        if (Jstatus != 3)
                        {
                            
                            respond = SetDataToJournal(out _SaveData);
                            if (respond != "True")
                                Program.VerningMessage(respond);
                            else
                            {
                                respond = MyJournal.Update(_SaveData);
                                if (respond == "True")
                                {
                                    
                                    Program.InformationMessage("Journal Entry saved successfully");
                                }
                                else
                                    Program.VerningMessage(respond);
                            }
                        }
                        else
                        {
                            Program.VerningMessage("This journal already Accounted you cannot change");
                        }
                    }
                }
            }
           
        }
    }
}
