using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessHandler
{
    public class CustomerMaster
    {
        public CustomerMaster()
        { }

        public DataTable DALGetCustomerMaster()
        {
            DataTable dt = new DataTable();
            try
            {
                string SqlCommand = "SELECT * FROM [tblCustomerMaster]";
                dt = new DALBase().Select(SqlCommand);
                dt.TableName = "tblCustomerMaster";
            }
            catch (Exception ex)
            { }
            return dt;
        }

    }
}
