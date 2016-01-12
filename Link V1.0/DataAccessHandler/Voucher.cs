using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntityHandler;

namespace DataAccessHandler
{
    public class Voucher
    {
        public DataTable DALGetVoucherALL()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_SelectVoucherALL";

                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPFinance(oSqlCommand);
                dt.TableName = "tblVoucher";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public DataTable DALGetMaxVoucherID()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_MAXVoucherID";

                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPFinance(oSqlCommand);
                dt.TableName = "tblVoucher";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public DataTable DALGetInvoice(LINKPayment objPayment)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_SelectInvoice";

                oSqlCommand.Parameters.AddWithValue("@SupplierID", objPayment.SupplierID);
                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPFinance(oSqlCommand);
                dt.TableName = "tblInvoice";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public DataTable DALGetLastPayment(LINKPayment objPayment)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_GetLastPayment";

                oSqlCommand.Parameters.AddWithValue("@SupplierID", objPayment.SupplierID);
                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPFinance(oSqlCommand);
                dt.TableName = "tblInvoice";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public DataTable DALGetInvoiceALL()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_SelectInvoiceALL";

                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPFinance(oSqlCommand);
                dt.TableName = "tblInvoice";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public DataTable DALGetMaxInvoiceID()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_MAXInvoiceID";

                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPFinance(oSqlCommand);
                dt.TableName = "tblInvoice";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public bool DALInsertVoucher(LINKPayment objPayment)
        {
            bool status = false;

            try
            {
                SqlConnection conn = DALConnManager.OpenFinance();
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_PaymentInserIntoVoucher";

                oSqlCommand.Connection = conn;
                oSqlCommand.CommandText = SqlQuery;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.AddWithValue("@VoucherID", objPayment.VoucherID);
                oSqlCommand.Parameters.AddWithValue("@ID", objPayment.ID);
                oSqlCommand.Parameters.AddWithValue("@VoucherDate", objPayment.VoucherDate);
                oSqlCommand.Parameters.AddWithValue("@Value", objPayment.Value);
                oSqlCommand.Parameters.AddWithValue("@AccNo", objPayment.AccNo);
                oSqlCommand.Parameters.AddWithValue("@CreateDate", objPayment.CreateDate);
                oSqlCommand.Parameters.AddWithValue("@CreateUser", objPayment.CreateUser);
                oSqlCommand.Parameters.AddWithValue("@Modifieddate", objPayment.Modifieddate);
                oSqlCommand.Parameters.AddWithValue("@ModifiedUser", objPayment.ModifiedUser);
                oSqlCommand.Parameters.AddWithValue("@Status", objPayment.Status);

                status = new DALBase().Insert(oSqlCommand);
                DALConnManager.Close(conn);


            }
            catch (Exception ex)
            {
            }

            return status;
        }

        public bool DALInsertVoucherFromInvoive(LINKPayment objPayment)
        {
            bool status = false;

            try
            {
                SqlConnection conn = DALConnManager.OpenFinance();
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_PaymentInserIntoVoucherFromInvoice";

                oSqlCommand.Connection = conn;
                oSqlCommand.CommandText = SqlQuery;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.AddWithValue("@VoucherID", objPayment.VoucherID);
                oSqlCommand.Parameters.AddWithValue("@InvoiceID", objPayment.InvoiceID);

                status = new DALBase().Insert(oSqlCommand);
                DALConnManager.Close(conn);


            }
            catch (Exception ex)
            {
                //StreamWriter file2 = new StreamWriter(@"c:\file.txt", true);
                //file2.WriteLine(ex.ToString() + "- " + DateTime.Now + "- DAL");
                //file2.Close();
            }

            return status;
        }

        public bool DALInvoiceStatus(LINKPayment objPayment)
        {
            bool status = false;
            int Invoicestatus = 9;

            try
            {
                SqlConnection conn = DALConnManager.OpenFinance();
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_UpdateStatusInvoice";

                oSqlCommand.Connection = conn;
                oSqlCommand.CommandText = SqlQuery;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.AddWithValue("@Status", Invoicestatus);
                oSqlCommand.Parameters.AddWithValue("@InvoiceID", objPayment.InvoiceID);

                status = new DALBase().Insert(oSqlCommand);
                DALConnManager.Close(conn);


            }
            catch (Exception ex)
            {
            }

            return status;
        }

        public bool DALInsertInvoice(LINKPayment objPayment)
        {
            bool status = false;

            try
            {
                SqlConnection conn = DALConnManager.OpenFinance();
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_PaymentInserIntoInvoice";

                oSqlCommand.Connection = conn;
                oSqlCommand.CommandText = SqlQuery;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.AddWithValue("@InvoiceID", objPayment.InvoiceID);
                oSqlCommand.Parameters.AddWithValue("@ID", objPayment.ID);
                oSqlCommand.Parameters.AddWithValue("@InvoiceDate", objPayment.InvoiceDate);
                oSqlCommand.Parameters.AddWithValue("@Value", objPayment.Value);
                oSqlCommand.Parameters.AddWithValue("@CreateDate", objPayment.CreateDate);
                oSqlCommand.Parameters.AddWithValue("@CreateUser", objPayment.CreateUser);
                oSqlCommand.Parameters.AddWithValue("@Modifieddate", objPayment.Modifieddate);
                oSqlCommand.Parameters.AddWithValue("@ModifiedUser", objPayment.ModifiedUser);
                oSqlCommand.Parameters.AddWithValue("@Status", objPayment.Status);
                oSqlCommand.Parameters.AddWithValue("@SupplierID", objPayment.SupplierID);

                status = new DALBase().Insert(oSqlCommand);
                DALConnManager.Close(conn);


            }
            catch (Exception ex)
            {
            }

            return status;
        }

        public bool DALInsertInvoiceFromGRN(LINKPayment objPayment)
        {
            bool status = false;

            try
            {
                SqlConnection conn = DALConnManager.OpenFinance();
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_PaymentInserIntoInvoiceFromGRN";

                oSqlCommand.Connection = conn;
                oSqlCommand.CommandText = SqlQuery;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.AddWithValue("@InvoiceID", objPayment.InvoiceID);
                oSqlCommand.Parameters.AddWithValue("@GRNNo", objPayment.GRNNo);

                status = new DALBase().Insert(oSqlCommand);
                DALConnManager.Close(conn);


            }
            catch (Exception ex)
            {
            }

            return status;
        }

        public bool DALVoucherIDStatus(LINKPayment objPayment)
        {
            bool status = false;
            int GRNstatus = 9;

            try
            {
                SqlConnection conn = DALConnManager.OpenFinance();
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_UpdateStatustblVoucher";

                oSqlCommand.Connection = conn;
                oSqlCommand.CommandText = SqlQuery;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.AddWithValue("@Status", GRNstatus);
                oSqlCommand.Parameters.AddWithValue("@VoucherID", objPayment.VoucherID);
                oSqlCommand.Parameters.AddWithValue("@ApproveDate", objPayment.ApproveDate);

                status = new DALBase().Insert(oSqlCommand);
                DALConnManager.Close(conn);


            }
            catch (Exception ex)
            {

            }

            return status;
        }


        //Update MRP Database
        public bool DALGRNStatus(LINKPayment objPayment)
        {
            bool status = false;
            int GRNstatus = 9;

            try
            {
                SqlConnection conn = DALConnManager.OpenMRP();
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_UpdateStatusGRN";

                oSqlCommand.Connection = conn;
                oSqlCommand.CommandText = SqlQuery;
                oSqlCommand.CommandType = CommandType.StoredProcedure;
                oSqlCommand.Parameters.AddWithValue("@Status", GRNstatus);
                oSqlCommand.Parameters.AddWithValue("@GRNNo", objPayment.GRNNo);

                status = new DALBase().Insert(oSqlCommand);
                DALConnManager.Close(conn);


            }
            catch (Exception ex)
            {

            }

            return status;
        }
    }
}
