using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityHandler;
using BusinessLayer;
using BusinessLayer.CommonOperation;


namespace AccountERP
{
    public partial class frmAcknowledgement : Form
    {
        private CommonOperations MyCommon = null;

        public frmAcknowledgement()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BALAcknowledgement objBAL=new BALAcknowledgement(Program.AccountStatic.LoggingAsLocal);
            Acknoladgement _obj = new Acknoladgement();
            string respond = setData(out _obj);

            if (respond == "True")
            {
                respond = objBAL.Save(_obj);
                if (respond == "True")
                { MessageBox.Show("Saved"); }
                else
                {
                    MessageBox.Show("Save Failed");
                }
            }
        }

        public string setData(out Acknoladgement obj)
        {
            obj = new Acknoladgement();

            try{
                obj.name = txtName.Text;
                obj.nic = txtNIC.Text;
                obj.tp = txtTP.Text;
                obj.chequeno = txtChequeNo.Text;
                obj.voucherno = txtVoucherNo.Text;
                obj.savedate = dateTimePicker1.Text;
                return "True";
             }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        private void LoadData()
        {
            MyCommon = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
            DataTable dt2 = new DataTable();
            dt2=MyCommon.GetDataTableAccount("select * from acknoladgement","Get Ack");

            if (dt2.Rows.Count > 0)
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt2;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.Controls[1])
            {
                LoadData();
            }
        }
    }
}
