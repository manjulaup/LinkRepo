namespace AccountERP
{
    partial class frmLedgers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLedgers));
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblFormdescription = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvAccounts = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkinFCur = new System.Windows.Forms.CheckBox();
            this.chKWithSub = new System.Windows.Forms.CheckBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.cmbPara3 = new System.Windows.Forms.ComboBox();
            this.dtrTo = new System.Windows.Forms.DateTimePicker();
            this.dtrFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.From = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRptType = new System.Windows.Forms.ComboBox();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Silver;
            this.panel5.Controls.Add(this.lblFormdescription);
            this.panel5.Controls.Add(this.btnExit);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1318, 39);
            this.panel5.TabIndex = 4;
            // 
            // lblFormdescription
            // 
            this.lblFormdescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFormdescription.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormdescription.ForeColor = System.Drawing.Color.Black;
            this.lblFormdescription.Location = new System.Drawing.Point(3, 6);
            this.lblFormdescription.Name = "lblFormdescription";
            this.lblFormdescription.Size = new System.Drawing.Size(566, 29);
            this.lblFormdescription.TabIndex = 17;
            this.lblFormdescription.Text = "Ledger";
            this.lblFormdescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Location = new System.Drawing.Point(1283, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(34, 32);
            this.btnExit.TabIndex = 16;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1318, 494);
            this.panel1.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvAccounts);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1318, 494);
            this.panel3.TabIndex = 1;
            // 
            // dgvAccounts
            // 
            this.dgvAccounts.AllowUserToAddRows = false;
            this.dgvAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccounts.Location = new System.Drawing.Point(0, 0);
            this.dgvAccounts.Name = "dgvAccounts";
            this.dgvAccounts.RowHeadersWidth = 4;
            this.dgvAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccounts.Size = new System.Drawing.Size(1318, 494);
            this.dgvAccounts.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkinFCur);
            this.panel2.Controls.Add(this.chKWithSub);
            this.panel2.Controls.Add(this.btnShow);
            this.panel2.Controls.Add(this.cmbPara3);
            this.panel2.Controls.Add(this.dtrTo);
            this.panel2.Controls.Add(this.dtrFrom);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.From);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbRptType);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1318, 44);
            this.panel2.TabIndex = 6;
            // 
            // chkinFCur
            // 
            this.chkinFCur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkinFCur.AutoSize = true;
            this.chkinFCur.Location = new System.Drawing.Point(1100, 27);
            this.chkinFCur.Name = "chkinFCur";
            this.chkinFCur.Size = new System.Drawing.Size(113, 19);
            this.chkinFCur.TabIndex = 7;
            this.chkinFCur.Text = "Amount In FCur";
            this.chkinFCur.UseVisualStyleBackColor = true;
            // 
            // chKWithSub
            // 
            this.chKWithSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chKWithSub.AutoSize = true;
            this.chKWithSub.Location = new System.Drawing.Point(1100, 5);
            this.chKWithSub.Name = "chKWithSub";
            this.chKWithSub.Size = new System.Drawing.Size(125, 19);
            this.chKWithSub.TabIndex = 6;
            this.chKWithSub.Text = "With Sub Account";
            this.chKWithSub.UseVisualStyleBackColor = true;
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShow.Location = new System.Drawing.Point(1231, 14);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 5;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // cmbPara3
            // 
            this.cmbPara3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPara3.FormattingEnabled = true;
            this.cmbPara3.Location = new System.Drawing.Point(791, 12);
            this.cmbPara3.Name = "cmbPara3";
            this.cmbPara3.Size = new System.Drawing.Size(302, 23);
            this.cmbPara3.Sorted = true;
            this.cmbPara3.TabIndex = 4;
            // 
            // dtrTo
            // 
            this.dtrTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtrTo.CustomFormat = "dd/MMM/yyyy";
            this.dtrTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtrTo.Location = new System.Drawing.Point(614, 13);
            this.dtrTo.Name = "dtrTo";
            this.dtrTo.Size = new System.Drawing.Size(128, 22);
            this.dtrTo.TabIndex = 3;
            // 
            // dtrFrom
            // 
            this.dtrFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtrFrom.CustomFormat = "dd/MMM/yyyy";
            this.dtrFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtrFrom.Location = new System.Drawing.Point(458, 14);
            this.dtrFrom.Name = "dtrFrom";
            this.dtrFrom.Size = new System.Drawing.Size(128, 22);
            this.dtrFrom.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(748, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Para 3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(520, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "To";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(589, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "To";
            // 
            // From
            // 
            this.From.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.From.AutoSize = true;
            this.From.Location = new System.Drawing.Point(419, 16);
            this.From.Name = "From";
            this.From.Size = new System.Drawing.Size(34, 15);
            this.From.TabIndex = 1;
            this.From.Text = "From";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(157, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type Report";
            // 
            // cmbRptType
            // 
            this.cmbRptType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRptType.FormattingEnabled = true;
            this.cmbRptType.Location = new System.Drawing.Point(236, 13);
            this.cmbRptType.Name = "cmbRptType";
            this.cmbRptType.Size = new System.Drawing.Size(184, 23);
            this.cmbRptType.TabIndex = 0;
            this.cmbRptType.SelectedIndexChanged += new System.EventHandler(this.cmbRptType_SelectedIndexChanged);
            // 
            // frmLedgers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 577);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Blue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLedgers";
            this.Text = "Ledgers";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmLedgers_Load);
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblFormdescription;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvAccounts;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cmbRptType;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.ComboBox cmbPara3;
        private System.Windows.Forms.DateTimePicker dtrTo;
        private System.Windows.Forms.DateTimePicker dtrFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label From;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chKWithSub;
        private System.Windows.Forms.CheckBox chkinFCur;
        private System.Windows.Forms.Label label4;
    }
}