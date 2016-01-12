using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessHandler
{
    public class BankAccount
    {
        public bool Insert(EntityHandler.BankAccount obj)
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



        public bool Update(EntityHandler.BankAccount obj)
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


        public bool Delete(EntityHandler.BankAccount obj)
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

        public DataTable SelectAll(EntityHandler.Bank objBank)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand oSqlCommand = new SqlCommand();
                string SqlQuery = "WCF_SelectBankAccALL";

                oSqlCommand.CommandText = SqlQuery;
                oSqlCommand.Parameters.AddWithValue("@code",objBank.Code);

                dt = new DALBase().SelectSPFinance(oSqlCommand);
                dt.TableName = "tblBankAccount";
            }
            catch (Exception ex)
            { }
            return dt;
        }
    }
}
