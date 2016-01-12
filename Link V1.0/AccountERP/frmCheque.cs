﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.BankChequess ;
namespace AccountERP
{
    public partial class frmCheque : Form
    {
        private BankCheques MyCheque=null;
        public frmCheque()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void frmCheque_Load(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
            panel1.Top = (this.Height - panel1.Height) / 2; 
            MyCheque=new BankCheques(Program.AccountStatic.LoggingAsLocal ) ;
        }

        private void frmCheque_Paint(object sender, PaintEventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
            panel1.Top = (this.Height - panel1.Height) / 2; 
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
            {

            }
        private string SetDataToClass(out BankCheques.BankChequesDataType _SaveData)
        {
            _SaveData = new BankCheques.BankChequesDataType();
            decimal d=0;
            bool resp = decimal.TryParse(txtAmount.Text, out d);
            _SaveData.Amount =d;
            _SaveData.BankID = 1;
            return "True";
        }

    }
}
