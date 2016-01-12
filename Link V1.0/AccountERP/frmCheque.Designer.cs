namespace AccountERP
{
    partial class frmCheque
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheque));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCLear = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbquenumbers = new System.Windows.Forms.ComboBox();
            this.txtAnyremarks = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.chkReconciliated = new System.Windows.Forms.CheckBox();
            this.chkReturn = new System.Windows.Forms.CheckBox();
            this.cmbstatus = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbBankAccNumber = new System.Windows.Forms.ComboBox();
            this.dtpRealized = new System.Windows.Forms.DateTimePicker();
            this.dtpWrite = new System.Windows.Forms.DateTimePicker();
            this.cmbToFrom = new System.Windows.Forms.ComboBox();
            this.rbtReceive = new System.Windows.Forms.RadioButton();
            this.rbtwrite = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.dgvChequeList = new System.Windows.Forms.DataGridView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel10 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.dtpChequeCancelDate = new System.Windows.Forms.DateTimePicker();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.chkChequeBook = new System.Windows.Forms.DataGridView();
            this.chkChequeBook_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkChequeBook_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtNofCheque = new System.Windows.Forms.TextBox();
            this.txtStChequeNumber = new System.Windows.Forms.TextBox();
            this.lblCancellChnumber = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbBankAccountNumber = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChequeList)).BeginInit();
            this.panel7.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkChequeBook)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(796, 559);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 41);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(796, 518);
            this.panel3.TabIndex = 1;
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
            this.tabControl1.Size = new System.Drawing.Size(796, 518);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tabPage1.Controls.Add(this.panel5);
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(788, 489);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cheque Details";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel5.Controls.Add(this.btnNew);
            this.panel5.Controls.Add(this.btnEdit);
            this.panel5.Controls.Add(this.btnCLear);
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 411);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(780, 74);
            this.panel5.TabIndex = 1;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(15, 17);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(83, 32);
            this.btnNew.TabIndex = 18;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(97, 17);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(83, 32);
            this.btnEdit.TabIndex = 19;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnCLear
            // 
            this.btnCLear.Location = new System.Drawing.Point(694, 14);
            this.btnCLear.Name = "btnCLear";
            this.btnCLear.Size = new System.Drawing.Size(83, 32);
            this.btnCLear.TabIndex = 20;
            this.btnCLear.Text = "&Clear";
            this.btnCLear.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Gray;
            this.panel8.Controls.Add(this.btnPrint);
            this.panel8.Controls.Add(this.btnDelete);
            this.panel8.Controls.Add(this.btnUpdate);
            this.panel8.Controls.Add(this.btnSave);
            this.panel8.Location = new System.Drawing.Point(221, 14);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(338, 39);
            this.panel8.TabIndex = 17;
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
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(88, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(83, 32);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
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
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel4.Controls.Add(this.cmbquenumbers);
            this.panel4.Controls.Add(this.txtAnyremarks);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.cmbstatus);
            this.panel4.Controls.Add(this.txtAmount);
            this.panel4.Controls.Add(this.txtName);
            this.panel4.Controls.Add(this.cmbBankAccNumber);
            this.panel4.Controls.Add(this.dtpRealized);
            this.panel4.Controls.Add(this.dtpWrite);
            this.panel4.Controls.Add(this.cmbToFrom);
            this.panel4.Controls.Add(this.rbtReceive);
            this.panel4.Controls.Add(this.rbtwrite);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.ForeColor = System.Drawing.Color.Blue;
            this.panel4.Location = new System.Drawing.Point(4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(780, 407);
            this.panel4.TabIndex = 0;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // cmbquenumbers
            // 
            this.cmbquenumbers.FormattingEnabled = true;
            this.cmbquenumbers.Location = new System.Drawing.Point(253, 72);
            this.cmbquenumbers.Name = "cmbquenumbers";
            this.cmbquenumbers.Size = new System.Drawing.Size(155, 24);
            this.cmbquenumbers.TabIndex = 14;
            // 
            // txtAnyremarks
            // 
            this.txtAnyremarks.Location = new System.Drawing.Point(253, 250);
            this.txtAnyremarks.Name = "txtAnyremarks";
            this.txtAnyremarks.Size = new System.Drawing.Size(476, 22);
            this.txtAnyremarks.TabIndex = 13;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Silver;
            this.panel6.Controls.Add(this.dtpReturnDate);
            this.panel6.Controls.Add(this.chkReconciliated);
            this.panel6.Controls.Add(this.chkReturn);
            this.panel6.Location = new System.Drawing.Point(52, 308);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(677, 61);
            this.panel6.TabIndex = 12;
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpReturnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReturnDate.Location = new System.Drawing.Point(94, 17);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(130, 22);
            this.dtpReturnDate.TabIndex = 2;
            // 
            // chkReconciliated
            // 
            this.chkReconciliated.AutoSize = true;
            this.chkReconciliated.Location = new System.Drawing.Point(232, 19);
            this.chkReconciliated.Name = "chkReconciliated";
            this.chkReconciliated.Size = new System.Drawing.Size(155, 20);
            this.chkReconciliated.TabIndex = 1;
            this.chkReconciliated.Text = "Reconciliationed";
            this.chkReconciliated.UseVisualStyleBackColor = true;
            // 
            // chkReturn
            // 
            this.chkReturn.AutoSize = true;
            this.chkReturn.Location = new System.Drawing.Point(6, 21);
            this.chkReturn.Name = "chkReturn";
            this.chkReturn.Size = new System.Drawing.Size(75, 20);
            this.chkReturn.TabIndex = 0;
            this.chkReturn.Text = "Return";
            this.chkReturn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReturn.UseVisualStyleBackColor = true;
            // 
            // cmbstatus
            // 
            this.cmbstatus.FormattingEnabled = true;
            this.cmbstatus.Location = new System.Drawing.Point(253, 219);
            this.cmbstatus.Name = "cmbstatus";
            this.cmbstatus.Size = new System.Drawing.Size(155, 24);
            this.cmbstatus.TabIndex = 11;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(253, 133);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(155, 22);
            this.txtAmount.TabIndex = 10;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(411, 38);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(173, 22);
            this.txtName.TabIndex = 9;
            // 
            // cmbBankAccNumber
            // 
            this.cmbBankAccNumber.FormattingEnabled = true;
            this.cmbBankAccNumber.Location = new System.Drawing.Point(253, 37);
            this.cmbBankAccNumber.Name = "cmbBankAccNumber";
            this.cmbBankAccNumber.Size = new System.Drawing.Size(155, 24);
            this.cmbBankAccNumber.TabIndex = 8;
            // 
            // dtpRealized
            // 
            this.dtpRealized.CustomFormat = "dd/MMM/yyyy";
            this.dtpRealized.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRealized.Location = new System.Drawing.Point(253, 190);
            this.dtpRealized.Name = "dtpRealized";
            this.dtpRealized.Size = new System.Drawing.Size(139, 22);
            this.dtpRealized.TabIndex = 7;
            // 
            // dtpWrite
            // 
            this.dtpWrite.CustomFormat = "dd/MMM/yyyy";
            this.dtpWrite.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWrite.Location = new System.Drawing.Point(253, 161);
            this.dtpWrite.Name = "dtpWrite";
            this.dtpWrite.Size = new System.Drawing.Size(139, 22);
            this.dtpWrite.TabIndex = 6;
            // 
            // cmbToFrom
            // 
            this.cmbToFrom.FormattingEnabled = true;
            this.cmbToFrom.Location = new System.Drawing.Point(253, 102);
            this.cmbToFrom.Name = "cmbToFrom";
            this.cmbToFrom.Size = new System.Drawing.Size(324, 24);
            this.cmbToFrom.TabIndex = 4;
            // 
            // rbtReceive
            // 
            this.rbtReceive.AutoSize = true;
            this.rbtReceive.Location = new System.Drawing.Point(393, 3);
            this.rbtReceive.Name = "rbtReceive";
            this.rbtReceive.Size = new System.Drawing.Size(82, 20);
            this.rbtReceive.TabIndex = 2;
            this.rbtReceive.TabStop = true;
            this.rbtReceive.Text = "Receive";
            this.rbtReceive.UseVisualStyleBackColor = true;
            // 
            // rbtwrite
            // 
            this.rbtwrite.AutoSize = true;
            this.rbtwrite.Location = new System.Drawing.Point(304, 3);
            this.rbtwrite.Name = "rbtwrite";
            this.rbtwrite.Size = new System.Drawing.Size(66, 20);
            this.rbtwrite.TabIndex = 1;
            this.rbtwrite.TabStop = true;
            this.rbtwrite.Text = "Write";
            this.rbtwrite.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(80, 253);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(160, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "Any Special Remarks";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(184, 222);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Cheque Realize Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(88, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "Write/Receive Date";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(184, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "Amount";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(176, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "To/From";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(136, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Bank Account";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Cheque Number";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel9);
            this.tabPage2.Controls.Add(this.panel7);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(788, 489);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cheque List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Silver;
            this.panel9.Controls.Add(this.dgvChequeList);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(4, 41);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(780, 444);
            this.panel9.TabIndex = 1;
            // 
            // dgvChequeList
            // 
            this.dgvChequeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChequeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChequeList.Location = new System.Drawing.Point(0, 0);
            this.dgvChequeList.Name = "dgvChequeList";
            this.dgvChequeList.Size = new System.Drawing.Size(780, 444);
            this.dgvChequeList.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Gray;
            this.panel7.Controls.Add(this.comboBox5);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(4, 4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(780, 37);
            this.panel7.TabIndex = 0;
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(76, 4);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(143, 24);
            this.comboBox5.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Gray;
            this.tabPage3.Controls.Add(this.panel10);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(788, 489);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Cheque Book";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Silver;
            this.panel10.Controls.Add(this.button1);
            this.panel10.Controls.Add(this.dtpChequeCancelDate);
            this.panel10.Controls.Add(this.textBox5);
            this.panel10.Controls.Add(this.chkChequeBook);
            this.panel10.Controls.Add(this.btnAdd);
            this.panel10.Controls.Add(this.txtNofCheque);
            this.panel10.Controls.Add(this.txtStChequeNumber);
            this.panel10.Controls.Add(this.lblCancellChnumber);
            this.panel10.Controls.Add(this.label16);
            this.panel10.Controls.Add(this.label15);
            this.panel10.Controls.Add(this.label14);
            this.panel10.Controls.Add(this.label13);
            this.panel10.Controls.Add(this.label12);
            this.panel10.Controls.Add(this.label11);
            this.panel10.Controls.Add(this.cmbBankAccountNumber);
            this.panel10.ForeColor = System.Drawing.Color.Blue;
            this.panel10.Location = new System.Drawing.Point(6, 6);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(776, 476);
            this.panel10.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(540, 265);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dtpChequeCancelDate
            // 
            this.dtpChequeCancelDate.CustomFormat = "dd/MMM/yyyy";
            this.dtpChequeCancelDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChequeCancelDate.Location = new System.Drawing.Point(513, 237);
            this.dtpChequeCancelDate.Name = "dtpChequeCancelDate";
            this.dtpChequeCancelDate.Size = new System.Drawing.Size(143, 22);
            this.dtpChequeCancelDate.TabIndex = 6;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(418, 188);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(351, 22);
            this.textBox5.TabIndex = 5;
            // 
            // chkChequeBook
            // 
            this.chkChequeBook.AllowUserToAddRows = false;
            this.chkChequeBook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.chkChequeBook.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkChequeBook_Number,
            this.chkChequeBook_Status});
            this.chkChequeBook.Location = new System.Drawing.Point(14, 105);
            this.chkChequeBook.Name = "chkChequeBook";
            this.chkChequeBook.RowHeadersWidth = 4;
            this.chkChequeBook.Size = new System.Drawing.Size(387, 357);
            this.chkChequeBook.TabIndex = 4;
            // 
            // chkChequeBook_Number
            // 
            this.chkChequeBook_Number.HeaderText = "Cheque Number";
            this.chkChequeBook_Number.Name = "chkChequeBook_Number";
            this.chkChequeBook_Number.Width = 250;
            // 
            // chkChequeBook_Status
            // 
            this.chkChequeBook_Status.HeaderText = "Status";
            this.chkChequeBook_Status.Name = "chkChequeBook_Status";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(536, 64);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(112, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add To List";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // txtNofCheque
            // 
            this.txtNofCheque.Location = new System.Drawing.Point(454, 64);
            this.txtNofCheque.Name = "txtNofCheque";
            this.txtNofCheque.Size = new System.Drawing.Size(48, 22);
            this.txtNofCheque.TabIndex = 2;
            // 
            // txtStChequeNumber
            // 
            this.txtStChequeNumber.Location = new System.Drawing.Point(177, 61);
            this.txtStChequeNumber.Name = "txtStChequeNumber";
            this.txtStChequeNumber.Size = new System.Drawing.Size(161, 22);
            this.txtStChequeNumber.TabIndex = 2;
            // 
            // lblCancellChnumber
            // 
            this.lblCancellChnumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCancellChnumber.Location = new System.Drawing.Point(540, 124);
            this.lblCancellChnumber.Name = "lblCancellChnumber";
            this.lblCancellChnumber.Size = new System.Drawing.Size(229, 22);
            this.lblCancellChnumber.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(533, 218);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 16);
            this.label16.TabIndex = 1;
            this.label16.Text = "Cancel Date";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(510, 164);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(176, 16);
            this.label15.TabIndex = 1;
            this.label15.Text = "Reason for the cancel";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(428, 125);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(104, 16);
            this.label14.TabIndex = 1;
            this.label14.Text = "To be Cancel";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(344, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(104, 16);
            this.label13.TabIndex = 1;
            this.label13.Text = "N of Cheques";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 64);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(152, 16);
            this.label12.TabIndex = 1;
            this.label12.Text = "Start Cheque Nuber";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(160, 16);
            this.label11.TabIndex = 1;
            this.label11.Text = "Bank Account Number";
            // 
            // cmbBankAccountNumber
            // 
            this.cmbBankAccountNumber.FormattingEnabled = true;
            this.cmbBankAccountNumber.Location = new System.Drawing.Point(177, 16);
            this.cmbBankAccountNumber.Name = "cmbBankAccountNumber";
            this.cmbBankAccountNumber.Size = new System.Drawing.Size(214, 24);
            this.cmbBankAccountNumber.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(796, 41);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 36);
            this.label1.TabIndex = 18;
            this.label1.Text = "Cheque Details";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Silver;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Location = new System.Drawing.Point(747, 1);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 36);
            this.btnExit.TabIndex = 17;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCheque";
            this.Text = "Cheque Details";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCheque_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmCheque_Paint);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChequeList)).EndInit();
            this.panel7.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkChequeBook)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cmbstatus;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbBankAccNumber;
        private System.Windows.Forms.DateTimePicker dtpRealized;
        private System.Windows.Forms.DateTimePicker dtpWrite;
        private System.Windows.Forms.ComboBox cmbToFrom;
        private System.Windows.Forms.RadioButton rbtReceive;
        private System.Windows.Forms.RadioButton rbtwrite;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCLear;
        private System.Windows.Forms.TextBox txtAnyremarks;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private System.Windows.Forms.CheckBox chkReconciliated;
        private System.Windows.Forms.CheckBox chkReturn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.DataGridView dgvChequeList;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.DataGridView chkChequeBook;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtNofCheque;
        private System.Windows.Forms.TextBox txtStChequeNumber;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbBankAccountNumber;
        private System.Windows.Forms.ComboBox cmbquenumbers;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dtpChequeCancelDate;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.DataGridViewTextBoxColumn chkChequeBook_Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn chkChequeBook_Status;
        private System.Windows.Forms.Label lblCancellChnumber;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
    }
}