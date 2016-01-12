using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityHandler;
using DataAccessHandler;
using System.IO;
using System.Data;

namespace BusinessHandler
{
    public class Bank
    {

        public DataTable BALGetBankAll()
        {
            DataTable dt = new DataTable();
            try
            {
                DataAccessHandler.Bank objDALBank = new DataAccessHandler.Bank();
                dt = objDALBank.SelectAll();

            }
            catch (Exception ex)
            {
            }

            return dt;
        }

    }
}
