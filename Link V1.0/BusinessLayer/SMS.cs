using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using GsmComm.PduConverter;
using GsmComm.GsmCommunication;
using System.Windows.Forms;


namespace BusinessLayer.SMSes
{
    public class SMS
        {
            private int port;
            private int baudRate=19200;
            private int timeout=300;
        //    private CommSetting comm_settings = new CommSetting();
            private delegate void SetTextCallback(string text);
            private delegate void MessageEventHandler();

            private bool IsSignalSearch = false;
        private void comm_PhoneConnected(object sender, EventArgs e)
        {
           //Invoke(new ConnectedHandler(OnPhoneConnectionChange), new object[] { true });
        }
        private void OnPhoneConnectionChange(bool connected)
        {
            string Status = "CONNECTED";
        }
        private void comm_MessageReceived(object sender, GsmComm.GsmCommunication.MessageReceivedEventArgs e)
        {
           // MessageReceived();
        }
        //public void IsPhoneConnectionOk()
        //{
        //    if (!EnterNewSettings())
               

        //    GetData(out port, out baudRate, out timeout);
        //    CommSetting.Comm_Port = port;
        //    CommSetting.Comm_BaudRate = baudRate;
        //    CommSetting.Comm_TimeOut = timeout;

        //    Cursor.Current = Cursors.WaitCursor;
        //    CommSetting.comm = new GsmCommMain(port, baudRate, timeout);
        //    Cursor.Current = Cursors.Default;
        //    CommSetting.comm.PhoneConnected += new EventHandler(comm_PhoneConnected);
        //    CommSetting.comm.MessageReceived += new MessageReceivedEventHandler(comm_MessageReceived);
        //    bool retry;
        //    do
        //    {
        //        retry = false;
        //        try
        //        {
        //            Cursor.Current = Cursors.WaitCursor;
        //            CommSetting.comm.Open();
        //            Cursor.Current = Cursors.Default;
                   
        //        }
        //        catch (Exception)
        //        {
                   
        //        }
        //    }
        //    while (retry);
        //    //CommSetting.comm.Close();
        //}
        //    public void ConnectToPhone()
        //    {
        //            string[] Comport = SerialPort.GetPortNames();
        //            foreach (string P1 in Comport)
        //            {
        //                int por1 = int.Parse(P1.Substring(3, P1.Length - 3));
        //                CommSetting.comm = new GsmCommMain(por1, baudRate, timeout);
        //                try
        //                {
        //                    CommSetting.comm.Open();
        //                    port = por1;
                           
        //                    if (CommSetting.comm.IsOpen())
        //                        CommSetting.comm.Close();

        //                }
        //                catch (Exception ex)
        //                {
                           
        //                }
        //            }
        //        }
        //public string  SendMassage(string txt_message, string txt_destination_numbers)
        //{
        //    ConnectToPhone();
        //    //IsPhoneConnectionOk();
        //    Cursor.Current = Cursors.WaitCursor;
        //    CommSetting.comm = new GsmCommMain(port , baudRate, timeout);

        //    try
        //    {
        //        // Send an SMS message
        //        SmsSubmitPdu pdu;
        //        bool alert = false;
        //        bool unicode = false;

        //        if (!alert && !unicode)
        //        {
        //            // The straightforward version
        //            pdu = new SmsSubmitPdu(txt_message, txt_destination_numbers, "");


        //        }
        //        else
        //        {
        //            // The extended version with dcs
        //            byte dcs;
        //            if (!alert && unicode)
        //                dcs = DataCodingScheme.NoClass_16Bit;
        //            else if (alert && !unicode)
        //                dcs = DataCodingScheme.Class0_7Bit;
        //            else if (alert && unicode)
        //                dcs = DataCodingScheme.Class0_16Bit;
        //            else
        //                dcs = DataCodingScheme.NoClass_7Bit; // should never occur here

        //            pdu = new SmsSubmitPdu(txt_message, txt_destination_numbers, "", dcs);
        //        }
        //        // Send the same message multiple times if this is set
        //        int times = int.Parse("1");
               
        //        if (!CommSetting.comm.IsConnected())
        //            CommSetting.comm.Open() ;
             
        //            bool rep1 = false;
        //            CommSetting.comm.SendMessage(pdu, rep1);
        //            CommSetting.comm.RequireAcknowledge(true);
               
        //            CommSetting.comm.Close();
        //            Cursor.Current = Cursors.Default;
        //            return "True";
        //    }
        //    catch (Exception ex)
        //    {
        //        Cursor.Current = Cursors.Default;
        //        return ex.Message; ;
        //    }
          
            
        //}

        
            private bool EnterNewSettings()
            {
                int newPort = port;
                int newBaudRate = 19200;
                int newTimeout = 300;
                SetData(newPort, newBaudRate, newTimeout);
                return true;
            }
            public void SetData(int port, int baudRate, int timeout)
            {
                this.port = port;
                this.baudRate = baudRate;
                this.timeout = timeout;
            }

            public void GetData(out int port, out int baudRate, out int timeout)
            {
                port = this.port;
                baudRate = this.baudRate;
                timeout = this.timeout;
            }
            private delegate void ConnectedHandler(bool connected);
            private string StatusToString(PhoneMessageStatus status)
            {
                // Map a message status to a string
                string ret;
                switch (status)
                {
                    case PhoneMessageStatus.All:
                        ret = "All";
                        break;
                    case PhoneMessageStatus.ReceivedRead:
                        ret = "Read";
                        break;
                    case PhoneMessageStatus.ReceivedUnread:
                        ret = "Unread";
                        break;
                    case PhoneMessageStatus.StoredSent:
                        ret = "Sent";
                        break;
                    case PhoneMessageStatus.StoredUnsent:
                        ret = "Unsent";
                        break;
                    default:
                        ret = "Unknown (" + status.ToString() + ")";
                        break;
                }
                return ret;
            }
            //private void MessageReceived()
            //{
            //    Cursor.Current = Cursors.WaitCursor;
            //    string storage = GetMessageStorage();

            //    DecodedShortMessage[] messages = CommSetting.comm.ReadMessages(PhoneMessageStatus.ReceivedUnread, storage);
            //    foreach (DecodedShortMessage message in messages)
            //    {
            //        Output(string.Format("Message status = {0}, Location = {1}/{2}",
            //            StatusToString(message.Status), message.Storage, message.Index));
            //        ShowMessage(message.Data);
            //        Output("");
            //    }
            //    Output(string.Format("{0,9} messages read.", messages.Length.ToString()));
            //    Output("");
            //}

        private string GetMessageStorage()
        {
            throw new Exception("The method or operation is not implemented.");
        }
            //private void Output(string text)
            //{
            //    if (this.txtOutput.InvokeRequired)
            //    {
            //        SetTextCallback stc = new SetTextCallback(Output);
            //        this.Invoke(stc, new object[] { text });
            //    }
            //    else
            //    {
            //        txtOutput.AppendText(text);
            //        txtOutput.AppendText("\r\n");

            //    }
            //}

            private void Output(string text, params object[] args)
            {
                string msg = string.Format(text, args);
                Output(msg);

            }
            private void ShowMessage(SmsPdu pdu)
            {
                if (pdu is SmsSubmitPdu)
                {
                    // Stored (sent/unsent) message
                    SmsSubmitPdu data = (SmsSubmitPdu)pdu;
                    Output("SENT/UNSENT MESSAGE");
                    Output("Recipient: " + data.DestinationAddress);
                    Output("Message text: " + data.UserDataText);
                    Output("-------------------------------------------------------------------");
                    return;
                }
                if (pdu is SmsDeliverPdu)
                {
                    // Received message
                    SmsDeliverPdu data = (SmsDeliverPdu)pdu;
                    Output("RECEIVED MESSAGE");
                    Output("Sender: " + data.OriginatingAddress);
                    Output("Sent: " + data.SCTimestamp.ToString());
                    Output("Message text: " + data.UserDataText);
                    Output("-------------------------------------------------------------------");
                    return;
                }
                if (pdu is SmsStatusReportPdu)
                {
                    // Status report
                    SmsStatusReportPdu data = (SmsStatusReportPdu)pdu;
                    Output("STATUS REPORT");
                    Output("Recipient: " + data.RecipientAddress);
                    Output("Status: " + data.Status.ToString());
                    Output("Timestamp: " + data.DischargeTime.ToString());
                    Output("Message ref: " + data.MessageReference.ToString());
                    Output("-------------------------------------------------------------------");
                    return;
                }
                Output("Unknown message type: " + pdu.GetType().ToString());
            }


        }
}
