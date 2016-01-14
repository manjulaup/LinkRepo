using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DataAccessHandler;
using EntityHandler;
using System.IO;

namespace BusinessHandler
{
   public class BALPayment
    {

        public List<LINKPayment> BALGetFinalCreditor(LINKPayment objPayment)
        {
            List<LINKPayment> listobj = new List<LINKPayment>();
            DataTable dt = new DataTable();
            try
            {
                DALPayment objDALPayment = new DALPayment();
                dt = objDALPayment.DALGetFinalCreditor(objPayment);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObject(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            {

            }

            return listobj;
        }

        private LINKPayment ConvertToObject(DataRow row)
        {
            LINKPayment obj = new LINKPayment();

            try
            {
                obj.SupplierCode = row["SupplierCode"].ToString();
                obj.SupName = row["SupName"].ToString();
                obj.Tot = Convert.ToDecimal(row["Tot"].ToString());
                obj.NoDays = Convert.ToInt32(row["NoDays"].ToString());
                obj.SupAddress = row["SupAddress"].ToString();
                obj.SupContact = row["SupPhone"].ToString();
                obj.SupplierID = Convert.ToInt32(row["SupplierID"].ToString());
                obj.CreditPeriod = Convert.ToInt32(row["CreditPeriod"].ToString());

            }
            catch (Exception ex)
            { }
            return obj;
        }


        public List<LINKPayment> BALGetFinalCreditorSupplierList()
        {
            List<LINKPayment> listobj = new List<LINKPayment>();
            DataTable dt = new DataTable();
            try
            {
                DALPayment objDALPayment = new DALPayment();
                dt = objDALPayment.DALGetFinalCreditorSupplierList();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObjectSupplier(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            {

            }

            return listobj;
        }

        private LINKPayment ConvertToObjectSupplier(DataRow row)
        {
            LINKPayment obj = new LINKPayment();

            try
            {
                 obj.SupName = row["SupName"].ToString();
            }
            catch (Exception ex)
            { }
            return obj;
        }


        public List<LINKPayment> BALGetFinalSupplier(LINKPayment objPayment)
        {
            List<LINKPayment> listobj = new List<LINKPayment>();
            DataTable dt = new DataTable();
            try
            {
                DALPayment objDALPayment = new DALPayment();
                dt = objDALPayment.DALGetFinalSupplier(objPayment);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObjectSupp(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            {

            }

            return listobj;
        }

        public List<LINKPayment> BALGetFinalSupplierByID(LINKPayment objPayment)
        {
            List<LINKPayment> listobj = new List<LINKPayment>();
            DataTable dt = new DataTable();
            try
            {
                DALPayment objDALPayment = new DALPayment();
                dt = objDALPayment.DALGetFinalSupplierByID(objPayment);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObjectSupp(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            {

            }

            return listobj;
        }

        private LINKPayment ConvertToObjectSupp(DataRow row)
        {
            LINKPayment obj = new LINKPayment();

            try
            {
                obj.SupplierCode = row["SupplierCode"].ToString();
                obj.SupName = row["SupName"].ToString();
                obj.SupAddress = row["SupAddress"].ToString();
                obj.SupContact = row["SupPhone"].ToString();
                obj.SupplierID = Convert.ToInt32(row["SupplierID"].ToString());
                obj.CreditPeriod = Convert.ToInt32(row["CreditPeriod"].ToString());

            }
            catch (Exception ex)
            { }
            return obj;
        }


        public List<LINKPayment> BALGetFinalCreditorGRN(LINKPayment objPayment)
        {

            List<LINKPayment> listobj = new List<LINKPayment>();
            DataTable dt = new DataTable();
            try
            {
                DALPayment objDALPayment = new DALPayment();
                dt = objDALPayment.DALGetFinalCreditorGRN(objPayment);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObjectGRN(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            {

            }

            return listobj;
        }

        private LINKPayment ConvertToObjectGRN(DataRow row)
        {
            LINKPayment obj = new LINKPayment();

            try
            {
                obj.GRNNo = row["GRNNO"].ToString();
                obj.GRNApprovedate = Convert.ToDateTime(row["GRNApproveDate"].ToString());
                obj.Tot = Convert.ToDecimal(row["Tot"]);
                obj.NoDays = Convert.ToInt32(row["NoDays"]);
            }
            catch (Exception ex)
            { }
            return obj;
        }


        public List<LINKPayment> BALGetGRNMaterial(LINKPayment objPayment)
        {

            List<LINKPayment> listobj = new List<LINKPayment>();
            DataTable dt = new DataTable();
            try
            {
                DALPayment objDALPayment = new DALPayment();
                dt = objDALPayment.DALGetGRNMaterial(objPayment);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObjectGRNMaterial(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            {

            }

            return listobj;
        }

        private LINKPayment ConvertToObjectGRNMaterial(DataRow row)
        {
            LINKPayment obj = new LINKPayment();

            try
            {
                obj.Tot = Convert.ToDecimal(row["AMOUNT"].ToString());
                obj.Value =Convert.ToDecimal(row["PN"].ToString());
                obj.Description = row["NAME"].ToString();

            }
            catch (Exception ex)
            { }
            return obj;
        }
      }
}
