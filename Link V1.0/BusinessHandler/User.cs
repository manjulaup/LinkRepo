using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using DataAccessHandler;
using System.Data;

namespace BusinessHandler
{
    public class User
    {
        public bool Insert(EntityHandler.User obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.User().Insert(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public bool Update(EntityHandler.User obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.User().Update(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public bool Delete(EntityHandler.User obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.User().Delete(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public List<EntityHandler.User> SelectAll(EntityHandler.User obj)
        {
            List<EntityHandler.User> listobj = new List<EntityHandler.User>();
            try
            {
                DataTable dt = new DataAccessHandler.User().SelectAll(obj);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObject(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            { }
            return listobj;
        }

        private EntityHandler.User ConvertToObject(DataRow row)
        {
            EntityHandler.User obj = new EntityHandler.User();
            try
            {
                obj.CreateDate = Convert.ToDateTime(row["CreateDate"].ToString());
                obj.CreateUser = Convert.ToInt32(row["CreateUser"].ToString());
                obj.UserID = Convert.ToInt32(row["UserID"].ToString());
               
                obj.HostID = row["HostID"].ToString();
                
                obj.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"].ToString());
                obj.ModifiedUser = Convert.ToInt32(row["ModifiedUser"].ToString());
                obj.Status = Convert.ToInt32(row["Status"].ToString());
                obj.Designation = row["Designation"].ToString();
                
                obj.FName = row["FName"].ToString();
                obj.LName = row["LName"].ToString();
                
                obj.UserName = row["Username"].ToString();
                obj.Password = row["Password"].ToString();
                obj.LastVisit = Convert.ToDateTime(row["LastVisit"].ToString());

            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
