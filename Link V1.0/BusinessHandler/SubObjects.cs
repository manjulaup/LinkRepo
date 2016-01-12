using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessHandler;
using EntityHandler;
using System.Data;

namespace BusinessHandler
{
    public class SubObjects
    {

        public bool Insert(EntityHandler.SubObjects obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.SubObjects().Insert(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }


        public bool Update(EntityHandler.SubObjects obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.SubObjects().Update(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public bool Delete(EntityHandler.SubObjects obj)
        {
            bool Status = false;
            try
            {
                Status = new DataAccessHandler.SubObjects().Delete(obj);
            }
            catch (Exception ex)
            { }
            return Status;
        }

        public List<EntityHandler.SubObjects> SelectAll(EntityHandler.SubObjects obj)
        {
            List<EntityHandler.SubObjects> listobj = new List<EntityHandler.SubObjects>();
            try
            {
                DataTable dt = new DataAccessHandler.SubObjects().SelectAll(obj);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObject(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            { }
            return listobj;
        }

        private EntityHandler.SubObjects ConvertToObject(DataRow row)
        {
            EntityHandler.SubObjects obj = new EntityHandler.SubObjects();
            try
            {

                obj.MainObjectCode = row["MainObjectCode"].ToString();
                obj.SubObjectCode = row["SubObjectCode"].ToString();
              

            }
            catch (Exception ex)
            { }
            return obj;
        }

    }
}
