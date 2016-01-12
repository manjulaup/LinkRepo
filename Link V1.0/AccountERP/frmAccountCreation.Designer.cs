namespace AccountERP
{
    partial class frmAccountCreation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccountCreation));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cmbBankAccountNo = new System.Windows.Forms.TextBox();
            this.cmbAccStatus = new System.Windows.Forms.ComboBox();
            this.txtAccID = new System.Windows.Forms.TextBox();
            this.cmbCurrenyType = new System.Windows.Forms.ComboBox();
            this.cmbMainAccount = new System.Windows.Forms.ComboBox();
            this.chkIsSubaccount = new System.Windows.Forms.CheckBox();
            this.cmbAccountType = new System.Windows.Forms.ComboBox();
            this.txtAccountDescription = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCLear = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.dgvAccountList = new System.Windows.Forms.DataGridView();
            this.dgvAccountList_AccountID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAccountList_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAccountList_AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAccountList_CurrenyType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAccountList_CurrentBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSearchAccType = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.HanchyGrid = new System.Windows.Forms.DataGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblFormdescription = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.chkPaybleREceiveble = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountList)).BeginInit();
            this.panel6.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HanchyGrid)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(849, 502);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tabControl1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 39);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(849, 463);
            this.panel4.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(849, 463);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Silver;
            this.tabPage1.Controls.Add(this.panel7);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.ForeColor = System.Drawing.Color.Blue;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(841, 434);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Account Description";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.chkPaybleREceiveble);
            this.panel7.Controls.Add(this.cmbBankAccountNo);
            this.panel7.Controls.Add(this.cmbAccStatus);
            this.panel7.Controls.Add(this.txtAccID);
            this.panel7.Controls.Add(this.cmbCurrenyType);
            this.panel7.Controls.Add(this.cmbMainAccount);
            this.panel7.Controls.Add(this.chkIsSubaccount);
            this.panel7.Controls.Add(this.cmbAccountType);
            this.panel7.Controls.Add(this.txtAccountDescription);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.label10);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(4, 4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(833, 379);
            this.panel7.TabIndex = 1;
            // 
            // cmbBankAccountNo
            // 
            this.cmbBankAccountNo.Location = new System.Drawing.Point(275, 228);
            this.cmbBankAccountNo.Name = "cmbBankAccountNo";
            this.cmbBankAccountNo.Size = new System.Drawing.Size(139, 22);
            this.cmbBankAccountNo.TabIndex = 27;
            // 
            // cmbAccStatus
            // 
            this.cmbAccStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccStatus.FormattingEnabled = true;
            this.cmbAccStatus.Location = new System.Drawing.Point(275, 256);
            this.cmbAccStatus.Name = "cmbAccStatus";
            this.cmbAccStatus.Size = new System.Drawing.Size(121, 24);
            this.cmbAccStatus.TabIndex = 26;
            // 
            // txtAccID
            // 
            this.txtAccID.Location = new System.Drawing.Point(275, 50);
            this.txtAccID.Name = "txtAccID";
            this.txtAccID.Size = new System.Drawing.Size(121, 22);
            this.txtAccID.TabIndex = 0;
            // 
            // cmbCurrenyType
            // 
            this.cmbCurrenyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrenyType.FormattingEnabled = true;
            this.cmbCurrenyType.Location = new System.Drawing.Point(275, 195);
            this.cmbCurrenyType.Name = "cmbCurrenyType";
            this.cmbCurrenyType.Size = new System.Drawing.Size(121, 24);
            this.cmbCurrenyType.TabIndex = 5;
            // 
            // cmbMainAccount
            // 
            this.cmbMainAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMainAccount.Enabled = false;
            this.cmbMainAccount.FormattingEnabled = true;
            this.cmbMainAccount.Location = new System.Drawing.Point(275, 134);
            this.cmbMainAccount.Name = "cmbMainAccount";
            this.cmbMainAccount.Size = new System.Drawing.Size(345, 24);
            this.cmbMainAccount.TabIndex = 3;
            this.cmbMainAccount.SelectedIndexChanged += new System.EventHandler(this.cmbMainAccount_SelectedIndexChanged);
            // 
            // chkIsSubaccount
            // 
            this.chkIsSubaccount.AutoSize = true;
            this.chkIsSubaccount.Location = new System.Drawing.Point(275, 112);
            this.chkIsSubaccount.Name = "chkIsSubaccount";
            this.chkIsSubaccount.Size = new System.Drawing.Size(15, 14);
            this.chkIsSubaccount.TabIndex = 2;
            this.chkIsSubaccount.UseVisualStyleBackColor = true;
            this.chkIsSubaccount.CheckedChanged += new System.EventHandler(this.chkIsSubaccount_CheckedChanged);
            // 
            // cmbAccountType
            // 
            this.cmbAccountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountType.FormattingEnabled = true;
            this.cmbAccountType.Location = new System.Drawing.Point(275, 78);
            this.cmbAccountType.Name = "cmbAccountType";
            this.cmbAccountType.Size = new System.Drawing.Size(175, 24);
            this.cmbAccountType.TabIndex = 1;
            this.cmbAccountType.SelectedIndexChanged += new System.EventHandler(this.cmbAccountType_SelectedIndexChanged);
            // 
            // txtAccountDescription
            // 
            this.txtAccountDescription.Location = new System.Drawing.Point(275, 166);
            this.txtAccountDescription.Name = "txtAccountDescription";
            this.txtAccountDescription.Size = new System.Drawing.Size(490, 22);
            this.txtAccountDescription.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(169, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Account ID";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Blue;
            this.label10.Location = new System.Drawing.Point(139, 259);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 16);
            this.label10.TabIndex = 11;
            this.label10.Text = "Account Status";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(97, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(160, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "Bank Account Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(145, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Currency Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(97, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Account Description";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(153, 137);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Main Account";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(105, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Is This Sub Acount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(153, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Account Type";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnCLear);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(4, 383);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(833, 47);
            this.panel2.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Gray;
            this.panel8.Controls.Add(this.btnPrint);
            this.panel8.Controls.Add(this.btnDelete);
            this.panel8.Controls.Add(this.btnUpdate);
            this.panel8.Controls.Add(this.btnSave);
            this.panel8.Location = new System.Drawing.Point(213, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(338, 39);
            this.panel8.TabIndex = 16;
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.Location = new System.Drawing.Point(169, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(83, 32);
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(250, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(83, 32);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(88, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(83, 32);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 32);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(3, 7);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(83, 32);
            this.btnNew.TabIndex = 8;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(85, 7);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(83, 32);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCLear
            // 
            this.btnCLear.Location = new System.Drawing.Point(747, 6);
            this.btnCLear.Name = "btnCLear";
            this.btnCLear.Size = new System.Drawing.Size(83, 32);
            this.btnCLear.TabIndex = 14;
            this.btnCLear.Text = "&Clear";
            this.btnCLear.UseVisualStyleBackColor = true;
            this.btnCLear.Click += new System.EventHandler(this.btnCLear_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Silver;
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(841, 434);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chart of Account";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel9);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(833, 426);
            this.panel5.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.dgvAccountList);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 38);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(833, 388);
            this.panel9.TabIndex = 2;
            // 
            // dgvAccountList
            // 
            this.dgvAccountList.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.dgvAccountList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAccountList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccountList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvAccountList_AccountID,
            this.dgvAccountList_Type,
            this.dgvAccountList_AccountName,
            this.dgvAccountList_CurrenyType,
            this.dgvAccountList_CurrentBalance});
            this.dgvAccountList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccountList.Location = new System.Drawing.Point(0, 0);
            this.dgvAccountList.Name = "dgvAccountList";
            this.dgvAccountList.RowHeadersWidth = 4;
            this.dgvAccountList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccountList.Size = new System.Drawing.Size(833, 388);
            this.dgvAccountList.TabIndex = 0;
            this.dgvAccountList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccountList_CellDoubleClick);
            // 
            // dgvAccountList_AccountID
            // 
            this.dgvAccountList_AccountID.HeaderText = "Acc ID";
            this.dgvAccountList_AccountID.Name = "dgvAccountList_AccountID";
            this.dgvAccountList_AccountID.Width = 80;
            // 
            // dgvAccountList_Type
            // 
            this.dgvAccountList_Type.HeaderText = "Type";
            this.dgvAccountList_Type.Name = "dgvAccountList_Type";
            this.dgvAccountList_Type.Width = 150;
            // 
            // dgvAccountList_AccountName
            // 
            this.dgvAccountList_AccountName.HeaderText = "Account Name";
            this.dgvAccountList_AccountName.Name = "dgvAccountList_AccountName";
            this.dgvAccountList_AccountName.Width = 250;
            // 
            // dgvAccountList_CurrenyType
            // 
            this.dgvAccountList_CurrenyType.HeaderText = "Currency";
            this.dgvAccountList_CurrenyType.Name = "dgvAccountList_CurrenyType";
            // 
            // dgvAccountList_CurrentBalance
            // 
            this.dgvAccountList_CurrentBalance.HeaderText = "Balance";
            this.dgvAccountList_CurrentBalance.Name = "dgvAccountList_CurrentBalance";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.cmbSearchAccType);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(833, 38);
            this.panel6.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Account Type";
            // 
            // cmbSearchAccType
            // 
            this.cmbSearchAccType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchAccType.FormattingEnabled = true;
            this.cmbSearchAccType.Location = new System.Drawing.Point(113, 5);
            this.cmbSearchAccType.Name = "cmbSearchAccType";
            this.cmbSearchAccType.Size = new System.Drawing.Size(297, 24);
            this.cmbSearchAccType.TabIndex = 0;
            this.cmbSearchAccType.SelectedIndexChanged += new System.EventHandler(this.cmbSearchAccType_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.HanchyGrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(841, 434);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Chart of Account By Hierarchical";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // HanchyGrid
            // 
            this.HanchyGrid.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.HanchyGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.HanchyGrid.DataMember = "";
            this.HanchyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HanchyGrid.FlatMode = true;
            this.HanchyGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.HanchyGrid.Location = new System.Drawing.Point(3, 3);
            this.HanchyGrid.Name = "HanchyGrid";
            this.HanchyGrid.ParentRowsVisible = false;
            this.HanchyGrid.PreferredColumnWidth = 150;
            this.HanchyGrid.RowHeaderWidth = 15;
            this.HanchyGrid.Size = new System.Drawing.Size(835, 428);
            this.HanchyGrid.TabIndex = 3;
            this.HanchyGrid.Navigate += new System.Windows.Forms.NavigateEventHandler(this.HanchyGrid_Navigate);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Controls.Add(this.lblFormdescription);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(849, 39);
            this.panel3.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Location = new System.Drawing.Point(810, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(35, 32);
            this.btnExit.TabIndex = 15;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblFormdescription
            // 
            this.lblFormdescription.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormdescription.Location = new System.Drawing.Point(0, 5);
            this.lblFormdescription.Name = "lblFormdescription";
            this.lblFormdescription.Size = new System.Drawing.Size(734, 29);
            this.lblFormdescription.TabIndex = 0;
            this.lblFormdescription.Text = "Create New Account";
            this.lblFormdescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(49, 286);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(208, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "Receiveble/Payble Account";
            // 
            // chkPaybleREceiveble
            // 
            this.chkPaybleREceiveble.AutoSize = true;
            this.chkPaybleREceiveble.Location = new System.Drawing.Point(275, 287);
            this.chkPaybleREceiveble.Name = "chkPaybleREceiveble";
            this.chkPaybleREceiveble.Size = new System.Drawing.Size(15, 14);
            this.chkPaybleREceiveble.TabIndex = 31;
            this.chkPaybleREceiveble.UseVisualStyleBackColor = true;
            // 
            // frmAccountCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(896, 528);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAccountCreation";
            this.Text = "frmAccountCreation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAccountCreation_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmAccountCreation_Paint);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountList)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HanchyGrid)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dgvAccountList;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCLear;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblFormdescription;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSearchAccType;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtAccID;
        private System.Windows.Forms.ComboBox cmbCurrenyType;
        private System.Windows.Forms.ComboBox cmbMainAccount;
        private System.Windows.Forms.CheckBox chkIsSubaccount;
        private System.Windows.Forms.ComboBox cmbAccountType;
        private System.Windows.Forms.TextBox txtAccountDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ComboBox cmbAccStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGrid HanchyGrid;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox cmbBankAccountNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountList_AccountID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountList_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountList_AccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountList_CurrenyType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAccountList_CurrentBalance;
        private System.Windows.Forms.CheckBox chkPaybleREceiveble;
        private System.Windows.Forms.Label label11;
    }
}