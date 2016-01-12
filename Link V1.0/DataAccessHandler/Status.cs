using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using System.Data;

namespace DataAccessHandler
{
    public class Status
    {
        public bool Insert(EntityHandler.Status obj)
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



        public bool Update(EntityHandler.Status obj)
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


        public bool Delete(EntityHandler.Status obj)
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

        public DataTable SelectAll(EntityHandler.Status obj)
        {
            DataTable dt = new DataTable();
            try
            {
                string SqlCommand = "";

                dt = new DALBase().Select(SqlCommand);
            }
            catch (Exception ex)
            { }
            return dt;
        }
    }
}
