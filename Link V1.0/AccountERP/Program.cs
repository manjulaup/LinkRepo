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
            Application.Run(new frmAcknowledgement());
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
              //  public static List<UserRights> CurrentUserRight = new List<UserRights>();

            }  
        #endregion

            //public static bool HeCanDoThisOperation(int OBGID, int Right1)
            //{
            //    bool IsitFound = false;
            //    foreach (UserRights Oneitem in AccountStatic.CurrentUserRight)
            //    {
            //        if (Oneitem.ObgID == OBGID)
            //        {
            //            switch (Right1)
            //            {
            //                case 1:                 //Save
            //                    if (Oneitem.S == 1)
            //                        IsitFound = true;
            //                    else
            //                        IsitFound = false;
            //                    break;
            //                case 2:                 //Udate
            //                    if (Oneitem.U == 1)
            //                        IsitFound = true;
            //                    else
            //                        IsitFound = false;
            //                    break;
            //                case 3:                 //Delete
            //                    if (Oneitem.D == 1)
            //                        IsitFound = true;
            //                    else
            //                        IsitFound = false;
            //                    break;
            //                case 4:                 //Print
            //                    if (Oneitem.P == 1)
            //                        IsitFound = true;
            //                    else
            //                        IsitFound = false;
            //                    break;
            //                case 5:                 //Acc Approved
            //                    if (Oneitem.App == 1)
            //                        IsitFound = true;
            //                    else
            //                        IsitFound = false;
            //                    break;
            //                case 6:                 //Post to Acc
            //                    if (Oneitem.Acc == 1)
            //                        IsitFound = true;
            //                    else
            //                        IsitFound = false;
            //                    break;
            //                case 7:                 //Show
            //                    if (Oneitem.Sh == 1)
            //                        IsitFound = true;
            //                    else
            //                        IsitFound = false;
            //                    break;
            //                default:
            //                    break;
            //            }
            //            return IsitFound;
            //        }
            //    }
            //    return IsitFound;
            //}
    }
}
