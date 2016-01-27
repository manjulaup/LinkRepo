using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.IO;


namespace DataAccessHandler
{
    public class DALBase
    {
        //FOR MRP
        public DataTable SelectSPMRP(SqlCommand oSqlCommand)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conn = DALConnManager.OpenMRP();

                oSqlCommand.Connection = conn;
                oSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adp = new SqlDataAdapter(oSqlCommand);
                adp.Fill(dt);
                conn.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
            return dt;
        }

        //FOR Finance
        public DataTable SelectSPFinance(SqlCommand oSqlCommand)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conn = DALConnManager.OpenFinance();

                oSqlCommand.Connection = conn;
                oSqlCommand.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adp = new SqlDataAdapter(oSqlCommand);
                adp.Fill(dt);
                conn.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
            return dt;
        }


        public bool Insert(SqlCommand oSqlCommand)
        {
            bool status = false;
            try
            {
               int rowAffected = oSqlCommand.ExecuteNonQuery();

               if (rowAffected > 0)
               {
                   status = true;
               }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return status;
        }

        public bool Update(SqlCommand oSqlCommand)
        {
            bool status = false;
            try
            {
                int rowAffected = oSqlCommand.ExecuteNonQuery();

                if (rowAffected > 0)
                {
                    status = true;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return status;
        }


        //Unallocated --Connection oriented
        public DataTable Select(string sql)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlConnection conn = DALConnManager.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(dt);
                conn.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
            return dt;
        }

        public bool Insert(string sql)
        {
            bool status = false;
            int rowAffected = 0;
            try
            {
                SqlConnection conn = DALConnManager.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                rowAffected = command.ExecuteNonQuery();
                DALConnManager.Close(conn);
                if (rowAffected > 0)
                    status = true;
            }
            catch (SqlException ex)
            {
                throw;
            }
            return status;
        }

        public bool Update(string sql)
        {
            bool status = false;
            int rowAffected = 0;
            try
            {
                SqlConnection conn = DALConnManager.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                rowAffected = command.ExecuteNonQuery();
                DALConnManager.Close(conn);
                if (rowAffected > 0)
                    status = true;
            }
            catch (SqlException ex)
            {
                throw;
            }
            return status;
        }

    }
}