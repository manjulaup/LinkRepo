using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntityHandler;
using DataAccessHandler;

namespace BusinessHandler
{
    public class BALBill
    {
        public List<Bill> BALGetFinalCreditor(Bill objBill)
        {
            List<Bill> listobj = new List<Bill>();
            DataTable dt = new DataTable();
            try
            {
                DALBill objDALBill = new DALBill();
                dt = objDALBill.DALGetGRNMaterial(objBill);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listobj.Add(ConvertToObject(dt.Rows[i]));
                }
            }
            catch (Exception ex)
            {

            }

            return listobj;
        }

        private Bill ConvertToObject(DataRow row)
        {
            Bill obj = new Bill();

            try
            {
                obj.FCr = Convert.ToDecimal(row["FCr"]);

            }
            catch (Exception ex)
            { }
            return obj;
        }
        
    }
}
