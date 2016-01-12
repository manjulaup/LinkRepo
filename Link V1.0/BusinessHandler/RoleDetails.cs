using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessHandler;
using EntityHandler;

namespace BusinessHandler
{
    public class RoleDetails
    
    {
        public bool Insert(EntityHandler.RoleDetails obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.RoleDetails().Insert(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }


        public bool Update(EntityHandler.RoleDetails obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.RoleDetails().Update(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public bool Delete(EntityHandler.RoleDetails obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.RoleDetails().Delete(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public List<EntityHandler.RoleDetails> SelectAll(EntityHandler.RoleDetails obj)
        {
            List<EntityHandler.RoleDetails> listobj = new List<EntityHandler.RoleDetails>();
            try
            {
                DataTable dt = new DataAccessHandler.RoleDetails().SelectAll(obj);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObject(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            { }
            return listobj;
        }

        private EntityHandler.RoleDetails ConvertToObject(DataRow row)
        {
            EntityHandler.RoleDetails obj = new EntityHandler.RoleDetails();
            try
            {
                obj.RoleID = row["RoleID"].ToString();
               
                obj.ObjectCode = row["ObjectCode"].ToString();
                obj.PermID = row["PermID"].ToString();
                obj.HostID = row["HostID"].ToString();
                
                obj.CreatedUser = Convert.ToInt32(row["CreatedUser"].ToString());
                obj.CreatedDate = Convert.ToDateTime(row["CreatedDate"].ToString());
                obj.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"].ToString());
                obj.ModifiedUser = Convert.ToInt32(row["ModifiedUser"].ToString());
                

            }
            catch (Exception ex)
            { }
            return obj;
        }
    }
}
