using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using System.Data;
using EntityHandler;
using DataAccessHandler;

namespace BusinessHandler
{
    public class Voucher
    {
        public List<LINKPayment> BALGetVoucherALL()
        {
            List<LINKPayment> listobj = new List<LINKPayment>();
            DataTable dt = new DataTable();
            try
            {
                DataAccessHandler.Voucher objDALPayment = new DataAccessHandler.Voucher();
                dt = objDALPayment.DALGetVoucherALL();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObjectVoucher(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            {

            }

            return listobj;
        }

        private LINKPayment ConvertToObjectVoucher(DataRow row)
        {
            LINKPayment obj = new LINKPayment();

            try
            {
                obj.VoucherID = row["VoucherID"].ToString();
                obj.VoucherDate = Convert.ToDateTime(row["Voucherdate"].ToString());
                obj.Value = Convert.ToDecimal(row["Value"].ToString());
                obj.AccNo = row["AccNo"].ToString();
                obj.ApproveDate = Convert.ToDateTime(row["ApproveDate"].ToString());

            }
            catch (Exception ex)
            { }
            return obj;
        }

        public List<LINKPayment> BALGetInvoice(LINKPayment objPayment)
        {
            List<LINKPayment> listobj = new List<LINKPayment>();
            DataTable dt = new DataTable();
            try
            {
                DataAccessHandler.Voucher objDALPayment = new DataAccessHandler.Voucher();
                dt = objDALPayment.DALGetInvoice(objPayment);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObjectInvoice(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            {

            }

            return listobj;
        }

        private LINKPayment ConvertToObjectInvoice(DataRow row)
        {
            LINKPayment obj = new LINKPayment();

            try
            {
                obj.InvoiceID = row["InvoiceID"].ToString();
                obj.InvoiceDate = Convert.ToDateTime(row["InvoiceDate"].ToString());
                obj.Value = Convert.ToDecimal(row["Value"].ToString());
                obj.SupplierID = Convert.ToInt32(row["SupplierID"].ToString());

            }
            catch (Exception ex)
            { }
            return obj;
        }

        public List<LINKPayment> BALGetLastPayment(LINKPayment objPayment)
        {
            List<LINKPayment> listobj = new List<LINKPayment>();
            DataTable dt = new DataTable();
            try
            {
                DataAccessHandler.Voucher objDALPayment = new DataAccessHandler.Voucher();
                dt = objDALPayment.DALGetLastPayment(objPayment);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObjectLastPayment(dt.Rows[i]));

                }
            }
            catch (Exception ex)
            {
            }

            return listobj;
        }

        private LINKPayment ConvertToObjectLastPayment(DataRow row)
        {
            LINKPayment obj = new LINKPayment();

            try
            {
                obj.ApproveDate = Convert.ToDateTime(row["ApproveDate"].ToString());
                obj.Value = Convert.ToDecimal(row["Value"].ToString());
                obj.SupplierID = Convert.ToInt32(row["SupplierID"].ToString());

            }
            catch (Exception ex)
            {
            }
            return obj;
        }


        public List<LINKPayment> BALGetInvoiceAll()
        {
            List<LINKPayment> listobj = new List<LINKPayment>();
            DataTable dt = new DataTable();
            try
            {
                DataAccessHandler.Voucher objDALPayment = new DataAccessHandler.Voucher();
                dt = objDALPayment.DALGetInvoiceALL();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObjectInvoiceAll(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            {

            }

            return listobj;
        }

        private LINKPayment ConvertToObjectInvoiceAll(DataRow row)
        {
            LINKPayment obj = new LINKPayment();

            try
            {
                obj.InvoiceID = row["InvoiceID"].ToString();
                obj.SupplierID = Convert.ToInt32(row["SupplierID"].ToString());

            }
            catch (Exception ex)
            { }
            return obj;
        }


        public int BALGetMAXInvoiceID()
        {
            int getID = 0;
            DataTable dt = new DataTable();
            try
            {
                DataAccessHandler.Voucher objDALPayment = new DataAccessHandler.Voucher();
                dt = objDALPayment.DALGetMaxInvoiceID();

                if (dt.Rows.Count > 0)
                {
                    getID = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {

            }

            return getID;
        }

        public int BALGetMAXVoucherID()
        {
            int getID = 0;
            DataTable dt = new DataTable();
            try
            {
                DataAccessHandler.Voucher objDALPayment = new DataAccessHandler.Voucher();
                dt = objDALPayment.DALGetMaxVoucherID();

                if (dt.Rows.Count > 0)
                {
                    getID = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {

            }

            return getID;
        }

        public bool BALInsertInvoice(LINKPayment objPayment, List<LINKPayment> objLists)
        {
            bool status = false;

            try
            {
                DataAccessHandler.Voucher objDALPayment = new DataAccessHandler.Voucher();
                LINKPayment objLink = new LINKPayment();

                if (objLists.Count > 0)
                {
                    status = objDALPayment.DALInsertInvoice(objPayment);

                    if (status)
                    {
                        foreach (LINKPayment objList in objLists)
                        {
                            objLink.GRNNo = objList.GRNNo;
                            objLink.InvoiceID = objList.InvoiceID;

                            status = objDALPayment.DALInsertInvoiceFromGRN(objLink);

                            //update tblGRN set status=9  MRP DB
                            if (status)
                            {
                                status = objDALPayment.DALGRNStatus(objLink);
                            }

                        }
                    }

                    if (status)
                    {

                    }

                }

            }
            catch (Exception ex)
            {

            }
            return status;
        }

         public bool BALInsertVoucher(LINKPayment objPayment, List<LINKPayment> objLists)
        {
            bool status = false;

            try
            {
                DataAccessHandler.Voucher objDALPayment = new DataAccessHandler.Voucher();
                LINKPayment objLink = new LINKPayment();

                if (objLists.Count > 0)
                {
                    status = objDALPayment.DALInsertVoucher(objPayment);

                    if (status)
                    {
                        foreach (LINKPayment objList in objLists)
                        {
                            objLink.VoucherID = objList.VoucherID;
                            objLink.InvoiceID = objList.InvoiceID;

                            status = objDALPayment.DALInsertVoucherFromInvoive(objLink);


                            if (status)
                            {
                                //update tblGRN set status=9
                                status = objDALPayment.DALInvoiceStatus(objLink);
                            }

                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return status;
        }

         public bool BALUpdateVoucher(LINKPayment objPayment)
         {
             bool status = false;

             try
             {
                 DataAccessHandler.Voucher objDALPayment = new DataAccessHandler.Voucher();

                 //update tblGRN set status=9
                 status = objDALPayment.DALVoucherIDStatus(objPayment);


             }
             catch (Exception ex)
             {

             }
             return status;
         }
    }
}
