using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using System.Data;

namespace DataAccessHandler
{
    public class Roles
    {
        public bool Insert(EntityHandler.Roles obj)
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



        public bool Update(EntityHandler.Roles obj)
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


        public bool Delete(EntityHandler.Roles obj)
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

        public DataTable SelectAll(EntityHandler.Roles obj)
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
