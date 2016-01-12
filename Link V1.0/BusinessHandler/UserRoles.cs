using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using DataAccessHandler;
using System.Data;

namespace BusinessHandler
{
    public class UserRoles
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

        public List<EntityHandler.UserRoles> SelectAll(EntityHandler.UserRoles obj)
        {
            List<EntityHandler.UserRoles> listobj = new List<EntityHandler.UserRoles>();
            try
            {
                DataTable dt = new DataAccessHandler.UserRoles().SelectAll(obj);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObject(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            { }
            return listobj;
        }

        private EntityHandler.UserRoles ConvertToObject(DataRow row)
        {
            EntityHandler.UserRoles obj = new EntityHandler.UserRoles();
            try
            {

                obj.UserID = row["UserID"].ToString();
                obj.RoleID = row["RoleID"].ToString();
                obj.HostID = row["HostID"].ToString();

                obj.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"].ToString());
                obj.ModifiedUser = Convert.ToInt32(row["ModifiedUser"].ToString());
                obj.CreatedDate = Convert.ToDateTime(row["CreatedDate"].ToString());
                obj.CreatedUser = Convert.ToInt32(row["CreatedUser"].ToString());

            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
