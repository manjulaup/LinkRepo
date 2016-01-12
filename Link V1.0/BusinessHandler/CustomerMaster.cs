using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessHandler
{
    public class CustomerMaster
    {
        public List<EntityHandler.CustomerMaster> BALGetCustomerMaster()
        {
            List<EntityHandler.CustomerMaster> listobj = new List<EntityHandler.CustomerMaster>();
            try
            {
                DataTable dt = new DataAccessHandler.CustomerMaster().DALGetCustomerMaster();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObject(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            { }
            return listobj;
        }
        private EntityHandler.CustomerMaster ConvertToObject(DataRow row)
        {
            EntityHandler.CustomerMaster obj = new EntityHandler.CustomerMaster();

            try
            {
                obj.Active = Convert.ToInt32(row["Active"].ToString());
                obj.Address = row["Address"].ToString();
                obj.ApprovedBy = Convert.ToInt32(row["ApprovedBy"].ToString());
                obj.ApprovedDate = Convert.ToDateTime(row["ApprovedDate"].ToString());
                obj.Area = Convert.ToInt32(row["Area"].ToString());
                obj.Category = row["Category"].ToString();
                obj.Credits_Limits = Convert.ToDecimal(row["Credits_Limits"].ToString());
                obj.CusFINCode = row["CusFINCode"].ToString();
                obj.Customer_Code = Convert.ToInt32(row["Customer_Code"].ToString());
                obj.Customer_Type = row["Customer_Type"].ToString();
                obj.Date = DateTime.Parse(row["Date"].ToString());
                obj.Name = row["Name"].ToString();
                obj.Other_Names = row["Other_Names"].ToString();
                obj.ProductCategory = row["ProductCategory"].ToString();
                obj.SalesMethod = row["SalesMethod"].ToString();
                obj.Status = Convert.ToInt32(row["Status"].ToString());
                obj.UserID = Convert.ToInt32(row["UserID"].ToString());
        


            }
            catch (Exception ex)
            { }
            return obj;
        }

    }
}
