using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using BusinessLayer.CommonOperation;
using DataLayer.DataService;
using System.Windows.Forms;
using System.Drawing;

namespace BusinessLayer
{
    public class BALAcknowledgement
    {
        private DataService Mycommon = null;

        public BALAcknowledgement(bool IsLocal)
        {
            Mycommon = new DataService(IsLocal);
        }

        ~BALAcknowledgement()
        {
            Mycommon.CloseDB();
        }


        public string Save(EntityHandler.Acknoladgement _SaveData){

            MySql.Data.MySqlClient.MySqlTransaction Mytrans;
            MySqlConnection CurCon = new MySqlConnection();
            CurCon = Mycommon.AccountConnection;
            string respond = "";

            if (CurCon.State == ConnectionState.Closed)
            {
                CurCon.Open();
            }

            Mytrans = Mycommon.AccountConnection.BeginTransaction();
            MySqlCommand oSqlCommand = new MySqlCommand();

            respond = SaveAcknoledgement(_SaveData, CurCon);
            if (respond != "True")
            {
                Mytrans.Rollback();
                return respond;
            }
            else
            {
                Mytrans.Commit();
                CurCon.Close();
                return "True";
            }

            return respond;

            }


        public string SaveAcknoledgement(EntityHandler.Acknoladgement _SaveData, MySqlConnection _CurCon)
        {
            MySqlCommand oSqlCommand = new MySqlCommand();
            string sqlQuery = "Insert Into Acknoladgement ("
          + "Name,"
          + "NIC,"
          + "TP,"
          + "VoucherNo,"
          + "ChequeNo,"
          + "SaveDate)"
           + " Values ("
           + "@Name,"
           + "@NIC,"
           + "@TP,"
           + "@VoucherNo,"
           + "@ChequeNo,"
           + "@SaveDate)";
            try
            {
                oSqlCommand.Parameters.AddWithValue("@Name", _SaveData.name);
                oSqlCommand.Parameters.AddWithValue("@NIC", _SaveData.nic);
                oSqlCommand.Parameters.AddWithValue("@TP", _SaveData.tp);
                oSqlCommand.Parameters.AddWithValue("@VoucherNo", _SaveData.voucherno);
                oSqlCommand.Parameters.AddWithValue("@ChequeNo", _SaveData.chequeno);
                oSqlCommand.Parameters.AddWithValue("@SaveDate", _SaveData.savedate);
                
                string respond = Mycommon.ExicuteAnyCommandAccountWithTrans(sqlQuery, oSqlCommand, _CurCon, "Save Ack");
                return respond;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

    }
}
