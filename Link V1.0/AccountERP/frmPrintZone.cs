using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using BusinessLayer.CommonOperation;
using EntityHandler;

namespace AccountERP
    {
    public partial class frmPrintZone : Form
        {
        private CommonOperations MyCommon = null;
        public string Voucherid = "";
        ChangeNumbersToWords objChangeNumber = new ChangeNumbersToWords();

        public frmPrintZone()
            {
            InitializeComponent();
            }

        private void frmPrintZone_Load(object sender, EventArgs e)
        {
            string ApproveDate = "";
            string AccNo = "";
            string TotValue = "";
            string Payee = "";
            string ChequeNo = "";
            string PrepaiedDate = "";
            string AmountInWords = "";
            string PrintCount = "0";

            MyCommon = new CommonOperations(Program.AccountStatic.LoggingAsLocal);
            DataTable dtSingle = new DataTable();
            dtSingle = MyCommon.GetDataTableAccount("select PaymentID,FCr,AccountID,PayToName,ChequeNumber,Approvedate,AccountDate from accounterp.tblpayment where PaymentID='" + Voucherid + "'", "GetBill Data");
            if (dtSingle.Rows.Count > 0)
            {
                TotValue =dtSingle.Rows[0][1].ToString();
                AccNo = dtSingle.Rows[0][2].ToString();
                Payee = dtSingle.Rows[0][3].ToString();
                ChequeNo = dtSingle.Rows[0][4].ToString();
                ApproveDate = dtSingle.Rows[0][5].ToString();
                PrepaiedDate = dtSingle.Rows[0][6].ToString();
                AmountInWords = objChangeNumber.changeToWords(TotValue);
            }

            // Set the processing mode for the ReportViewer to Local
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            LocalReport localReport = reportViewer1.LocalReport;
            localReport.ReportPath = "PrintPaymentVoucher.rdlc";

            ReportParameter[] reportParameterCollection = new ReportParameter[9];      
            reportParameterCollection[0] = new ReportParameter();
            reportParameterCollection[0].Name = "VoucherID";
            reportParameterCollection[0].Values.Add(Voucherid);
            reportParameterCollection[1] = new ReportParameter();
            reportParameterCollection[1].Name = "Amount";
            reportParameterCollection[1].Values.Add(TotValue);
            reportParameterCollection[2] = new ReportParameter();
            reportParameterCollection[2].Name = "Approvedate";
            reportParameterCollection[2].Values.Add(ApproveDate);
            reportParameterCollection[3] = new ReportParameter();
            reportParameterCollection[3].Name = "Payee";
            reportParameterCollection[3].Values.Add(Payee);
            reportParameterCollection[4] = new ReportParameter();
            reportParameterCollection[4].Name = "AccNo";
            reportParameterCollection[4].Values.Add(AccNo);
            reportParameterCollection[5] = new ReportParameter();
            reportParameterCollection[5].Name = "ChequeNo";
            reportParameterCollection[5].Values.Add(ChequeNo);
            reportParameterCollection[6] = new ReportParameter();
            reportParameterCollection[6].Name = "PrepaiedDate";
            reportParameterCollection[6].Values.Add(PrepaiedDate);
            reportParameterCollection[7] = new ReportParameter();
            reportParameterCollection[7].Name = "AmountInWords";
            reportParameterCollection[7].Values.Add(AmountInWords);
            reportParameterCollection[8] = new ReportParameter();
            reportParameterCollection[8].Name = "No";
            reportParameterCollection[8].Values.Add(PrintCount);

            //Load DataSet
            DataTable dt = new DataTable();
            string str = "select pb.pono as pono,pb.billno as billno,pb.billno as grnno,pd.Fdr as amount from tblpayablebill as pb  " +
                         "inner join tblpaymetdetails as pd on pb.BIllno=pd.JobNo where pd.PvnNo='" + Voucherid + "'";

            dt = MyCommon.GetDataTableAccount(str,"GetBill Data");

            if (dt.Rows.Count > 0)
            {
                ReportDataSource rds = new ReportDataSource("DataSetPaymentVoucher", dt);

                try
                {
                    reportViewer1.LocalReport.SetParameters(reportParameterCollection);
                    reportViewer1.LocalReport.DataSources.Add(rds);
                }
                catch (Exception ex)
                {

                }
            }

            // Refresh the report
            reportViewer1.RefreshReport();
        }


        }
    }
