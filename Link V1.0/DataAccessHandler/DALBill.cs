using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntityHandler;

namespace DataAccessHandler
{
    public class DALBill
    {
        public DataTable DALGetGRNMaterial(Bill objBill)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "GetOutstandingBill";

                oSqlCommand.Parameters.AddWithValue("@SupplierID", objBill.SupplierID);
                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPMRP(oSqlCommand);
                dt.TableName = "tblpendingpayablebill";
            }
            catch (Exception ex)
            { }
            return dt;
        }
    }
}
