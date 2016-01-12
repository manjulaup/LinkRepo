//Create by SATICIN On 16/Dec/2015
//
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using BusinessLayer.CommonOperation;
namespace BusinessLayer.BankChequess
    {
    public class BankCheques
        {
        private CommonOperations Mycommon = null ;
        public BankCheques(bool IsLocal)
        {
        Mycommon = new CommonOperations(IsLocal);
        }
        public struct BankChequesDataType
            {
                public int SysID;
                public string ChequeNo;
                public int BankID;
                public string ChOwner;
                public int ChType;
                public DateTime ChDate;
                public DateTime ReceiveRaisedDate;
                public DateTime DepositDate;
                public int ChStatus;
                public int IsReconciled;
                public string LastUser;
                public DateTime LastModiDate;
                public decimal Amount;
            }
        public string SaveBankCheques(BankChequesDataType _SaveData)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into tblcheque ("
          + "ChequeNo,"
          + "BankID,"
          + "ChOwner,"
          + "ChType,"
          + "ChDate,"
          + "ReceiveRaisedDate,"
          + "DepositDate,"
          + "ChStatus,"
          + "IsReconciled,"
          + "LastUser,"
          + "LastModiDate,"
          + "Amount)"
           + " Values ("
           + "@ChequeNo,"
           + "@BankID,"
           + "@ChOwner,"
           + "@ChType,"
           + "@ChDate,"
           + "@ReceiveRaisedDate,"
           + "@DepositDate,"
           + "@ChStatus,"
           + "@IsReconciled,"
           + "@LastUser,"
           + "@LastModiDate,"
           + "@Amount)";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ChequeNo", _SaveData.ChequeNo);
                oSqlCommand.Parameters.AddWithValue("@BankID", _SaveData.BankID);
                oSqlCommand.Parameters.AddWithValue("@ChOwner", _SaveData.ChOwner);
                oSqlCommand.Parameters.AddWithValue("@ChType", _SaveData.ChType);
                oSqlCommand.Parameters.AddWithValue("@ChDate", _SaveData.ChDate);
                oSqlCommand.Parameters.AddWithValue("@ReceiveRaisedDate", _SaveData.ReceiveRaisedDate);
                oSqlCommand.Parameters.AddWithValue("@DepositDate", _SaveData.DepositDate);
                oSqlCommand.Parameters.AddWithValue("@ChStatus", _SaveData.ChStatus);
                oSqlCommand.Parameters.AddWithValue("@IsReconciled", _SaveData.IsReconciled);
                oSqlCommand.Parameters.AddWithValue("@LastUser", _SaveData.LastUser);
                oSqlCommand.Parameters.AddWithValue("@LastModiDate", _SaveData.LastModiDate);
                oSqlCommand.Parameters.AddWithValue("@Amount", _SaveData.Amount);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Save BankCheques");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string UpdateBankCheques(BankChequesDataType _Update)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Update tblcheque Set "
                 + "ChequeNo=@ChequeNo,"
                + "BankID=@BankID,"
                + "ChOwner=@ChOwner,"
                + "ChType=@ChType,"
                + "ChDate=@ChDate,"
                + "ReceiveRaisedDate=@ReceiveRaisedDate,"
                + "DepositDate=@DepositDate,"
                + "ChStatus=@ChStatus,"
                + "IsReconciled=@IsReconciled,"
                + "LastUser=@LastUser,"
                + "LastModiDate=@LastModiDate,"
                + "Amount=@Amount"
                + " Where 1=1 "
                + " and ChequeNo=@ChequeNo";
            try
                {
                 oSqlCommand.Parameters.AddWithValue("@ChequeNo", _Update.ChequeNo);
                oSqlCommand.Parameters.AddWithValue("@BankID", _Update.BankID);
                oSqlCommand.Parameters.AddWithValue("@ChOwner", _Update.ChOwner);
                oSqlCommand.Parameters.AddWithValue("@ChType", _Update.ChType);
                oSqlCommand.Parameters.AddWithValue("@ChDate", _Update.ChDate);
                oSqlCommand.Parameters.AddWithValue("@ReceiveRaisedDate", _Update.ReceiveRaisedDate);
                oSqlCommand.Parameters.AddWithValue("@DepositDate", _Update.DepositDate);
                oSqlCommand.Parameters.AddWithValue("@ChStatus", _Update.ChStatus);
                oSqlCommand.Parameters.AddWithValue("@IsReconciled", _Update.IsReconciled);
                oSqlCommand.Parameters.AddWithValue("@LastUser", _Update.LastUser);
                oSqlCommand.Parameters.AddWithValue("@LastModiDate", _Update.LastModiDate);
                oSqlCommand.Parameters.AddWithValue("@Amount", _Update.Amount);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand, "Update BankCheques");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public string SetStatus(string Chequenumber, int Status1)
        {
        return "True";
            }
        public string DeleteBankCheques(string ChequeNo)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Delete from tblcheque"
                + " Where 1=1 "
                + " and ChequeNo=@ChequeNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ChequeNo", ChequeNo);
                string respond = Mycommon.ExicuteAnyCommandAccount(sqlQuery, oSqlCommand,  "Delete BankCheques");
                return respond;
                }
            catch (Exception ex)
                {
                return ex.Message;
                }
            }
        public bool ExistBankCheques(string ChequeNo)
            {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select SysID from tblcheque"
                + " Where 1=1 "
                + " and ChequeNo=@ChequeNo";
            try
                {
                oSqlCommand.Parameters.AddWithValue("@ChequeNo", ChequeNo);
                bool respond = Mycommon.ExistInTableAccount(sqlQuery, oSqlCommand,"Exist Cheque in table");
                return respond;
                }
            catch (Exception ex)
                {
                return false;
                }
            }
        public string GetExistBankCheques(string ChequeNo, out BankChequesDataType _ExistData)
            {
            _ExistData = new BankChequesDataType();
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Select "
          + "SysID,"
          + "ChequeNo,"
          + "BankID,"
          + "ChOwner,"
          + "ChType,"
          + "ChDate,"
          + "ReceiveRaisedDate,"
          + "DepositDate,"
          + "ChStatus,"
          + "IsReconciled,"
          + "LastUser,"
          + "LastModiDate,"
          + "Amount"
          + " from tblcheque"
          + " Where 1=1 "
                + " and ChequeNo=@ChequeNo";
            oSqlCommand.Parameters.AddWithValue("@ChequeNo", ChequeNo);
            DataRow r = Mycommon.GetDataRowAccount(sqlQuery, oSqlCommand,  "Get Exist data BankCheques");
            if (r != null)
                {
                try
                    {
                    bool resp = false;
                    int inSysID = 0;
                    resp = int.TryParse(r["SysID"].ToString(), out inSysID);
                    _ExistData.SysID = inSysID;
                    _ExistData.ChequeNo = r["ChequeNo"].ToString();
                    int inBankID = 0;
                    resp = int.TryParse(r["BankID"].ToString(), out inBankID);
                    _ExistData.BankID = inBankID;
                    _ExistData.ChOwner = r["ChOwner"].ToString();
                    int inChType = 0;
                    resp = int.TryParse(r["ChType"].ToString(), out inChType);
                    _ExistData.ChType = inChType;
                    DateTime dtChDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["ChDate"].ToString(), out dtChDate);
                    if (resp)
                        _ExistData.ChDate = dtChDate;
                    else
                        _ExistData.ChDate = new DateTime(1900, 1, 1);
                    DateTime dtReceiveRaisedDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["ReceiveRaisedDate"].ToString(), out dtReceiveRaisedDate);
                    if (resp)
                        _ExistData.ReceiveRaisedDate = dtReceiveRaisedDate;
                    else
                        _ExistData.ReceiveRaisedDate = new DateTime(1900, 1, 1);
                    DateTime dtDepositDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["DepositDate"].ToString(), out dtDepositDate);
                    if (resp)
                        _ExistData.DepositDate = dtDepositDate;
                    else
                        _ExistData.DepositDate = new DateTime(1900, 1, 1);
                    int inChStatus = 0;
                    resp = int.TryParse(r["ChStatus"].ToString(), out inChStatus);
                    _ExistData.ChStatus = inChStatus;
                    int inIsReconciled = 0;
                    resp = int.TryParse(r["IsReconciled"].ToString(), out inIsReconciled);
                    _ExistData.IsReconciled = inIsReconciled;
                    _ExistData.LastUser = r["LastUser"].ToString();
                    DateTime dtLastModiDate = new DateTime(1900, 1, 1);
                    resp = DateTime.TryParse(r["LastModiDate"].ToString(), out dtLastModiDate);
                    if (resp)
                        _ExistData.LastModiDate = dtLastModiDate;
                    else
                        _ExistData.LastModiDate = new DateTime(1900, 1, 1);
                    decimal deAmount = 0;
                    resp = decimal.TryParse(r["Amount"].ToString(), out deAmount);
                    _ExistData.Amount = deAmount;
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
    }
