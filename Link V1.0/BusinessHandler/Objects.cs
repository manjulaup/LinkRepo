using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using DataAccessHandler;
using System.Data;

namespace BusinessHandler
{
    public class Objects
    {
        public bool Insert(EntityHandler.Objects obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.Objects().Insert(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }


        public bool Update(EntityHandler.Objects obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.Objects().Update(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public bool Delete(EntityHandler.Objects obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.Objects().Delete(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public List<EntityHandler.Objects> SelectAll(EntityHandler.Objects obj)
        {
            List<EntityHandler.Objects> listobj = new List<EntityHandler.Objects>();
            try
            {
                DataTable dt = new DataAccessHandler.Objects().SelectAll(obj);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObject(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            { }
            return listobj;
        }

        private EntityHandler.Objects ConvertToObject(DataRow row)
        {
            EntityHandler.Objects obj = new EntityHandler.Objects();
            try
            {
                obj.ListOrdering = Convert.ToInt32(row["ListOrdering"].ToString());
                obj.ObjectCode = row["ObjectCode"].ToString();
                obj.ObjectFileName = row["ObjectFileName"].ToString();
                obj.Description = row["Description"].ToString();
                obj.LevelType = Convert.ToInt32(row["LevelType"].ToString());
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
