using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using EntityHandler;
using System.IO;

/// <summary>
/// Summary description for DLAGetFinalCreditor
/// </summary>

namespace DataAccessHandler
{

    public class DALPayment
    {
        public DALPayment()
        {
            //
            // TODO: Add constructor logic here
            //
        }


      
        public DataTable DALGetFinalCreditor(LINKPayment objPayment)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_PaymentCreditorFinal";

                oSqlCommand.Parameters.AddWithValue("@SupName", objPayment.SupName);
                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPMRP(oSqlCommand);
                dt.TableName = "tblSupplier";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public DataTable DALGetFinalCreditorSupplierList()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_PaymentCreditorFinalSupllierList";

                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPMRP(oSqlCommand);
                dt.TableName = "tblSupplier";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public DataTable DALGetFinalCreditorGRN(LINKPayment objPayment)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_PaymentCreditorGetGRN";

                oSqlCommand.Parameters.AddWithValue("@SupName", objPayment.SupName);
                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPMRP(oSqlCommand);
                dt.TableName = "tblSupplier";
            }
            catch (Exception ex)
            {
                
            }
            return dt;
        }

        public DataTable DALGetFinalSupplier(LINKPayment objPayment)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_PaymentSupplier";

                oSqlCommand.Parameters.AddWithValue("@SupName", objPayment.SupName);
                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPMRP(oSqlCommand);
                dt.TableName = "tblSupplier";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public DataTable DALGetFinalSupplierByID(LINKPayment objPayment)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_PaymentSupplierByID";

                oSqlCommand.Parameters.AddWithValue("@SupplierID", objPayment.SupplierID);
                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPMRP(oSqlCommand);
                dt.TableName = "tblSupplier";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public DataTable DALGetGRNMaterial(LINKPayment objPayment)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_GRNMaterials";

                oSqlCommand.Parameters.AddWithValue("@GRNNO", objPayment.GRNNo);
                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPMRP(oSqlCommand);
                dt.TableName = "tblGRNMaterials";
            }
            catch (Exception ex)
            { }
            return dt;
        }

        public bool DALUpdateMaterial(LINKPayment objPayment)
        {
            bool result = false;

            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_UpdateStatusGRNMateril";
                oSqlCommand.Parameters.AddWithValue("@Status", objPayment.Status);
                oSqlCommand.Parameters.AddWithValue("@GRNNO", objPayment.GRNNo);
                oSqlCommand.Parameters.AddWithValue("@MaterialCode", objPayment.MaterialCode);
                oSqlCommand.CommandText = SqlQuery;
                result = new DALBase().UpdateMRP(oSqlCommand);
            }
            catch (Exception ex)
            {

                StreamWriter file2 = new StreamWriter(@"c:\file.txt", true);
                file2.WriteLine(ex.ToString() + "- " + DateTime.Now + "- meeeeeeeeeeeee");
                file2.Close();
            }

            return result;
        }

    }
}