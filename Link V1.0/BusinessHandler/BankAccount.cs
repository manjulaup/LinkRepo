using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccessHandler;

namespace BusinessHandler
{
    public class BankAccount
    {
        public DataTable BALGetBankAccAll(EntityHandler.Bank objBank)
        {
            DataTable dt = new DataTable();
            try
            {
                DataAccessHandler.BankAccount objDALBankAcc = new DataAccessHandler.BankAccount();
                dt = objDALBankAcc.SelectAll(objBank);

            }
            catch (Exception ex)
            {
            }

            return dt;
        }
    }
}
