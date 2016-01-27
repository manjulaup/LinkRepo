//Create by SATICIN On 06/Dec/2015
//
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using BusinessLayer.CommonOperation;
using BusinessLayer.PaymentsAndReciept;
using BusinessLayer.Billings;
using BusinessLayer.Journals;
using DataLayer.DataService;
using System.Threading;
using System.Drawing;
using BusinessLayer.Invoices;
using EntityHandler;
using BusinessHandler;

namespace BusinessLayer.AccountTranactions
{
    public class AccountTranaction
    {
        
        private DataService Mycommon;
        private CommonOperations MyGeneral;
      
        private bool IsLocalLoging=false ;
        public AccountTranaction(bool IsLocal)
        {
            Mycommon = new DataService(IsLocal);
            MyGeneral = new CommonOperations(IsLocal);
            IsLocalLoging = IsLocal;
          
        }
         ~AccountTranaction()
            {
                Mycommon.CloseDB();
            }
         public string DoReceipts(BusinessLayer.Receipts.REceiptTypes.ReceiptDataType _Receipt, string Usrname, out string FixRcpt)
         {
             FixRcpt = "";
             MySql.Data.MySqlClient.MySqlTransaction Mytrans;
             MySqlConnection CurCon = new MySqlConnection();
             CurCon = Mycommon.AccountConnection;
             string respond = "";
             if (CurCon.State == ConnectionState.Closed)
                 CurCon.Open();
             Mytrans = Mycommon.AccountConnection.BeginTransaction();
             MySqlCommand oSqlCommand = new MySqlCommand();

             string MainAccountRef = "", TrReference = "", RcptNumber = "";
             MainAccountRef = GetAccReference(_Receipt.AccountID, CurCon);

             respond = UpdateFixRCptHeader(_Receipt.ReceiptID, CurCon, out RcptNumber);
             if (respond != "True")
                 {
                     Mytrans.Rollback();
                     CurCon.Close();
                     CurCon.Dispose();
                     return respond;
                 }
             else
                 {
                    respond = UpdateFixRCptDetails(_Receipt.ReceiptID, RcptNumber, CurCon);
                    if (respond != "True")
                        {
                            Mytrans.Rollback();
                            CurCon.Close();
                            CurCon.Dispose();
                            return respond;
                        }
                    else
                        {
                            string DrAccRef = "";
                            int Counter = 0;
                            string Mackadd = MyGeneral.GetMACAddress();
                            foreach (BusinessLayer.Receipts.REceiptTypes.ReceiptDetailsDataType OneItem in _Receipt.ReceiptList )
                                {
                                    AccountTransactionType.AccountTransactionDataType _OneTrans = new AccountTransactionType.AccountTransactionDataType();
                                    string TransRefDr = "";
                                    string AccRef = GetAccReference(OneItem.AccID, CurCon);
                                    Counter += 1;
                                    if (Counter < 5)
                                        DrAccRef = DrAccRef + "," + AccRef;

                                    _OneTrans.AccountID = OneItem.AccID;                //1
                                    _OneTrans.AccountYear = _Receipt.AccPeriod;         //2
                                    _OneTrans.CompanyID = _Receipt.CompanyID;           //3
                                    _OneTrans.Cr = OneItem.Cr;                          //4
                                    _OneTrans.CurRate = OneItem.Exrate;                 //5
                                    _OneTrans.Description = OneItem.Description;        //6
                                    _OneTrans.Dr = 0;                                   //7
                                    _OneTrans.FCr = OneItem.FCr;                        //8
                                    _OneTrans.FDr =0 ;                                  //9
                                    _OneTrans.MainRef = AccRef;                         //10
                                    _OneTrans.PayMethod = _Receipt.ReceiptMethod;       //11
                                    _OneTrans.RelRef = MainAccountRef;                  //12
                                    _OneTrans.TransType = 4;                            //13
                                    _OneTrans.TrMachine = Mackadd;                      //14
                                    _OneTrans.VoucherID = RcptNumber;                   //15
                                    _OneTrans.TrUser = Usrname;                         //16
                                    _OneTrans.ActualDate = _Receipt.RcptActualDate;     //17

                                    respond = SaveAccountTransaction(_OneTrans, CurCon, out TransRefDr);
                                    if (respond != "True")
                                        {
                                            Mytrans.Rollback();
                                            CurCon.Close();
                                            CurCon.Dispose();
                                            return respond;
                                        }
                                    else
                                        {
                                        respond = UpdateTranseRefinReleventTable(5, false, TransRefDr, RcptNumber, OneItem.ItemNo.ToString(), CurCon);
                                        if (respond != "True")
                                            {
                                            Mytrans.Rollback();
                                            CurCon.Close();
                                            CurCon.Dispose();
                                            return respond;
                                            }
                                        else
                                            {

                                            respond = UpdateLastRefonAccount(_OneTrans.AccountID, AccRef, CurCon);
                                            if (respond != "True")
                                                {
                                                Mytrans.Rollback();
                                                CurCon.Close();
                                                CurCon.Dispose();
                                                return respond;
                                                }
                                            }
                                        }
                                }
                            //==================
                            if (respond != "True")
                                {
                                Mytrans.Rollback();
                                CurCon.Close();
                                CurCon.Dispose();
                                return respond;
                                }
                            else
                                {
                                    AccountTransactionType.AccountTransactionDataType _SaveTranData = new AccountTransactionType.AccountTransactionDataType();
                                    _SaveTranData.AccountID = _Receipt.AccountID;       //1
                                    _SaveTranData.AccountYear = _Receipt.AccPeriod;     //2
                                    _SaveTranData.Description = _Receipt.Description;   //3
                                    _SaveTranData.CompanyID = _Receipt.CompanyID;       //4
                                    _SaveTranData.Cr =0;                     //5
                                    _SaveTranData.CurRate = _Receipt.CurRate;           //6
                                    _SaveTranData.Dr = _Receipt.Dr ;                               //7
                                    _SaveTranData.FCr = 0; //8
                                    _SaveTranData.FDr = _Receipt.FDr ;                              //9
                                    _SaveTranData.MainRef = MainAccountRef;             //10
                                    _SaveTranData.PayMethod = _Receipt.ReceiptMethod;   //11
                                    _SaveTranData.TrID = TrReference;                   //12
                                    _SaveTranData.TransType = 4;                        //13
                                    _SaveTranData.RelRef = DrAccRef;                    //14
                                    _SaveTranData.TrMachine = Mackadd;                  //14
                                    _SaveTranData.VoucherID = RcptNumber;                //15
                                    _SaveTranData.TrUser = Usrname;                    //16
                                    _SaveTranData.ActualDate = _Receipt.RcptActualDate;  //17  
                                    string MainTrRef1 = "";
                                    respond = SaveAccountTransaction(_SaveTranData, CurCon, out MainTrRef1);
                                    if (respond != "True")
                                    {
                                        Mytrans.Rollback();
                                        CurCon.Close();
                                        return respond;
                                    }
                                    else
                                    {
                                        respond = UpdateTranseRefinReleventTable(5, true, MainTrRef1, RcptNumber, "", CurCon);
                                        if (respond != "True")
                                        {
                                            Mytrans.Rollback();
                                            CurCon.Close();
                                            return respond;
                                        }
                                        else
                                        {
                                            respond = UpdateLastRCPT( RcptNumber, CurCon);
                                            if (respond != "True")
                                            {
                                                Mytrans.Rollback();
                                                CurCon.Close();
                                                return respond;
                                            }
                                            else
                                            {
                                                respond = SetReceipVoucherAsAccounted(RcptNumber, Usrname, CurCon);
                                                if (respond == "True")
                                                {
                                                    Mytrans.Commit();
                                                    CurCon.Close();
                                                    return "True";
                                                }
                                                else
                                                {
                                                    Mytrans.Rollback();
                                                    CurCon.Close();
                                                    return respond;
                                                }
                                            }
                                        }
                                    }  
                                }
                        //========
                        }
                 }
             return "True";
         }
        public string DoPaymentTransaction(AccountTypes.Payment_GeneralDataType _Payment,string UserName, out string FixedPVN)
            {
            FixedPVN = "";
            MySql.Data.MySqlClient.MySqlTransaction Mytrans;
            MySqlConnection CurCon = new MySqlConnection();
            CurCon = Mycommon.AccountConnection;
            string respond = "";
            if (CurCon.State == ConnectionState.Closed)
                CurCon.Open();
            Mytrans = Mycommon.AccountConnection.BeginTransaction();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string MainAccountRef = "", TrReference = "",PVNNumber="";
            MainAccountRef=  GetAccReference(_Payment.AccountID,CurCon) ;
          
            respond = UdateFixPVNHeader(_Payment.AccountID, _Payment.PaymentID, out PVNNumber, CurCon);
            FixedPVN = PVNNumber;
            if (respond != "True")
                {
                Mytrans.Rollback();
                CurCon.Close();
                return respond;
                }
            else
            {
                respond= UpdateFixPVNDetails(_Payment.PaymentID, PVNNumber, CurCon);
                if (respond != "True")
                    {
                    Mytrans.Rollback();
                    CurCon.Close();
                    return respond;
                    }
                else
                {
                string DrAccRef = "";
                int Counter = 0;
                    string Mackadd=MyGeneral.GetMACAddress();
                foreach (AccountTypes.Payment_GeneralDetailsDataType OneItem in _Payment.Details)
                    {
                        AccountTransactionType.AccountTransactionDataType _OneTrans = new AccountTransactionType.AccountTransactionDataType();
                        string TransRefDr="";
                        
                        string AccRef = GetAccReference(OneItem.AccID, CurCon);
                        Counter +=1;
                        if (Counter < 5)
                            DrAccRef = DrAccRef + "," + AccRef;

                        _OneTrans.AccountID = OneItem.AccID;                //1
                        _OneTrans.AccountYear = _Payment.AccPeriod;         //2
                        _OneTrans.CompanyID = _Payment.CompanyID;           //3
                        _OneTrans.Cr = 0;                                   //4
                        _OneTrans.CurRate = OneItem.Exrate;               //5
                        _OneTrans.Description = OneItem.Description;        //6
                        _OneTrans.Dr = OneItem.Dr;                          //7
                        _OneTrans.FCr = 0;                                  //8
                        _OneTrans.FDr =  OneItem.Fdr;                       //9
                        _OneTrans.MainRef = AccRef;                         //10
                        _OneTrans.PayMethod = _Payment.PaymentMethod;       //11
                        _OneTrans.RelRef = MainAccountRef;                  //12
                        _OneTrans.TransType = 1;                            //13
                        _OneTrans.TrMachine = Mackadd;                      //14
                        _OneTrans.VoucherID = PVNNumber;                    //15
                        _OneTrans.TrUser = UserName;                        //16
                        _OneTrans.ActualDate = _Payment.PayActualDate;      //17
                      
                        respond = SaveAccountTransaction(_OneTrans, CurCon, out TransRefDr);
                        if (respond != "True")
                            {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return respond;
                            }
                        else
                            {
                           
                               
                            respond = UpdateTranseRefinReleventTable(1, true, TransRefDr, PVNNumber, OneItem.ItemNo.ToString (), CurCon);
                            if (respond != "True")
                                {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return respond;
                                }
                            else
                                {
                                
                                    respond = UpdateLastRefonAccount(_OneTrans.AccountID, AccRef, CurCon);
                                    if (respond != "True")
                                        {
                                            Mytrans.Rollback();
                                            CurCon.Close();
                                            return respond;
                                        }
                                }
                            }
                        
                    }

                    //===========
                    if (respond != "True")
                        {
                            Mytrans.Rollback();
                            CurCon.Close();
                            return respond;
                        }
                    else
                        {
                        //Credit Side Transaction
                            AccountTransactionType.AccountTransactionDataType _SaveTranData = new AccountTransactionType.AccountTransactionDataType();
                            _SaveTranData.AccountID = _Payment.AccountID;       //1
                            _SaveTranData.AccountYear = _Payment.AccPeriod;     //2
                            _SaveTranData.Description = _Payment.Description;   //3
                            _SaveTranData.CompanyID = _Payment.CompanyID;       //4
                            _SaveTranData.Cr = _Payment.Cr;                     //5
                            _SaveTranData.CurRate = _Payment.CurRate;           //6
                            _SaveTranData.Dr = 0;                               //7
                            _SaveTranData.FCr = _Payment.FCr; //8
                            _SaveTranData.FDr = 0;                              //9
                            _SaveTranData.MainRef = MainAccountRef;             //10
                            _SaveTranData.PayMethod = _Payment.PaymentMethod;   //11
                            _SaveTranData.TrID = TrReference;                   //12
                            _SaveTranData.TransType = 1;                        //13
                            _SaveTranData.RelRef = DrAccRef;                    //14
                            _SaveTranData.TrMachine = Mackadd;                  //14
                            _SaveTranData.VoucherID = PVNNumber;                //15
                            _SaveTranData.TrUser = UserName;                    //16
                            _SaveTranData.ActualDate = _Payment.PayActualDate;  //17  
                            string MainTrRef1 = "";
                            respond = SaveAccountTransaction(_SaveTranData, CurCon, out MainTrRef1);
                            if (respond != "True")
                                {
                                    Mytrans.Rollback();
                                    CurCon.Close();
                                    return respond;
                                }
                            else
                            {
                                respond = UpdateTranseRefinReleventTable(1, false, MainTrRef1, PVNNumber, "", CurCon);
                                if (respond != "True")
                                    {
                                    Mytrans.Rollback();
                                    CurCon.Close();
                                    return respond;
                                    }
                                else
                                    {
                                    respond = UpdateLastPVN(_Payment.AccountID, PVNNumber, CurCon);
                                    if (respond != "True")
                                        {
                                            Mytrans.Rollback();
                                            CurCon.Close();
                                            return respond;
                                        }
                                    else
                                        {
                                            respond = SetPaymentVoucherAsAccounted(PVNNumber, UserName, CurCon);
                                            if (respond == "True")
                                            {

                                                Mytrans.Commit();
                                                CurCon.Close();
                                                return "True";
                                            }
                                            else
                                            {
                                                Mytrans.Rollback();
                                                CurCon.Close();
                                                return respond;
                                            }
                                        }
                                    }
                            }
                        }
                    //===========

                }



            }
            return "True";
            }
        public string DataTransformFromPaymentToAdvance(PaymentsAndReciept.AccountTypes.Payment_GeneralDataType _Payment,out PaymentsAndReciept.AccountTypes.AdvancePaymentDataType _AdvanceData)
        {
                 _AdvanceData = new AccountTypes.AdvancePaymentDataType(); 
            try
                {
                    _AdvanceData.AdvDate = _Payment.PayActualDate;
                    _AdvanceData.AdvStatus = 0;
                    _AdvanceData.AdvType = _Payment.PayToCatID;
                    _AdvanceData.Dr  = _Payment.Cr ;
                    _AdvanceData.FDr  = _Payment.FCr ;
                    _AdvanceData.Exrate  = _Payment.CurRate ;
                    _AdvanceData.IssuedReference  = _Payment.PaymentID ;
                    _AdvanceData.RvcAmount  = 0;
                    _AdvanceData.ToID  = 0;
                    _AdvanceData.ToName  = _Payment.PayToName ;
                    
                    
                    
                return "True";
                }
            catch (Exception ex)
                {
                    return ex.Message;
                } 
        }
        public string SetReceipVoucherAsAccounted(string RcptID, string User1, MySqlConnection _ActCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
            {
            string sql1 = "Update tblreceipt set ReceiptStatus=3,AcountedBy=@TrUser,AccountDate=curDate(),AccountedTime=curtime() where ReceiptID=@ReceiptID";
                oSqlCommand.Parameters.AddWithValue("@TrUser", User1);
                oSqlCommand.Parameters.AddWithValue("@ReceiptID", RcptID);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, _ActCon, "Exist bill details");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public string SetPaymentVoucherAsAccounted(string PVN, string User1, MySqlConnection _ActCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
            {
                string sql1 = "Update tblpayment set PayStatus=3,AcountedBy=@TrUser,AccountDate=curDate(),AccountedTime=curtime() where PaymentID=@PaymentID";
                oSqlCommand.Parameters.AddWithValue("@TrUser", User1);
                oSqlCommand.Parameters.AddWithValue("@PaymentID", PVN);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, _ActCon, "Exist bill details");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public string SetBillGetToLog(string GRNNumber, int SupID)
        {
            string sql1 = "Update tblrowmaterilgrnreceive set Billed=1 where GRNnumber='" + GRNNumber + "' and GRNSupplier=" + SupID;
            string respond = Mycommon.ExicuteAnyCommand(sql1, "Update Bill as a loged");
            return respond;
        }
        public string JEAccounted(string JE, string UserN, MySqlConnection _ActCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
            {
                string sql1 = "Update tbljournalheader set AcountedBy=@AcountedBy,JeStatus=3,AccountDate=curdate(),AccountedTime=curTime() where JounalID=@JounalID";
                oSqlCommand.Parameters.AddWithValue("@JounalID", JE);
                oSqlCommand.Parameters.AddWithValue("@AcountedBy", UserN);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, _ActCon, "Update TR ref on Journal details");
                return respond;
            }
            catch (Exception ex)
            {
                return "True";
            }
        }
        public string DoJournal(JournalType.JournalDataDataType _DoTransData,string UserID)
            {
                try
                    {

                        MySql.Data.MySqlClient.MySqlTransaction Mytrans;
                        MySqlConnection CurCon = new MySqlConnection();
                        CurCon = Mycommon.AccountConnection;
                        string respond = "";
                        decimal FullPaymentDecbit = 0, FullPaymentCredit=0;
                        if (CurCon.State == ConnectionState.Open)
                            {
                            CurCon.Close();
                            CurCon.Open();
                            }
                        else
                            {
                            CurCon.Open();
                            }
                        Mytrans = Mycommon.AccountConnection.BeginTransaction();
                        MySqlCommand oSqlCommand = new MySqlCommand();
                        string Mackadd = MyGeneral.GetMACAddress();
                        respond = JEAccounted(_DoTransData.JounalID, UserID, CurCon);
                        if (respond != "True")
                        {
                            Mytrans.Rollback();
                            CurCon.Close();
                            return respond;
                        }
                        else
                        {
                            foreach (JournalType.JournalDetailsDataType _OneItem in _DoTransData.DetailList)
                            {
                                AccountTransactionType.AccountTransactionDataType _OneTrans = new AccountTransactionType.AccountTransactionDataType();
                                string TransRefDr = "";

                                string AccRef = GetAccReference(_OneItem.AccountID, CurCon);

                                _OneTrans.AccountID = _OneItem.AccountID;
                                _OneTrans.AccountYear = _DoTransData.TimePeriod;
                                _OneTrans.ActualDate = _DoTransData.Jedate;
                                _OneTrans.CompanyID = _DoTransData.CompanyID;
                                _OneTrans.Cr = _OneItem.Cr;
                                _OneTrans.CurRate = _OneItem.CurRate;
                                _OneTrans.Description = _OneItem.Description;
                                _OneTrans.Dr = _OneItem.Dr;
                                _OneTrans.FCr = _OneItem.Cr / _OneItem.CurRate;
                                _OneTrans.FDr = _OneItem.Dr / _OneItem.CurRate;
                                _OneTrans.MainRef = AccRef;
                                _OneTrans.PayMethod = -1;
                                _OneTrans.RelRef = _DoTransData.JounalID;
                                _OneTrans.TransType = 3;
                                _OneTrans.TrMachine = Mackadd;
                                _OneTrans.TrUser = _DoTransData.JeUser;
                                _OneTrans.VoucherID = _DoTransData.JounalID;

                                FullPaymentDecbit = FullPaymentDecbit + _OneItem.Dr;
                                FullPaymentCredit = FullPaymentCredit + _OneItem.Cr;

                                respond = SaveAccountTransaction(_OneTrans, CurCon, out TransRefDr);
                                if (respond != "True")
                                {
                                    Mytrans.Rollback();
                                    CurCon.Close();
                                    return respond;
                                }
                                else
                                {
                                    respond = UpdateTranseRefinReleventTable(3, true, TransRefDr, _OneItem.JeNumber, _OneItem.LineRef.ToString(), CurCon);
                                    if (respond != "True")
                                    {
                                        Mytrans.Rollback();
                                        CurCon.Close();
                                        return respond;
                                    }
                                    else
                                    {
                                        respond = UpdateLastRefonAccount(_OneTrans.AccountID, AccRef, CurCon);
                                        if (respond != "True")
                                        {
                                            Mytrans.Rollback();
                                            CurCon.Close();
                                            return respond;
                                        }
                                    }
                                }

                            }
                            //====
                            if ((FullPaymentDecbit - FullPaymentCredit) != 0)
                            {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return respond;
                            }
                            else
                            {
                                if (respond != "True")
                                {
                                    Mytrans.Rollback();
                                    CurCon.Close();
                                    return respond;
                                }
                                else
                                {
                                    Mytrans.Commit();
                                    CurCon.Close();
                                    return "True";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    return ex.Message;
                    }
            }
        public string DoInvoice(InvoiceDataTypes.InvoiceDataType _SaveData)
        {
            Invoice MyInvoice = new Invoice(IsLocalLoging);
            //===================================================================
            try
                {
                MySql.Data.MySqlClient.MySqlTransaction Mytrans;
                MySqlConnection CurCon = new MySqlConnection();
                CurCon = Mycommon.AccountConnection;
                string respond = "";
                decimal FullPayment = 0;
                if (CurCon.State == ConnectionState.Open)
                    {
                    CurCon.Close();
                    CurCon.Open();
                    }
                else
                    {
                    CurCon.Open();
                    }
                Mytrans = Mycommon.AccountConnection.BeginTransaction();
                //===============================================================
                
                string MainAccountRef = "", TrReference = "", PVNNumber = "";
                MainAccountRef = GetAccReference(_SaveData.AccountID, CurCon);
                int Counter = 0;
                string DrAccRef = "";
                string macAdd = MyGeneral.GetMACAddress();

                if (_SaveData.InvoiceDtails.Count > 0)
                    {
                    foreach (InvoiceDataTypes.InvoiceDetailsDataType _OneData in _SaveData.InvoiceDtails)
                        {
                            AccountTransactionType.AccountTransactionDataType _OneTrans = new AccountTransactionType.AccountTransactionDataType();
                            string TransRefDr = "";

                            string AccRef = GetAccReference(_OneData.AccID, CurCon);
                            Counter += 1;
                            if (Counter < 5)
                                DrAccRef = DrAccRef + "," + AccRef;
                            _OneTrans.AccountID = _OneData.AccID;
                            _OneTrans.AccountYear = _SaveData.AccPeriod;
                            _OneTrans.ActualDate = _SaveData.InvoiceDate;
                            _OneTrans.CompanyID = _SaveData.CompanyID;
                            _OneTrans.Cr =  _OneData.Cr;
                            _OneTrans.CurRate = _SaveData.CurRate;
                            _OneTrans.Description = _OneData.Description;
                            _OneTrans.Dr =0;
                            _OneTrans.FDr = 0;
                            _OneTrans.FCr = _OneData.FCr;
                            _OneTrans.MainRef = AccRef;
                            _OneTrans.PayMethod = 5;
                            _OneTrans.RelRef = MainAccountRef;
                            _OneTrans.TransType = 4;
                            _OneTrans.TrMachine = macAdd;
                            _OneTrans.TrUser = _SaveData.TrUser;
                            _OneTrans.VoucherID = _SaveData.InvoiceNo;
                            FullPayment = FullPayment + _OneData.Cr;
                            respond = SaveAccountTransaction(_OneTrans, CurCon, out TransRefDr);
                            if (respond != "True")
                                {
                                    Mytrans.Rollback();
                                    CurCon.Close();
                                    return respond;
                                }
                            else
                            {
                            respond = UpdateTranseRefinReleventTable(4, false, TransRefDr, _SaveData.InvoiceNo , _OneData.ItemNo.ToString(), CurCon);
                            if (respond != "True")
                                {
                                    Mytrans.Rollback();
                                    CurCon.Close();
                                    return respond;
                                }
                            else
                                {
                                respond = UpdateLastRefonAccount(_OneTrans.AccountID, AccRef, CurCon);
                                if (respond != "True")
                                    {
                                        Mytrans.Rollback();
                                        CurCon.Close();
                                        return respond;
                                    }
                                }
                            }
                        }

                        if (respond != "True")
                            {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return respond;
                            }
                            else
                            {
                            AccountTransactionType.AccountTransactionDataType _SaveTranData = new AccountTransactionType.AccountTransactionDataType();

                                _SaveTranData.AccountID = _SaveData.AccountID;       //1
                                _SaveTranData.AccountYear = _SaveData.AccPeriod;     //2
                                _SaveTranData.Description = _SaveData.Description;   //3
                                _SaveTranData.CompanyID = _SaveData.CompanyID;       //4
                                _SaveTranData.Cr = 0;                                //5
                                _SaveTranData.CurRate = _SaveData.CurRate;           //6
                                _SaveTranData.Dr = _SaveData.Dr;                     //7
                                _SaveTranData.FCr = 0;                               //8
                                _SaveTranData.FDr = _SaveData.FDr; ;                 //9
                                _SaveTranData.MainRef = MainAccountRef;              //10
                                _SaveTranData.PayMethod = 5;                         //11
                                _SaveTranData.TrID = TrReference;                    //12
                                _SaveTranData.TransType = 2;                         //13
                                _SaveTranData.RelRef = DrAccRef;                     //14
                                _SaveTranData.TrMachine = macAdd;                    //14
                                _SaveTranData.VoucherID = _SaveData.InvoiceNo ;      //15
                                _SaveTranData.TrUser = _SaveData.TrUser;             //16
                                _SaveTranData.ActualDate = _SaveData.InvoiceDate;    //17
                                respond = SaveAccountTransaction(_SaveTranData, CurCon, out TrReference);
                                if (respond != "True")
                                    {
                                        Mytrans.Rollback();
                                        CurCon.Close();
                                        return respond;
                                    }
                                else
                                    {
                                    respond = UpdateTranseRefinReleventTable(4, true, MainAccountRef, _SaveData.InvoiceNo, "", CurCon);
                                    if (respond != "True")
                                        {
                                        Mytrans.Rollback();
                                        CurCon.Close();
                                        return respond;
                                        }
                                    else
                                        {
                                        respond = UpdateLastRefonAccount(_SaveData.AccountID, MainAccountRef, CurCon);
                                        if (respond != "True")
                                            {
                                            Mytrans.Rollback();
                                            CurCon.Close();
                                            return respond;
                                            }
                                        else
                                            { 
                                                //=====================================
                                                InvoiceDataTypes.PendingInvoiceDataType _PendingInvoice = new InvoiceDataTypes.PendingInvoiceDataType();
                                                _PendingInvoice.AccountID = _SaveData.AccountID;
                                                _PendingInvoice.AccPeriod = _SaveData.AccPeriod;
                                                _PendingInvoice.BillStatus = 0;
                                                _PendingInvoice.CompanyID = _SaveData.CompanyID;
                                                _PendingInvoice.CurRate = _SaveData.CurRate;
                                                _PendingInvoice.Customer = _SaveData.RcvFromID;
                                                _PendingInvoice.Description = _SaveData.Description;
                                                _PendingInvoice.Dr = _SaveData.Dr;
                                                _PendingInvoice.FDr = _SaveData.FDr;
                                                _PendingInvoice.InvoiceNo = _SaveData.InvoiceNo;
                                                _PendingInvoice.InvoiceNoDate = _SaveData.InvoiceDate;
                                                _PendingInvoice.RevievedAmount = 0;
                                                _PendingInvoice.TobeRcvDate = _SaveData.TobeRcvDate;
                                                _PendingInvoice.TotalAmount = _SaveData.Dr;
                                                respond = MyInvoice.SavePending(_PendingInvoice, CurCon);
                                                if (respond != "True")
                                                    {
                                                    Mytrans.Rollback();
                                                    CurCon.Close();
                                                    return respond;
                                                    }
                                                else
                                                    {
                                                        if (respond != "True")
                                                        {
                                                            Mytrans.Rollback();
                                                            CurCon.Close();
                                                            return respond;
                                                        }
                                                        else
                                                        {
                                                            respond =MyInvoice .InvoiceAccountUpdated( _SaveData.InvoiceNo, _SaveData.TrUser,CurCon);
                                                            if (respond == "True")
                                                            {
                                                                Mytrans.Commit();
                                                                CurCon.Close();
                                                                return "True";
                                                            }
                                                            else
                                                            {
                                                                Mytrans.Rollback();
                                                                CurCon.Close();
                                                                return respond;
                                                            }
                                                           
                                                        }
                                                      
                                                    }
                                                //=====================================
                                            }
                                        }
                                    }
                            }
                    }
                else
                    {
                    Mytrans.Rollback();
                    CurCon.Close();
                    return "No Details to save";
                    }

                }
            catch (Exception ex)
                {
                return ex.Message;
                }
        }

        

        public string DoBillTransaction(BillingDataTypes.BillingDataType _SaveData)
        {
            Billing  MyBill= new Billing(IsLocalLoging);
            try
            {
                MySql.Data.MySqlClient.MySqlTransaction Mytrans;
                MySqlConnection CurCon = new MySqlConnection();
                CurCon = Mycommon.AccountConnection;
                string respond = "";
                decimal FullPayment = 0;
                if (CurCon.State == ConnectionState.Open)
                {
                    CurCon.Close();
                    CurCon.Open();
                }
                else
                {
                    CurCon.Open();
                }
                Mytrans = Mycommon.AccountConnection.BeginTransaction();
                MySqlCommand oSqlCommand = new MySqlCommand();

                string MainAccountRef = "", TrReference = "", PVNNumber = "";
                MainAccountRef = GetAccReference(_SaveData.AccountID, CurCon);
                int Counter = 0;
                string DrAccRef = "";
                string macAdd = MyGeneral.GetMACAddress();
                foreach (BillingDataTypes.BillingDetailsDataType _OneData in _SaveData.BillingDetails)
                {
                    AccountTransactionType.AccountTransactionDataType _OneTrans = new AccountTransactionType.AccountTransactionDataType();
                    string TransRefDr = "";

                    string AccRef = GetAccReference(_OneData.AccID, CurCon);
                    Counter += 1;
                    if (Counter < 5)
                        DrAccRef = DrAccRef + "," + AccRef;
                    _OneTrans.AccountID = _OneData.AccID;
                    _OneTrans.AccountYear = _SaveData.AccPeriod;
                    _OneTrans.ActualDate = _SaveData.BillDate;
                    _OneTrans.CompanyID = _SaveData.CompanyID;
                    _OneTrans.Cr = 0;
                    _OneTrans.CurRate = _SaveData.CurRate;
                    _OneTrans.Description = _OneData.Description;
                    _OneTrans.Dr = _OneData.Dr;
                    _OneTrans.FCr = 0;
                    _OneTrans.FDr = _OneData.Fdr ;
                    _OneTrans.MainRef = AccRef;
                    _OneTrans.PayMethod = 5;
                    _OneTrans.RelRef = MainAccountRef;
                    _OneTrans.TransType = 2;
                    _OneTrans.TrMachine = macAdd;
                    _OneTrans.TrUser = _SaveData.TrUser;
                    _OneTrans.VoucherID = _SaveData.BillNo;
                    FullPayment = FullPayment + _OneData.Dr;
                    respond = SaveAccountTransaction(_OneTrans, CurCon, out TransRefDr);
                    if (respond != "True")
                    {
                        Mytrans.Rollback();
                        CurCon.Close();
                        return respond;
                    }
                    else
                    {
                        respond = UpdateTranseRefinReleventTable(2, true, TransRefDr, _SaveData.BillNo, _OneData.ItemNo.ToString(), CurCon);
                        if (respond != "True")
                        {
                            Mytrans.Rollback();
                            CurCon.Close();
                            return respond;
                        }
                        else
                        {
                            respond = UpdateLastRefonAccount(_OneTrans.AccountID, AccRef, CurCon);
                            if (respond != "True")
                            {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return respond;
                            }
                        }
                    }

                }

                if (respond != "True")
                {
                    Mytrans.Rollback();
                    CurCon.Close();
                    return respond;
                }
                else
                {
                    AccountTransactionType.AccountTransactionDataType _SaveTranData = new AccountTransactionType.AccountTransactionDataType();
                    
                    _SaveTranData.AccountID = _SaveData.AccountID;       //1
                    _SaveTranData.AccountYear = _SaveData.AccPeriod;     //2
                    _SaveTranData.Description = _SaveData.Description;   //3
                    _SaveTranData.CompanyID = _SaveData.CompanyID;       //4
                    _SaveTranData.Cr = _SaveData.Cr;                     //5
                    _SaveTranData.CurRate = _SaveData.CurRate;           //6
                    _SaveTranData.Dr = 0;                               //7
                    _SaveTranData.FCr = _SaveData.FCr ; //8
                    _SaveTranData.FDr = 0;                              //9
                    _SaveTranData.MainRef = MainAccountRef;             //10
                    _SaveTranData.PayMethod = 5;   //11
                    _SaveTranData.TrID = TrReference;                   //12
                    _SaveTranData.TransType = 2;                        //13
                    _SaveTranData.RelRef = DrAccRef;                    //14
                    _SaveTranData.TrMachine = macAdd;                  //14
                    _SaveTranData.VoucherID = _SaveData.BillNo;       //15
                    _SaveTranData.TrUser = _SaveData.TrUser;          //16
                    _SaveTranData.ActualDate = _SaveData.BillDate;

                    respond = SaveAccountTransaction(_SaveTranData, CurCon, out TrReference);

                    if ( FullPayment != _SaveData.Cr)
                    {
                        Mytrans.Rollback();
                        CurCon.Close();
                        return "Amount Not balance";
                    }

                    if (respond != "True")
                    {
                        Mytrans.Rollback();
                        CurCon.Close();
                        return respond;
                    }
                    else
                    {
                        respond = UpdateTranseRefinReleventTable(2, false, TrReference, _SaveData.BillNo, "", CurCon);
                        if (respond != "True")
                        {
                            Mytrans.Rollback();
                            CurCon.Close();
                            return respond;
                        }
                        else
                        {
                            respond = UpdateLastRefonAccount(_SaveData.AccountID, MainAccountRef, CurCon);
                            if (respond != "True")
                            {
                                Mytrans.Rollback();
                                CurCon.Close();
                                return respond;
                            }
                            else
                            {
                                BillingDataTypes.PendingPaymentDataType _SavependingData = new BillingDataTypes.PendingPaymentDataType();
                                _SavependingData.AccountID = _SaveData.AccountID;
                                _SavependingData.AccPeriod = _SaveData.AccPeriod;
                                _SavependingData.BillDate = _SaveData.BillDate;
                                _SavependingData.BillNo = _SaveData.BillNo;
                                _SavependingData.BillStatus = 0;
                                _SavependingData.CompanyID = _SaveData.CompanyID;
                                _SavependingData.Cr = _SaveData.Cr;
                                _SavependingData.CurRate = _SaveData.CurRate;
                                _SavependingData.Description = _SaveData.Description;
                                _SavependingData.FCr = _SaveData.FCr;
                                _SavependingData.PayedAmount = 0;
                                _SavependingData.Supplier = _SaveData.PayToID;
                                _SavependingData.TobePayDate = _SaveData.TobePayDate;
                                _SavependingData.TotalAmount = _SaveData.Cr;
                                 respond = MyBill.SavePP(_SavependingData, CurCon);
                                if (respond != "True")
                                {
                                    Mytrans.Rollback();
                                    CurCon.Close();
                                    return respond;
                                }
                                else
                                {
                                  //respond =  SetBillGetToLog(_SaveData.BillNo, _SaveData.PayToID);

                                    Mytrans.Commit();
                                    CurCon.Close();
                                    return "True";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               return ex.Message;
            }
           
        }

        
        

        private string GetTransactionID(MySqlConnection ActCon,int AcYear)
        {
             MySqlCommand oSqlCommand = new MySqlCommand();
            string sql1 = "Select max(substr(TrID,5)) as MaxID from tblaccounttransaction";
            int i = 0;
            DataRow r = Mycommon.GetDataRowAccountTrans(sql1, oSqlCommand, ActCon, "Get AccTransRef");
            if (r != null)
                {
                string FirstL = r["MaxID"].ToString();
                bool resp = int.TryParse(FirstL, out i);
                i += 1;
                return AcYear.ToString() + i.ToString("0#####");
 
                }
            else
                return AcYear.ToString() + "000001";

        }
        private string UpdateTranseRefinReleventTable( int TrType,bool DrCr, string TransRef,string HeaderRef,string SubRef, MySqlConnection ActCon)
        {
            //1=Payment
            //2=Bill
            //3=Journal
            //4=Invoice
        MySqlCommand oSqlCommand = new MySqlCommand();
        try
            {
            string sql1 = "";
            switch (TrType)
                {
                case 1:  //Payment
                    if (DrCr)
                        sql1 = "Update tblpaymetdetails set PayTrRef=@TransRef where ItemNo=@SubRef and PvnNo=@HeaderRef";
                    else
                        sql1 = "Update tblpayment set TrRef=@TransRef where PaymentID=@HeaderRef";
                    break;
                case 2: // Bill
                    if (DrCr)
                        sql1 = "Update tblpayablebilldetails set TrRef=@TransRef where ItemNo=@SubRef and BillNo=@HeaderRef";
                    else
                        sql1 = "Update tblpayablebill set TrRef=@TransRef where BillNo=@HeaderRef";
                    break;
                case 3: // Journal
                    sql1 = "Update tbljournaldetails set AcTrRef=@TransRef where JeNumber=@HeaderRef and LineRef=@SubRef";
                    break;
                case 4: // Invoice
                    if (!DrCr)
                        sql1 = "Update tblrecievebleinvoicedetails set TrRef=@TransRef where ItemNo=@SubRef and InvoiceNO=@HeaderRef";
                    else
                        sql1 = "Update tblrecievebleinvoice set TrRef =@TransRef where InvoiceNo=@HeaderRef";
                        break;
                case 5: // Receipt
                        if (DrCr)
                            sql1 = "Update tblreceipt set TrRef=@TransRef where ReceiptID=@HeaderRef";
                        else
                            sql1 = "Update tblreceiptdetails set  RcptTrRef=@TransRef where RcptNo=@HeaderRef and ItemNo=@SubRef";
                        break;
                default:
                    break;
                }
            oSqlCommand.Parameters.AddWithValue("@TransRef", TransRef);
            oSqlCommand.Parameters.AddWithValue("@SubRef", SubRef);
            oSqlCommand.Parameters.AddWithValue("@HeaderRef", HeaderRef);
            string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, ActCon, "Update Relevent Ref on Account");
            return respond;
            }
        catch (Exception)
            {
            
            throw;
            }
        }
        private string GetAccReference(string AccountID,  MySqlConnection ActCon)
        {
            string sql1 = "SELECT  LastTransRef FROM accountname where AccountID=@AccountID";
            MySqlCommand oSqlCommand = new MySqlCommand();
            oSqlCommand.Parameters.AddWithValue("@AccountID", AccountID);
            DataRow r = Mycommon.GetDataRowAccountTrans(sql1, oSqlCommand, ActCon, "Get last Acc Ref ");
            int lstid = 0;
            if (r != null)
            {
                string[] splt = r["LastTransRef"].ToString().Split('-');
                if (splt.Length > 1)
                {
                    int i = 0;
                    bool resp = int.TryParse(splt[splt.Length-1], out i);
                    lstid = i + 1;
                }
                else
                    return AccountID + "-01";
            }
            else 
                {
                lstid=1;
                }
            return AccountID + "-" + lstid.ToString(); 
        }

        public string UpdateLastReference(string Acnumber, string Ref1, MySqlConnection _ActCon)
        {
        try
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sql1 = "Update accountname set LastTransRef=@Ref1 where AccountID=@AccountID";
            oSqlCommand.Parameters.AddWithValue("@AccountID", Acnumber);
            oSqlCommand.Parameters.AddWithValue("@Ref1", Ref1);
            string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, _ActCon, "Update Last Acn Ref");
            return respond;
            }
        catch (Exception ex)
            {

            return ex.Message;
            }



        }
        public string UdateFixPVNHeader(string Acid, string TmpPVN,out string FixPVN,MySqlConnection _ActCon)
        {
            FixPVN = "";
            try
                {
                FixPVN = GetPVNNumber(Acid, _ActCon);
                MySqlCommand oSqlCommand = new MySqlCommand();
                string sql1 = "Update tblpayment set PaymentID=@FixPVN , PayStatus=3 where PaymentID=@TmpPVN";
                oSqlCommand.Parameters.AddWithValue("@FixPVN", FixPVN);
                oSqlCommand.Parameters.AddWithValue("@TmpPVN", TmpPVN);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, _ActCon, "Update Fixed PVN");
                return respond;
                }
            catch (Exception ex)
                {
                
               return ex.Message;
                }
        }

        public string UpdateFixPVNDetails(string TmpPVN, string FixPVN, MySqlConnection _ActCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            try
                {
                string sql1 = "Update tblpaymetdetails set PvnNo=@FixPVN where PvnNo=@TmpPVN";
                oSqlCommand.Parameters.AddWithValue("@FixPVN", FixPVN);
                oSqlCommand.Parameters.AddWithValue("@TmpPVN", TmpPVN);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, _ActCon, "Update Fixed PVN DetAILS");
                return respond;
                }
            catch (Exception ex)
                {

                return ex.Message;
                }
            }
        public string  UpdateLastPVN(string AcID,string PVN, MySqlConnection _ActCon)
        {
            try
                {
                    MySqlCommand oSqlCommand = new MySqlCommand();
                    string sql1 = "Update tblaccprefix set LastPVNNumber=@LastPVNNumber where AccID=@AccID";
                    oSqlCommand.Parameters.AddWithValue("@LastPVNNumber", PVN);
                    oSqlCommand.Parameters.AddWithValue("@AccID", AcID);
                    string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, _ActCon, "Update Last PVN");
                    return respond;
                }
            catch (Exception ex)
                {
                    return ex.Message;
                }
        
        }
        public string UpdateLastRCPT(string PVN, MySqlConnection _ActCon)
        {
            try
            {
                MySqlCommand oSqlCommand = new MySqlCommand();
                string sql1 = "Update tblaccprefix set LastPVNNumber=@LastPVNNumber where AccID=@AccID";
                oSqlCommand.Parameters.AddWithValue("@LastPVNNumber", PVN);
                oSqlCommand.Parameters.AddWithValue("@AccID", "RCV00");
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, _ActCon, "Update Last PVN");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public string GetRCVNumber( MySqlConnection _ActCon)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sql1 = "select Prefix,LastPVNNumber from tblaccprefix where AccID=@PayAcN";
            oSqlCommand.Parameters.AddWithValue("@PayAcN", "RCV00");
            DataRow r = Mycommon.GetDataRowAccountTrans(sql1, oSqlCommand, _ActCon, "Get Pvn Number");
            string Prefix = "", LastPVN = "";
            if (r != null)
                {

                Prefix = r["Prefix"].ToString();
                LastPVN = r["LastPVNNumber"].ToString();
                string[] splt = LastPVN.Split('-');
                if (splt.Length == 2)
                    {
                    int i = 0;
                    bool resp = int.TryParse(splt[1], out i);
                    int lstVn = i + 1;
                    string lstpvn = Prefix + "-" + lstVn.ToString("0########");
                    return lstpvn;
                    }
                else
                    {
                    return Prefix + "-" + "000000001";

                    }
                }
            else
                {
                return "XXX-0000000";
                }

            }
        public string UpdateFixRCptHeader(string OldRcpt,MySqlConnection _ActCon,out string RcptNumber )
        {
             RcptNumber = GetRCVNumber(_ActCon);
             MySqlCommand CurCommand = new MySqlCommand();
            string sql1 = "Update tblreceipt set ReceiptID=@NewNumber,ReceiptStatus=3 where ReceiptID=@OldrcpNumber";
            CurCommand.Parameters.AddWithValue("@OldrcpNumber", OldRcpt);
            CurCommand.Parameters.AddWithValue("@NewNumber", RcptNumber);
            string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, CurCommand, _ActCon, "Update Fixed RCPT Number");
            return respond;

        }
        public string UpdateFixRCptDetails(string OldRcpt, string FixedRcpt, MySqlConnection _ActCon)
        {
            MySqlCommand CurCommand = new MySqlCommand();
            string sql1 = "Update tblreceiptdetails set RcptNo=@NewNumber where RcptNo=@OldrcpNumber";
            CurCommand.Parameters.AddWithValue("@OldrcpNumber", OldRcpt);
            CurCommand.Parameters.AddWithValue("@NewNumber", FixedRcpt);
            string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, CurCommand, _ActCon, "Update Fixed RCPT Details");
            return respond;
        }
        public string GetPVNNumber(string PayAcN,MySqlConnection _ActCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sql1 = "select Prefix,LastPVNNumber from tblaccprefix where AccID=@PayAcN";
            oSqlCommand.Parameters.AddWithValue("@PayAcN", PayAcN);
            DataRow r = Mycommon.GetDataRowAccountTrans(sql1, oSqlCommand, _ActCon, "Get Pvn Number");
            string Prefix = "", LastPVN = "";
            if (r != null)
                {

                Prefix = r["Prefix"].ToString();
                LastPVN = r["LastPVNNumber"].ToString();
                string[] splt = LastPVN.Split('-');
                if (splt.Length == 2)
                    {
                        int i = 0;
                        bool resp = int.TryParse(splt[1], out i);
                        int lstVn = i + 1;
                        string lstpvn = Prefix + "-" + lstVn.ToString("0########");
                        return lstpvn;
                    }
                else
                    {
                         return Prefix + "-" + "000000001";

                    }
                }
            else
                {
                        return "XXX-0000000";
                }

        }
        private string UpdateLastRefonAccount(string  AccountID, string LastRef, MySqlConnection ActCon)
        {
            MySqlCommand CurCommand = new MySqlCommand(); 
            string sql1 = "Update accountname set  LastTransRef=@LastTransRef where AccountID=@AccountID";
            CurCommand.Parameters.AddWithValue("@AccountID", AccountID);
            CurCommand.Parameters.AddWithValue("@LastTransRef", LastRef);
            string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, CurCommand, ActCon, "Update AccountTranaction");
            return respond;
        }
        public string SaveAccountTransaction(AccountTransactionType.AccountTransactionDataType _SaveData, MySqlConnection ActCon, out string TrReference)
            {
                TrReference = GetTransactionID(ActCon, _SaveData.AccountYear);
                MySqlCommand oSqlCommand = new MySqlCommand();
                string sqlQuery = "Insert Into tblaccounttransaction ("
                  + "TrID,"
                  + "CompanyID,"
                  + "AccountID,"
                  + "MainRef,"
                  + "RelRef,"
                  + "Description,"
                  + "CurRate,"
                  + "Dr,"
                  + "Cr,"
                  + "FDr,"
                  + "FCr,"
                  + "VoucherID,"
                  + "PayMethod,"
                  + "AnyOtherRemarks,"
                  + "TrDate,"
                  + "TrTime,"
                  + "TrUser,"
                  + "TrMachine,"
                  + "AccountYear,"
                 + "TransType,ActualDate,IsitHomeCurrency)"
                   + " Values ("
                   + "@TrID,"
                   + "@CompanyID,"
                   + "@AccountID,"
                   + "@MainRef,"
                   + "@RelRef,"
                   + "@Description,"
                   + "@CurRate,"
                   + "@Dr,"
                   + "@Cr,"
                   + "@FDr,"
                   + "@FCr,"
                   + "@VoucherID,"
                   + "@PayMethod,"
                   + "@AnyOtherRemarks,"
                   + "curDate(),"
                   + "curTime(),"
                   + "@TrUser,"
                   + "@TrMachine,"
                   + "@AccountYear,"
                   + "@TransType,@ActualDate,@IsitHomeCurrency)";
            try
                {
                    oSqlCommand.Parameters.AddWithValue("@TrID", TrReference);
                    oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                    oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                    oSqlCommand.Parameters.AddWithValue("@MainRef", _SaveData.MainRef);
                    oSqlCommand.Parameters.AddWithValue("@RelRef", _SaveData.RelRef);
                    oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                    oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                    oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                    oSqlCommand.Parameters.AddWithValue("@Cr", _SaveData.Cr);
                    oSqlCommand.Parameters.AddWithValue("@FDr", _SaveData.FDr);
                    oSqlCommand.Parameters.AddWithValue("@FCr", _SaveData.FCr);
                    oSqlCommand.Parameters.AddWithValue("@VoucherID", _SaveData.VoucherID);
                    oSqlCommand.Parameters.AddWithValue("@PayMethod", _SaveData.PayMethod);
                    oSqlCommand.Parameters.AddWithValue("@AnyOtherRemarks", _SaveData.AnyOtherRemarks);
                    oSqlCommand.Parameters.AddWithValue("@TrDate", _SaveData.TrDate);
                    oSqlCommand.Parameters.AddWithValue("@TrTime", _SaveData.TrTime);
                    oSqlCommand.Parameters.AddWithValue("@TrUser", _SaveData.TrUser);
                    oSqlCommand.Parameters.AddWithValue("@TrMachine", _SaveData.TrMachine);
                    oSqlCommand.Parameters.AddWithValue("@AccountYear", _SaveData.AccountYear);
                    oSqlCommand.Parameters.AddWithValue("@TransType", _SaveData.TransType);
                    oSqlCommand.Parameters.AddWithValue("@ActualDate", _SaveData.ActualDate);
                    
                if ( _SaveData.CurRate==1)
                    oSqlCommand.Parameters.AddWithValue("@IsitHomeCurrency", 1);
                else
                    oSqlCommand.Parameters.AddWithValue("@IsitHomeCurrency",0);
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, ActCon, "Save AccountTransaction");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdateBillStatus(string InvoiceNumber, int Status, MySqlConnection ActCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sql1 = "Update  tblpayablebill  set  BillStatus=@Status where  BillNo=@InvoiceNumber";
            oSqlCommand.Parameters.AddWithValue("@InvoiceNumber", InvoiceNumber);
            oSqlCommand.Parameters.AddWithValue("@Status", Status);
            string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sql1, oSqlCommand, ActCon, "Update Last PVN");
            return respond;
        }
        public string SaveAccountTransaction(AccountTransactionType.AccountTransactionDataType _SaveData)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblaccounttransaction ("
              + "TrID,"
              + "CompanyID,"
              + "AccountID,"
              + "MainRef,"
              + "RelRef,"
              + "Description,"
              + "CurRate,"
              + "Dr,"
              + "Cr,"
              + "FDr,"
              + "FCr,"
              + "VoucherID,"
              + "PayMethod,"
              + "AnyOtherRemarks,"
              + "TrDate,"
              + "TrTime,"
              + "TrUser,"
              + "TrMachine,"
              + "AccountYear,"
              + "SysID,"
              + "TransType,"
              + "ActualDate,IsBaseCurreny)"
               + " Values ("
               + "@TrID,"
               + "@CompanyID,"
               + "@AccountID,"
               + "@MainRef,"
               + "@RelRef,"
               + "@Description,"
               + "@CurRate,"
               + "@Dr,"
               + "@Cr,"
               + "@FDr,"
               + "@FCr,"
               + "@VoucherID,"
               + "@PayMethod,"
               + "@AnyOtherRemarks,"
               + "@TrDate,"
               + "@TrTime,"
               + "@TrUser,"
               + "@TrMachine,"
               + "@AccountYear,"
               + "@SysID,"
               + "@TransType,"
               + "@ActualDate,@IsBaseCurreny)";
            try
                {
                    oSqlCommand.Parameters.AddWithValue("@TrID", _SaveData.TrID);
                    oSqlCommand.Parameters.AddWithValue("@CompanyID", _SaveData.CompanyID);
                    oSqlCommand.Parameters.AddWithValue("@AccountID", _SaveData.AccountID);
                    oSqlCommand.Parameters.AddWithValue("@MainRef", _SaveData.MainRef);
                    oSqlCommand.Parameters.AddWithValue("@RelRef", _SaveData.RelRef);
                    oSqlCommand.Parameters.AddWithValue("@Description", _SaveData.Description);
                    oSqlCommand.Parameters.AddWithValue("@CurRate", _SaveData.CurRate);
                    oSqlCommand.Parameters.AddWithValue("@Dr", _SaveData.Dr);
                    oSqlCommand.Parameters.AddWithValue("@Cr", _SaveData.Cr);
                    oSqlCommand.Parameters.AddWithValue("@FDr", _SaveData.FDr);
                    oSqlCommand.Parameters.AddWithValue("@FCr", _SaveData.FCr);
                    oSqlCommand.Parameters.AddWithValue("@VoucherID", _SaveData.VoucherID);
                    oSqlCommand.Parameters.AddWithValue("@PayMethod", _SaveData.PayMethod);
                    oSqlCommand.Parameters.AddWithValue("@AnyOtherRemarks", _SaveData.AnyOtherRemarks);
                    oSqlCommand.Parameters.AddWithValue("@TrDate", _SaveData.TrDate);
                    oSqlCommand.Parameters.AddWithValue("@TrTime", _SaveData.TrTime);
                    oSqlCommand.Parameters.AddWithValue("@TrUser", _SaveData.TrUser);
                    oSqlCommand.Parameters.AddWithValue("@TrMachine", _SaveData.TrMachine);
                    oSqlCommand.Parameters.AddWithValue("@AccountYear", _SaveData.AccountYear);
                    oSqlCommand.Parameters.AddWithValue("@SysID", _SaveData.SysID);
                    oSqlCommand.Parameters.AddWithValue("@TransType", _SaveData.TransType);
                    oSqlCommand.Parameters.AddWithValue("@ActualDate", _SaveData.ActualDate);
                if (_SaveData.CurRate==1)
                    oSqlCommand.Parameters.AddWithValue("@IsBaseCurreny",1);
                else
                    oSqlCommand.Parameters.AddWithValue("@IsBaseCurreny", 0);

                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Save AccountTransaction");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdateAccountTransaction(AccountTransactionType.AccountTransactionDataType _Update)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblaccounttransaction Set "
                + "TrID=@TrID,"
                + "CompanyID=@CompanyID,"
                + "AccountID=@AccountID,"
                + "MainRef=@MainRef,"
                + "RelRef=@RelRef,"
                + "Description=@Description,"
                + "CurRate=@CurRate,"
                + "Dr=@Dr,"
                + "Cr=@Cr,"
                + "FDr=@FDr,"
                + "FCr=@FCr,"
                + "VoucherID=@VoucherID,"
                + "PayMethod=@PayMethod,"
                + "AnyOtherRemarks=@AnyOtherRemarks,"
                + "TrDate=@TrDate,"
                + "TrTime=@TrTime,"
                + "TrUser=@TrUser,"
                + "TrMachine=@TrMachine,"
                + "AccountYear=@AccountYear,"
               
                + "TransType=@TransType,"
                + "ActualDate=@ActualDate,IsBaseCurreny=@IsBaseCurreny"
                + " Where 1=1 "
                + " and TrID=@TrID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@TrID", _Update.TrID);
                oSqlCommand.Parameters.AddWithValue("@CompanyID", _Update.CompanyID);
                oSqlCommand.Parameters.AddWithValue("@AccountID", _Update.AccountID);
                oSqlCommand.Parameters.AddWithValue("@MainRef", _Update.MainRef);
                oSqlCommand.Parameters.AddWithValue("@RelRef", _Update.RelRef);
                oSqlCommand.Parameters.AddWithValue("@Description", _Update.Description);
                oSqlCommand.Parameters.AddWithValue("@CurRate", _Update.CurRate);
                oSqlCommand.Parameters.AddWithValue("@Dr", _Update.Dr);
                oSqlCommand.Parameters.AddWithValue("@Cr", _Update.Cr);
                oSqlCommand.Parameters.AddWithValue("@FDr", _Update.FDr);
                oSqlCommand.Parameters.AddWithValue("@FCr", _Update.FCr);
                oSqlCommand.Parameters.AddWithValue("@VoucherID", _Update.VoucherID);
                oSqlCommand.Parameters.AddWithValue("@PayMethod", _Update.PayMethod);
                oSqlCommand.Parameters.AddWithValue("@AnyOtherRemarks", _Update.AnyOtherRemarks);
                oSqlCommand.Parameters.AddWithValue("@TrDate", _Update.TrDate);
                oSqlCommand.Parameters.AddWithValue("@TrTime", _Update.TrTime);
                oSqlCommand.Parameters.AddWithValue("@TrUser", _Update.TrUser);
                oSqlCommand.Parameters.AddWithValue("@TrMachine", _Update.TrMachine);
                oSqlCommand.Parameters.AddWithValue("@AccountYear", _Update.AccountYear);
                oSqlCommand.Parameters.AddWithValue("@TransType", _Update.TransType);
                oSqlCommand.Parameters.AddWithValue("@ActualDate", _Update.ActualDate);
                if (_Update.CurRate==1)
                    oSqlCommand.Parameters.AddWithValue("@IsBaseCurreny", 1);
                else
                    oSqlCommand.Parameters.AddWithValue("@IsBaseCurreny", 0);

                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,"Update AccountTransaction");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string DeleteAccountTransaction(string TrID)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblaccounttransaction"
                + " Where 1=1 "
                + " and TrID=@TrID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@TrID", TrID);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Delete AccountTransaction");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistAccountTransaction(string TrID)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tblaccounttransaction"
                + " Where 1=1 "
                + " and TrID=@TrID";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@TrID", TrID);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist AcTransRef");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public string GetExistAccountTransaction(string TrID, out  AccountTransactionType.AccountTransactionDataType _ExistData)
            {
            _ExistData = new AccountTransactionType.AccountTransactionDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "TrID,"
          + "CompanyID,"
          + "AccountID,"
          + "MainRef,"
          + "RelRef,"
          + "Description,"
          + "CurRate,"
          + "Dr,"
          + "Cr,"
          + "FDr,"
          + "FCr,"
          + "VoucherID,"
          + "PayMethod,"
          + "AnyOtherRemarks,"
          + "TrDate,"
          + "TrTime,"
          + "TrUser,"
          + "TrMachine,"
          + "AccountYear,"
          + "SysID,"
          + "TransType,"
          + "ActualDate"
          + " from tblaccounttransaction"
          + " Where 1=1 "
                + " and TrID=@TrID";
            oSqlCommand.Parameters.AddWithValue("@TrID", TrID);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand, "Get Exist data AccountTransaction");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    _ExistData.TrID = r["TrID"].ToString();
                    int inCompanyID = 0;
                    resp = int.TryParse(r["CompanyID"].ToString(), out inCompanyID);
                    _ExistData.CompanyID = inCompanyID;
                    _ExistData.AccountID = r["AccountID"].ToString();
                    _ExistData.MainRef = r["MainRef"].ToString();
                    _ExistData.RelRef = r["RelRef"].ToString();
                    _ExistData.Description = r["Description"].ToString();
                    decimal deCurRate = 0;
                    resp = decimal.TryParse(r["CurRate"].ToString(), out deCurRate);
                    _ExistData.CurRate = deCurRate;
                    decimal deDr = 0;
                    resp = decimal.TryParse(r["Dr"].ToString(), out deDr);
                    _ExistData.Dr = deDr;
                    decimal deCr = 0;
                    resp = decimal.TryParse(r["Cr"].ToString(), out deCr);
                    _ExistData.Cr = deCr;
                    decimal deFDr = 0;
                    resp = decimal.TryParse(r["FDr"].ToString(), out deFDr);
                    _ExistData.FDr = deFDr;
                    decimal deFCr = 0;
                    resp = decimal.TryParse(r["FCr"].ToString(), out deFCr);
                    _ExistData.FCr = deFCr;
                    _ExistData.VoucherID = r["VoucherID"].ToString();
                    int inPayMethod = 0;
                    resp = int.TryParse(r["PayMethod"].ToString(), out inPayMethod);
                    _ExistData.PayMethod = inPayMethod;
                    _ExistData.AnyOtherRemarks = r["AnyOtherRemarks"].ToString();
                    DateTime dtTrDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["TrDate"].ToString(), out dtTrDate);
                    if (resp)
                        _ExistData.TrDate = dtTrDate;
                    else
                        _ExistData.TrDate = new DateTime(1900, 1, 1);
                    _ExistData.TrTime = r["TrTime"].ToString();
                    _ExistData.TrUser = r["TrUser"].ToString();
                    _ExistData.TrMachine = r["TrMachine"].ToString();
                    int inAccountYear = 0;
                    resp = int.TryParse(r["AccountYear"].ToString(), out inAccountYear);
                    _ExistData.AccountYear = inAccountYear;
                    int inSysID = 0;
                    resp = int.TryParse(r["SysID"].ToString(), out inSysID);
                    _ExistData.SysID = inSysID;
                    int inTransType = 0;
                    resp = int.TryParse(r["TransType"].ToString(), out inTransType);
                    _ExistData.TransType = inTransType;
                    DateTime dtActualDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["ActualDate"].ToString(), out dtActualDate);
                    if (resp)
                        _ExistData.ActualDate = dtActualDate;
                    else
                        _ExistData.ActualDate = new DateTime(1900, 1, 1);
                    return "True";
                    }
                catch (Exception ex)
                    {
                    return ex.Message;
                    }
                }
            else
                return "data not found ";
            }
   
    }
    public class AccountTransactionType
    {
        public struct AccountTransactionDataType
            {
                public string TrID;
                public int CompanyID;
                public string AccountID;
                public string MainRef;
                public string RelRef;
                public string Description;
                public decimal CurRate;
                public decimal Dr;
                public decimal Cr;
                public decimal FDr;
                public decimal FCr;
                public string VoucherID;
                public int PayMethod;
                public string AnyOtherRemarks;
                public DateTime TrDate;
                public string TrTime;
                public string TrUser;
                public string TrMachine;
                public int AccountYear;
                public int SysID;
                public int TransType;
                public DateTime ActualDate;
                public int IsBaseCurreny;
          }
    }
}
