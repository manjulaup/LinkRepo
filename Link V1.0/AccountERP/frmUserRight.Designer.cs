namespace AccountERP
    {
    partial class frmUserRight
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserRight));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblCorrectuser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.chkall = new System.Windows.Forms.CheckBox();
            this.chkPostAc = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbUserRoll = new System.Windows.Forms.ComboBox();
            this.chkAprova = new System.Windows.Forms.CheckBox();
            this.chkPrint = new System.Windows.Forms.CheckBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.chkupdate = new System.Windows.Forms.CheckBox();
            this.chkSave = new System.Windows.Forms.CheckBox();
            this.chkShow = new System.Windows.Forms.CheckBox();
            this.dgvObject = new System.Windows.Forms.DataGridView();
            this.dgvObject_select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvObject_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvObject_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvObject_Show = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvObject_Save = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvObject_Update = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvObject_Delete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvObject_Print = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvObject_Aproval = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvObject_PostToAc = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TreeMenu = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObject)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.lblCorrectuser);
            this.panel1.Controls.Add(this.txtUser);
            this.panel1.Controls.Add(this.chkall);
            this.panel1.Controls.Add(this.chkPostAc);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbUserRoll);
            this.panel1.Controls.Add(this.chkAprova);
            this.panel1.Controls.Add(this.chkPrint);
            this.panel1.Controls.Add(this.chkDelete);
            this.panel1.Controls.Add(this.chkupdate);
            this.panel1.Controls.Add(this.chkSave);
            this.panel1.Controls.Add(this.chkShow);
            this.panel1.Controls.Add(this.dgvObject);
            this.panel1.Controls.Add(this.TreeMenu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(974, 548);
            this.panel1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(911, 88);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblCorrectuser
            // 
            this.lblCorrectuser.BackColor = System.Drawing.Color.Red;
            this.lblCorrectuser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCorrectuser.Location = new System.Drawing.Point(545, 32);
            this.lblCorrectuser.Name = "lblCorrectuser";
            this.lblCorrectuser.Size = new System.Drawing.Size(35, 22);
            this.lblCorrectuser.TabIndex = 12;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(444, 32);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 22);
            this.txtUser.TabIndex = 11;
            this.txtUser.TextChanged += new System.EventHandler(this.txtUser_TextChanged);
            // 
            // chkall
            // 
            this.chkall.AutoSize = true;
            this.chkall.Location = new System.Drawing.Point(298, 37);
            this.chkall.Name = "chkall";
            this.chkall.Size = new System.Drawing.Size(42, 19);
            this.chkall.TabIndex = 10;
            this.chkall.Text = "All";
            this.chkall.UseVisualStyleBackColor = true;
            this.chkall.CheckedChanged += new System.EventHandler(this.chkall_CheckedChanged);
            // 
            // chkPostAc
            // 
            this.chkPostAc.AutoSize = true;
            this.chkPostAc.Location = new System.Drawing.Point(833, 41);
            this.chkPostAc.Name = "chkPostAc";
            this.chkPostAc.Size = new System.Drawing.Size(15, 14);
            this.chkPostAc.TabIndex = 9;
            this.chkPostAc.UseVisualStyleBackColor = true;
            this.chkPostAc.CheckedChanged += new System.EventHandler(this.chkPostAc_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "User Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(382, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "User Roll";
            // 
            // cmbUserRoll
            // 
            this.cmbUserRoll.FormattingEnabled = true;
            this.cmbUserRoll.Location = new System.Drawing.Point(444, 7);
            this.cmbUserRoll.Name = "cmbUserRoll";
            this.cmbUserRoll.Size = new System.Drawing.Size(171, 23);
            this.cmbUserRoll.TabIndex = 7;
            // 
            // chkAprova
            // 
            this.chkAprova.AutoSize = true;
            this.chkAprova.Location = new System.Drawing.Point(796, 41);
            this.chkAprova.Name = "chkAprova";
            this.chkAprova.Size = new System.Drawing.Size(15, 14);
            this.chkAprova.TabIndex = 6;
            this.chkAprova.UseVisualStyleBackColor = true;
            this.chkAprova.CheckedChanged += new System.EventHandler(this.chkAprova_CheckedChanged);
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Location = new System.Drawing.Point(757, 41);
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.Size = new System.Drawing.Size(15, 14);
            this.chkPrint.TabIndex = 6;
            this.chkPrint.UseVisualStyleBackColor = true;
            this.chkPrint.CheckedChanged += new System.EventHandler(this.chkPrint_CheckedChanged);
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(718, 41);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(15, 14);
            this.chkDelete.TabIndex = 6;
            this.chkDelete.UseVisualStyleBackColor = true;
            this.chkDelete.CheckedChanged += new System.EventHandler(this.chkDelete_CheckedChanged);
            // 
            // chkupdate
            // 
            this.chkupdate.AutoSize = true;
            this.chkupdate.Location = new System.Drawing.Point(678, 41);
            this.chkupdate.Name = "chkupdate";
            this.chkupdate.Size = new System.Drawing.Size(15, 14);
            this.chkupdate.TabIndex = 6;
            this.chkupdate.UseVisualStyleBackColor = true;
            this.chkupdate.CheckedChanged += new System.EventHandler(this.chkupdate_CheckedChanged);
            // 
            // chkSave
            // 
            this.chkSave.AutoSize = true;
            this.chkSave.Location = new System.Drawing.Point(639, 41);
            this.chkSave.Name = "chkSave";
            this.chkSave.Size = new System.Drawing.Size(15, 14);
            this.chkSave.TabIndex = 6;
            this.chkSave.UseVisualStyleBackColor = true;
            this.chkSave.CheckedChanged += new System.EventHandler(this.chkSave_CheckedChanged);
            // 
            // chkShow
            // 
            this.chkShow.AutoSize = true;
            this.chkShow.Location = new System.Drawing.Point(600, 41);
            this.chkShow.Name = "chkShow";
            this.chkShow.Size = new System.Drawing.Size(15, 14);
            this.chkShow.TabIndex = 6;
            this.chkShow.UseVisualStyleBackColor = true;
            this.chkShow.CheckedChanged += new System.EventHandler(this.chkShow_CheckedChanged);
            // 
            // dgvObject
            // 
            this.dgvObject.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MistyRose;
            this.dgvObject.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvObject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvObject.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvObject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObject.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvObject_select,
            this.dgvObject_ID,
            this.dgvObject_Name,
            this.dgvObject_Show,
            this.dgvObject_Save,
            this.dgvObject_Update,
            this.dgvObject_Delete,
            this.dgvObject_Print,
            this.dgvObject_Aproval,
            this.dgvObject_PostToAc});
            this.dgvObject.Location = new System.Drawing.Point(278, 60);
            this.dgvObject.Name = "dgvObject";
            this.dgvObject.RowHeadersWidth = 4;
            this.dgvObject.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvObject.Size = new System.Drawing.Size(618, 490);
            this.dgvObject.TabIndex = 5;
            // 
            // dgvObject_select
            // 
            this.dgvObject_select.FalseValue = "0";
            this.dgvObject_select.HeaderText = "Select";
            this.dgvObject_select.Name = "dgvObject_select";
            this.dgvObject_select.TrueValue = "1";
            this.dgvObject_select.Width = 50;
            // 
            // dgvObject_ID
            // 
            this.dgvObject_ID.HeaderText = "ID";
            this.dgvObject_ID.Name = "dgvObject_ID";
            this.dgvObject_ID.Width = 50;
            // 
            // dgvObject_Name
            // 
            this.dgvObject_Name.HeaderText = "Name";
            this.dgvObject_Name.Name = "dgvObject_Name";
            this.dgvObject_Name.Width = 200;
            // 
            // dgvObject_Show
            // 
            this.dgvObject_Show.FalseValue = "0";
            this.dgvObject_Show.HeaderText = "Sh";
            this.dgvObject_Show.Name = "dgvObject_Show";
            this.dgvObject_Show.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvObject_Show.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvObject_Show.TrueValue = "1";
            this.dgvObject_Show.Width = 40;
            // 
            // dgvObject_Save
            // 
            this.dgvObject_Save.FalseValue = "0";
            this.dgvObject_Save.HeaderText = "S";
            this.dgvObject_Save.Name = "dgvObject_Save";
            this.dgvObject_Save.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvObject_Save.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvObject_Save.TrueValue = "1";
            this.dgvObject_Save.Width = 40;
            // 
            // dgvObject_Update
            // 
            this.dgvObject_Update.FalseValue = "0";
            this.dgvObject_Update.HeaderText = "U";
            this.dgvObject_Update.Name = "dgvObject_Update";
            this.dgvObject_Update.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvObject_Update.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvObject_Update.TrueValue = "1";
            this.dgvObject_Update.Width = 40;
            // 
            // dgvObject_Delete
            // 
            this.dgvObject_Delete.FalseValue = "0";
            this.dgvObject_Delete.HeaderText = "D";
            this.dgvObject_Delete.Name = "dgvObject_Delete";
            this.dgvObject_Delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvObject_Delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvObject_Delete.TrueValue = "1";
            this.dgvObject_Delete.Width = 40;
            // 
            // dgvObject_Print
            // 
            this.dgvObject_Print.FalseValue = "0";
            this.dgvObject_Print.HeaderText = "P";
            this.dgvObject_Print.Name = "dgvObject_Print";
            this.dgvObject_Print.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvObject_Print.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvObject_Print.TrueValue = "1";
            this.dgvObject_Print.Width = 40;
            // 
            // dgvObject_Aproval
            // 
            this.dgvObject_Aproval.FalseValue = "0";
            this.dgvObject_Aproval.HeaderText = "A";
            this.dgvObject_Aproval.Name = "dgvObject_Aproval";
            this.dgvObject_Aproval.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvObject_Aproval.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvObject_Aproval.TrueValue = "1";
            this.dgvObject_Aproval.Width = 40;
            // 
            // dgvObject_PostToAc
            // 
            this.dgvObject_PostToAc.FalseValue = "0";
            this.dgvObject_PostToAc.HeaderText = "Acc";
            this.dgvObject_PostToAc.Name = "dgvObject_PostToAc";
            this.dgvObject_PostToAc.TrueValue = "1";
            this.dgvObject_PostToAc.Width = 40;
            // 
            // TreeMenu
            // 
            this.TreeMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TreeMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.TreeMenu.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeMenu.ImageIndex = 0;
            this.TreeMenu.ImageList = this.imageList1;
            this.TreeMenu.Location = new System.Drawing.Point(0, 0);
            this.TreeMenu.Name = "TreeMenu";
            this.TreeMenu.SelectedImageIndex = 0;
            this.TreeMenu.Size = new System.Drawing.Size(272, 548);
            this.TreeMenu.TabIndex = 4;
            this.TreeMenu.DoubleClick += new System.EventHandler(this.TreeMenu_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Avast (2).ico");
            this.imageList1.Images.SetKeyName(1, "Arrow.ico");
            this.imageList1.Images.SetKeyName(2, "index.png");
            this.imageList1.Images.SetKeyName(3, "funnel_preferences.png");
            this.imageList1.Images.SetKeyName(4, "Address Book (3).ico");
            this.imageList1.Images.SetKeyName(5, "Address Book (4).ico");
            this.imageList1.Images.SetKeyName(6, "AlienAqua - Run.ico");
            this.imageList1.Images.SetKeyName(7, "AlienAqua - Scanner (2).ico");
            this.imageList1.Images.SetKeyName(8, "Book (2).ico");
            this.imageList1.Images.SetKeyName(9, "Adobe - Photoshop (7).ico");
            this.imageList1.Images.SetKeyName(10, "Book - Apple (Red).ico");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(988, 582);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(980, 554);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Create Obect Profile";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(980, 554);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Create User Profile";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // frmUserRight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 582);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Blue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUserRight";
            this.Text = "User Right";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmUserRight_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObject)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAprova;
        private System.Windows.Forms.CheckBox chkPrint;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.CheckBox chkupdate;
        private System.Windows.Forms.CheckBox chkSave;
        private System.Windows.Forms.CheckBox chkShow;
        private System.Windows.Forms.DataGridView dgvObject;
        private System.Windows.Forms.TreeView TreeMenu;
        private System.Windows.Forms.CheckBox chkPostAc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbUserRoll;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.CheckBox chkall;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvObject_select;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvObject_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvObject_Name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvObject_Show;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvObject_Save;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvObject_Update;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvObject_Delete;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvObject_Print;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvObject_Aproval;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvObject_PostToAc;
        private System.Windows.Forms.Label lblCorrectuser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSave;
        }
    }