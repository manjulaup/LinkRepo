using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessHandler
{
    public class Bank
    {
        public bool Insert(EntityHandler.Bank obj)
        {
            bool Status = false;
            try
            {
                string SqlCommand = "";

                Status = new DALBase().Insert(SqlCommand);
            }
            catch (Exception ex)
            { }
            return Status;
        }


        public bool Update(EntityHandler.Bank obj)
        {
            bool Status = false;
            try
            {
                string SqlCommand = "";

                Status = new DALBase().Update(SqlCommand);
            }
            catch (Exception ex)
            { }
            return Status;
        }


        public bool Delete(EntityHandler.Bank obj)
        {
            bool Status = false;
            try
            {
                string SqlCommand = "";

                Status = new DALBase().Update(SqlCommand);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_SelectBankALL";

                oSqlCommand.CommandText = SqlQuery;

                dt = new DALBase().SelectSPFinance(oSqlCommand);
                dt.TableName = "Bank";
            }
            catch (Exception ex)
            { }
            return dt;
        }


    }
}
