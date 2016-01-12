namespace AccountERP
{
    partial class frmJobList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmJobList));
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblFormdescription = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvProject = new System.Windows.Forms.DataGridView();
            this.dgvProject_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvProject_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvProject_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvProject_Eng = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProject)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel5.Controls.Add(this.lblFormdescription);
            this.panel5.Controls.Add(this.btnExit);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(676, 37);
            this.panel5.TabIndex = 3;
            // 
            // lblFormdescription
            // 
            this.lblFormdescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblFormdescription.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormdescription.ForeColor = System.Drawing.Color.Black;
            this.lblFormdescription.Location = new System.Drawing.Point(3, 3);
            this.lblFormdescription.Name = "lblFormdescription";
            this.lblFormdescription.Size = new System.Drawing.Size(433, 29);
            this.lblFormdescription.TabIndex = 17;
            this.lblFormdescription.Text = "Project List";
            this.lblFormdescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.Location = new System.Drawing.Point(629, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(35, 32);
            this.btnExit.TabIndex = 16;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvProject);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(676, 405);
            this.panel1.TabIndex = 4;
            // 
            // dgvProject
            // 
            this.dgvProject.AllowUserToAddRows = false;
            this.dgvProject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProject.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvProject_ID,
            this.dgvProject_Name,
            this.dgvProject_Description,
            this.dgvProject_Eng});
            this.dgvProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProject.Location = new System.Drawing.Point(0, 0);
            this.dgvProject.MultiSelect = false;
            this.dgvProject.Name = "dgvProject";
            this.dgvProject.RowHeadersWidth = 4;
            this.dgvProject.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProject.Size = new System.Drawing.Size(676, 405);
            this.dgvProject.TabIndex = 0;
            this.dgvProject.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProject_CellDoubleClick);
            this.dgvProject.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvProject_CellMouseClick);
            this.dgvProject.DragLeave += new System.EventHandler(this.dgvProject_DragLeave);
            this.dgvProject.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvProject_MouseUp);
            // 
            // dgvProject_ID
            // 
            this.dgvProject_ID.HeaderText = "ID";
            this.dgvProject_ID.Name = "dgvProject_ID";
            // 
            // dgvProject_Name
            // 
            this.dgvProject_Name.HeaderText = "Name";
            this.dgvProject_Name.Name = "dgvProject_Name";
            this.dgvProject_Name.Width = 200;
            // 
            // dgvProject_Description
            // 
            this.dgvProject_Description.HeaderText = "Description";
            this.dgvProject_Description.Name = "dgvProject_Description";
            this.dgvProject_Description.Width = 250;
            // 
            // dgvProject_Eng
            // 
            this.dgvProject_Eng.HeaderText = "Engineer";
            this.dgvProject_Eng.Name = "dgvProject_Eng";
            // 
            // frmJobList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 442);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(100, 100);
            this.Name = "frmJobList";
            this.Text = "JOB List";
            this.panel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblFormdescription;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvProject;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProject_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProject_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProject_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvProject_Eng;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}