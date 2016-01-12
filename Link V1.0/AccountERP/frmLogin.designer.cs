namespace AccountERP
    {
    partial class frmLogin
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
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
                this.txtUserName = new System.Windows.Forms.TextBox();
                this.label1 = new System.Windows.Forms.Label();
                this.label2 = new System.Windows.Forms.Label();
                this.txtPWD = new System.Windows.Forms.TextBox();
                this.btnOk = new System.Windows.Forms.Button();
                this.picture = new System.Windows.Forms.PictureBox();
                this.imageList1 = new System.Windows.Forms.ImageList(this.components);
                this.cmbCompanyName = new System.Windows.Forms.ComboBox();
                this.label3 = new System.Windows.Forms.Label();
                this.chkLocalLogging = new System.Windows.Forms.CheckBox();
                this.rbtLocal = new System.Windows.Forms.RadioButton();
                this.rbt3Sfab = new System.Windows.Forms.RadioButton();
                this.btnExit = new System.Windows.Forms.Button();
                ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
                this.SuspendLayout();
                // 
                // txtUserName
                // 
                this.txtUserName.Enabled = false;
                this.txtUserName.Location = new System.Drawing.Point(267, 141);
                this.txtUserName.Name = "txtUserName";
                this.txtUserName.Size = new System.Drawing.Size(150, 22);
                this.txtUserName.TabIndex = 3;
                this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
                // 
                // label1
                // 
                this.label1.AutoSize = true;
                this.label1.BackColor = System.Drawing.Color.Transparent;
                this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.label1.Location = new System.Drawing.Point(173, 141);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(82, 17);
                this.label1.TabIndex = 1;
                this.label1.Text = "User Name";
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.BackColor = System.Drawing.Color.Transparent;
                this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.label2.Location = new System.Drawing.Point(185, 170);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(70, 17);
                this.label2.TabIndex = 1;
                this.label2.Text = "Password";
                // 
                // txtPWD
                // 
                this.txtPWD.Enabled = false;
                this.txtPWD.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.txtPWD.Location = new System.Drawing.Point(267, 167);
                this.txtPWD.Name = "txtPWD";
                this.txtPWD.PasswordChar = '*';
                this.txtPWD.Size = new System.Drawing.Size(150, 25);
                this.txtPWD.TabIndex = 4;
                this.txtPWD.TextChanged += new System.EventHandler(this.txtPWD_TextChanged);
                this.txtPWD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPWD_KeyDown);
                // 
                // btnOk
                // 
                this.btnOk.Image = global::Finance.Properties.Resources.btnLogin2;
                this.btnOk.Location = new System.Drawing.Point(298, 193);
                this.btnOk.Name = "btnOk";
                this.btnOk.Size = new System.Drawing.Size(75, 32);
                this.btnOk.TabIndex = 6;
                this.btnOk.UseVisualStyleBackColor = true;
                this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
                // 
                // picture
                // 
                this.picture.BackColor = System.Drawing.Color.Transparent;
                this.picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.picture.Location = new System.Drawing.Point(418, 173);
                this.picture.Name = "picture";
                this.picture.Size = new System.Drawing.Size(24, 22);
                this.picture.TabIndex = 6;
                this.picture.TabStop = false;
                // 
                // imageList1
                // 
                this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
                this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
                this.imageList1.Images.SetKeyName(0, "wrong.jpg");
                this.imageList1.Images.SetKeyName(1, "rightmark2.jpg");
                // 
                // cmbCompanyName
                // 
                this.cmbCompanyName.FormattingEnabled = true;
                this.cmbCompanyName.Location = new System.Drawing.Point(267, 111);
                this.cmbCompanyName.Name = "cmbCompanyName";
                this.cmbCompanyName.Size = new System.Drawing.Size(150, 23);
                this.cmbCompanyName.TabIndex = 2;
                this.cmbCompanyName.SelectedIndexChanged += new System.EventHandler(this.cmbCompanyName_SelectedIndexChanged);
                // 
                // label3
                // 
                this.label3.AutoSize = true;
                this.label3.BackColor = System.Drawing.Color.Transparent;
                this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                this.label3.Location = new System.Drawing.Point(151, 113);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(104, 17);
                this.label3.TabIndex = 1;
                this.label3.Text = "Account Name";
                // 
                // chkLocalLogging
                // 
                this.chkLocalLogging.AutoSize = true;
                this.chkLocalLogging.BackColor = System.Drawing.Color.Transparent;
                this.chkLocalLogging.ForeColor = System.Drawing.Color.Black;
                this.chkLocalLogging.Location = new System.Drawing.Point(21, 220);
                this.chkLocalLogging.Name = "chkLocalLogging";
                this.chkLocalLogging.Size = new System.Drawing.Size(74, 19);
                this.chkLocalLogging.TabIndex = 8;
                this.chkLocalLogging.Text = "Logging ";
                this.chkLocalLogging.UseVisualStyleBackColor = false;
                this.chkLocalLogging.Visible = false;
                this.chkLocalLogging.CheckedChanged += new System.EventHandler(this.chkLocalLogging_CheckedChanged);
                // 
                // rbtLocal
                // 
                this.rbtLocal.AutoSize = true;
                this.rbtLocal.BackColor = System.Drawing.Color.Transparent;
                this.rbtLocal.Location = new System.Drawing.Point(223, 79);
                this.rbtLocal.Name = "rbtLocal";
                this.rbtLocal.Size = new System.Drawing.Size(54, 19);
                this.rbtLocal.TabIndex = 0;
                this.rbtLocal.TabStop = true;
                this.rbtLocal.Text = "Local";
                this.rbtLocal.UseVisualStyleBackColor = false;
                this.rbtLocal.CheckedChanged += new System.EventHandler(this.rbtLocal_CheckedChanged);
                // 
                // rbt3Sfab
                // 
                this.rbt3Sfab.AutoSize = true;
                this.rbt3Sfab.BackColor = System.Drawing.Color.Transparent;
                this.rbt3Sfab.Location = new System.Drawing.Point(312, 79);
                this.rbt3Sfab.Name = "rbt3Sfab";
                this.rbt3Sfab.Size = new System.Drawing.Size(61, 19);
                this.rbt3Sfab.TabIndex = 1;
                this.rbt3Sfab.TabStop = true;
                this.rbt3Sfab.Text = "3S Fab";
                this.rbt3Sfab.UseVisualStyleBackColor = false;
                this.rbt3Sfab.CheckedChanged += new System.EventHandler(this.rbt3Sfab_CheckedChanged);
                // 
                // btnExit
                // 
                this.btnExit.AutoSize = true;
                this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
                this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.btnExit.Location = new System.Drawing.Point(423, 12);
                this.btnExit.Name = "btnExit";
                this.btnExit.Size = new System.Drawing.Size(31, 30);
                this.btnExit.TabIndex = 17;
                this.btnExit.UseVisualStyleBackColor = true;
                this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
                // 
                // frmLogin
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.Gray;
                this.BackgroundImage = global::Finance.Properties.Resources.AccountLogInBackgeround2;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.ClientSize = new System.Drawing.Size(466, 264);
                this.ControlBox = false;
                this.Controls.Add(this.btnExit);
                this.Controls.Add(this.rbt3Sfab);
                this.Controls.Add(this.rbtLocal);
                this.Controls.Add(this.chkLocalLogging);
                this.Controls.Add(this.cmbCompanyName);
                this.Controls.Add(this.picture);
                this.Controls.Add(this.btnOk);
                this.Controls.Add(this.txtPWD);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.label3);
                this.Controls.Add(this.label1);
                this.Controls.Add(this.txtUserName);
                this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ForeColor = System.Drawing.Color.Blue;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                this.MaximizeBox = false;
                this.Name = "frmLogin";
                this.Text = "User Logging - Accounting System [2016]";
                this.Load += new System.EventHandler(this.frmLogin_Load);
                ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

        #endregion

        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cmbCompanyName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkLocalLogging;
        private System.Windows.Forms.RadioButton rbtLocal;
        private System.Windows.Forms.RadioButton rbt3Sfab;
        private System.Windows.Forms.Button btnExit;
        }
    }