using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.CommonOperation;
using BusinessLayer.PaymentsAndReciept;
namespace AccountERP
{
    public partial class frmJobList : Form
    {
        CommonOperations MyCommon = null;
        Payment MyPay = null;
        public frmJobList()
        {
            InitializeComponent();
            MyCommon = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
            MyPay = new Payment(Program.AccountStatic.LoggingAsLocal);
            DataTable tb = MyPay.GetProjectList();
            MyCommon.LoadDatatoTableWithoutBind(dgvProject, tb, "Load JOBS");

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProject_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmPaymentVoucher fm = new frmPaymentVoucher();
            fm.txtJobNumber.Text  = dgvProject.Rows[e.RowIndex].Cells[0].Value.ToString();
            Program.AccountStatic.JobNumber = dgvProject.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.Close();
        }
        string JOBN = "";
        private void dgvProject_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            JOBN = dgvProject.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dgvProject_MouseUp(object sender, MouseEventArgs e)
        {
            dgvProject.DoDragDrop(JOBN, DragDropEffects.Copy | DragDropEffects.Move);

        }

        private void dgvProject_DragLeave(object sender, EventArgs e)
        {
            
        }

        
        
    }
}
