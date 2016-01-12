using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using DataAccessHandler;
using System.Data;

namespace BusinessHandler
{
    public class Roles
    {
        public bool Insert(EntityHandler.Roles obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.Roles().Insert(obj);
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
                Status = new DataAccessHandler.Roles().Update(obj);
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
                Status = new DataAccessHandler.Roles().Delete(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public List<EntityHandler.Roles> SelectAll(EntityHandler.Roles obj)
        {
            List<EntityHandler.Roles> listobj = new List<EntityHandler.Roles>();
            try
            {
                DataTable dt = new DataAccessHandler.Roles().SelectAll(obj);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObject(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            { }
            return listobj;
        }

        private EntityHandler.Roles ConvertToObject(DataRow row)
        {
            EntityHandler.Roles obj = new EntityHandler.Roles();
            try
            {
                obj.RoleID = row["RoleID"].ToString();
               
                obj.RoleName = row["RoleName"].ToString();
                obj.HostID = row["HostID"].ToString();
                
                obj.CreateUser = Convert.ToInt32(row["CreateUser"].ToString());
                obj.CreateDate = Convert.ToDateTime(row["CreateDate"].ToString());
                obj.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"].ToString());
                obj.ModifiedUser = Convert.ToInt32(row["ModifiedUser"].ToString());
                obj.Status = Convert.ToInt32(row["Status"].ToString());

            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
