using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AccountERP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MDIMain());
        }
        public static void InformationMessage(string Massage)
        {
            MessageBox.Show(Massage ,"Information", MessageBoxButtons.OK , MessageBoxIcon.Information);
        }
        public static void VerningMessage(string Massage)
        {
            MessageBox.Show(Massage, "E r r o r", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #region Public Variable
         
            public  struct AccountStatic
            {
                public static  string UserName;
                public static int  CompanyID;
                public static bool LoggingAsLocal;
                public static bool IsAuthenticated;
                public static string DBName;
                public static string DBUsername;
                public static string DBPasswd;
                public static string DBServerName;
                public static string JobNumber;
                public static string ChequeNumber;
                public static int CurrentAccPeriod;
                public static decimal ExchangeRate;
                public static string HomeCurreny;

            }  
        #endregion
    }
}
