using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using System.Data;

namespace DataAccessHandler
{
    public class UserRoles
    {
        public bool Insert(EntityHandler.UserRoles obj)
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



        public bool Update(EntityHandler.UserRoles obj)
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


        public bool Delete(EntityHandler.UserRoles obj)
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

        public DataTable SelectAll(EntityHandler.UserRoles obj)
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
