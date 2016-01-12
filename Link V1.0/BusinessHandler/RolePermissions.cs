using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using DataAccessHandler;
using System.Data;

namespace BusinessHandler
{
    public class RolePermissions
    {

        public bool Insert(EntityHandler.RolePermissions obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.RolePermissions().Insert(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public bool Update(EntityHandler.RolePermissions obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.RolePermissions().Update(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public bool Delete(EntityHandler.RolePermissions obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.RolePermissions().Delete(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public List<EntityHandler.RolePermissions> SelectAll(EntityHandler.RolePermissions obj)
        {
            List<EntityHandler.RolePermissions> listobj = new List<EntityHandler.RolePermissions>();
            try
            {
                DataTable dt = new DataAccessHandler.RolePermissions().SelectAll(obj);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObject(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            { }
            return listobj;
        }

        private EntityHandler.RolePermissions ConvertToObject(DataRow row)
        {
            EntityHandler.RolePermissions obj = new EntityHandler.RolePermissions();
            try
            {

                obj.PermID = row["PermID"].ToString();
                obj.PermName = row["PermName"].ToString();
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
